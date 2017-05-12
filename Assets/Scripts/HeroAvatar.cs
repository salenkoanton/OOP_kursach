
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroAvatar : EventTrigger
{
    public override void OnPointerClick(PointerEventData data)
    {
        transform.parent.parent.SendMessage("SelectAsEnemy");
    }

}