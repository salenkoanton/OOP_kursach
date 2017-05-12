using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpringGuardianMinion : Minion {


    public int heal;
    // Use this for initialization
    void Start()
    {

    }

    public override Event Cause(IEnemy enemy)
    {
        if (isPlayed)
            return base.Cause(enemy);
        else
        {
            GameManager.instance.Play();
            enemy.Heal(heal);
            return new Event(EventType.HEAL, heal);
        }

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
        if (isPlayed)
            return base.FilterEnemies(allEnemies);
        return allEnemies;
    }
    protected override void OnMouseUp()
    {
        if (!isPlayed) { 
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
    }

    // Update is called once per frame
    void Update()
    {

    }

}
