using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMove : MonoBehaviour
{
    // Constant Variable

    private const float MOVETIME = 0.5f;

    // Stat

    public bool isMoving;
    public float percentTrue;
    public float percentThisMove;

    public Vector3 moveTaskStart;
    public Vector3 moveTaskEnd;


    public Vector3Int currTile;
    public Vector3Int prevTile;

    // Object Reference

    private Tilemap unWalkableTileMap;
    private GridLayout gridLayout;
    private PlayerMovement playerMove;
    public Tile unWalkableTile;

    // ==========================================================================================

    void Start ()
    {
        // Get grid
        gridLayout = GameObject.Find(
            "Grid").GetComponent<GridLayout>();

        // Get the tilemap of all the non-walkable tiles
        unWalkableTileMap = GameObject.Find(
            "Tilemap_NonWalkable").GetComponent<Tilemap>();

        // Get player
        playerMove = GameObject.FindGameObjectWithTag(
            "Player").GetComponent<PlayerMovement>();

        // Set starting value
        percentTrue = 0.0f;
        isMoving = false;

        if (this.gameObject.tag != "Player")
        {
            currTile = gridLayout.WorldToCell(this.transform.position);
            prevTile = currTile;
            unWalkableTileMap.SetTile(currTile, unWalkableTile);
        }
    }

    void Update()
    {
        if(this.gameObject.tag != "Player")
        {
            currTile = gridLayout.WorldToCell(this.transform.position);

            if (currTile != prevTile)
            {
                unWalkableTileMap.SetTile(prevTile, null);
                unWalkableTileMap.SetTile(currTile, unWalkableTile);
                prevTile = currTile;
            }
        }
    }
	
	private void FixedUpdate ()
    {
        if (isMoving)
        {

            Vector3 distance = moveTaskEnd - moveTaskStart;
            percentThisMove = Time.deltaTime / MOVETIME;
            percentTrue += percentThisMove;

            if (percentTrue > 1.0f)
            {
                this.transform.position = moveTaskEnd;
                percentTrue = 0.0f;
                isMoving = false;
                playerMove.FinishPush();
                return;
            }

            Vector3 currPos = this.transform.position;
            currPos += distance * percentThisMove;
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
        if (_pos == gridLayout.WorldToCell(
            playerMove.GetGrabbingObj().transform.position))
        {
            return true;
        }
        else if (unWalkableTileMap.HasTile(_pos))
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
