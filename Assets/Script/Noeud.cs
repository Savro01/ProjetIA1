using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noeud {

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    //A* algo
    public int gCost;
    public int hCost;
    public Noeud parent;

    //Disjktra algo
    public int distance;

    public Noeud(bool _walkable, Vector3 _worldPos, int _grixX, int _grixY) {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _grixX;
        gridY = _grixY;
    }

    public int fCost
    {
        get{
            return gCost + hCost;
        }
    }
}
