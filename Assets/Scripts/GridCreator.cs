using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = System.Random;

[assembly: InternalsVisibleTo("PlayMode")]

public class GridCreator : MonoBehaviour
{
    public static GridCreator Instance;
    [SerializeField] private Point pointPrefab;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridLength;
    [SerializeField] private int gridHeight;
    [SerializeField] private Vector3Int startPosition;
    [SerializeField] private int pointDistance;
    public Point[,,] Grid;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CreateGrid();
    }

    internal void SetValues(int gridWidth, int gridLength, int gridHeight, Vector3Int startPosition, int pointDistance,
        Point pointPrefab)
    {
        this.gridWidth = gridWidth;
        this.gridLength = gridLength;
        this.gridHeight = gridHeight;
        this.startPosition = startPosition;
        this.pointDistance = pointDistance;
        this.pointPrefab = pointPrefab;
    }

    internal void PublicCreateGrid()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        Grid = new Point[gridWidth, gridHeight, gridLength];
        for (var i = 0; i < gridWidth; i++)
        {
            for (var j = 0; j < gridHeight; j++)
            {
                for (var k = 0; k < gridLength; k++)
                {
                    var point = Instantiate(pointPrefab, startPosition + new Vector3(i, j, k) * pointDistance,
                        Quaternion.identity);
                    point.OnIsValidChanged += point.CreateWall;
                    point.IsNotValid = !Physics.CheckSphere(point.transform.position, 1) || new Random().Next(0,100) < 10;
                    Grid[i, j, k] = point;
                    point.indexes = new Vector3Int(i, j, k);
                }
            }
        }
    }

    public List<Point> AddNeighboursToPoint(Point point)
    {
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                for (var k = -1; k <= 1; k++)
                {
                    if (i == 0 && j == 0 && k == 0)
                    {
                        continue;
                    }

                    if (point.indexes.x + i < gridWidth && point.indexes.x + i >= 0 &&
                        point.indexes.y + j < gridHeight && point.indexes.y + j >= 0 &&
                        point.indexes.z + k < gridLength && point.indexes.z + k >= 0 &&
                        Grid[point.indexes.x + i, point.indexes.y + j, point.indexes.z + k].IsNotValid == false)
                    {
                        point.neighbours.Add(Grid[point.indexes.x + i, point.indexes.y + j, point.indexes.z + k]);
                    }
                }
            }
        }

        return point.neighbours;
    }

    private void CreateWall(Point point)
    {
        var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = point.transform.position;
        wall.transform.localScale *= pointDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(startPosition + new Vector3(gridWidth - 1, gridHeight - 1, gridLength - 1) / 2,
            (startPosition + new Vector3(gridWidth - 1, gridHeight - 1, gridLength - 1)) *
            pointDistance);
    }
}