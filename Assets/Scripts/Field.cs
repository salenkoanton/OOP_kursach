using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    private List<Minion> list = new List<Minion>();
    public float cardWidth = 1f;
    // Use this for initialization
    void Start () {
		
	}
	
    public List<IEnemy> GetEnemies()
    {
        return list.ConvertAll<IEnemy>(Converter);
    }

    private static IEnemy Converter(Minion target)
    {
        return target as IEnemy;
    }

    public void Summon(Minion toSummon)
    {
        toSummon.transform.SetParent(transform);
        list.Add(toSummon);
        SetCardsPositions();
    }

    public bool CanPlay(Card toPlay)
    {
        if (list.Count < 7 && toPlay is Minion)
            return true;
        if (toPlay is Spell)
            return true;
        return false;
    }

    private void SetCardsPositions()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 newPosition = new Vector3((float)(1 - list.Count) / 2f + (float)i, 0f, 0f);
            list[i].transform.position = transform.position + newPosition * cardWidth;
        }
    }

    private void OnMouseOver()
    {
        // todo if (GameManager)
    }

    // Update is called once per frame
    void Update () {
		
	}
}
