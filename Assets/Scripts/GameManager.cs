using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Table table;
    public UserInterface UI;
    public CardPrototypeManager cards;
    public History history;
    public static GameManager instance;
    public bool yourTurn;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }

    

    public Card GetCard(int id)
    {
        return cards.GetCard(id);
    }

    public void SetCardInfoImage(Card card)
    {
        UI.cardInfoImage.enabled = true;
        UI.cardInfoImage.sprite = card.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void DisableCardInfoImage()
    {
        UI.cardInfoImage.enabled = false;
    }

    public void NextTurn()
    {
        yourTurn = !yourTurn;
        if (yourTurn)
        {
            table.opponent.EndTurn();
            table.you.StartTurn();
            UI.nextTurn.state = true;
           
            
        }
        else
        {
            table.you.EndTurn();
            table.opponent.StartTurn();

        }
        
    }

    public bool Play(Card toPlay, Hero owner)
    {
        if (owner.CanPlay(toPlay))
        {
            owner.Play(toPlay);
            Debug.Log(owner);
            history.CreateEvent((ICauser)owner, (IEnemy)toPlay, new Event(EventType.PLAYED));
            return true;
        }
        return false;
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
