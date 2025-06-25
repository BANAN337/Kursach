using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.PlayMode
{
    public class GridCreatorTests
    {
        [ExcludeFromCoverage]
        public static IEnumerable<(Vector3Int,int)> AddNeighboursToPointsData()
        {
            yield return (new Vector3Int(0, 0, 0), 7);
            yield return (new Vector3Int(1, 1, 1), 26);
            yield return (new Vector3Int(2, 2, 2), 7);
        }

        [UnityTest]
        public IEnumerator CreateGrid_CorrectNumberOfPoints()
        {
            //Arrange
            var gridCreator = new GameObject().AddComponent<GridCreator>();
            gridCreator.SetValues(3, 3, 3, 1,
                AssetDatabase.LoadAssetAtPath<Point>(@"Assets/Prefabs/Point.prefab"));

            //Act
            gridCreator.PublicCreateGrid();

            yield return new WaitForSeconds(1);

            //Assert
            Assert.AreEqual(27, gridCreator.Grid.Length);
        }

        [UnityTest]
        public IEnumerator AddNeighboursToPoints_CorrectNumberOfNeighbouringPoints(
            [ValueSource(nameof(AddNeighboursToPointsData))] (Vector3Int, int) data)
        {
            //Arrange
            var gridCreator = new GameObject().AddComponent<GridCreator>();
            gridCreator.SetValues(3, 3, 3, 1,
                AssetDatabase.LoadAssetAtPath<Point>(@"Assets/Prefabs/Point.prefab"));

            //Act
            gridCreator.PublicCreateGrid();

            yield return new WaitForSeconds(1);
            
            //Assert
            Assert.AreEqual(data.Item2, gridCreator.AddNeighboursToPoint(gridCreator.Grid[data.Item1.x, data.Item1.y, data.Item1.z]).Count);
        }
    }
}