using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        gridLayout = GameObject.Find("Grid").GetComponent<GridLayout>();
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
            //Debug.Log(cellPosition);
        }
        else
        {
            DotheSlimeMove();
            
            //Vector3Int cellPosition = gridLayout.WorldToCell(this.transform.position);
            //Debug.Log(cellPosition);
            //if (!pressteleport) {
            //    if (Input.GetKeyDown(KeyCode.W))
            //    {
            //        cellPosition.y += 2;
            //        pressteleport = true;
            //    }
            //    if (Input.GetKeyDown(KeyCode.S))
            //    {
            //        cellPosition.y -= 2;
            //        pressteleport = true;
            //    }
            //    if (Input.GetKeyDown(KeyCode.D))
            //    {
            //        cellPosition.x += 2;
            //        pressteleport = true;
            //    }
            //    if (Input.GetKeyDown(KeyCode.A))
            //    {
            //        cellPosition.x -= 2;
            //        pressteleport = true;
            //    }
            //}
            //if (pressteleport) {
            //    if (this.transform.position.x < 0 && this.transform.position.y >= 0) {
            //        //this.transform.position = new Vector3(-60 + (60 * cellPosition.x), 80 + (80 * cellPosition.y), 0);
            //        //slime.transform.position = new Vector3(-55 + (55 * cellPosition.x), 54 + (54 * cellPosition.y), 0);
            //    }
            //    else if (this.transform.position.x < 0 && this.transform.position.y < 0)
            //    {
            //        this.transform.position = new Vector3(-60 + (60 * cellPosition.x), -80 + (80 * cellPosition.y), 0);
            //        slime.transform.position = new Vector3(-55 + (55 * cellPosition.x), -54 + (54 * cellPosition.y), 0);
            //    }
            //    else if (this.transform.position.x >= 0 && this.transform.position.y < 0)
            //    {
            //        this.transform.position = new Vector3(60 + (60 * cellPosition.x), -80 + (80 * cellPosition.y), 0);
            //        slime.transform.position = new Vector3(55 + (55 * cellPosition.x), -54 + (54 * cellPosition.y), 0);
            //    }
            //    else if (this.transform.position.x >= 0 && this.transform.position.y >= 0)
            //    {
            //        this.transform.position = new Vector3(60 + (60 * cellPosition.x), 80 + (80 * cellPosition.y), 0);
            //        slime.transform.position = new Vector3(55 + (55 * cellPosition.x), 54 + (54 * cellPosition.y), 0);
            //    }


            //    //hold = false;
            //    pressteleport = false;
            //}
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                hold = true;
                slime = other.gameObject;
            }
        }
    }


    private void DotheSlimeMove()
    {
        // Direction variable
        char dir = ' ';

        // Get Input from player
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = 'W';
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = 'S';
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir = 'A';
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir = 'D';
        }

        if (dir != ' '
            && slime.GetComponent<SlimeLogic>().CheckMoveDir(dir)
            && this.CheckMoveDir(dir))
        {
            slime.GetComponent<SlimeLogic>().MoveSlime(dir);
            MovePlayerByGrid(dir);
        }
    }

    private bool CheckMoveDir(char _dir)
    {
        // Get the current position on the tilemap
        Vector3Int thisGridPos = gridLayout.WorldToCell(this.transform.position);

        switch (_dir)
        {
            case 'W':
                thisGridPos.y++;
                break;

            case 'S':
                thisGridPos.y--;
                break;

            case 'A':
                thisGridPos.x--;
                break;

            case 'D':
                thisGridPos.x++;
                break;

            default: break;
        }

        if (CheckWalkableTile(thisGridPos))
        {
            return true;
        }

        return false;
    }

    private bool CheckWalkableTile(Vector3Int _pos)
    {
        // Get the tilemap of all the non-walkable tiles
        Tilemap tilemap = GameObject.Find("Tilemap_NonWalkable").GetComponent<Tilemap>();

        if (tilemap.HasTile(_pos))
        {
            return false;
        }

        return true;
    }

    private void MovePlayerByGrid(char _dir)
    {
        // Get the current position
        Vector3 selfPos = this.transform.position;

        switch (_dir)
        {
            case 'W':
                selfPos.y += 108;
                break;

            case 'S':
                selfPos.y -= 108;
                break;

            case 'A':
                selfPos.x -= 108;
                break;

            case 'D':
                selfPos.x += 108;
                break;

            default: break;
        }

        this.transform.position = selfPos;
    }
}