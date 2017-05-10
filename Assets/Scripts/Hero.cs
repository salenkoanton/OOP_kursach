using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IEnemy
{
    void DealDamage(int damage);
    void Highlight();
    void Downlight();
    Hero Owner { get; }
    int Attack { get; }
    void Freeze();
    bool IsDead { get; }
    void Destroy();
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
    private int attack = 0; //replace on weapon
    private bool frozen = false;
    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }
    //private List<Card> playedCards = new List<Card>();
    public Hero Owner{
        get { return this; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public void Destroy()
    {
        //todo endGame
    }
    public void DestroyMinion(Minion minion)
    {
        field.Destroy(minion);
    }
    public void Highlight() {

    }
    public void Downlight()
    {

    }
    public void Freeze()
    {
        frozen = true;
    }
    // Use this for initialization
    void Start() {

    }

    public Event Cause(IEnemy enemy)
    {
        return new Event(EventType.DEAL_DAMAGE, 0); // todo damage
    }

    public List<IEnemy> FilterEnemies(List<IEnemy> allEnemies)
    {
        for (int i = allEnemies.Count - 1; i >= 0; i-- ){
            if (allEnemies[i].Owner == this)
            {
                allEnemies.RemoveAt(i);
            }
        }
        return allEnemies;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            GameManager.instance.EndGame(this);
        }
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
        PutInHand(deck.DrawCard()); //fatigue
        field.StartTurn();
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

    public virtual void StartGame(bool coin)
    {
        if (coin)
        {
            PutInHand(GameManager.instance.GetCard(2, this));
            PutInHand(deck.DrawCard());
        }
        PutInHand(deck.DrawCard());
        PutInHand(deck.DrawCard());
        PutInHand(deck.DrawCard());
        Debug.Log("play");
    }
    public virtual void PutInHand(Card card)
    {
        hand.Put(card);
    }
}
