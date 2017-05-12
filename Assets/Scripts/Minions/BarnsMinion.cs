using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnsMinion : Minion {

	// Use this for initialization
	void Start () {
		
	}

    public override void Play()
    {
        base.Play();
        Minion copy = GameManager.instance.cards.Copy(owner.RandomDeckMinion());
        if (copy != null)
        {
            Debug.Log(copy);
            copy.SetAttack(1);
            copy.SetHealth(1);
            copy.Owner = owner;
            copy.Summon();
            //owner.field.Summon(copy);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
