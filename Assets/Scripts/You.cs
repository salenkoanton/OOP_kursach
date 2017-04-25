using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class You : Hero, ICauser, IEnemy {
    public string name_info = "You";
	// Use this for initialization
	void Start () {
		
	}
	
    public override string ToString()
    {
        return name_info;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
