using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Card, ICauser, IEnemy{
    public int attack, health;
    private static float HIGHLIGHT_SCALE = 1.1f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Highlight()
    {
        transform.localScale = transform.localScale * HIGHLIGHT_SCALE;
    }
    public override void Downlight()
    {
        transform.localScale = transform.localScale / HIGHLIGHT_SCALE;
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
            GameManager.instance.Select(this);
            GameManager.instance.PrepareToAttack(this);
            
            
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
