using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public Transform target;
	
	public float speed = 20;
	public float turnSpeed = 5;
	Vector3[] path;
	int targetIndex;

	public float turnSmoothTime = .1f;

	public bool isMoving;
	public bool colliding;

	Collider col;

	void Update()
	{
		startFindPath();
	}

	void startFindPath()
    {
		PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
			isMoving = true;
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
			colliding = true;
        }
    }

    IEnumerator FollowPath()
	{
		targetIndex = 0;
		Vector3 currentWaypoint = path[0];

		Vector3 targetDir = currentWaypoint - this.transform.position;
		float step = this.turnSpeed * Time.deltaTime;
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDirection); //ngebuat objek mengarah sesuai path

		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					targetIndex = 0;
					path = new Vector3[0];
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
			else
            {
				transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint, this.speed * Time.deltaTime);
			}		
			yield return null;
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
