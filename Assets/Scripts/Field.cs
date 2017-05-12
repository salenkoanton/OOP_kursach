using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour, IObservable {
    private List<Minion> list = new List<Minion>();
    public float cardWidth = 1f;

    // Use this for initialization
    void Start () {
		
	}

    public int SpellDamage()
    {
        int damage = 0;
        foreach (Minion minion in list)
        {
            damage += minion.spellDamage;
        }
        return damage;
    }

    public bool CanBeAttacked(IEnemy enemy)
    {
        if (enemy is Minion && list.Contains((Minion)enemy))
        {
            if (((Minion)enemy).taunt)
            {
                return true;
            }
        }
        if (enemy is Minion && !list.Contains((Minion)enemy))
        {
            return false;
        }
        foreach (Minion minion in list)
        {
            if (minion.taunt)
                return false;
        }
        return true;
    }
    public List<IEnemy> GetEnemies()
    {
        return list.ConvertAll<IEnemy>(Converter);
    }

    public void Destroy(Minion minion)
    {
        list.Remove(minion);
        SetCardsPositions();
    }
    public void StartTurn()
    {
        foreach (Minion minion in list)
        {
            minion.StartTurn();
            if (minion.canAttack)
            {
                minion.SetLight();
            }
        }
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

    public void EndTurn()
    {
        foreach (Minion minion in list)
        {
            //minion.StartTurn();

            if (minion.canAttack)
            {
                minion.UnsetLight();
            }
            minion.EndTurn();
        }
    }
    

    public bool CanPlay(ICauser toPlay)
    {
        if (list.Count >= 7 && toPlay is Minion)
        {
            GameManager.instance.history.Message("Your field is full");
            return false;
        }
        return true;
    }

    public void Transform(IEnemy enemy, Minion into)
    {
        int toTransform = list.FindIndex((x) => (object)x == enemy);
        list[toTransform].gameObject.SetActive(false);
        list[toTransform] = into;
        SetCardsPositions();
    }

    public void SetCardsPositions()
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

    public void SpellPlayed()
    {
        foreach (IObservable minion in list)
        {
            minion.SpellPlayed();
        }
    }
    public void CardPlayed()
    {
        foreach (IObservable minion in list)
        {
            minion.CardPlayed();
        }
    }
    public void DealedDamage()
    {
        foreach (IObservable minion in list)
        {
            minion.DealedDamage();
        }
    }
    public void MinionSummoned()
    {
        foreach (IObservable minion in list)
        {
            minion.MinionSummoned();
        }
    }
}
