using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("PlayMode")]

public class GridCreator : MonoBehaviour
{
    [SerializeField] private Point pointPrefab;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridLength;
    [SerializeField] private int gridHeight;
    [SerializeField] private int pointDistance;
    public Point[,,] Grid { get; private set; }

    private void Awake()
    {
        CreateGrid();
    }

    internal void SetValues(int gridWidthValue, int gridLengthValue, int gridHeightValue,
        int pointDistanceValue,
        Point pointPrefabValue)
    {
        gridWidth = gridWidthValue;
        gridLength = gridLengthValue;
        gridHeight = gridHeightValue;
        pointDistance = pointDistanceValue;
        pointPrefab = pointPrefabValue;
    }

    internal void PublicCreateGrid()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        var pointsParent = new GameObject("Points");
        Grid = new Point[gridWidth, gridHeight, gridLength];
        for (var i = 0; i < gridWidth; i++)
        {
            for (var j = 0; j < gridHeight; j++)
            {
                for (var k = 0; k < gridLength; k++)
                {
                    var point = Instantiate(pointPrefab, transform.position + new Vector3(i, j, k) * pointDistance,
                        Quaternion.identity);
                    point.OnIsValidChanged += point.DisablePoint;
                    point.IsNotValid = !Physics.CheckSphere(point.transform.position, 1);
                    Grid[i, j, k] = point;
                    point.indexes = new Vector3Int(i, j, k);
                    point.transform.SetParent(pointsParent.transform);
                }
            }
        }
    }

    public List<Point> AddNeighboursToPoint(Point point)
    {
        var neighbours = new List<Point>();
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

                    if ((point.indexes.x + i < gridWidth && point.indexes.x + i >= 0 &&
                         point.indexes.y + j < gridHeight && point.indexes.y + j >= 0 &&
                         point.indexes.z + k < gridLength && point.indexes.z + k >= 0) &&
                        Grid[point.indexes.x + i, point.indexes.y + j, point.indexes.z + k].IsNotValid == false)
                    {
                        {
                            neighbours.Add(Grid[point.indexes.x + i, point.indexes.y + j, point.indexes.z + k]);
                        }
                    }
                }
            }
        }

        return neighbours;
    }

    [ExcludeFromCodeCoverage]
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(gridWidth - 1, gridHeight - 1, gridLength - 1) / 2,
            (new Vector3(gridWidth - 1, gridHeight - 1, gridLength - 1)) *
            pointDistance);
    }
}