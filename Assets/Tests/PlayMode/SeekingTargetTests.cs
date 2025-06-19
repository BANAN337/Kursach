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
        public IEnumerator StartMovement_MovesAccordingToShortestPath()
        {
            var grid = new GameObject().AddComponent<GridCreator>();
            grid.SetValues(5, 5, 5,1,
                AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            grid.PublicCreateGrid();

            yield return new WaitForSeconds(1);

            var gameObject = new GameObject();
            var movement = gameObject.AddComponent<Move>();
            var seekingTarget = gameObject.AddComponent<SeekingTarget>();
            var runningTarget = new GameObject().AddComponent<RunningTarget>();

            var pathHandler = new GameObject().AddComponent<PathHandler>();
            var pathHandlerRunningTarget = pathHandler.GetType()
                .GetField("runningTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            pathHandlerRunningTarget?.SetValue(pathHandler, runningTarget);

            var seekingTargetGrid = seekingTarget.GetType()
                .GetField("gridCreator", BindingFlags.NonPublic | BindingFlags.Instance);
            seekingTargetGrid?.SetValue(seekingTarget, grid);

            var runningTargetGrid = runningTarget.GetType()
                .GetField("gridCreator", BindingFlags.NonPublic | BindingFlags.Instance);
            runningTargetGrid?.SetValue(runningTarget, grid);

            var currentPathHandler = seekingTarget.GetType()
                .GetField("pathHandler", BindingFlags.NonPublic | BindingFlags.Instance);
            currentPathHandler?.SetValue(seekingTarget, pathHandler);

            runningTarget.transform.position = grid.Grid[4, 4, 4].transform.position;
            seekingTarget.transform.position = grid.Grid[0, 0, 0].transform.position;

            seekingTarget.StartMovement();

            yield return new WaitForSeconds(3.5f);

            Assert.AreEqual(seekingTarget.transform.position, grid.Grid[1, 1, 1].transform.position);
        }

        [UnityTest]
        public IEnumerator StartMovement_SetsClosestPoint()
        {
            var grid = new GameObject().AddComponent<GridCreator>();
            grid.SetValues(5, 5, 5, 1,
                AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            grid.PublicCreateGrid();

            yield return new WaitForSeconds(1);

            var gameObject = new GameObject();
            var seekingTarget = gameObject.AddComponent<SeekingTarget>();
            var move = gameObject.AddComponent<Move>();
            seekingTarget.transform.position = new Vector3(-100, -100, -100);
            var seekingTargetGrid = seekingTarget.GetType()
                .GetField("gridCreator", BindingFlags.NonPublic | BindingFlags.Instance);
            seekingTargetGrid?.SetValue(seekingTarget, grid);

            seekingTarget.StartMovement();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(seekingTarget.transform.position, grid.Grid[0, 0, 0].transform.position);
        }
    }
}