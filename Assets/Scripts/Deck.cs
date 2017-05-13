using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public List<int> ids = new List<int>();
    private List<Card> list = new List<Card>();
    // Use this for initialization
    void Awake () {
        
    }

    public Card DrawCard()
    {
        if (list.Count <= 0)
        {
            //todo fatigue
            return null;
        }
        Card toDraw = list[Random.Range(0, list.Count)];
        list.Remove(toDraw);
        return toDraw;
    }
    public void InitDeck(List<int> ids)
    {
        this.ids = ids;
        list.Clear();
        foreach (int id in ids)
        {
            Card instance = GameManager.instance.GetCard(id, transform.parent.gameObject.GetComponent<Hero>());
            instance.transform.SetParent(transform);
            instance.gameObject.SetActive(false);
            list.Add(instance);
        }
    }
    /*public void Hide()
    {
        foreach (Card card in list)
        {
            card.Hide();
        }
    }*/

    // Update is called once per frame
    void Update () {
		
	}

    void Start()
    {
        
    }

    public Minion RandomMinion()
    {
        List<Minion> minions = new List<Minion>();
        foreach (Card card in list)
        {
            if (card is Minion)
            {
                minions.Add((Minion)card);
            }
        }
        if (minions.Count == 0)
        {
            return null;
        }
        return minions[Random.Range(0, minions.Count)];
    }
}
