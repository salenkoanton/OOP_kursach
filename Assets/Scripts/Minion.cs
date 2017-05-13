using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    void SpellPlayed();
    void CardPlayed();
    void DealedDamage();
    void MinionSummoned();
    void EndTurn();
}

public class Minion : Card, ICauser, IEnemy, IObservable
{
    public int health, maxHealth, startHealth, startAttack;
    private static float HIGHLIGHT_SCALE = 1.15f;
    private bool isHighlighting = false;
    public bool taunt = false;
    public bool canAttack = false;
    private bool frozen = false;
    private MinionInfo info;
    private bool isDead = false;
    public int spellDamage = 0;
    private Highligth light;
    public override bool IsDead
    {
        get { return isDead; }
    }
    public override int Attack
    {
        get { return attack; }
        set { attack = value;
            SetInfo();
        }
    }
   
    public override int Health
    {
        get { return health; }
        set {
            maxHealth += value - health;
            health = value;
            SetInfo();
        }
    }
    
    public override void Destroy()
    {
        owner.DestroyMinion(this);
        base.Destroy();
    }
    // Use this for initialization
    void Start () {
        
	}
    public virtual void SpellPlayed() { }
    public virtual void CardPlayed() { }
    public virtual void DealedDamage() { }
    public virtual void MinionSummoned() { }
    public override void InitLigth()
    {
        light = GameManager.instance.cards.GetLight();
        light.transform.SetParent(transform);
        UnsetLight();
    }
    // Update is called once per frame
    void Update () {
		
	}

    public override void Freeze()
    {

        frozen = true;
    }
    public override void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        SetInfo();
    }
    public override Event Cause(IEnemy enemy)
    {
        int returnDamage = enemy.Attack;
        int damage = Attack;
        enemy.DealDamage(damage);
        GameManager.instance.history.CreateEvent(this, enemy, new Event(EventType.DEAL_DAMAGE, damage));
        DealDamage(returnDamage);
        GameManager.instance.history.CreateEvent(this, enemy, new Event(EventType.GOT_DAMAGE, returnDamage));
        light.gameObject.SetActive(false);
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
        owner.DealedDamage();
    }
    public override void Play()
    {
        
        
        Summon();
        base.Play();

    }
    public void Summon()
    {
        owner.field.Summon(this);
        owner.MinionSummoned();
        isPlayed = true;
        
    }

    public virtual void StartTurn()
    {
        if (frozen) { 
            canAttack = false;
            frozen = false;
            light.gameObject.SetActive(false);
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
    
    public void UnsetLight()
    {
        light.gameObject.SetActive(false);
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
        maxHealth = health;
        startAttack = attack;
        startHealth = health;
        SetInfo();
        


    }
    
    public void SetLight()
    {
        light.gameObject.SetActive(true);
    }



    public override void Hide()
    {
        if (isSwown)
        {
            info.gameObject.SetActive(false);
        }
        base.Hide();
        
    }

    public override void Show()
    {
        if (!isSwown)
        {
            info.gameObject.SetActive(true);
        }
        base.Show();
        
    }

    protected void SetInfo()
    {
        if (health != maxHealth)
            info.health.color = info.damagedColor;
        else if (health > startHealth)
            info.health.color = info.raisedColor;
        else
            info.health.color = info.undamagedColor;
        if (attack > startAttack)
            info.attack.color = info.raisedColor;
        else
            info.attack.color = info.undamagedColor;
        info.attack.text = Attack.ToString();
        info.health.text = health.ToString();
        
    }

    public void SetAttack(int newAttack)
    {
        attack = newAttack; //todo add affect
        SetInfo();
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth; //todo add affect
        maxHealth = newHealth;
        SetInfo();
    }
    public void Copy()
    {
        info = transform.GetComponentsInChildren<MinionInfo>()[0];
    }
    public Color GetAttackColor()
    {
        return info.attack.color;
    }
    public Color GetHealthColor()
    {
        return info.health.color;
    }
    public virtual void EndTurn() { }

}
