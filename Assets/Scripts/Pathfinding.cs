using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
	PathRequestManager requestManager; //create reference for pathrequest manager
	Grid grid;

	public bool pathSuccess;

	public void Awake()
	{
		requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<Grid>();
	}

	public void StartFindPath(Vector3 startPos, Vector3 targetPos) // untuk memulai courutine findpath
	{
		StartCoroutine(FindPath(startPos, targetPos));
	}

	public IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
	{

		//Stopwatch stopwatch = new Stopwatch(); //1
		//stopwatch.Start(); //1

		Vector3[] waypoints = new Vector3[0]; //menampung array waypoint  //1
		pathSuccess = false; //cek path sukses or not //1

		Node startNode = grid.NodeFromWorldPoint(startPos); //1
		Node targetNode = grid.NodeFromWorldPoint(targetPos); //1


		if (startNode.walkable && targetNode.walkable) //cek jika target berada di area walkable //1
		{
			List<Node> openSet = new List<Node>(); //1
			HashSet<Node> closedSet = new HashSet<Node>(); //1
			openSet.Add(startNode);   //1

			if (openSet.Count > 0) ///n
			{
				Node node = openSet[0];

				for (int i = 1; i < openSet.Count; i++)  //looping A* pathfinding nya  ///n
				{
					if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost && openSet[i].hCost < node.hCost) 
					{
						if (openSet[i].hCost < node.hCost)
							node = openSet[i];
					}
				}

				openSet.Remove(node);
				closedSet.Add(node);

				if (node == targetNode) //jika ketemu pathnya
				{
					//stopwatch.Stop();
					//print("Path found: " + stopwatch.Elapsed);
					
					RetracePath(startNode, targetNode);
					pathSuccess = true; //nge return true pada bool pathsuccess
					yield break; //keluar dari loop pathfinding
				}

				foreach (Node neighbour in grid.GetNeighbours(node))
				{
					if (!neighbour.walkable || closedSet.Contains(neighbour))
					{
						continue;
					}

					int newCostToNeighbour = node.gCost + GetManhattanDistance(node, neighbour);
					if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) 
					{
						neighbour.gCost = newCostToNeighbour;
						neighbour.hCost = GetManhattanDistance(neighbour, targetNode);
						neighbour.parent = node;

						if (!openSet.Contains(neighbour))
                        {
							openSet.Add(neighbour);
						}
						/*else
                        {
							openSet.UpdateItem(neighbour);
                        }	*/
					}
				}
			}
		}


		yield return null; //wait 1 frame before return to process next line code
		
		while (pathSuccess)
		{
			waypoints = RetracePath(startNode, targetNode);
			//stopwatch.Reset();
		}
		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	}

	Vector3[] RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		if (currentNode == startNode)
        {
			path.Add(currentNode);
		}

		path.Add (startNode);
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;

	}

	Vector3[] SimplifyPath(List<Node> path) //menghilangkan waypoint yg nggak berguna, waypoint hanya muncul di sepanjang path yg ketemu
	{
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		//waypoints.Add(path[0].worldPosition);

		for (int i = 1; i < path.Count - 1; i++)
		{
			Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
			if (directionNew != directionOld)
			{
				waypoints.Add(path[i - 1].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	int GetManhattanDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		return dstX + dstY;
	}
}
