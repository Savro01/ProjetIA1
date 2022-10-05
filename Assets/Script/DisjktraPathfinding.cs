using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisjktraPathfinding : MonoBehaviour
{
    public bool active = true;

    PathRequestManager requestManager;
    Grid grid;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        List<Noeud> path = new List<Noeud>();
        bool pathSucces = false;

        Noeud startNode = grid.NodeFromWorldPoint(startPos);
        Noeud targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable)
        {
            List<Noeud> openSet = new List<Noeud>();
            HashSet<Noeud> closeSet = new HashSet<Noeud>();

            openSet.Add(startNode);
            startNode.distance = 0;

            while (openSet.Count > 0)
            {
                Noeud currentNode = openSet[0];
                foreach (Noeud neighbour in grid.getNeighbourgs(currentNode))
                {
                    if (!neighbour.walkable || closeSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.distance + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.distance || !openSet.Contains(neighbour))
                    {
                        neighbour.distance = newMovementCostToNeighbour;
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
                if (currentNode == targetNode)
                {
                    pathSucces = true;
                }
                openSet.Remove(currentNode);
                closeSet.Add(currentNode);
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].distance < currentNode.distance)
                    {
                        currentNode = openSet[i];
                    }
                }

            }
        }
        yield return null;
        if (pathSucces)
        {
            path = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(path, pathSucces);
    }

    List<Noeud> RetracePath(Noeud startNode, Noeud endNode)
    {
        List<Noeud> path = new List<Noeud>();
        Noeud currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    int GetDistance(Noeud nodeA, Noeud nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
