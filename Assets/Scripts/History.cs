using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EventType
{
    PLAYED = 0,
    DEAL_DAMAGE,
    HEAL,
    FREEZE,
    NEXT_TURN
    
}

public class Event
{
    private int value = -1;
    private EventType type;
    public EventType Type
    {
        get { return type; }
    }
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
    List<IEnemy> FilterEnemies(List<IEnemy> allEnemies);
    Event Cause(IEnemy enemy);
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
        Message(historyEvent.ToString());
        return historyEvent;
    }

    public void Message(string text)
    {
        historyQueue.Insert(0, text);
        if (historyQueue.Count > maxEvents)
        {
            historyQueue.RemoveAt(maxEvents);
        }
        SetText();
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
