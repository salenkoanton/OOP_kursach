using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Table table;
    public UserInterface UI;
    public CardPrototypeManager cards;
    public History history;
    public GameManagerFSM state;
    public static GameManager instance;
    public bool yourTurn;
    private Card selected;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }

    

    public Card GetCard(int id, Hero owner)
    {
        return cards.GetCard(id, owner);
    }

    public void SetCardInfoImage(Card card)
    {
        UI.cardInfoImage.enabled = true;
        UI.cardInfoImage.sprite = card.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void DisableCardInfoImage()
    {
        UI.cardInfoImage.enabled = false;
    }

    public void NextTurn()
    {
        yourTurn = !yourTurn;
        if (yourTurn)
        {
            table.opponent.EndTurn();
            table.you.StartTurn();
            UI.nextTurn.state = true;
           
            
        }
        else
        {
            table.you.EndTurn();
            table.opponent.StartTurn();

        }
        
    }

    public void Select(Card card)
    {
        selected = card;
    }

    public bool Play()
    {
        if (selected == null)
            return false;
        if (selected.owner.CanPlay(selected))
        {
            selected.owner.Play(selected);
            //selected.Play();
            Debug.Log(selected);
            history.CreateEvent((ICauser)selected.owner, (IEnemy)selected, new Event(EventType.PLAYED));
            selected.isPlayed = true;
            selected = null;

            return true;
        }
        return false;
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


public enum GameManagerEvent
{
    NEXT_TURN = 0,
    CHOOSE_ENEMY,
    SUMMON,
    END_ACTION,
    RETURN
}
public class GameManagerFSM
{
    ManagerState current;
    ManagerState summon = new ManagerToSummon();
    ManagerState waitForActions = new ManagerWaitForActions();
    ManagerState waitForOpponent = new ManagerWaitForOpponent();
    ManagerState chooseEnemy = new ManagerChooseEnemy();

    public GameManagerFSM(bool yourTurn)
    {
        current = yourTurn ? waitForActions : waitForOpponent;
    }

    public void Next(GameManagerEvent eve)
    {
        switch (eve)
        {
            case GameManagerEvent.CHOOSE_ENEMY: current.ChooseEnemy(this); break;
            case GameManagerEvent.NEXT_TURN: current.NextTurn(this); break;
            case GameManagerEvent.END_ACTION: current.EndAction(this); break;
            case GameManagerEvent.SUMMON: current.Summon(this); break;
        }
    }
    public abstract class ManagerState
    {
        protected HistoryEvent initiator;
        public abstract void Start();
        public abstract void End();
        public virtual void NextTurn(GameManagerFSM fsm) { }
        public virtual void Summon(GameManagerFSM fsm) { }
        public virtual void ChooseEnemy(GameManagerFSM fsm) { }
        public virtual void EndAction(GameManagerFSM fsm) { }
        public virtual void Return(GameManagerFSM fsm) { }

    }

    public class ManagerToSummon : ManagerState
    {
        public override void ChooseEnemy(GameManagerFSM fsm)
        {
            fsm.current.End();
            fsm.current = fsm.chooseEnemy;
        }
        public override void EndAction(GameManagerFSM fsm)
        {
            fsm.current.End();
            fsm.current = fsm.waitForActions;
            Start();
        }
        public override void Return(GameManagerFSM fsm)
        {
            fsm.current.End();
            fsm.current = fsm.waitForActions;
        }
        public override void Start()
        {
            GameManager.instance.Play();
        }

        public override void End()
        {

        }


    }

    public class ManagerChooseEnemy : ManagerState
    {
        public override void Start()
        {

        }

        public override void End()
        {

        }
    }

    public class ManagerWaitForActions : ManagerState
    {
        public override void Start()
        {

        }

        public override void End()
        {

        }
    }

    public class ManagerWaitForOpponent : ManagerState
    {
        public override void Start()
        {

        }

        public override void End()
        {

        }
    }
}


