using System.Collections.Generic;
using UnityEngine;

public class SeekingTarget : Target
{
    [SerializeField] private PathHandler pathHandler;
    private List<Point> _shortestPath;
    
    private void MoveToNextPoint()
    {
        if (canMove)
        {
            _shortestPath = pathHandler.GetShortestPath(_currentPoint);
            if (_shortestPath.Count <= 0)
            {
                canMove = false;
                return;
            }
            Movement.MoveToNextPoint(_shortestPath[0]);
            _currentPoint = _shortestPath[0];
            canMove = pathHandler.IsEndNotReached(_currentPoint);
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        _currentPoint = GetClosestPoint();
        transform.position = _currentPoint.transform.position;
        InvokeRepeating(nameof(MoveToNextPoint), 3, 3);
    }
}
