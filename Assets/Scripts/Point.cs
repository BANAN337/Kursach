using System;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour, IHeapItem<Point>
{
    private bool _isNotValid;
    public Material material;
    public Point previousPoint;
    public List<Point> neighbours;
    public int hScore;
    public int gScore;
    public event Action OnIsValidChanged;
    public Vector3Int indexes;

    public int HeapIndex { get; set; }
    public int FScore => gScore + hScore;

    public bool IsNotValid
    {
        get => _isNotValid;
        set
        {
            if (value != _isNotValid)
            {
                OnIsValidChanged?.Invoke();
            }

            _isNotValid = value;
        }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        neighbours = new List<Point>();
    }

    public void DisablePoint()
    {
        if (IsNotValid == false)
        {
            material.color = Color.red;
        }

        if (IsNotValid)
        {
            material.color = Color.white;
        }
    }

    public int CompareTo(Point other)
    {
        var compare = FScore.CompareTo(other.FScore);
        if (compare == 0)
        {
            compare = hScore.CompareTo(other.hScore);
        }
        return -compare;
    }
}