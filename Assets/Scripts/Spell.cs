﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Card {
	// Use this for initialization
	void Start () {
		
	}

    public override void Play()
    {
        base.Play();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
