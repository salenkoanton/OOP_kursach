using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Card, ICauser, IEnemy{
    public int attack, health;
    private static float HIGHLIGHT_SCALE = 1.15f;
    private bool isHighlighting = false;
    public bool taunt = false;
    public bool canAttack = false;
    private bool frozen = false;
    private MinionInfo info;
    private bool isDead = false;
    
    public override bool IsDead
    {
        get { return isDead; }
    }
    public override int Attack
    {
        get { return attack; }
    }
    public override void Destroy()
    {
        owner.DestroyMinion(this);
        base.Destroy();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Freeze()
    {
        frozen = true;
    }

    public override Event Cause(IEnemy enemy)
    {
        GameManager.instance.Attack(this, enemy);
        canAttack = false;
        return base.Cause(enemy);
    }

    protected override bool InitiatePlaying()
    {
        return base.InitiatePlaying();
    }

    public override void Highlight()
    {
        if (!isHighlighting)
        {
            transform.localScale = transform.localScale * HIGHLIGHT_SCALE;
            isHighlighting = true;
        }
    }
    public override void Downlight()
    {
        if (isHighlighting)
        {
            transform.localScale = transform.localScale / HIGHLIGHT_SCALE;
            isHighlighting = false;
        }
    }

    public override List<IEnemy> FilterEnemies(List<IEnemy> allEnemies)
    {
        for (int i = allEnemies.Count - 1; i >= 0; i--)
        {
            if (allEnemies[i].Owner == owner)
            {
                allEnemies.RemoveAt(i);
            }
            else if (!owner.opponent.field.CanBeAttacked(allEnemies[i]))
            {
                allEnemies.RemoveAt(i);
            }
        }
        return allEnemies;
    }


    public override void DealDamage(int damage)
    {

        health -= damage;
        SetInfo();
        if (health <= 0)
        {
            isDead = true; 
        }
    }
    public override void Play()
    {
        base.Play();
        owner.field.Summon(this);
    }
    void Summon(Field field)
    {
        
    }

    public void StartTurn()
    {
        if (frozen) { 
            canAttack = false;
            frozen = false;
        }
        else
            canAttack = true;
    }
    protected override void OnMouseDown()
    {
        if (!isPlayed)
            base.OnMouseDown();
        else
        {
            if (isHighlighting)
                GameManager.instance.SelectEnemy(this);
            else {
                if (owner is You && canAttack)
                {
                    GameManager.instance.SelectAttacker(this);
                    GameManager.instance.PrepareToAttack(this);
                }
            }
            
        }


    }
    protected override void OnMouseDrag()
    {
        if (!isPlayed)
            base.OnMouseDrag();
    }

    protected override void OnMouseUp()
    {
        if (!isPlayed)
            base.OnMouseUp();
    }

    protected override void OnMouseEnter()
    {
        base.OnMouseEnter();
        if (isSwown)
            GameManager.instance.SetMinionInfo(this);
    }

    public override void Creating(MinionInfo infoUI)
    {
        base.Creating(infoUI);
        info = Instantiate(infoUI, new Vector3(0, 0, 0), Quaternion.identity);
        info.transform.parent = transform;
        SetInfo();


    }

    public override void Hide()
    {
        if (isSwown)
        {
            info.gameObject.SetActive(false);
        }
        base.Hide();
        
    }

    protected override void Show()
    {
        if (!isSwown)
        {
            info.gameObject.SetActive(true);
        }
        base.Show();
        
    }

    private void SetInfo()
    {
        info.attack.text = Attack.ToString();
        info.health.text = health.ToString();
    }

}
