using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    private List<TableMinion> list = new List<TableMinion>();
    public float cardWidth = 1f;
    // Use this for initialization
    void Start () {
		
	}
	
    public void Summon(Card toSummon)
    {
        toSummon.transform.SetParent(transform);
        SetCardsPositions();
    }

    private void SetCardsPositions()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 newPosition = new Vector3((1 - list.Count) / 2 + i, 0, 0);
            list[i].transform.position = this.transform.position + newPosition * cardWidth;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
