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


}
