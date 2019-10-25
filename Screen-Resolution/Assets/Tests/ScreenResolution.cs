using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ScreenResolution
    {
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator NewTestScriptSimplePasses()
        {
            float height = Screen.currentResolution.height;
            float width = Screen.currentResolution.width;
			bool isTrue;

			Screen.SetResolution(400, 600, Screen.fullScreen);
			yield return new WaitForSeconds(2);

			isTrue = !((height == Screen.height) && (width == Screen.width));
			Debug.Log("height  " + Screen.height + "  previous height  " + height);
			Assert.IsTrue(isTrue);
        }
    }
}