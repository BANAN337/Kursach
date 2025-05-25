using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private List<Point> _path = new();
    private GridCreator _currentGrid;

    public Pathfinding(GridCreator currentGrid)
    {
        _currentGrid = currentGrid;
    }
    public List<Point> FindPath(Point start, Point end)
    {
        var openSet = new Heap<Point>(_currentGrid.Grid.Length);
        var closedSet = new HashSet<Point>();

        openSet.Add(start);
        start.hScore = DistanceBetweenPoints(start, end);

        while (openSet.Count > 0)
        {
            var currentPoint = openSet.RemoveFirstItem();

            if (currentPoint == end)
            {
                return ReconstructPath(start, end);
            }
            
            closedSet.Add(currentPoint);

            foreach (var neighbour in _currentGrid.AddNeighboursToPoint(currentPoint))
            {
                if (neighbour.IsNotValid || closedSet.Contains(neighbour))
                {
                    continue;
                }
                
                var temporaryGScore = currentPoint.gScore + DistanceBetweenPoints(currentPoint, neighbour);
                if (temporaryGScore < neighbour.gScore || !openSet.Contains(neighbour))
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