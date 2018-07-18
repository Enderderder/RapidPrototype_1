using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
	// Boolean that tells if the pressure plate is down or not
    [System.NonSerialized]
	public bool isDown;

    public Door door;

	// Sprite array that shows the state of the pressure plate
    public Sprite plateUpSprite;
    public Sprite plateDownSprite;

	void Start ()
	{
		// Make sure the plate is up at the beginning
		PressurePlateUp();
	}

	private void OnTriggerStay2D(Collider2D _other)
	{
		PressurePlateDown();
	}

	private void OnTriggerExit2D(Collider2D _other)
    {
		PressurePlateUp();
	}

	private void PressurePlateDown()
	{
		GetComponent<SpriteRenderer>().sprite = plateDownSprite;
        door.isOpen = true;
        isDown = true;
    }

	private void PressurePlateUp()
	{
		GetComponent<SpriteRenderer>().sprite = plateUpSprite;
        door.isOpen = false;
        isDown = false;
	}
}
