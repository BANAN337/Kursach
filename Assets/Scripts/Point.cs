using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Point : MonoBehaviour
{
    public bool isInvalid;
    public List<Point> neighbours;

    private void Start()
    {
        neighbours = new List<Point>();
        
    }

    public void PrintNeighbourCoordinates()
    {
        foreach (var neighbour in neighbours)
        {
            //Debug.Log($"Point coordinates:{transform.position}, Neighbours coordinates:{neighbour.transform.position}");
        }
    }
}
