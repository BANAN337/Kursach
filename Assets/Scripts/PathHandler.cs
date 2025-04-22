using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    [SerializeField] private Target runningTarget;
    private Pathfinding _pathfinding;

    private void Awake()
    {
        _pathfinding = new Pathfinding(GridCreator.Instance);
    }

    public List<Point> GetShortestPath(Point startPoint)
    {
        return _pathfinding.FindPath(startPoint, runningTarget.GetClosestPoint());
    }

    public bool IsEndReached(Point currentPoint)
    {
        if (currentPoint.transform.position == runningTarget.transform.position)
        {
            runningTarget.canMove = false;
            return false;
        }

        return true;
    }
}
