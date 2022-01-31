using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestLibrary;
using SampleLibrary;
using System;

namespace MSUnitTestSample
{
    [TestClass]
    public class SampleTest : TestClassBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context) { }

        [TestInitialize]
        public void TestInitialize()
        {
            var accessor = PrivateAccessorFactory.Create(typeof(SampleClass));
            accessor.SetMember("s_id", 0);
        }

        [TestCleanup]
        public void TestCleanup() { }

        [ClassCleanup]
        public static void ClassCleanup() { }


        [TestMethod]
        public void UnitTestMethod1()
        {
            var instance = new SampleClass("foo", 456);
            Assert.IsNotNull(instance);
            Assert.AreEqual("foo", instance.Name);
            Assert.AreEqual(456, instance.Age);

            var typeAccessor = PrivateAccessorFactory.Create(typeof(SampleClass));
            var instanceAccessor = PrivateAccessorFactory.Create(instance);


            // インスタンスメソッド
            Assert.AreEqual("foo 456", instance.GetName());
            Assert.AreEqual("foo 456", instanceAccessor.DoMethod<string>("GetNameInternal"));

            // staticメソッド 
            Assert.AreEqual(1, SampleClass.GetNextId());
            Assert.AreEqual(1, typeAccessor.DoMethod<int>("GetNextIdInternal"));

            Assert.AreEqual(456, instance.Age);
            Assert.AreEqual(456, instanceAccessor.GetMember<int>("m_age"));

            // staticメンバ
            Assert.AreEqual(1, SampleClass.NextId);
            Assert.AreEqual(1, typeAccessor.GetMember<int>("s_id"));

            int seed = 0xdead;
            // インスタンスメソッド 引数付き
            Assert.AreEqual(456 ^ seed, instance.GetCryptedAge(seed));
            Assert.AreEqual(456 ^ seed, instanceAccessor.DoMethod<int>("GetCryptedAgeInternal", seed));

            // staticメソッド 引数付き
            Assert.AreEqual(1 ^ seed, SampleClass.GetCryptedNextId(seed));
            Assert.AreEqual(1 ^ seed, typeAccessor.DoMethod<int>("GetCryptedNextIdInternal", seed));
        }

        [TestMethod]
        public void UnitTestException1()
        {
            var ex = ThrowsException<ArgumentNullException>(() =>
            {
                new SampleClass("foo", 456).DummyWrite(null);
            });

            Assert.AreEqual("text", ex.ParamName);
        }

        [TestMethod]
        [DeploymentItem("TestData\\Data1.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\Data1.csv", "Data1#csv", DataAccessMethod.Random)]
        public void UnitTestDataSource1()
        {
            Assert.AreEqual(TestField<int>("Num2"), TestField<int>("Num1") * 2);
        }
    }
}
