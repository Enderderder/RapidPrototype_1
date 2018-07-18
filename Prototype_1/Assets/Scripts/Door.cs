using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [System.NonSerialized]
    public bool isOpen;

    public Sprite OpenSprite;
    public Sprite ClosedSprite;

	private void Start ()
    {
        isOpen = false;
	}

    private void Update()
    {
        if (isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = OpenSprite;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = ClosedSprite;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
