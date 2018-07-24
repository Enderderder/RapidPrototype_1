using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PushPull : MonoBehaviour
{
    // Private Variables

    private Tilemap unWalkableTileMap;
    private GridLayout gridLayout;


    // Use this for initialization
    void Start ()
    {
        // Get grid
        gridLayout = GameObject.Find("Grid").GetComponent<GridLayout>();

        // Get the tilemap of all the non-walkable tiles
        unWalkableTileMap = GameObject.Find("Tilemap_NonWalkable").GetComponent<Tilemap>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // ===============================================================================

    public bool CheckMoveDir(char _dir)
    {
        // Get the current position on the tilemap
        Vector3Int thisGridPos = gridLayout.WorldToCell(this.transform.position);

        switch (_dir)
        {
            case 'W':
                thisGridPos.y++;
                break;

            case 'S':
                thisGridPos.y--;
                break;

            case 'A':
                thisGridPos.x--;
                break;

            case 'D':
                thisGridPos.x++;
                break;

            default: break;
        }

        if (CheckWalkableTile(thisGridPos))
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
        Vector3 thisPos = this.transform.position;

        switch (_dir)
        {
            case 'W':
                thisPos.y += 108;
                break;

            case 'S':
                thisPos.y -= 108;
                break;

            case 'A':
                thisPos.x -= 108;
                break;

            case 'D':
                thisPos.x += 108;
                break;

            default: break;
        }

        this.transform.position = thisPos;
    }
}
