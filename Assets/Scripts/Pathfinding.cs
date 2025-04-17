using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private GridCreator _grid;
    private List<Point> _path = new List<Point>();
    public Pathfinding(GridCreator grid)
    {
        _grid = grid;
    }
    public List<Point> FindPath(Point start, Point end)
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
                return ReconstructPath(start, end);
            }
            
            openSet.Remove(currentPoint);
            closedSet.Add(currentPoint);

            foreach (var neighbour in _grid.AddNeighboursToPoint(currentPoint))
            {
                if (neighbour.IsNotValid || closedSet.Contains(neighbour))
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
        return new List<Point>();
    }

    private List<Point> ReconstructPath(Point start, Point end)
    {
        var currentPoint = end;
        foreach (var point in _path)
        {
            point.material.color = Color.white;
        }
        _path.Clear();

        while (currentPoint != start)
        {
            currentPoint.material.color = Color.black;
            _path.Add(currentPoint);
            currentPoint = currentPoint.previousPoint;
        }

        //startPoint.material.color = Color.black; error comes out at this point during testing
        _path.Reverse();

        return _path;
    }

    private int DistanceBetweenPoints(Point pointA, Point pointB)
    {
        return (int)MathF.Ceiling(Math.Abs(pointA.indexes.x - pointB.indexes.x) + Math.Abs(
            pointA.indexes.y -
            pointB.indexes.y) + Math.Abs(pointA.indexes.z - pointB.indexes.z));
    }
}