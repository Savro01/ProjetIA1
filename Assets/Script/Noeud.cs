using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noeud {

    public bool walkable;
    public Vector3 worldPosition;

    public Noeud(bool _walkable, Vector3 _worldPos) {
        walkable = _walkable;
        worldPosition = _worldPos;
    }

}
