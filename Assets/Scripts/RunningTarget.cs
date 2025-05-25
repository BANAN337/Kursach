public class RunningTarget : GridDecorator
{
    private Point _closestPoint;

    private void Awake()
    {
        Movement = GetComponent<Move>();
    }
    
    private void MoveToNextPoint()
    {
        if (canMove)
        {
            var closestPointNeighbours = gridCreator.AddNeighboursToPoint(_closestPoint);
            var newPointIndex = new System.Random().Next(0, closestPointNeighbours.Count);
            if (closestPointNeighbours.Count <= 0) return;
            Movement.MoveToNextPoint(closestPointNeighbours[newPointIndex]);
            _closestPoint = closestPointNeighbours[newPointIndex];
        }
    }

    public override void StartMovement()
    {
        CancelInvoke();
        var closestPoint = GetClosestPoint();
        transform.position = closestPoint.transform.position;
        _closestPoint = closestPoint;
        InvokeRepeating(nameof(MoveToNextPoint), 1, 5);
    }
}
