using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeJugglerMinion : Minion {

	// Use this for initialization
	void Start () {
		
	}

    public override void MinionSummoned()
    {
        base.MinionSummoned();
        if (isPlayed)
        {
            List<IEnemy> enemies = GameManager.instance.AllEnemies();
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Owner == owner)
                    enemies.RemoveAt(i);
            }
            IEnemy random = enemies[Random.Range(0, enemies.Count)];
            random.DealDamage(1);
            GameManager.instance.history.CreateEvent(this, random, new Event(EventType.DEAL_DAMAGE, 1));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
