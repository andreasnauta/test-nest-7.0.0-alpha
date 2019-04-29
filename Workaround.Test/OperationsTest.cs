using System;
using System.Linq;
using Common.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Workaround.Test
{
    [TestClass]
    public class OperationsTest
    {
        [TestMethod]
        public void InsertTest()
        {
            Operations operations = new Operations();

            operations.Insert();
        }
    }
}
