using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class PathfindingTests
    {
        [UnityTest]
        public IEnumerator FindPath_CorrectNumberOfPointsInShortestPath()
        {
            var gameObject = new GameObject();
            var gameObject1 = new GameObject();

            var pathfindingType = typeof(Pathfinding);

            var gridCreator = gameObject1.AddComponent<GridCreator>();
            var pathfinding = gameObject.AddComponent<Pathfinding>();

            gridCreator.SetValues(5, 5, 5, new Vector3Int(0, 0, 0), 1,
                AssetDatabase.LoadAssetAtPath<Point>(@"Assets/Prefabs/Point.prefab"));
            gridCreator.PublicCreateGrid();

            yield return new WaitForSeconds(1);

            var findPathMethod = pathfindingType.GetMethod("FindPath", BindingFlags.NonPublic | BindingFlags.Instance);
            var startPoint = gridCreator.Grid[0, 0, 0];
            var endPoint = gridCreator.Grid[4, 4, 4];
            findPathMethod?.Invoke(pathfinding, new object[] { startPoint, endPoint });

            yield return new WaitForSeconds(1);

            Assert.AreEqual(4, pathfinding.ShortestPath.Count);
        }
    }
}