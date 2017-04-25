using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EventType
{
    PLAYED = 0,
    DEAL_DAMAGE,
    HEAL,
    FREEZE
    
}

public class Event
{
    private int value = -1;
    private EventType type;
    public static string[] EventTypeStr = { "played", "deal damage", "heal", "freeze", "", "" };
    public Event(EventType type, int value = -1)
    {
        this.type = type;
        this.value = value;
    }

    public override string ToString()
    {
        if (value == -1)
            return EventTypeStr[(int)type];
        return EventTypeStr[(int)type] + " " + value;
    }
}



public interface ICauser
{

}

public class History : MonoBehaviour {
    public HistoryEvent eventPrototype;
    public Text text;
    public int maxEvents = 10;
    private List<string> historyQueue = new List<string>();
    private List<HistoryEvent> list = new List<HistoryEvent>();

    public HistoryEvent CreateEvent(ICauser causer, IEnemy enemy, Event eve)
    {
        Debug.Log(causer);
        HistoryEvent historyEvent = Instantiate(eventPrototype, new Vector3(0, 0, 0), Quaternion.identity);
        historyEvent.SetValues(causer, enemy, eve);
        list.Add(historyEvent);
        historyQueue.Insert(0, historyEvent.ToString());
        if(historyQueue.Count > maxEvents)
        {
            historyQueue.RemoveAt(maxEvents);
        }
        SetText();
        return historyEvent;
    }

    private void SetText()
    {
        text.text = "";
        foreach (string str in historyQueue)
        {
            text.text += str + "\n";
        }
    }

	// Use this for initialization
	void Start () {
        text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
