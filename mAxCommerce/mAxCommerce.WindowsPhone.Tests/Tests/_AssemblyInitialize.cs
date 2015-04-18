using mAxCommerce.WindowsPhone.Tests.Helpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class _AssemblyInitialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            UriHelper.SetupViewLocatorForAllTypes();
        }
    }
}