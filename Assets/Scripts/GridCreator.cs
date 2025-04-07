using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

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
    }

    private void Start()
    {
        CreateGrid();
    }

    public void SetValues(int gridWidth, int gridLength, int gridHeight, Vector3Int startPosition, int pointDistance,
        Point pointPrefab)
    {
        this.gridWidth = gridWidth;
        this.gridLength = gridLength;
        this.gridHeight = gridHeight;
        this.startPosition = startPosition;
        this.pointDistance = pointDistance;
        this.pointPrefab = pointPrefab;
    }

    public void PublicCreateGrid()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        Grid = new Point[gridWidth, gridHeight, gridHeight];
        for (var i = 0; i < gridWidth; i++)
        {
            for (var j = 0; j < gridHeight; j++)
            {
                for (var k = 0; k < gridLength; k++)
                {
                    Point point = Instantiate(pointPrefab, startPosition + new Vector3(i, j, k) * pointDistance,
                        Quaternion.identity);
                    point.IsValid = !Physics.CheckSphere(point.transform.position, 1);
                    Grid[i, j, k] = point;
                    point.indexes = new Vector3Int(i, j, k);
                }
            }
        }
        //AddNeighbours();
    }

    private void AddNeighbours()
    {
        for (var i = 0; i < gridWidth; i++)
        {
            for (var j = 0; j < gridHeight; j++)
            {
                for (var k = 0; k < gridLength; k++)
                {
                    for (var nearbyPoint = -1; nearbyPoint <= 1; nearbyPoint += 2)
                    {
                        if (i + nearbyPoint >= 0 && i + nearbyPoint < gridWidth)
                        {
                            Grid[i, j, k].neighbours.Add(Grid[i + nearbyPoint, j, k]);
                        }

                        if (j + nearbyPoint >= 0 && j + nearbyPoint < gridHeight)
                        {
                            Grid[i, j, k].neighbours.Add(Grid[i, j + nearbyPoint, k]);
                        }

                        if (k + nearbyPoint >= 0 && k + nearbyPoint < gridLength)
                        {
                            Grid[i, j, k].neighbours.Add(Grid[i, j, k + nearbyPoint]);
                        }
                    }
                }
            }
        }
    }

    public List<Point> AddNeighboursToPoints(Point point)
    {
        for (var nearbyPoint = -1; nearbyPoint <= 1; nearbyPoint += 2)
        {
            if (point.indexes.x + nearbyPoint >= 0 && point.indexes.x + nearbyPoint < gridWidth)
            {
                Grid[point.indexes.x, point.indexes.y, point.indexes.z].neighbours.Add(Grid[point.indexes.x + nearbyPoint, point.indexes.y, point.indexes.z]);
            }

            if (point.indexes.y + nearbyPoint >= 0 && point.indexes.y + nearbyPoint < gridHeight)
            {
                Grid[point.indexes.x, point.indexes.y, point.indexes.z].neighbours.Add(Grid[point.indexes.x, point.indexes.y + nearbyPoint, point.indexes.z]);
            }

            if (point.indexes.z + nearbyPoint >= 0 && point.indexes.z + nearbyPoint < gridLength)
            {
                Grid[point.indexes.x, point.indexes.y, point.indexes.z].neighbours.Add(Grid[point.indexes.x, point.indexes.y, point.indexes.z + nearbyPoint]);
            }
        }
        return point.neighbours;
    }
}