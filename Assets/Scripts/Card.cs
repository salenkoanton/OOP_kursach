using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour, IEnemy, ICauser{
    public int manacost, id;
    public string name_info;
    protected Vector3 screenSpace;
    protected Vector3 offset;
    protected bool dragging = false;
    protected Vector3 returnPosition;
    protected Hero owner;
    public bool isPlayed = false;
    public Sprite back;
    protected bool isSwown = true;
    public virtual bool IsDead
    {
        get { return false; }
    }
    public virtual void Destroy() {
        gameObject.SetActive(false);
    }
    public virtual void Freeze() { }
    public Hero Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    public virtual int Attack
    {
        get { return -1;}
    }
    public virtual Event Cause(IEnemy enemy)
    {
        //TODO attack
        return null;
    }
    void Start () {
		
	}

    protected virtual void Show()
    {
        if (!isSwown)
        {
            Reverse();
            isSwown = true;
        }
    }
    public virtual void Hide()
    {
        if (isSwown)
        {
            Reverse();
            isSwown = false;
        }
    }

    private void Reverse()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Sprite tmp = spriteRenderer.sprite;
        spriteRenderer.sprite = back;
        back = tmp;
    }

    void Update () {

    }
    public virtual void Creating(MinionInfo infoUI) { }
    public virtual List<IEnemy> FilterEnemies(List<IEnemy> allEnemies)
    {
        return allEnemies;
    }
    public virtual void Highlight()
    {

    }
    public virtual void Downlight()
    {

    }

    public virtual void DealDamage(int damage)
    {

    }
    protected virtual void OnMouseDown()
    {
        dragging = true;
        returnPosition = transform.position;
        GameManager.instance.Select(this);
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

    }
    protected virtual void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        //update the position of the object in the world
        transform.position = curPosition;
    }

    protected virtual void OnMouseUp()
    {
        dragging = false;
        if (transform.position.y > -2.5f)
            if (InitiatePlaying())
                return;
            else
                transform.position = returnPosition;
        else
            transform.position = returnPosition;
    }

    protected virtual bool InitiatePlaying()
    {
        return GameManager.instance.Play();
    }


    protected virtual void OnMouseEnter()
    {
        if (isSwown)
        {
            if (!isPlayed && owner is You)
                transform.position -= new Vector3(0, -0.3f, 1);
            GameManager.instance.SetCardInfoImage(this);
        }
    }

    protected virtual void OnMouseExit()
    {
        if (isSwown)
        {
            if (!isPlayed && owner is You)
                transform.position += new Vector3(0, -0.3f, 1);
            GameManager.instance.DisableCardInfoImage();
        }
    }

    public virtual void Play()
    {
        isPlayed = true;
        Show();
    }

    public override string ToString()
    {
        return name_info;
    }

}
