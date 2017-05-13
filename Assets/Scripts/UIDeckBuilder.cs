using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckBuilder : MonoBehaviour {
    private InputField input;
    public Image card;
    public Text addedCards;
    public Button start;
    private string text = "";

    private List<int> list = new List<int>();
    public List<int> List
    {
        get { return list; }
    }
	// Use this for initialization
	void Start () {
        input = gameObject.GetComponent<InputField>();

    }
	
    void Add()
    {
        if (list.Count >= 30)
            return;
        int id = int.Parse(input.text);
        if (GameManager.instance.cards.GetCardName(id) != null) {
            list.Add(id);
            text += "\n" + GameManager.instance.cards.GetCardName(id);
            addedCards.text = list.Count.ToString() + "/30" + text;
        }
    }

    public bool IsReady()
    {
        return list.Count == 30;
    }

    public void SetImage(string str)
    {
        int id = int.Parse(input.text);
        Sprite sprite = GameManager.instance.cards.GetCardSprite(id);
        card.sprite = sprite;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
