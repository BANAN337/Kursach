using System;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool _isValid = true;
    public Material material;
    public Point previousPoint;
    public List<Point> neighbours;
    public int hScore;
    public int gScore;
    public event Action OnIsValidChanged;
    public Vector3Int indexes;

    public int FScore => gScore + hScore;

    public bool IsValid
    {
        get => _isValid;
        set
        {
            if (value != _isValid)
            {
                OnIsValidChanged?.Invoke();
            }
            _isValid = value;
        }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        neighbours = new List<Point>();
    }
}
