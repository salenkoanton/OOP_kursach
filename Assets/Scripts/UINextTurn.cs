
using UnityEngine;
using UnityEngine.EventSystems;

public class UINextTurn : EventTrigger
{
    public bool state = true;
    public override void OnPointerClick(PointerEventData data)
    {
        if (state)
        {
            state = false;
            GameManager.instance.NextTurn();
        }
        Debug.Log("OnPointerClick called.");
    }

}