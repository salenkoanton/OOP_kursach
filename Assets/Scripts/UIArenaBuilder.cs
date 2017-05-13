using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArenaBuilder : MonoBehaviour {

    public Image first, second, third;
    private int firstId, secondId, thirdId;
    public Text text;
    private string str = ""; 
    private List<int> yourIds = new List<int>(), opponentIds = new List<int>();
    private bool side = false;
    public void ChooseFirst()
    {
        if (side)
            opponentIds.Add(firstId);
        else
            yourIds.Add(firstId);
        str += "\n" + GameManager.instance.cards.GetCardName(firstId);
        text.text = (side ? opponentIds.Count.ToString() : yourIds.Count.ToString()) + "/30" + str;
        ResetCards();
    }
    public void ChooseSecond()
    {
        if (side)
            opponentIds.Add(secondId);
        else
            yourIds.Add(secondId);
        str += "\n" + GameManager.instance.cards.GetCardName(secondId);
        text.text = (side ? opponentIds.Count.ToString() : yourIds.Count.ToString()) + "/30" + str;
        ResetCards();
    }
    public void ChooseThird()
    {
        if (side)
            opponentIds.Add(thirdId);
        else
            yourIds.Add(thirdId);
        str += "\n" + GameManager.instance.cards.GetCardName(thirdId);
        text.text = (side ? opponentIds.Count.ToString() : yourIds.Count.ToString()) + "/30" + str;
        ResetCards();
    }
    private void ResetCards()
    {
        GameManager.instance.cards.GetRandomId(out firstId, out secondId, out thirdId);
        first.sprite = GameManager.instance.cards.GetCardSprite(firstId);
        second.sprite = GameManager.instance.cards.GetCardSprite(secondId);
        third.sprite = GameManager.instance.cards.GetCardSprite(thirdId);
        if (yourIds.Count >= 30 && !side)
        {
            side = true;
            str = "";
            text.text = "0/30";
        }
        if (opponentIds.Count >= 30)
        {
            first.enabled = false;
            second.enabled = false;
            third.enabled = false;
            text.enabled = false;
            gameObject.SetActive(false);
            GameManager.instance.StartGame(yourIds, opponentIds);
        }
    }
    // Use this for initialization
    void Start () {
        ResetCards();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
