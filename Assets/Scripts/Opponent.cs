using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : Hero, ICauser, IEnemy {
    public string name_info = "Opponent";

    public override string ToString()
    {
        return name_info;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void StartTurn()
    {
        base.StartTurn();
        foreach (Card toPlay in hand.list)
        {
            if (TryPlay(toPlay))
                break;
        }
        GameManager.instance.NextTurn();
    }
    public bool TryPlay(Card toPlay)
    {
        if (CanPlay(toPlay))
        {
            GameManager.instance.Select(toPlay);
            return GameManager.instance.Play();
        }
        return false;
    }
    public override void StartGame(bool coin)
    {
        //deck.Hide();
        base.StartGame(coin);
    }
    public override void PutInHand(Card card)
    {
        card.Hide();
        base.PutInHand(card);
    }
}
