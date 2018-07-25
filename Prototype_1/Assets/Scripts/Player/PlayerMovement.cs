﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    // Component

    private Animator animator;
    private Rigidbody2D rgb2d;
    private PushPull pushPullComponent;

    // Stats

    public float maxMovSpeed;
    public float maxGrabSpeed;

    private float horizontalIput;
    private float verticalInput;
    private Vector2 resultMovement;
    [SerializeField]
    private bool isHolding;
    [SerializeField]
    private bool isPushing;

    // Game Object Reference

    private GridLayout gridLayout;  
    private GameObject grabbingObj;

    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        pushPullComponent = GetComponent<PushPull>();
    }

    public void Start()
    {
        gridLayout = GameObject.Find("Grid").GetComponent<GridLayout>();

        isHolding = false;
        isPushing = false;
    }

    public void Update()
    {

        if (!isHolding && !isPushing)
        {
            horizontalIput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalIput < 0)
            {
                //GetComponentInChildren<SpriteRenderer>().flipX = true;
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);

                if (this.pushPullComponent.CheckMoveDir('A', this.transform.position))
                {
                    this.pushPullComponent.MoveByGrid('A');
                    animator.SetBool("isWalking", true);
                    isPushing = true;
                }
            }
            else if (horizontalIput > 0)
            {
                //GetComponentInChildren<SpriteRenderer>().flipX = false;
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);

                if (this.pushPullComponent.CheckMoveDir('D', this.transform.position))
                {
                    this.pushPullComponent.MoveByGrid('D');
                    animator.SetBool("isWalking", true);
                    isPushing = true;
                }
            }
            else if (verticalInput > 0)
            {
                if (this.pushPullComponent.CheckMoveDir('W', this.transform.position))
                {
                    this.pushPullComponent.MoveByGrid('W');
                    animator.SetBool("isWalking", true);
                    isPushing = true;
                }
            }
            else if (verticalInput < 0)
            {
                if (this.pushPullComponent.CheckMoveDir('S', this.transform.position))
                {
                    this.pushPullComponent.MoveByGrid('S');
                    animator.SetBool("isWalking", true);
                    isPushing = true;
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            //resultMovement = new Vector2(horizontalIput, verticalInput);
            //resultMovement = resultMovement.normalized * maxMovSpeed;
        }
        else
        {
            DoGrabMove();
        }
    }

    public void FixedUpdate()
    {
        //rgb2d.velocity = resultMovement * ( Time.deltaTime * 100 );
        //resultMovement = Vector3.zero;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Slime" || other.gameObject.tag == "Crate")
        {
            if (GetComponent<PlayerLogic>().pickedUp && other.gameObject.tag == "Slime")
            {
                GetComponent<PlayerLogic>().GivePickUp();
                other.gameObject.GetComponent<SlimeLogic>().HealthUp();
            }

            if (Input.GetButtonDown("Grab") && !isHolding)
            {
                isHolding = true;
                grabbingObj = other.gameObject;
                Debug.Log(grabbingObj);
                animator.SetBool("isPushing", true);
            }
            else if (Input.GetButtonDown("Grab") && isHolding)
            {
                isHolding = false;
                grabbingObj = null;
                animator.SetBool("isPushing", false);
            }
        }
    }

    private void DoGrabMove()
    {
        if (!isPushing && grabbingObj != null)
        {
            // Get Input from player
            if (Input.GetKeyDown(KeyCode.W))
            {
                GridMoveAttempt('W');
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                GridMoveAttempt('S');
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                GridMoveAttempt('A');
            }
            else if (Input.GetKeyDown(KeyCode.D))
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
            && this.pushPullComponent.CheckMoveDir(_dir, currPos))
        {
            grabbingObj.GetComponent<PushPull>().MoveByGrid(_dir);
            this.pushPullComponent.MoveByGrid(_dir);
            isPushing = true;
        }
    }

    public void FinishPush()
    {
        isPushing = false;
    }

    public GameObject GetGrabbingObj()
    {
        return grabbingObj;
    }


}