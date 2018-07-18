using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
	// Boolean that tells if the pressure plate is being stand on or not
	public bool pressureState;

    public Door door;

	// Sprite array that shows the state of the pressure plate
    public Sprite pressurePlateUpSprite;
    public Sprite pressurePlateDownSprite;

	void Start ()
	{
		// Make sure the plate is up at the beginning
		PressurePlateUp();
	}
	
	void Update ()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D _other)
	{
		//// Get the tag of the target when trigger collider
		//GameObject otherObject = _other.gameObject;
		
		//// See if anything is on the pressure plate
		//if (otherObject.tag == "Player" 
		//	|| otherObject.tag == "Slime"
		//	|| otherObject.tag == "WorldObject")
		//{
		//	PressurePlateDown();
		//}

		PressurePlateDown();
	}

	private void OnTriggerExit2D(Collider2D _other)
	{
		//// Get the tag of the target when trigger collide
		//GameObject otherObject = _other.gameObject;

		//// See if anything is on the pressure plate
		//if (otherObject.tag == "Player"
		//	|| otherObject.tag == "Slime"
		//	|| otherObject.tag == "WorldObejct")
		//{
		//	PressurePlateDown();
		//}

		PressurePlateUp();
	}

	private void PressurePlateDown()
	{
		GetComponent<SpriteRenderer>().sprite = pressurePlateUpSprite;
        door.isOpen = true;
	}

	private void PressurePlateUp()
	{
		GetComponent<SpriteRenderer>().sprite = pressurePlateDownSprite;
        door.isOpen = false;
	}
}
