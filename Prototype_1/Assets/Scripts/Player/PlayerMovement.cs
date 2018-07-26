using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    // Component

    private Animator animator;
    //private Rigidbody2D rgb2d;
    private PushPull gridMoveComponent;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
	private PlayerLogic logicComponent;

    // Stats

    public float maxMovSpeed;
    public float maxGrabSpeed;

    private float horizontalIput;
    private float verticalInput;
    private Vector2 resultMovement;
    [SerializeField]
    private bool isHolding;
    [SerializeField]
    private bool isGridMoving;

    // Game Object Reference

    //private GridLayout gridLayout;  
    private GameObject grabbingObj;

    private void Awake()
    {
        //rgb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
		gridMoveComponent = GetComponent<PushPull>();
		logicComponent = GetComponent<PlayerLogic>();
		boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Start()
    {

        // Set the collider direction and sprite facing
        spriteRenderer.flipX = false;
        boxCollider.offset = new Vector2(60.0f, 0.0f);
        boxCollider.size = new Vector2(30.0f, 1.0f);

        isHolding = false;
        isGridMoving = false;
    }

    public void Update()
    {
		GetDirectionInput();

		if (!isHolding && !isGridMoving)
        {
            if (horizontalIput < 0)
            {
                spriteRenderer.flipX = true;
				boxCollider.offset = new Vector2(-60.0f, 0.0f);
				boxCollider.size = new Vector2(30.0f, 1.0f);

				if (this.gridMoveComponent.CheckMoveDir('A', this.transform.position))
                {
                    this.gridMoveComponent.MoveByGrid('A');
                    animator.SetBool("isWalking", true);
                    isGridMoving = true;
                }
            }
            else if (horizontalIput > 0)
            {
                spriteRenderer.flipX = false;
				boxCollider.offset = new Vector2(60.0f, 0.0f);
				boxCollider.size = new Vector2(30.0f, 1.0f);

				if (this.gridMoveComponent.CheckMoveDir('D', this.transform.position))
                {
                    this.gridMoveComponent.MoveByGrid('D');
                    animator.SetBool("isWalking", true);
                    isGridMoving = true;
                }
            }
            else if (verticalInput > 0)
            {
                boxCollider.offset = new Vector2(0.0f, 60.0f);
                boxCollider.size = new Vector2(1.0f, 30.0f);

                if (this.gridMoveComponent.CheckMoveDir('W', this.transform.position))
                {
                    this.gridMoveComponent.MoveByGrid('W');
                    animator.SetBool("isWalking", true);
                    isGridMoving = true;
                }
            }
            else if (verticalInput < 0)
            {
				boxCollider.offset = new Vector2(0.0f, -60.0f);
				boxCollider.size = new Vector2(1.0f, 30.0f);

				if (this.gridMoveComponent.CheckMoveDir('S', this.transform.position))
                {
                    this.gridMoveComponent.MoveByGrid('S');
                    animator.SetBool("isWalking", true);
                    isGridMoving = true;
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            DoGrabMove();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Slime" || other.gameObject.tag == "Crate")
        {
            if (logicComponent.pickedUp && other.gameObject.tag == "Slime")
            {
				logicComponent.GivePickUp();
                other.gameObject.GetComponent<SlimeLogic>().HealthUp();
            }

            if (Input.GetButtonDown("Grab") && !isHolding)
            {
                isHolding = true;
                grabbingObj = other.gameObject;
                animator.SetBool("isGrabbing", true);
            }
            else if (Input.GetButtonDown("Grab") && isHolding)
            {
                isHolding = false;
                grabbingObj = null;
                animator.SetBool("isGrabbing", false);
            }
        }
    }

    private void DoGrabMove()
    {
        if (!isGridMoving && grabbingObj != null)
        {
            // Get Input from player
            if (verticalInput > 0)
            {
                GridMoveAttempt('W');
            }
            else if (verticalInput < 0)
            {
                GridMoveAttempt('S');
            }
            else if (horizontalIput < 0)
            {
                GridMoveAttempt('A');
            }
            else if (horizontalIput > 0)
            {
                GridMoveAttempt('D');
            }
        }
    }

    private void GridMoveAttempt(char _dir)
    {
        Vector3 currPos = this.transform.position;
        currPos.y -= 35;

        if (grabbingObj.GetComponent<PushPull>().CheckMoveDir(_dir, grabbingObj.transform.position)
            && this.gridMoveComponent.CheckMoveDir(_dir, currPos))
        {
            grabbingObj.GetComponent<PushPull>().MoveByGrid(_dir);
            this.gridMoveComponent.MoveByGrid(_dir);
            isGridMoving = true;
        }
    }

    public void FinishPush()
    {
        isGridMoving = false;
    }

    public GameObject GetGrabbingObj()
    {
        return grabbingObj;
    }

    public bool GetIsHolding()
    {
        return isHolding;
    }

	private void GetDirectionInput()
	{
		horizontalIput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");
	}
}