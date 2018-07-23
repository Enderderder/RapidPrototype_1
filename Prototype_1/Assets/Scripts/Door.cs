using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour {

    // Components
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Sprite Collection
    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    // Tile Collection
    public Tile unWalkableTile;
    private Tilemap unWalkableTileMap;
    private Vector3Int gridPos;

    // =======================================================================================

    private void Awake()
    {
        // Get Component
        spriteRenderer = GetComponent<SpriteRenderer>();
        unWalkableTileMap = GameObject.Find("Tilemap_NonWalkable").GetComponent<Tilemap>();

    }

    private void Start ()
    {
        // Get the position in the tilemap
        gridPos = GameObject.Find("Grid").GetComponent<GridLayout>().WorldToCell(this.transform.position);

        // Make sure the door is in close state at the beginning
        spriteRenderer.sprite = ClosedSprite;
        unWalkableTileMap.SetTile(gridPos, unWalkableTile);
    }

    // ==============================================================================================
    
  public void OpenDoor()
    {
        unWalkableTileMap.SetTile(gridPos, null);
        spriteRenderer.sprite = OpenSprite;
    }

    public void CloseDoor()
    {
        unWalkableTileMap.SetTile(gridPos, unWalkableTile);
        spriteRenderer.sprite = ClosedSprite;
    }
}
