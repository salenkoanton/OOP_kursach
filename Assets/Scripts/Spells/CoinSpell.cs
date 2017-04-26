using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpell : Spell {

	// Use this for initialization
	void Start () {
		
	}



    public override void Play()
    {

        base.Play();
        owner.AddCurMana(1);
    }

    // Update is called once per frame
    void Update () {
		
	}

    
}
