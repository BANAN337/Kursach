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
    public class RunningTargetTests
    {
        [UnityTest]
        public IEnumerator MoveToNextPoint_TargetChangesItsPosition()
        {
            var grid = new GameObject().AddComponent<GridCreator>();
            grid.SetValues(5, 5, 5, 1,
                AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            grid.PublicCreateGrid();
            var possiblePoints = new List<Point>
            {
                grid.Grid[0, 0, 1], grid.Grid[0, 1, 0], grid.Grid[1, 0, 0], grid.Grid[0, 1, 1], grid.Grid[1, 0, 1],
                grid.Grid[1, 1, 0], grid.Grid[1, 1, 1]
            };
        
            yield return new WaitForSeconds(1);
        
            var gameObject = new GameObject();
            var movement = gameObject.AddComponent<Move>();
            var runningTarget = gameObject.AddComponent<RunningTarget>();
            var moveToNextPoint =
                runningTarget.GetType().GetMethod("MoveToNextPoint", BindingFlags.NonPublic | BindingFlags.Instance);
            var currentPoint =
                runningTarget.GetType().GetField("_closestPoint", BindingFlags.NonPublic | BindingFlags.Instance);
            currentPoint?.SetValue(runningTarget, grid.Grid[0, 0, 0]);
            var runningTargetGrid = runningTarget.GetType()
                .GetField("gridCreator", BindingFlags.NonPublic | BindingFlags.Instance);
            runningTargetGrid?.SetValue(runningTarget, grid);
            
            moveToNextPoint?.Invoke(runningTarget, new object[] { });
            
        
            yield return new WaitForSeconds(1);
        
            var changedPos = false;
            foreach (var point in possiblePoints)
            {
                if (runningTarget.transform.position == point.transform.position)
                {
                    changedPos = true;
                }
            }
        
            Assert.IsTrue(changedPos);
        }

        [UnityTest]
        public IEnumerator StartMovement_SetsStartingPosition()
        {
            var grid = new GameObject().AddComponent<GridCreator>();
            grid.SetValues(5, 5, 5, 1,
                AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            grid.PublicCreateGrid();

            yield return new WaitForSeconds(1);

            var gameObject = new GameObject();
            gameObject.transform.position = new Vector3(-1, -1, -1);
            var movement = gameObject.AddComponent<Move>();
            var runningTarget = gameObject.AddComponent<RunningTarget>();
            
            var runningTargetGrid = runningTarget.GetType()
                .GetField("gridCreator", BindingFlags.NonPublic | BindingFlags.Instance);
            runningTargetGrid?.SetValue(runningTarget, grid);
            
            yield return new WaitForSeconds(0.5f);
            
            runningTarget.StartMovement();
            
            Assert.AreEqual(grid.Grid[0, 0, 0].transform.position, runningTarget.transform.position);
        }
    }
}