using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealbotMinion : Minion {
    private int healing = 8;
    // Use this for initialization
    void Start () {
		
	}
    public override void Play()
    {
        base.Play();
        owner.Heal(healing);
        GameManager.instance.history.CreateEvent(this, owner, new Event(EventType.HEAL, healing));
    }
    // Update is called once per frame
    void Update () {
		
	}
}
