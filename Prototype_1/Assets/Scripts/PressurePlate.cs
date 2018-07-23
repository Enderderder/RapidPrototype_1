using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Components
    private SpriteRenderer spriteRenderer;

    // Sprite Collection
    public Sprite plateUpSprite;
    public Sprite plateDownSprite;

    // Boolean that tells if the pressure plate is down or not
    [SerializeField]
    private bool isPlateDown;

    // Counter for how many object is on the plate
    private int countOnTop;

    // The connected door object with this pressure plate
    public Door connectedDoor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Make sure the plate is up at the beginning
        spriteRenderer.sprite = plateUpSprite;
        isPlateDown = false;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        // Add a count whenever object goes on to it
        countOnTop++; 

        if (!isPlateDown)
        {
            PressurePlateDown();
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        // Minus counter when one object goes off the plate
        countOnTop--;

        // If nothing is on top of the plate, go back up
        if (countOnTop <= 0)
        {
            PressurePlateUp();
        }
    }

    private void PressurePlateDown()
    {
        spriteRenderer.sprite = plateDownSprite;
        if (connectedDoor != null)
        {
            connectedDoor.OpenDoor();
            isPlateDown = true;
        }
    }

    private void PressurePlateUp()
    {
        spriteRenderer.sprite = plateUpSprite;


        connectedDoor.CloseDoor();
        isPlateDown = false;
 
    }
}
