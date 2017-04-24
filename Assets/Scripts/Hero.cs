using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    public Hand hand;
    public Deck deck;
    private int health = 30;
    private int armor = 0;
    private int max_mana = 0;
    private int cur_mana = 0;

	// Use this for initialization
	void Start () {
        

    }

    public void StartTurn()
    {
        if (max_mana < 10)
            max_mana++;
        cur_mana = max_mana;
        hand.Put(deck.DrawCard()); //fatigue
        //run start_turn event and enable controls
    }

    public void EndTurn()
    {
        //run end_turn event and disable all controls
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame(bool coin)
    {
        if (coin)
            hand.Put(deck.DrawCard());
        hand.Put(deck.DrawCard());
        hand.Put(deck.DrawCard());
        hand.Put(deck.DrawCard());
    }
}
