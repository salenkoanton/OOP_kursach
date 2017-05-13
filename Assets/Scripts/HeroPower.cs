using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPower : MonoBehaviour, ICauser {

    public Hero Owner {
        get { return owner; }
        set { owner = value; }
    }
    private bool canAttack = true;

    public Hero owner;
    public void Play()
    {

    }
    public int Manacost
    {
        get { return 2; }
    }
    public bool IsPlayed
    {
        get;
        set;
    }
    public void StartTurn()
    {
        canAttack = true;
    }
    public List<IEnemy> FilterEnemies(List<IEnemy> enemies) {
        return enemies;
    }
    public Event Cause(IEnemy enemy)
    {
        enemy.DealDamage(1);
        owner.Play(this);
        canAttack = false;
        return new Event(EventType.DEAL_DAMAGE, 1);
    }
    protected void OnMouseDown()
    {
        if (canAttack && Owner.CanPlay(this))
        {
            GameManager.instance.Select(this);
            GameManager.instance.PrepareToAttack(this);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
