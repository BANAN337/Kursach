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
             //Arrange
             var gridCreator = new GameObject().AddComponent<GridCreator>();
        
             gridCreator.SetValues(5, 5, 5,  1,
                 AssetDatabase.LoadAssetAtPath<Point>(@"Assets/Prefabs/Point.prefab"));
             gridCreator.PublicCreateGrid();
             
             var pathfinding = new Pathfinding(gridCreator);
        
             yield return new WaitForSeconds(1);

             //Act
             var startPoint = gridCreator.Grid[0, 0, 0];
             var endPoint = gridCreator.Grid[4, 4, 4];
             
             var shortestPath = pathfinding.FindPath(startPoint, endPoint);
        
             yield return new WaitForSeconds(1);
        
             //Assert
             Assert.AreEqual(4, shortestPath.Count);
         }
    }
}