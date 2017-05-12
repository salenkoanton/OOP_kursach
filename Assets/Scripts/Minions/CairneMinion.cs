using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CairneMinion : Minion {
    private int idToSummon = 18;
    // Use this for initialization
    void Start () {
		
	}
	
    public override void Destroy()
    {
        base.Destroy();
        Minion copy = (Minion)GameManager.instance.GetCard(idToSummon, owner);
        copy.isPlayed = true;
        if (copy != null)
        {
            Debug.Log(copy);
            copy.Summon();
            
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
