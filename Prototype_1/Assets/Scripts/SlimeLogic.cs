using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLogic : MonoBehaviour
{

    // Global Variables

    public bool isMoving;



    // Private Variables

    private GameObject pPlayer;


	
	void Start ()
    {
        pPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	
	void Update ()
    {
        // Check if the player pointer is valid
        if (pPlayer == null)
        {
            pPlayer = GameObject.FindGameObjectWithTag("Player");
            return;
        }



    }
}
