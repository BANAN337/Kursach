using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEngine.Object;

namespace Tests.PlayMode
{
    public class PointTests
    {
        [ExcludeFromCodeCoverage]
        public static IEnumerable<(bool isPointAllowed, Color expectedColor)> DisablePointData()
        {
            yield return (true, Color.red);
            yield return (false, Color.white);
        }
            
        [UnityTest]
        public IEnumerator DisablePoint_ChangesColorOnIsNotValidChange([ValueSource(nameof(DisablePointData))] (bool isPointAllowed, Color expectedColor) data)
        {
            //Arrange
            var point = Instantiate(AssetDatabase.LoadAssetAtPath<Point>("Assets/Prefabs/Point.prefab"));
            
            point.OnIsValidChanged += point.DisablePoint;
            
            //Act
            for (var i = 0; i < 3; i++)
            {
                point.IsNotValid = data.isPointAllowed;
                data.isPointAllowed = !data.isPointAllowed;
            }

            yield return new WaitForSeconds(1);
        
            //Assert
            Assert.AreEqual(data.expectedColor, point.material.color);
        }
    }
}
