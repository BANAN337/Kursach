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

    private void Start()
    {
        var closestPoint = GetClosestPoint();
        transform.position = closestPoint.transform.position;
        InvokeRepeating(nameof(MoveToNextPoint), 1, 1);
    }
    
    private void MoveToNextPoint()
    {
        var closestPointNeighbours = GridCreator.Instance.AddNeighboursToPoint(GetClosestPoint());
        _movement.MoveToNextPoint(closestPointNeighbours[new System.Random().Next(0, closestPointNeighbours.Count)]);
    }
}
