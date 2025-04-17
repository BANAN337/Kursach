using System;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool _isNotValid;
    public Material material;
    public Point previousPoint;
    public List<Point> neighbours;
    public int hScore;
    public int gScore;
    public event Action<Point> OnIsValidChanged;
    public Vector3Int indexes;

    public int FScore => gScore + hScore;

    public bool IsNotValid
    {
        get => _isNotValid;
        set
        {
            if (value != _isNotValid)
            {
                OnIsValidChanged?.Invoke(this);
            }

            _isNotValid = value;
        }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        neighbours = new List<Point>();
    }
}
