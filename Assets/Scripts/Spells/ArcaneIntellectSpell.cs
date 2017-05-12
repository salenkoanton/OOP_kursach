using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneIntellectSpell : Spell {

	// Use this for initialization
	void Start () {
		
	}

    public override void Play()
    {

        base.Play();
        owner.DrawCard();
        owner.DrawCard();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
