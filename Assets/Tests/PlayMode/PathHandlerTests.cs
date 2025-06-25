using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.PlayMode
{
    public class PathHandlerTests
    {
        public static IEnumerable<(Vector3Int, bool)> TestData()
        {
            yield return (new Vector3Int(0, 1, 0), false);
            yield return (new Vector3Int(1, 0, 0), true);
        }

        [UnityTest]
        public IEnumerator IsEndNotReached_ReturnsTrueIfPositionsAreTheSameFalseIfNot(
            [ValueSource(nameof(TestData))] (Vector3Int runningTargetPos, bool isNotReached) data)
        {
            //Arrange
            var gridCreator = new GameObject().AddComponent<GridCreator>();
            gridCreator.SetValues(5, 5, 5, 1,
                AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            gridCreator.PublicCreateGrid();

            yield return new WaitForSeconds(1);
            
            var runningTarget = new GameObject().AddComponent<RunningTarget>();
            var runningTargetGrid = runningTarget.GetType()
                .GetField("gridCreator", BindingFlags.NonPublic | BindingFlags.Instance);
            runningTargetGrid?.SetValue(runningTarget, gridCreator);
            
            var pathHandler = new GameObject().AddComponent<PathHandler>();
            var pathHandlerRunningTarget = pathHandler.GetType()
                .GetField("runningTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            pathHandlerRunningTarget?.SetValue(pathHandler, runningTarget);
            
            yield return new WaitForSeconds(1);

            //Act
            runningTarget.transform.position = gridCreator
                .Grid[data.runningTargetPos.x, data.runningTargetPos.y, data.runningTargetPos.z].transform.position;

            yield return new WaitForSeconds(1);

            //Assert
            Assert.AreEqual(data.isNotReached, pathHandler.IsEndNotReached(gridCreator.Grid[0, 1, 0]));
        }
    }
}