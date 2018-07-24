using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PushPull : MonoBehaviour
{
    // Constant Variable

    private const float MOVETIME = 1.0f;

    // Stat

    public bool isMoving;
    public float percentageTrue;

    public Vector3 moveTaskStart;
    public Vector3 moveTaskEnd;

    public float percentage;

    // Object Reference

    private Tilemap unWalkableTileMap;
    private GridLayout gridLayout;

    // Use this for initialization
    void Start ()
    {
        // Get grid
        gridLayout = GameObject.Find("Grid").GetComponent<GridLayout>();

        // Get the tilemap of all the non-walkable tiles
        unWalkableTileMap = GameObject.Find("Tilemap_NonWalkable").GetComponent<Tilemap>();

        percentageTrue = 0.0f;
        isMoving = false;
    }
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        if (isMoving)
        {
            Debug.Log("dsf");

            Vector3 distance = moveTaskEnd - moveTaskStart;
            percentage = Time.deltaTime / MOVETIME;
            percentageTrue += percentage;

            if (percentageTrue > 1.0f)
            {
                this.transform.position = moveTaskEnd;
                percentageTrue = 0.0f;
                isMoving = false;
                return;
            }

            Vector3 currPos = this.transform.position;
            currPos += distance * percentage;
            this.transform.position = currPos;

        }

    }

    // ===============================================================================

    public bool CheckMoveDir(char _dir, Vector3 currPos)
    {
        // Get the current position on the tilemap
        Vector3Int targetGrid = gridLayout.WorldToCell(currPos);

        switch (_dir)
        {
            case 'W':
                targetGrid.y++;
                break;

            case 'S':
                targetGrid.y--;
                break;

            case 'A':
                targetGrid.x--;
                break;

            case 'D':
                targetGrid.x++;
                break;

            default: break;
        }

        if (CheckWalkableTile(targetGrid))
        {
            return true;
        }

        return false;
    }

    private bool CheckWalkableTile(Vector3Int _pos)
    {
        if (unWalkableTileMap.HasTile(_pos))
        {
            return false;
        }

        return true;
    }

    public void MoveByGrid(char _dir)
    {
        // Get the current position
        moveTaskStart = this.transform.position;

        // Get the target position
        moveTaskEnd = moveTaskStart;
        switch (_dir)
        {
            case 'W':
                moveTaskEnd.y += 108;
                break;

            case 'S':
                moveTaskEnd.y -= 108;
                break;

            case 'A':
                moveTaskEnd.x -= 108;
                break;

            case 'D':
                moveTaskEnd.x += 108;
                break;

            default: break;
        }

        // Start the move task
        isMoving = true;
    }
}
