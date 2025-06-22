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
            var closestPointNeighbours = gridCreator.AddNeighboursToPoint(_currentPoint);
            var newPointIndex = new System.Random().Next(0, closestPointNeighbours.Count);
            if (closestPointNeighbours.Count <= 0) return;
            Movement.MoveToNextPoint(closestPointNeighbours[newPointIndex]);
            _currentPoint = closestPointNeighbours[newPointIndex];
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        var closestPoint = GetClosestPoint();
        transform.position = closestPoint.transform.position;
        _currentPoint = closestPoint;
        InvokeRepeating(nameof(MoveToNextPoint), 1, 5);
    }
}
