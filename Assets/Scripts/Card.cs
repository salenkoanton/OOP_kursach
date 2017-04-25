using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour, IEnemy, ICauser{
    public int manacost, id;
    public string name_info;
    Vector3 screenSpace;
    Vector3 offset;
    bool dragging = false;
    Vector3 returnPosition;
    void Start () {
		
	}

	void Update () {

    }
    protected void OnMouseDown()
    {
        dragging = true;
        returnPosition = transform.position;
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

    }
    protected void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        //update the position of the object in the world
        transform.position = curPosition;
    }

    protected void OnMouseUp()
    {
        dragging = false;
        if (transform.position.y > -2.5f)
            if (Play())
                return;
            else
                transform.position = returnPosition;
        else
            transform.position = returnPosition;
    }

    protected void OnMouseEnter()
    {
        transform.position -= Vector3.forward;
        GameManager.instance.SetCardInfoImage(this);
    }

    protected void OnMouseExit()
    {
        transform.position += Vector3.forward;
        GameManager.instance.DisableCardInfoImage();
    }

    protected bool Play()
    {
        return GameManager.instance.Play(this, transform.parent.parent.gameObject.GetComponent<Hero>());
    }

    public override string ToString()
    {
        return name_info;
    }

}
