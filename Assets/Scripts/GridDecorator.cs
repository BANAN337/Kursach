using System;
using UnityEngine;

public abstract class GridDecorator : Target
{
    protected Target Target;
    protected Move Movement;
    [SerializeField] protected GridCreator gridCreator;
    public GridCreator GridCreator => gridCreator;
    
    private void Awake()
    {
        Movement = GetComponent<Move>();
    }

    public override void StartMovement()
    {
        Target.StartMovement();
    }

    public Point GetClosestPoint()
    {
        var closestDistance = Mathf.Infinity;
        foreach (var point in gridCreator.Grid)
        {
            var distance = Math.Abs(transform.position.x - point.transform.position.x) +
                           Math.Abs(transform.position.y - point.transform.position.y) +
                           Math.Abs(transform.position.z - point.transform.position.z);
            if (distance < closestDistance && !point.IsNotValid)
            {
                closestDistance = distance;
                CurrentPoint = point;
            }
        }

        return CurrentPoint;
    }
}