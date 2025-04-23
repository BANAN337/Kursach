using System;
using System.Collections.Generic;
using UnityEngine;

public class RunningTarget : Target
{
    private Move _movement;

    private void Awake()
    {
        _movement = GetComponent<Move>();
    }
    
    private void MoveToNextPoint()
    {
        if (canMove)
        {
            var closestPointNeighbours = GridCreator.Instance.AddNeighboursToPoint(GetClosestPoint());
            _movement.MoveToNextPoint(closestPointNeighbours[new System.Random().Next(0, closestPointNeighbours.Count)]);
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        var closestPoint = GetClosestPoint();
        transform.position = closestPoint.transform.position;
        InvokeRepeating(nameof(MoveToNextPoint), 1, 5);
    }
}
