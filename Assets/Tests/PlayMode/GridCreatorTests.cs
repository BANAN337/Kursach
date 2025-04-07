using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class GridCreatorTests
    {
        [UnityTest]
        public IEnumerator CreateGrid_CorrectNumberOfPoints()
        {
            var gameObject = new GameObject();
            var gridCreator = gameObject.AddComponent<GridCreator>();
            gridCreator.SetValues(3, 3, 3, new Vector3Int(0, 0, 0), 1, AssetDatabase.LoadAssetAtPath<Point>(@"Assets/Prefabs/Point.prefab"));

            gridCreator.PublicCreateGrid();

            yield return new WaitForSeconds(1);
            
            Assert.AreEqual(27, gridCreator.Grid.Length);
        }

        [UnityTest]
        public IEnumerator AddNeighboursTpPoints_CorrectNumberOfNeighbouringPoints()
        {
            var gameObject = new GameObject();
            var gridCreator = gameObject.AddComponent<GridCreator>();
            gridCreator.SetValues(3, 3, 3, new Vector3Int(0, 0, 0), 1, AssetDatabase.LoadAssetAtPath<Point>(@"Assets/Prefabs/Point.prefab"));

            gridCreator.PublicCreateGrid();
            
            yield return new WaitForSeconds(1);
            
            Assert.AreEqual(3, gridCreator.AddNeighboursToPoints(gridCreator.Grid[0,0,0]).Count);
            Assert.AreEqual(6, gridCreator.AddNeighboursToPoints(gridCreator.Grid[1,1,1]).Count);
            Assert.AreEqual(3, gridCreator.AddNeighboursToPoints(gridCreator.Grid[2,2,2]).Count);
        }

        [UnityTest]
        public IEnumerator Awake_OnlyOneInstanceIsPresent()
        {
            var firstGrid = new GameObject().AddComponent<GridCreator>();
            var secondGrid = new GameObject().AddComponent<GridCreator>();
            
            yield return new WaitForSeconds(1);
            
            Assert.IsNull(secondGrid);
        }
    }
}
