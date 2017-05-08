using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Card, ICauser, IEnemy{
    public int attack, health;
    private static float HIGHLIGHT_SCALE = 1.1f;
    private bool isHighlighting = false;
    public bool taunt = false;
    public override int Attack
    {
        get { return attack; }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override Event Cause(IEnemy enemy)
    {
        GameManager.instance.Attack(this, enemy);
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
    }
    public override void Play()
    {
        base.Play();
        owner.field.Summon(this);
    }
    void Summon(Field field)
    {
        
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
                if (owner is You)
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

}
