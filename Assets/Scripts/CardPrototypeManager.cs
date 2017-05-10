using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrototypeManager : MonoBehaviour {

    public List<Card> list = new List<Card>();
    public MinionInfo minionInfo;
    public Sprite back;
    public Card GetCard(int id, Hero owner)
    {
        Card copy;
        if (id < list.Count && id >= 0)
            copy = Instantiate(list[id], new Vector3(0, 0, 0), Quaternion.identity);
        else
            return null;
        copy.Owner = owner;
        
        copy.Creating(minionInfo);
        return copy;
    }

   

	// Use this for initialization
	void Start () {
        for (int i = 0; i < list.Count; i++) { //set id
            list[i].id = i;
            list[i].name_info = list[i].name;
            list[i].back = back;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
