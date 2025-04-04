using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private Point pointPrefab;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridLength;
    [SerializeField] private int gridHeight;
    [SerializeField] private Vector3Int startPosition;
    [SerializeField] private int pointDistance;
    public Point[,,] _grid;
    

    private void Start()
    {
        CreateGrid();
    }

    public void SetValues(int gridWidth, int gridLength, int gridHeight, Vector3Int startPosition, int pointDistance, Point pointPrefab)
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
        _grid = new Point[gridWidth, gridHeight, gridHeight];
        for (var i = 0; i < gridWidth; i++)
        {
            for (var j = 0; j < gridHeight; j++)
            {
                for (var k = 0; k < gridLength; k++)
                {
                    Point point = Instantiate(pointPrefab, startPosition + new Vector3(i, j, k) * pointDistance,
                        Quaternion.identity);
                    _grid[i,j,k] = point;
                }
            }
        }
        Debug.Log(_grid.Length);
        AddNeighbours();
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
                            _grid[i,j,k].neighbours.Add(_grid[i + nearbyPoint,j,k]);
                        }

                        if (j + nearbyPoint >= 0 && j + nearbyPoint < gridHeight)
                        {
                            _grid[i,j,k].neighbours.Add(_grid[i,j + nearbyPoint,k]);
                        }

                        if (k + nearbyPoint >= 0 && k + nearbyPoint < gridLength)
                        {
                            _grid[i,j,k].neighbours.Add(_grid[i,j,k + nearbyPoint]);
                        }
                        
                        _grid[i,j,k].PrintNeighbourCoordinates();
                    }
                }
            }
        }
    }
}