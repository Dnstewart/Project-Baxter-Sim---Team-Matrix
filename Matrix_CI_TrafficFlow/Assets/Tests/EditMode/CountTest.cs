using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CountTest
{

    [Test]
    public void CountTestSimplePasses()
    {
        var gameObject = new GameObject();
        var manager = gameObject.AddComponent<TestManager>();
        Assert.AreEqual(0, manager.getCount());
    }

}
