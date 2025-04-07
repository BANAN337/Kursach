using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Point startPoint;
    [SerializeField] private Point endPoint;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        startPoint = GridCreator.Instance.Grid[1,1,1 ];
        endPoint = GridCreator.Instance.Grid[4,4,4];
        FindPath();
    }

    private void Update()
    {
        
    }

    private void FindPath()
    {
        var openSet = new List<Point>();
        var closedSet = new HashSet<Point>();
        
        openSet.Add(startPoint);

        while (openSet.Count > 0)
        {
            var currentPoint = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FScore < currentPoint.FScore || openSet[i].FScore == currentPoint.FScore)
                {
                    if (openSet[i].hScore < currentPoint.hScore)
                    {
                        currentPoint = openSet[i];
                    }
                }
            }
            openSet.Remove(currentPoint);
            closedSet.Add(currentPoint);
            if (currentPoint == endPoint)
            {
                ReconstructPath(startPoint, endPoint);
                return;
            }

            foreach (var neighbour in GridCreator.Instance.AddNeighboursToPoints(currentPoint))
            {
                if (!neighbour.IsValid || closedSet.Contains(neighbour))
                {
                    continue;
                }

                var newCostToNeighbour = currentPoint.gScore + DistanceBetweenPoints(currentPoint, neighbour);

                if (newCostToNeighbour < neighbour.gScore || !openSet.Contains(neighbour))
                {
                    neighbour.gScore = newCostToNeighbour;
                    neighbour.hScore = DistanceBetweenPoints(neighbour, endPoint);
                    neighbour.previousPoint = currentPoint;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    private List<Point> ReconstructPath(Point start, Point end)
    {
        var path = new List<Point>();
        var currentPoint = end;

        while (currentPoint != start)
        {
            currentPoint.material.color = Color.black;
            path.Add(currentPoint);
            currentPoint = currentPoint.previousPoint;
        }
        startPoint.material.color = Color.black;
        path.Reverse();
        
        return path;
    }
    
    private int DistanceBetweenPoints(Point pointA, Point pointB)
    {
         return (int)MathF.Ceiling(Math.Abs(pointA.transform.position.x - pointB.transform.position.x) + Math.Abs(pointA.transform.position.y -
            pointB.transform.position.y) + Math.Abs(pointA.transform.position.z - pointB.transform.position.z));
    }
}