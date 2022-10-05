using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    float speed = 5;
    List<Noeud> path;
    int targetIndex;

    float timer = 0;

    void Update(){
        float time = 0.15f;
        if (timer <= time)
            timer += Time.deltaTime;

        if (timer > time)
        {
            PathRequestManager.RequestPath(transform.position, target.position, onPathFound);
            timer = 0;
        }
    }

    public void onPathFound(List<Noeud> newPath, bool pathSuccesful){
        if(pathSuccesful){
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        Noeud currentWaypoint = path[0];

        while(true){
            if(transform.position == currentWaypoint.worldPosition){
                targetIndex++;
                if(targetIndex >= path.Count){
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.worldPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos(){
        if(path != null){
            for(int i = targetIndex; i < path.Count; i++){
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i].worldPosition, Vector3.one * (1 - .1f));
            }
        }
    }
}
