using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Card {
	// Use this for initialization
	void Start () {
		
	}

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
    }
    public override void Play()
    {
        base.Play();
        gameObject.SetActive(false);
    }

    protected override bool InitiatePlaying()
    {
        return base.InitiatePlaying();
    }

    public override Event Cause(IEnemy enemy)
    {
        return base.Cause(enemy);
    }

    public override List<IEnemy> FilterEnemies(List<IEnemy> allEnemies)
    {
        return base.FilterEnemies(allEnemies);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
