using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    [SerializeField] private GridDecorator runningTarget;
    private Pathfinding _pathfinding;

    private void Start()
    {
        _pathfinding = new Pathfinding(runningTarget.GridCreator);
    }

    [ExcludeFromCodeCoverage]
    public List<Point> GetShortestPath(Point startPoint)
    {
        return _pathfinding.FindPath(startPoint, runningTarget.GetClosestPoint());
    }

    public bool IsEndNotReached(Point currentPoint)
    {
        if (currentPoint.transform.position == runningTarget.transform.position)
        {
            runningTarget.canMove = false;
            return false;
        }

        return true;
    }
}
