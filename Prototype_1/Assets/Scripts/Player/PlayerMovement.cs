using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    // Component

    private Animator animator;
    private Rigidbody2D rgb2d;

    // Stats

    public float maxMovSpeed;
    public float maxGrabSpeed;

    private float horizontalIput;
    private float verticalInput;
    private Vector2 resultMovement;
    [SerializeField]
    private bool isHolding;
    private bool isPushing;

    // Game Object Reference

    private GridLayout gridLayout;
    private GameObject slime;

    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        gridLayout = GameObject.Find("Grid").GetComponent<GridLayout>();

        isHolding = false;
        isPushing = false;
    }

    public void Update()
    {

        if (!isHolding)
        {
            horizontalIput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalIput < 0)
            {

                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (horizontalIput > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            resultMovement = new Vector2(horizontalIput, verticalInput);
            resultMovement = resultMovement.normalized * maxMovSpeed;
        }
        else
        {
            DoGrabMove();
        }
    }

    public void FixedUpdate()
    {
        rgb2d.velocity = resultMovement * ( Time.deltaTime * 100 );
        resultMovement = Vector3.zero;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Slime")
        {
            if (Input.GetButtonDown("Grab") && !isHolding)
            {
                isHolding = true;
                slime = other.gameObject;
                animator.SetBool("isPushing", true);
                //slime.transform.parent = this.transform;
            }
            else if (Input.GetButtonDown("Grab") && isHolding)
            {
                isHolding = false;
                animator.SetBool("isPushing", false);
                //slime.transform.parent = null;
            }
        }
    }

    private void DoGrabMove()
    {
        if (!isPushing)
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
                && this.CheckPlayerMoveDir(dir))
            {
                slime.GetComponent<SlimeLogic>().MoveSlime(dir);
                MovePlayerByGrid(dir);
                isPushing = true;
            }
        }
    }

    private bool CheckPlayerMoveDir(char _dir)
    {
        Vector3 centrePos = this.transform.position;
        centrePos.y -= 35;

        // Get the current position on the tilemap
        Vector3Int thisGridPos = gridLayout.WorldToCell(centrePos);
        
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