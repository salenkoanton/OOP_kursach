using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerMinion : Minion {

	// Use this for initialization
	void Start () {
		
	}

    public override void DealedDamage()
    {
        base.DealedDamage();
        attack += 1;
        SetInfo();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
