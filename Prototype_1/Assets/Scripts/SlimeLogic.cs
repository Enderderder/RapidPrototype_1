using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlimeLogic : MonoBehaviour
{
    // Global Variables

    public bool isMoving;



    // Private Variables

    private GameObject pPlayer;
    public GridLayout gridLayout;


    void Start()
    {
        pPlayer = GameObject.FindGameObjectWithTag("Player");

        gridLayout = GameObject.Find("Grid").GetComponent<Grid>();
    }


    void Update()
    {
        // Check if the player pointer is valid
        if (pPlayer == null)
        {
            pPlayer = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        CheckTileData(gridLayout.WorldToCell(this.transform.position));

    }


    public void CheckTile(char _dir)
    {
        Vector3Int thisGridPos = gridLayout.WorldToCell(this.transform.position);


        switch (_dir)
        {
            case 'W':
                {

                    break;
                }
            case 'S':
                {

                    break;
                }
            case 'A':
                {

                    break;
                }
            case 'D':
                {

                    break;
                }

            default: break;
        }
    }

    private bool CheckTileData(Vector3Int _pos)
    {

        Tilemap tilemap = GameObject.Find("Tilemap_UnWalkable").GetComponent<Tilemap>();

        //TileBase tileBase = tilemap.GetTile(_pos);

        if (tilemap.HasTile(_pos))
        {
            Debug.Log("Its a wall");
        }

        return true;
    }

}
