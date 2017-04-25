using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Card {
    public int attack, health;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Summon(Field field)
    {
        
    }
    public static TableMinion Convert(Minion minion)
    {
        TableMinion new_minion = new TableMinion();
        new_minion.SetMinion(minion);
        return new_minion;
    }
}
