using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public List<Card> list = new List<Card>();
    public float cardWidth = 1f;
    // Use this for initialization
    void Start () {
		
	}
	
    public void Put(Card card)
    {
        if (list.Count < 10 && card != null)
        {
            card.gameObject.SetActive(true);
            card.transform.SetParent(transform);
            list.Add(card);
            SetCardsPositions();
        }
    }

    private void SetCardsPositions()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 newPosition = new Vector3((float)(1 - list.Count) / 2f + (float)i, 0, 2 - i/10f);
            list[i].transform.position = this.transform.position + newPosition * cardWidth;
        }
    }

    public void Play(Card card)
    {
        list.Remove(card);
        SetCardsPositions();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
