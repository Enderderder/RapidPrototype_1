using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    // Components
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Sprite Collection
    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    // Bool that tells if the door is open or not
    [SerializeField]
    private bool isOpen;

	private void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Make sure the door is in close state at the beginning
        spriteRenderer.sprite = ClosedSprite;
        boxCollider.enabled = true;
        isOpen = false;
    }

    //private void Update() {}

    public void OpenDoor()
    {
        isOpen = true;
        spriteRenderer.sprite = OpenSprite;
        boxCollider.enabled = false;
    }

    public void CloseDoor()
    {
        isOpen = false;
        spriteRenderer.sprite = ClosedSprite;
        boxCollider.enabled = true;
    }
 
}
