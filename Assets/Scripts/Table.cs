using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    //private List<TableMinion> yourSide = new List<TableMinion>(), opponentSide = new List<TableMinion>();
    public Hero opponent, you;
    
	// Use this for initialization
	void Start () {
        bool coin = Random.Range(0, 2) == 1;
        opponent.StartGame(coin);
        you.StartGame(!coin);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
