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

        resultMovement = new Vector2(horizontalCurrSpeed, verticalCurrSpeed);
        resultMovement = resultMovement.normalized * moveSpeed;

        //resultMovement = new Vector2(horizontalCurrSpeed * moveSpeed, verticalCurrSpeed * moveSpeed);
    }

    public void FixedUpdate()
    {
        rgb2d.velocity = resultMovement;
    }
}
