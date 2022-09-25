using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarPathfinding : MonoBehaviour
{
    public Transform seeker, target;
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Noeud startNode = grid.NodeFromWorldPoint(startPos);
        Noeud targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Noeud> openSet = new List<Noeud>();
        HashSet<Noeud> closeSet = new HashSet<Noeud>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Noeud currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePAth(startNode, targetNode);
                return;
            }

            foreach (Noeud neighbour in grid.getNeighbourgs(currentNode)) {
                if (!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    void RetracePAth(Noeud startNode, Noeud endNode)
    {
        List<Noeud> path = new List<Noeud>();
        Noeud currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    int GetDistance(Noeud nodeA, Noeud nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridY);
        int dstY = Mathf.Abs(nodeA.gridX - nodeB.gridY);

        if(dstX > dstY)
        {
            return 14*dstY + 10*(dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
