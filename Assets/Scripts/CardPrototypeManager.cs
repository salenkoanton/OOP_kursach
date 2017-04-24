using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrototypeManager : MonoBehaviour {

    public List<Card> list = new List<Card>();

    public Card GetCard(int id)
    {
        if (id < list.Count && id >= 0)
            return Instantiate(list[id], new Vector3(0, 0, 0), Quaternion.identity);
        return null;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
