using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    //private List<TableMinion> yourSide = new List<TableMinion>(), opponentSide = new List<TableMinion>();
    public Hero opponent, you;

    public void StartGame()
    {

        bool coin = Random.Range(0, 2) == 1;
        if (coin)
        {
            you.SwitchPositions(opponent);
        }
        opponent.StartGame(coin);
        you.StartGame(!coin);
        GameManager.instance.yourTurn = coin;
        //GameManager.instance.UI.nextTurn.state = coin;
        GameManager.instance.InitialiseStateManager();
        opponent.EndTurn();
        you.EndTurn();
       
        
    }
    public void SwitchPositions()
    {
        Vector3 tmp = you.transform.position;
        you.SwitchPositions(opponent);
        //you.transform.position = opponent.transform.position;
        //opponent.transform.position = you.transform.position;
        


    }
    // Use this for initialization
    void Start () {
        StartGame();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
