using System;
using UnityEngine;
using Random = System.Random;

public abstract class Target : MonoBehaviour
{
    protected Point CurrentPoint;
    
    public bool canMove = true;

    public abstract void StartMovement();
}
