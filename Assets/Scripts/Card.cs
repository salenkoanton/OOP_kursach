using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {
    public int manacost;
	void Start () {
		
	}

	void Update () {
		
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

    void Play()
    {
        GameManager.instance.Play(this, transform.parent.parent.gameObject.GetComponent<Hero>());
    }


}
