using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisjktraPathfinding : MonoBehaviour
{
    Grid grid;

    void Awake(){
        grid = GetComponent<Grid>();
    }

    void FindPath(Vector3 startPos, Vector3 targetPos){
        Noeud startNode = grid.NodeFromWorldPoint(startPos);
        Noeud targetNode = grid.NodeFromWorldPoint(targetPos);

        
    }
}
