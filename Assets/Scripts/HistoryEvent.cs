using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryEvent : MonoBehaviour {
    private IEnemy enemy;
    private ICauser causer;
    private Event eve;
    
    public IEnemy Enemy
    {
        get
        {
            return enemy;
        }
    }
    public ICauser Causer
    {
        get
        {
            return causer;
        }
    }
    public Event Eve
    {
        get
        {
            return eve;
        }
    }

    public void SetValues(ICauser causer, IEnemy enemy, Event eve)
    {
        this.causer = causer;
        this.enemy = enemy;
        this.eve = eve;
    }

    public override string ToString()
    {
        string str = ">>> " + causer.ToString();
        str += " " + eve.ToString();
        str += " " + enemy.ToString();
        
        return str;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
