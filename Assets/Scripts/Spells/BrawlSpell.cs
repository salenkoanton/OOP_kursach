using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlSpell : Spell {

	// Use this for initialization
	void Start () {
		
	}
    public override void Play()
    {

        base.Play();
        List<IEnemy> enemies = GameManager.instance.AllEnemies();
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] is Hero)
                enemies.RemoveAt(i);

        }
        IEnemy surviver = enemies[Random.Range(0, enemies.Count)];
        foreach(IEnemy enemy in enemies)
        {
            if (enemy != surviver)
                enemy.Destroy();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
