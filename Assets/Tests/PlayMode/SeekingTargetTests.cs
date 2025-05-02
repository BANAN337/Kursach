using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SeekingTargetTests
    {
        [UnityTest]
        public IEnumerator MoveToNextPoint_MovesAccordingToShortestPath()
        {
            var grid = new GameObject().AddComponent<GridCreator>();
            grid.SetValues(5, 5, 5, new Vector3Int(0, 0, 0), 1,
                AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            grid.PublicCreateGrid();

            yield return new WaitForSeconds(1);

            var gameObject = new GameObject();
            var movement = gameObject.AddComponent<Move>();
            var seekingTarget = gameObject.AddComponent<SeekingTarget>();
            var pathHandler = new GameObject().AddComponent<PathHandler>();
            var runningTarget = new GameObject().AddComponent<RunningTarget>();

            var closestPoint = seekingTarget.GetType()
                .GetField("_closestPoint", BindingFlags.NonPublic | BindingFlags.Instance);
            closestPoint?.SetValue(seekingTarget, grid.Grid[0, 0, 0]);

            runningTarget.transform.position = grid.Grid[4, 4, 4].transform.position;
            var endGoal = pathHandler.GetType()
                .GetField("runningTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            endGoal?.SetValue(pathHandler, runningTarget);

            var currentPathHandler = seekingTarget.GetType()
                .GetField("pathHandler", BindingFlags.NonPublic | BindingFlags.Instance);
            currentPathHandler?.SetValue(seekingTarget, pathHandler);

            var moveToNextPoint = seekingTarget.GetType()
                .GetMethod("MoveToNextPoint", BindingFlags.NonPublic | BindingFlags.Instance);

            moveToNextPoint?.Invoke(seekingTarget, new object[] { });

            yield return new WaitForSeconds(1);

            Assert.AreEqual(seekingTarget.transform.position, grid.Grid[1, 1, 1].transform.position);
        }

        [UnityTest]
        public IEnumerator StartMovement_SetsClosestPoint()
        {
            var grid = new GameObject().AddComponent<GridCreator>();
            grid.SetValues(5, 5, 5, new Vector3Int(0, 0, 0), 1,
                AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            grid.PublicCreateGrid();

            yield return new WaitForSeconds(1);

            var gameObject = new GameObject();
            var seekingTarget = gameObject.AddComponent<SeekingTarget>();
            var move = gameObject.AddComponent<Move>();
            seekingTarget.transform.position = new Vector3(-100,-100,-100);
            
            seekingTarget.StartMovement();
            
            yield return new WaitForSeconds(1);
            
            Assert.AreEqual(seekingTarget.transform.position, grid.Grid[0, 0, 0].transform.position);
        }
    }
}