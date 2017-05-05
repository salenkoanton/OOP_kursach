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
    public List<IEnemy> enemies = new List<IEnemy>();
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

    public void PrepareToAttack(Minion minion)
    {
        Debug.Log(state.Current);
        if (state.Current == state.waitForActions)
        {
            Debug.Log("ATTACK");
            enemies = CollectEnemies(minion);
            state.Next(GameManagerEvent.CHOOSE_ENEMY);
            foreach (IEnemy enemy in enemies)
            {
                enemy.Highlight();
            }
        }
    }


    public void InitialiseStateManager()
    {
        if (state == null)
        {
            state = new GameManagerFSM(yourTurn);
        }
    }

    public void EndGame(Hero loser)
    {
        if (loser == table.you)
        {

        }
        else;

    }

    private List<IEnemy> CollectEnemies(ICauser causer)
    {
        List<IEnemy> enem = new List<IEnemy>();
        if (causer is Minion)
        {
            Minion _causer = causer as Minion;
            enem.AddRange(_causer.owner.opponent.field.GetEnemies()); 
        }
        return enem;
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
        if (state.Current == state.waitForActions || state.Current == state.waitForOpponent)
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
            state.Next(GameManagerEvent.NEXT_TURN);
        }
    }

    public void Select(Card card)
    {
        if (state.waitForActions == state.Current)
            selected = card;
        else if (state.chooseEnemy == state.Current && card is Minion && selected is Minion)
        {
            Attack((Minion)selected, (Minion)card);
        }
    }

    private void Attack(Minion causer, Minion enemy)
    {
        enemy.DealDamage(causer.attack);
        causer.DealDamage(enemy.attack);
        EndAttack();
    }

    private void EndAttack()
    {
        selected = null;
        state.Next(GameManagerEvent.END_ACTION);
        foreach (IEnemy enemy in enemies)
        {
            enemy.Downlight();
        }
        enemies.Clear();
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
    private void UndoAttack()
    {
        selected = null;
        state.Next(GameManagerEvent.RETURN);
        foreach (IEnemy enemy in enemies)
        {
            enemy.Downlight();
        }
        enemies.Clear();
    }
    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonDown(1) && state.Current == state.chooseEnemy)
        {
            UndoAttack();
        }
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
    private ManagerState _summon = new ManagerToSummon();
    private ManagerState _waitForActions = new ManagerWaitForActions();
    private ManagerState _waitForOpponent = new ManagerWaitForOpponent();
    private ManagerState _chooseEnemy = new ManagerChooseEnemy();

    public ManagerState summon
    {
        get { return _summon; }
    }
    public ManagerState waitForActions
    {
        get { return _waitForActions; }
    }
    public ManagerState waitForOpponent
    {
        get { return _waitForOpponent; }
    }
    public ManagerState chooseEnemy
    {
        get { return _chooseEnemy; }
    }
    public ManagerState Current
    {
        get { return current; }
    }

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
            case GameManagerEvent.RETURN: current.Return(this); break;
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

        public override void EndAction(GameManagerFSM fsm)
        {
            fsm.current = fsm.waitForActions;
        }

        public override void Return(GameManagerFSM fsm)
        {
            fsm.current = fsm.waitForActions;
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
        public override void NextTurn(GameManagerFSM fsm)
        {
            fsm.current = fsm.waitForOpponent;
        }

        public override void ChooseEnemy(GameManagerFSM fsm)
        {
            fsm.current = fsm.chooseEnemy;
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
        public override void NextTurn(GameManagerFSM fsm)
        {
            fsm.current = fsm.waitForActions;
        }
    }
}


