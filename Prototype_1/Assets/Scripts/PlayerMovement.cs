using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    [SerializeField]
    private bool hold = false;
    private bool pressteleport = false;
    private float horizontalCurrSpeed;
    private float verticalCurrSpeed;
    [SerializeField]
    private GridLayout gridLayout;
    private Vector2 resultMovement;
    private Rigidbody2D rgb2d;
    private GameObject slime;

    public void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (!hold) {
            horizontalCurrSpeed = Input.GetAxisRaw("Horizontal");
            verticalCurrSpeed = Input.GetAxisRaw("Vertical");

            if (horizontalCurrSpeed < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (horizontalCurrSpeed > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            resultMovement = new Vector2(horizontalCurrSpeed, verticalCurrSpeed);
            resultMovement = resultMovement.normalized * moveSpeed;
            Vector3Int cellPosition = gridLayout.WorldToCell(this.transform.position);
            Debug.Log(cellPosition);
        }
        else
        {

            
            Vector3Int cellPosition = gridLayout.WorldToCell(this.transform.position);
            Debug.Log(cellPosition);
            if (!pressteleport) {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    cellPosition.y += 2;
                    pressteleport = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    cellPosition.y -= 2;
                    pressteleport = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    cellPosition.x += 2;
                    pressteleport = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    cellPosition.x -= 2;
                    pressteleport = true;
                }
            }
            if (pressteleport) {
                if (this.transform.position.x < 0 && this.transform.position.y >= 0) {
                    //this.transform.position = new Vector3(-60 + (60 * cellPosition.x), 80 + (80 * cellPosition.y), 0);
                    //slime.transform.position = new Vector3(-55 + (55 * cellPosition.x), 54 + (54 * cellPosition.y), 0);
                }
                else if (this.transform.position.x < 0 && this.transform.position.y < 0)
                {
                    this.transform.position = new Vector3(-60 + (60 * cellPosition.x), -80 + (80 * cellPosition.y), 0);
                    slime.transform.position = new Vector3(-55 + (55 * cellPosition.x), -54 + (54 * cellPosition.y), 0);
                }
                else if (this.transform.position.x >= 0 && this.transform.position.y < 0)
                {
                    this.transform.position = new Vector3(60 + (60 * cellPosition.x), -80 + (80 * cellPosition.y), 0);
                    slime.transform.position = new Vector3(55 + (55 * cellPosition.x), -54 + (54 * cellPosition.y), 0);
                }
                else if (this.transform.position.x >= 0 && this.transform.position.y >= 0)
                {
                    this.transform.position = new Vector3(60 + (60 * cellPosition.x), 80 + (80 * cellPosition.y), 0);
                    slime.transform.position = new Vector3(55 + (55 * cellPosition.x), 54 + (54 * cellPosition.y), 0);
                }


                //hold = false;
                pressteleport = false;
            }
        }
    }

    public void FixedUpdate()
    {
        rgb2d.velocity = resultMovement;
        resultMovement = Vector3.zero;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Slime")
        {
            if (Input.GetKeyDown(KeyCode.E)) {
                hold = true;
                slime = other.gameObject;
            }
        }
    }


    private void DotheSlimeMove()
    {
        // Get the current position
        Vector3 position = this.transform.position;

        if (Input.GetKeyDown(KeyCode.W))
        { 
            position.y += 54;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            position.y -= 54;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            position.x -= 54;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            position.x += 54;
        }

        this.transform.position = position;

    }

}