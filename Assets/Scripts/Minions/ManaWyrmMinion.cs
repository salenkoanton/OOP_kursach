using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaWyrmMinion : Minion {

	// Use this for initialization
	void Start () {
		
	}

    public override void SpellPlayed()
    {
        base.SpellPlayed();
        attack += 1;
        SetInfo();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
