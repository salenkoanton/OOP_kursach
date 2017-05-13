using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrototypeManager : MonoBehaviour {

    public List<Card> list = new List<Card>();
    public MinionInfo minionInfo;
    public Sprite back;
    public Highligth light;
    public Card GetCard(int id, Hero owner)
    {
        Card copy;
        if (id < list.Count && id >= 0)
            copy = Instantiate(list[id], new Vector3(0, 0, 0), Quaternion.identity);
        else
            return null;
        copy.Owner = owner;
        copy.InitLigth();
        copy.Creating(minionInfo);
        return copy;
    }

    public T GetRandomCard<T>(System.Predicate<Card> filter) where T : Card
    {
        List<T> filteredCards = list.FindAll((card) => filter(card) && card is T).ConvertAll<T>((target) => target as T);
        T copy = Instantiate(filteredCards[Random.Range(0, filteredCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        copy.InitLigth();
        copy.Creating(minionInfo);
        return copy;
    }
    private static IEnemy Converter(Minion target)
    {
        return target as IEnemy;
    }

    public void GetRandomId(out int first, out int second, out int trird)
    {
        int random = Random.Range(0, list.Count);
        first = random;
        while(first == random)
            random = Random.Range(0, list.Count);
        second = random;
        while (first == random || second == random)
            random = Random.Range(0, list.Count);
        trird = random;
    }

    public Highligth GetLight()
    {
        return Instantiate(light, light.transform);
    }
    public Sprite GetCardSprite(int id)
    {
        if (id >= list.Count || id < 0)
            return null;
        return list[id].GetComponent<SpriteRenderer>().sprite;
    }
    public string GetCardName(int id)
    {
        
        if (id >= list.Count || id < 0)
            return null;
        return list[id].name_info + " " + list[id].manacost.ToString();
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < list.Count; i++) { //set id
            list[i].id = i;
            list[i].name_info = list[i].name;
            list[i].back = back;
        }
        light.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Minion Copy(Minion minion)
    {
        if (minion == null)
            return null;
        Minion copy = Instantiate(minion);
        copy.Copy();
        copy.gameObject.SetActive(true);
        copy.maxHealth = copy.health;
        copy.startAttack = copy.attack;
        copy.startHealth = copy.health;
        copy.InitLigth();
        return copy;
    }

}
