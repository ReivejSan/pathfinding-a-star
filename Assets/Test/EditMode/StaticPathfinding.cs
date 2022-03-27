using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StaticPathfinding
{
    // A Test behaves as an ordinary method
    [Test]
    public void StaticPathfindingSimplePasses()
    {
        // Use the Assert class to test conditions
        var pathfinding = new GameObject("GameManager");
        pathfinding.GetComponent<Pathfinding>();

        Assert.IsTrue(pathfinding);
    }
}
