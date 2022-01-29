using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisualizer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        GameObject[] walkableTiles = GameObject.FindGameObjectsWithTag("Walkable");

        Gizmos.color = Color.green;
        foreach(GameObject tile in walkableTiles)
        {
            Gizmos.DrawWireCube(tile.transform.position, new Vector3(1, 1, 1));
        }

        Gizmos.color = Color.red;
        GameObject[] unWalkableTiles = GameObject.FindGameObjectsWithTag("UnWalkable");

        foreach (GameObject tile in unWalkableTiles)
        {
            Gizmos.DrawWireCube(tile.transform.position, new Vector3(1, 1, 1));
        }
    }
}
