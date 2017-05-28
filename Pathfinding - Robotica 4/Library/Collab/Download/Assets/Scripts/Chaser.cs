using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sabrina Sampaio e Paulo Tozzo - projeto robotica 4
//este codigo é repectivo ao chaser, e é muito similar ao seeker, com pequenas exceções
public class Chaser : MonoBehaviour {

    public Transform target;
    float speed = 10;
    Vector3[] path;
    int targetIndex;
	public GameObject tela;
    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        StartCoroutine("updatePath");
    }


    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator updatePath()
    {
        while (true)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator FollowPath()
    {
		if (path.Length != 0) {
			Vector3 currentWaypoint = path [0];
			while (true) {
				if (transform.position == currentWaypoint) {
					targetIndex++;
					if (targetIndex >= path.Length) {
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}

				transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;

			}
		} else {
			// you win goes here
			tela.SetActive(true);
			Time.timeScale = 0;
		}
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}