using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool isInvalid;
    public List<Point> neighbours;

    private void Awake()
    {
        neighbours = new List<Point>();
    }
}
