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
            _shortestPath = pathHandler.GetShortestPath(CurrentPoint);
            if (_shortestPath.Count <= 0)
            {
                canMove = false;
                return;
            }
            Movement.MoveToNextPoint(_shortestPath[0]);
            CurrentPoint = _shortestPath[0];
            canMove = pathHandler.IsEndNotReached(CurrentPoint);
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        CurrentPoint = GetClosestPoint();
        transform.position = CurrentPoint.transform.position;
        InvokeRepeating(nameof(MoveToNextPoint), 3, 3);
    }
}
