using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IEnemy
{

    void DealDamage(int damage);
    void Highlight();
    void Downlight();
    Hero Owner { get; set; }
    int Attack { get; set; }
    int Health { get; set; }
    void Freeze();
    bool IsDead { get; }
    void Destroy();
    void Heal(int heal);
    
}

public abstract class Hero : MonoBehaviour, IEnemy, ICauser, IObservable {

    public Hero opponent;
    public Hand hand;
    public Deck deck;
    public Field field;
    public Text manaText;
    public MinionInfo info;
    private int health = 30;
    private int armor = 0;
    private int max_mana = 0;
    private int cur_mana = 0;
    private int fatigue = 1;
    private bool active = false;
    private int attack = 0; //replace on weapon
    private bool frozen = false;
    private bool isDead = false;
    private bool isHighlighting = false;
    private static float HIGHLIGHT_SCALE = 1.15f;
    public Vector3 HIGHLIGHT_OFFSET;
    public Image avatar;
    public HeroPower heroPower;
    public void Play() { }
    public int Manacost
    {
        get { return 0; }
    }
    public int Health
    {
        get { return health; }
        set { }
    }
    
    public bool IsPlayed
    {
        get;
        set;
    }
    public bool IsDead
    {
        get { return isDead; }
    }

    //private List<Card> playedCards = new List<Card>();
    public Hero Owner{
        get { return this; }
        set { }
    }
    public int Attack
    {
        get { return attack; }
        set { }
    }
    public void Destroy()
    {
        //todo endGame
    }
    public void Heal(int heal)
    {
        health += heal;
        if (health > 30)
            health = 30;
        SetInfo();
    }
    public void DestroyMinion(Minion minion)
    {
        field.Destroy(minion);
    }
    public void Highlight()
    {
        if (!isHighlighting)
        {
            info.transform.localScale *= HIGHLIGHT_SCALE;
            info.transform.position += HIGHLIGHT_OFFSET;
            isHighlighting = true;
        }
    }
    public void Downlight()
    {
        if (isHighlighting)
        {
            info.transform.localScale /= HIGHLIGHT_SCALE;
            info.transform.position -= HIGHLIGHT_OFFSET;
            isHighlighting = false;
        }
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
        SetInfo();
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
        active = true;
        if (max_mana < 10)
            max_mana++;
        cur_mana = max_mana;
        SetManaText();
        //PutInHand(deck.DrawCard()); 
        DrawCard();
        field.StartTurn();
        hand.Show();
        heroPower.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //run start_turn event and enable controls
    }

    public bool CanPlay(ICauser toPlay)
    {
        if (cur_mana < toPlay.Manacost)
            return false;
        if (field.CanPlay(toPlay) && active)
            return true;
        return false;
    }

    public bool CanPlay(HeroPower power)
    {
        if (cur_mana >= 2 && active)
            return true;
        return false;
    }



    public void Play(ICauser toPlay)
    {
        if (CanPlay(toPlay))
        {
            cur_mana -= toPlay.Manacost;
            SetManaText();
            if (toPlay is Card)
                hand.Play((Card)toPlay);
            toPlay.Play();
        }    
    }

    public void Control(Minion minion)
    {
        if (minion.Owner == this)
            return;
        minion.Owner.field.Destroy(minion);
        minion.Owner = this;
        minion.Summon();
        minion.UnsetLight();
        minion.canAttack = false;
    }

    private void SetManaText()
    {
        manaText.text = name + " mana: " + cur_mana + "/" + max_mana;
    }

    public virtual void EndTurn()
    {
        active = false;
        field.EndTurn();
        hand.Hide();
        heroPower.gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
        if (coin)
        {
            hand.Hide();
        }
        Debug.Log("play");
    }
    public virtual void PutInHand(Card card)
    {
        hand.Put(card);
    }
    private void SetInfo()
    {
        if (health != 30)
            info.health.color = info.damagedColor;
        else
            info.health.color = info.undamagedColor;
        info.attack.text = Attack.ToString();
        info.health.text = health.ToString();
        
    }
    public void SelectAsEnemy()
    {
        if (isHighlighting)
            GameManager.instance.SelectEnemy(this);
    }
    public void DrawCard()
    {
        Card c = deck.DrawCard();
        if (c != null)
            PutInHand(c);
        else
        {
            DealDamage(fatigue++);
            GameManager.instance.history.CreateEvent(this, this, new Event(EventType.DEAL_DAMAGE, fatigue - 1));
        }
    }
    public Minion RandomDeckMinion()
    {
        return deck.RandomMinion();
    }
    public int SpellDamage
    {
        get { return field.SpellDamage(); }
    }
    public void SpellPlayed()
    {
        field.SpellPlayed();
    }
    public void CardPlayed()
    {
        field.CardPlayed();
    }
    public void DealedDamage()
    {
        field.DealedDamage();
        opponent.field.DealedDamage();
    }
    public void MinionSummoned() {
        field.MinionSummoned();
    }
    public void Transform(IEnemy enemy, Minion into)
    {
        field.Transform(enemy, into);
    }
    public void SwitchPositions(Hero opponent)
    {
        SwitchPositions(field.transform, opponent.field.transform);
        SwitchPositions(hand.transform, opponent.hand.transform);
        SwitchPositions(info.transform, opponent.info.transform);
        SwitchPositions(manaText.transform, opponent.manaText.transform);
        SwitchPositions(heroPower.transform, opponent.heroPower.transform);
        Vector3 tmp = HIGHLIGHT_OFFSET;
        HIGHLIGHT_OFFSET = opponent.HIGHLIGHT_OFFSET;
        opponent.HIGHLIGHT_OFFSET = tmp;
        field.SetCardsPositions();
        opponent.field.SetCardsPositions();
        hand.SetCardsPositions();
        opponent.hand.SetCardsPositions();

    }
    private void SwitchPositions(Transform one, Transform another)
    {
        Vector3 tmp = one.position;
        one.position = another.position;
        another.position = tmp;
    }
}
