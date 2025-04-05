using System;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool _isValid = true;
    public List<Point> neighbours;
    public int hScore;
    public int gScore;
    public event Action OnIsValidChanged;

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
        neighbours = new List<Point>();
    }
}
