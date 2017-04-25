using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : Hero {

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
            GameManager.instance.Play(toPlay, this);
            return true;
        }
        return false;
    }
}
