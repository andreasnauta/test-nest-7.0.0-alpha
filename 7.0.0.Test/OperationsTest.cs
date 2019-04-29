using System;
using System.Linq;
using Common.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace _7._0._0.Test
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
