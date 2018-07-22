using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateLogic : MonoBehaviour
{
    // Component

    private SpriteRenderer spriteRenderer;

    // Sprite Collection

    public Sprite grateEmpty;
    public Sprite grateFull;

    // Stats

    private bool isGrateEmpty;

    //====================================================

    void Start ()
    {
        // Get Object Component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Empty the Grate
        this.spriteRenderer.sprite = grateEmpty;
        isGrateEmpty = true;
    }


    private void OnTriggerEnter2D(Collider2D _other)
    {


        if(_other.tag == "Slime")
        {
            FillGrate();
            _other.GetComponent<SlimeLogic>().HealthDown();
        }
    }

    //=====================================================

    public void FillGrate()
    {
        this.spriteRenderer.sprite = grateFull;
        this.isGrateEmpty = false;
    }
}
