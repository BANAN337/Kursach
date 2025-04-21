using System;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool _isNotValid;
    private GameObject _wall;
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

    public void CreateWall()
    {
        if (IsNotValid == false)
        {
            _wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _wall.transform.position = transform.position;
        }

        if (IsNotValid)
        {
            Destroy(_wall);
        }
    }
}
