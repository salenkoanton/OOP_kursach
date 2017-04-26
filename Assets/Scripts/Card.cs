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
    public Hero owner;
    public bool isPlayed = false;
    void Start () {
		
	}

	void Update () {

    }
    protected void OnMouseDown()
    {
        dragging = true;
        returnPosition = transform.position;
        GameManager.instance.Select(this);
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
            if (GameManager.instance.Play())
                return;
            else
                transform.position = returnPosition;
        else
            transform.position = returnPosition;
    }

    protected void OnMouseEnter()
    {
        if (!isPlayed && owner is You)
            transform.position -= new Vector3(0, -0.3f, 1);
        GameManager.instance.SetCardInfoImage(this);
    }

    protected void OnMouseExit()
    {
        if (!isPlayed && owner is You)
            transform.position += new Vector3(0, -0.3f, 1);
        GameManager.instance.DisableCardInfoImage();
    }

    public virtual void Play()
    {
        
    }

    public override string ToString()
    {
        return name_info;
    }

}
