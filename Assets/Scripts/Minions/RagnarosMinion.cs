using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosMinion : Minion {

	// Use this for initialization
	void Start () {
		
	}
    public override void EndTurn()
    {
        base.EndTurn();
        List<IEnemy> enemies = owner.opponent.field.GetEnemies();
        enemies.Add(owner.opponent);
        IEnemy enemy = enemies[Random.Range(0, enemies.Count)];
        enemy.DealDamage(8);
        GameManager.instance.history.CreateEvent(this, enemy, new Event(EventType.DEAL_DAMAGE, 8));
    }
    public override void StartTurn()
    {
        base.StartTurn();
        canAttack = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
