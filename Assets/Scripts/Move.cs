using UnityEngine;

public class Move : MonoBehaviour
{
    public void MoveToNextPoint(Point point)
    {
        transform.position = point.transform.position;
        
    }
}
