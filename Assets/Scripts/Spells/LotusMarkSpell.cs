using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusMarkSpell : Spell {

	// Use this for initialization
	void Start () {
		
	}
    public override void Play()
    {

        base.Play();
        List<IEnemy> enemies = owner.field.GetEnemies();
        foreach(IEnemy enemy in enemies)
        {
            enemy.Health += 1;
            enemy.Attack += 1;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
