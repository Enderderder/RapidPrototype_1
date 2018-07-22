using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlimeLogic : MonoBehaviour
{
    // Stats

    public bool isMoving;
    public int totalHealth;

    [SerializeField]
    private int slimeHealth;


    // Private Variables

    private GameObject pPlayer;
    private GridLayout gridLayout;


    //====================================================================

    void Start()
    {
        pPlayer = GameObject.FindGameObjectWithTag("Player");
        gridLayout = GameObject.Find("Grid").GetComponent<Grid>();


        // Set health at the beginning
        slimeHealth = totalHealth;
    }

    void Update()
    {
        // Check if the player pointer is valid
        if (pPlayer == null)
        {
            pPlayer = GameObject.FindGameObjectWithTag("Player");
            return;
        }
    }


    //====================================================================

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
        // Get the tilemap of all the non-walkable tiles
        Tilemap tilemap = GameObject.Find("Tilemap_NonWalkable").GetComponent<Tilemap>();

        if (tilemap.HasTile(_pos))
        {
            return false;
        }

        return true;
    }

    public void MoveSlime(char _dir)
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

    public void HealthDown()
    {
        slimeHealth--;
    }

}
