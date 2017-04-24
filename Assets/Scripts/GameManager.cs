using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Table table;
    public UserInterface UI;
    public CardPrototypeManager cards;
    public static GameManager instance;
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

    public void Play(Card toPlay, Hero owner)
    {
        owner.Play(toPlay);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
