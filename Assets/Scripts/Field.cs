using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    private List<TableMinion> list = new List<TableMinion>();
    public float cardWidth = 1f;
    // Use this for initialization
    void Start () {
		
	}
	
    public void Summon(Minion toSummon)
    {
        toSummon.transform.SetParent(transform);
        list.Add(Minion.Convert(toSummon));
        SetCardsPositions();
    }

    public bool CanPlay(Card toPlay)
    {
        if (list.Count < 7 && toPlay is Minion)
            return true;
        if (toPlay is Spell)
            return false;
        return false;
    }

    private void SetCardsPositions()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 newPosition = new Vector3((float)(1 - list.Count) / 2f + (float)i, 0f, 0f);
            list[i].GetMinion().transform.position = transform.position + newPosition * cardWidth;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
