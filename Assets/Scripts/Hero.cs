using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

}
public class Hero : MonoBehaviour, IEnemy {
    public Hero opponent;
    public Hand hand;
    public Deck deck;
    public Field field;
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

    public void Play(Card toPlay)
    {
        hand.Play(toPlay);
        field.Summon(toPlay);
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
        {
            hand.Put(GameManager.instance.GetCard(2));
            hand.Put(deck.DrawCard());
        }
        hand.Put(deck.DrawCard());
        hand.Put(deck.DrawCard());
        hand.Put(deck.DrawCard());
    }
}
