using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IEnemy
{

}
public abstract class Hero : MonoBehaviour, IEnemy, ICauser {
    public Hero opponent;
    public Hand hand;
    public Deck deck;
    public Field field;
    public Text manaText;
    private int health = 30;
    private int armor = 0;
    private int max_mana = 0;
    private int cur_mana = 0;

    // Use this for initialization
    void Start() {

    }

    public void AddCurMana(int val)
    {
        cur_mana += val;
        if (cur_mana > 10)
            cur_mana = 10;
        SetManaText();
    }

    public virtual void StartTurn()
    {
        if (max_mana < 10)
            max_mana++;
        cur_mana = max_mana;
        SetManaText();
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
            SetManaText();
            hand.Play(toPlay);
            toPlay.Play();
        }    
    }

    private void SetManaText()
    {
        manaText.text = "Mana: " + cur_mana + "/" + max_mana;
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
            hand.Put(GameManager.instance.GetCard(2, this));
            hand.Put(deck.DrawCard());
        }
        hand.Put(deck.DrawCard());
        hand.Put(deck.DrawCard());
        hand.Put(deck.DrawCard());
        Debug.Log("play");
    }
}
