using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnTest
{
    [UnityTest]
    public IEnumerator SpawnTestWithEnumeratorPasses()
    {
        var gameObject = new GameObject();
        var manager = gameObject.AddComponent<TestManager>();

        Assert.AreEqual(0, manager.getCount());

        yield return new WaitForSeconds(manager.time + 0.1f);

        Assert.AreEqual(1, manager.getCount());
    }
}
