using System.Collections.Generic;
using UnityEngine;

public class SeekingTarget : Target
{
    [SerializeField] private PathHandler pathHandler;
    private List<Point> _shortestPath;
    private Point _closestPoint;
    
    private void MoveToNextPoint()
    {
        if (canMove)
        {
            _shortestPath = pathHandler.GetShortestPath(_closestPoint);
            if (_shortestPath.Count <= 0)
            {
                canMove = false;
                return;
            }
            Movement.MoveToNextPoint(_shortestPath[0]);
            _closestPoint = _shortestPath[0];
            canMove = pathHandler.IsEndNotReached(_closestPoint);
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        _closestPoint = GetClosestPoint();
        transform.position = _closestPoint.transform.position;
        InvokeRepeating(nameof(MoveToNextPoint), 3, 3);
    }
}
