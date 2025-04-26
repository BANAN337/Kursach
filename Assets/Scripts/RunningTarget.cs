using System;
using System.Collections.Generic;
using UnityEngine;

public class RunningTarget : Target
{
    private Move _movement;
    private Point _closestPoint;

    private void Awake()
    {
        _movement = GetComponent<Move>();
    }
    
    private void MoveToNextPoint()
    {
        if (canMove)
        {
            var closestPointNeighbours = GridCreator.Instance.AddNeighboursToPoint(_closestPoint);
            var newPointIndex = new System.Random().Next(0, closestPointNeighbours.Count);
            _movement.MoveToNextPoint(closestPointNeighbours[newPointIndex]);
            _closestPoint = closestPointNeighbours[newPointIndex];
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        var closestPoint = GetClosestPoint();
        transform.position = closestPoint.transform.position;
        _closestPoint = closestPoint;
        InvokeRepeating(nameof(MoveToNextPoint), 1, 5);
    }
}
