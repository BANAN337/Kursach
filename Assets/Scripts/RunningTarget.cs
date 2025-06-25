public class RunningTarget : Target
{
    private void Awake()
    {
        Movement = GetComponent<Move>();
    }
    
    private void MoveToNextPoint()
    {
        if (canMove)
        {
            var closestPointNeighbours = gridCreator.AddNeighboursToPoint(CurrentPoint);
            var newPointIndex = new System.Random().Next(0, closestPointNeighbours.Count);
            if (closestPointNeighbours.Count <= 0) return;
            Movement.MoveToNextPoint(closestPointNeighbours[newPointIndex]);
            CurrentPoint = closestPointNeighbours[newPointIndex];
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        var closestPoint = GetClosestPoint();
        transform.position = closestPoint.transform.position;
        CurrentPoint = closestPoint;
        InvokeRepeating(nameof(MoveToNextPoint), 1, 5);
    }
}
