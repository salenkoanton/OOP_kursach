using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylvanasMinion : Minion {

	// Use this for initialization
	void Start () {
		
	}

    public override void Destroy()
    {
        base.Destroy();
        List<IEnemy> enemies = owner.opponent.field.GetEnemies();
        if (enemies.Count == 0)
            return;
        IEnemy toControl = enemies[Random.Range(0, enemies.Count)];
        owner.Control((Minion)toControl);
        GameManager.instance.history.CreateEvent(this, toControl, new Event(EventType.CONTROL));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
