using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBoltSpell : Spell {
    public int damage;
	// Use this for initialization
	void Start () {
		
	}

    public override Event Cause(IEnemy enemy)
    {
        GameManager.instance.Play();
        enemy.DealDamage(damage);
        enemy.Freeze();
        return new Event(EventType.DEAL_DAMAGE, damage);
        
    }

    public override void Play()
    {
        base.Play();
    }

    protected override bool InitiatePlaying()
    {
        Debug.Log("PREPARE");
        if (owner.CanPlay(this))
        {
            GameManager.instance.PrepareToAttack(this);
            //gameObject.SetActive(false); to remove

        }
        return false;
    }
    
    public override List<IEnemy> FilterEnemies(List<IEnemy> allEnemies)
    {
        return base.FilterEnemies(allEnemies);
    }
    protected override void OnMouseUp()
    {
        Debug.Log("fefdrfef");
        dragging = false;
        if (transform.position.y > -2.5f)
            if (InitiatePlaying())
                return;
            else
                transform.position = returnPosition;
        else
            transform.position = returnPosition;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
