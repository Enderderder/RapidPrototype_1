using UnityEngine;

public class GrateLogic : MonoBehaviour
{
    // Component

    private SpriteRenderer spriteRenderer;

    // Sprite Collection

    public Sprite grateEmpty;
    public Sprite grateFull;


    //====================================================

    private void Awake()
    {
        // Get Object Component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start ()
    {
        // Empty the Grate
        this.spriteRenderer.sprite = grateEmpty;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Slime")
        {
            FillGrate();
            _other.GetComponent<SlimeLogic>().HealthDown();
        }
    }

    //=====================================================

    public void FillGrate()
    {
        this.spriteRenderer.sprite = grateFull;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
