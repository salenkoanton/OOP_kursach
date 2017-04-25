using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMinion : Minion {

    private Minion card;
    
    public void SetMinion(Minion card)
    {
        this.card = card;
    }
    public Minion GetMinion()
    {
        return card;
    }

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
