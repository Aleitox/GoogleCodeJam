using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoogleCodeJam.Interpreter;
using GoogleCodeJamPractice;

namespace TestGoogleJamPractice
{
    [TestClass]
    public class TestInfoToInitializeObject
    {
        [TestMethod]
        public void TestConstructor()
        {
            var problem = new Problem();
            var hola = new ObjectInitializer(problem);
        }
    }
}
