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
        public IEnumerator GridCreatorTestsWithEnumeratorPasses()
        {
            var gameObject = new GameObject();
            var gridCreator = gameObject.AddComponent<GridCreator>();
            gridCreator.SetValues(3, 3, 3, new Vector3Int(0, 0, 0), 1, AssetDatabase.LoadAssetAtPath<Point>(@"Assets/Prefabs/Point.prefab"));

            gridCreator.PublicCreateGrid();

            yield return new WaitForSeconds(1);
            
            Assert.AreEqual(27, gridCreator._grid.Length);
        }
    }
}
