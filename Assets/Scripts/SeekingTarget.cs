using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SeekingTarget : Target
{
    [SerializeField] private PathHandler pathHandler;
    private Move _movement;
    private List<Point> _shortestPath;
    private Point _closestPoint;

    private void Awake()
    {
        _movement = GetComponent<Move>();
    }

    private void Start()
    {
        _closestPoint = GetClosestPoint();
        transform.position = _closestPoint.transform.position;
        InvokeRepeating(nameof(MoveToNextPoint), 3, 1);
    }

    private void MoveToNextPoint()
    {
        _shortestPath = pathHandler.GetShortestPath(_closestPoint);
        if (_shortestPath.Count <= 0)
        {
            return;
        }
        _movement.MoveToNextPoint(_shortestPath[0]);
        _closestPoint = _shortestPath[0];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Target reached");
        }
    }
}
