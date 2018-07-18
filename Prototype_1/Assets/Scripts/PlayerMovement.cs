using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;

    private float horizontalCurrSpeed;
    private float verticalCurrSpeed;

    private Vector2 resultMovement;
    private Rigidbody2D rgb2d;

    public void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
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
    }

    public void FixedUpdate()
    {
        rgb2d.velocity = resultMovement;
    }
}
