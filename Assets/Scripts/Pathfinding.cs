using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Point startPoint;
    [SerializeField] private Point endPoint;
    public List<Point> ShortestPath { get; private set; }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        //FindPath(startPoint, endPoint);
    }

    private void FindPath(Point start, Point end)
    {
        var openSet = new List<Point> { start };
        var closedSet = new HashSet<Point>();

        start.hScore = DistanceBetweenPoints(start, end);

        while (openSet.Count > 0)
        {
            var currentPoint = openSet[0];
            for (var i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FScore < currentPoint.FScore || openSet[i].FScore == currentPoint.FScore)
                {
                    if (openSet[i].hScore < currentPoint.hScore)
                    {
                        currentPoint = openSet[i];
                    }
                }
            }

            if (currentPoint == end)
            {
                ShortestPath = ReconstructPath(start, end);
                return;
            }
            
            openSet.Remove(currentPoint);
            closedSet.Add(currentPoint);

            foreach (var neighbour in GridCreator.Instance.AddNeighboursToPoints(currentPoint))
            {
                if (!neighbour.IsValid || closedSet.Contains(neighbour))
                {
                    continue;
                }
                
                var temporaryGScore = currentPoint.gScore + DistanceBetweenPoints(currentPoint, neighbour);
                if (temporaryGScore < neighbour.gScore || !closedSet.Contains(neighbour))
                {
                    neighbour.gScore = temporaryGScore;
                    neighbour.hScore = DistanceBetweenPoints(neighbour, end);
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

        //startPoint.material.color = Color.black; error comes out at this point during testing
        path.Reverse();

        return path;
    }

    private int DistanceBetweenPoints(Point pointA, Point pointB)
    {
        return (int)MathF.Ceiling(Math.Abs(pointA.indexes.x - pointB.indexes.x) + Math.Abs(
            pointA.indexes.y -
            pointB.indexes.y) + Math.Abs(pointA.indexes.z - pointB.indexes.z));
    }
}