using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

}
public abstract class Hero : MonoBehaviour, IEnemy, ICauser {
    public Hero opponent;
    public Hand hand;
    public Deck deck;
    public Field field;
    private int health = 30;
    private int armor = 0;
    private int max_mana = 0;
    private int cur_mana = 0;

    // Use this for initialization
    void Start() {

    }

    

    public virtual void StartTurn()
    {
        if (max_mana < 10)
            max_mana++;
        cur_mana = max_mana;
        hand.Put(deck.DrawCard()); //fatigue
        //run start_turn event and enable controls
    }

    public bool CanPlay(Card toPlay)
    {
        if (cur_mana < toPlay.manacost)
            return false;
        if (field.CanPlay(toPlay))
            return true;
        return false;
    }

    public void Play(Card toPlay)
    {
        if (CanPlay(toPlay))
        {
            cur_mana -= toPlay.manacost;
            hand.Play(toPlay);
            field.Summon((Minion)toPlay);
        }    
    }

    public virtual void EndTurn()
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
