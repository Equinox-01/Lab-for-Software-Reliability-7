using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TestDLL.Tests
{
    [TestClass()]
    public class MainTestDLL
    {
        public TestContext TestContext { get; set; }

        //SetMethod
        [ExpectedException(typeof(ArgumentNullException), "Indata is null")]
        [TestMethod()]
        public void Set_NullPointer_Test()
        {
            string indata = null;
            var instance = new Test<string>();
            instance.Set(indata);
        }

        [TestMethod()]
        public void Set_Success_Test()
        {
            string indata = "test";
            var instance = new Test<string>();
            instance.Set(indata);
        }

        //GetIntegerStatus
        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        public void GetIntegerStatus_Success_Test()
        {
            string indata = TestContext.DataRow["CorrectInt"].ToString();
            var instance = new Test<string>();
            instance.Set(indata);
            Assert.IsTrue(instance.GetIntegerStatus());
        }

        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        public void GetIntegerStatus_Broken_Test()
        {
            string indata = TestContext.DataRow["IncorrectInt"].ToString();
            var instance = new Test<string>();
            instance.Set(indata);
            Assert.IsFalse(instance.GetIntegerStatus());
        }

        //GetDoubleStatus
        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        public void GetDoubleStatus_Success_Test()
        {
            string indata = TestContext.DataRow["CorrectDouble"].ToString();
            var instance = new Test<string>();
            instance.Set(indata);
            Assert.IsTrue(instance.GetDoubleStatus());
        }

        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        public void GetDoubleStatus_Broken_Test()
        {
            string indata = TestContext.DataRow["IncorrectDouble"].ToString();
            var instance = new Test<string>();
            instance.Set(indata);
            Assert.IsFalse(instance.GetDoubleStatus());
        }

        //GetEmailStatus
        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        public void GetEmailStatus_Success_Test()
        {
            string indata = TestContext.DataRow["CorrectEmail"].ToString();
            var instance = new Test<string>();
            instance.Set(indata);
            Assert.IsTrue(instance.GetEmailStatus());
        }

        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        public void GetEmailStatus_Broken_Test()
        {
            string indata = TestContext.DataRow["IncorrectEmail"].ToString();
            var instance = new Test<string>();
            instance.Set(indata);
            Assert.IsFalse(instance.GetEmailStatus());
        }

        //PrintMainMetaInfo
        [TestMethod()]
        public void PrintMainMetaInfo_Success_Test()
        {
            List<int> indata = new List<int>();
            var instance = new Test<object>();
            instance.Set(indata);
            StringAssert.Contains(instance.PrintMainMetaInfo(), "Serializable ");
        }

        //SerializeObj
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Data is not serializable")]
        public void SerializeObj_NotSerialize_Test()
        {
            NotSerialize indata = new NotSerialize();
            var instance = new Test<NotSerialize>();
            instance.Set(indata);
            instance.SerializeObj("data.dat");
        }

        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        [ExpectedException(typeof(ArgumentException))]
        public void SerializeObj_FileIncorrent_Test()
        {
            string indata = TestContext.DataRow["IncorrectFilename"].ToString();
            int tmp = 0;
            var instance = new Test<int>();
            instance.Set(tmp);
            instance.SerializeObj(indata);
        }

        [TestMethod()]
        public void SerializeObj_Success_Test()
        {
            int tmp = 0;
            var instance = new Test<int>();
            instance.Set(tmp);
            instance.SerializeObj("data.dat");
        }

        //DeserializeObj
        [TestMethod()]
        [DataSource("MyExcelDataSource")]
        [DeploymentItem("TestDLL.Tests\\DataDrivenSource.xlsx")]
        [ExpectedException(typeof(ArgumentException))]
        public void DeserializeObj_FileIncorrent_Test()
        {
            string indata = TestContext.DataRow["IncorrectFilename"].ToString();
            int tmp = 0;
            var instance = new Test<int>();
            instance.Set(tmp);
            instance.SerializeObj(indata);
        }

        [TestMethod()]
        public void DeserializeObj_Success_Test()
        {
            int tmp = 0;
            var instance = new Test<int>();
            instance.Set(tmp);
            instance.SerializeObj("data.dat");
        }
    }
}