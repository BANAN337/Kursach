using System;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    private Point _currentPoint;
    public bool canMove = true;

    public Point GetClosestPoint()
    {
        var closestDistance = Mathf.Infinity;
        foreach (var point in GridCreator.Instance.Grid)
        {
            var distance = Math.Abs(transform.position.x - point.transform.position.x)+Math.Abs(transform.position.y - point.transform.position.y)+Math.Abs(transform.position.z - point.transform.position.z);
            if (distance < closestDistance && !point.IsNotValid)
            {
                closestDistance = distance;
                _currentPoint = point;
            }
        }
        return _currentPoint;
    }
}
