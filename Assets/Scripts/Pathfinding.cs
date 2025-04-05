using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private float DistanceBetweenPoints(Point pointA, Point pointB)
    {
        return pointA.transform.position.x - pointB.transform.position.x + pointA.transform.position.y -
            pointB.transform.position.y + pointA.transform.position.z - pointB.transform.position.z;
    }
}