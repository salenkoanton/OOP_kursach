using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpell : Spell {

    void Start()
    {

    }

    public override Event Cause(IEnemy enemy)
    {
        GameManager.instance.Play();
        enemy.Attack += 2;
        enemy.Health += 2;
        Minion card = GameManager.instance.GetRandomCard<Minion>((minion) => minion.Manacost == 2);
        card.Owner = Owner;
        card.gameObject.SetActive(true);
        card.Summon();
        card.IsPlayed = true;
        return new Event(EventType.PLAYED);

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
