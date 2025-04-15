using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SeekingTarget : Target
{
    [SerializeField] private PathHandler pathHandler;
    private Move _movement;
    private List<Point> _shortestPath;

    private void Awake()
    {
        _movement = GetComponent<Move>();
    }

    private void Start()
    {
        var closestPoint = GetClosestPoint();
        transform.position = closestPoint.transform.position;
        _shortestPath = pathHandler.GetShortestPath(closestPoint);
        InvokeRepeating(nameof(MoveToNextPoint), 1, 1);
    }

    private void MoveToNextPoint()
    {
        if (_shortestPath.Count <= 0)
        {
            return;
        }
        _movement.MoveToNextPoint(_shortestPath[0]);
        _shortestPath.RemoveAt(0);
    }
}
