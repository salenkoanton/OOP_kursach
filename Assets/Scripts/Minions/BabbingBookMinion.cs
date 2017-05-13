using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabbingBookMinion : Minion {

	// Use this for initialization
	void Start () {
		
	}

    public override void Play()
    {
        base.Play();
        Spell spell = GameManager.instance.GetRandomCard<Spell>((card) => (true));
        spell.Owner = owner;
        owner.PutInHand(spell);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
