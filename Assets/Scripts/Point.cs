using System;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool _isNotNotValid;
    public Material material;
    public Point previousPoint;
    public List<Point> neighbours;
    public int hScore;
    public int gScore;
    public event Action OnIsValidChanged;
    public Vector3Int indexes;

    public int FScore => gScore + hScore;

    public bool IsNotValid
    {
        get => _isNotNotValid;
        set
        {
            if (value != _isNotNotValid)
            {
                OnIsValidChanged?.Invoke();
            }

            _isNotNotValid = value;
        }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        neighbours = new List<Point>();
    }
}
