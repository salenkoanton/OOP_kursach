using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polymorph : Spell {
    public int transformMinionId = 12;
    // Use this for initialization
    void Start()
    {

    }
    
    public override Event Cause(IEnemy enemy)
    {
        GameManager.instance.Play();
        Minion card = (Minion)GameManager.instance.GetCard(transformMinionId, enemy.Owner);
        card.IsPlayed = true;
        enemy.Owner.Transform(enemy, card);

        return new Event(EventType.TRANSFORM);

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
        for (int i = allEnemies.Count - 1; i >= 0; i--) 
        {
            if (allEnemies[i] is Hero)
                allEnemies.RemoveAt(i);

        }
        return allEnemies;
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
}
