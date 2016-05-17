using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace UnityTest
{
    [TestFixture]
    //[Category("Sample Tests")]
    internal class SampleTests
    {

   public class Employee
        {
            public string access_token { get; set; }
            //public string LastName { get; set; }
        }

        List<Course> courses;
        Parser parser;
        //Employee employee;

        [SetUp]
        public void Setup()
        {
            courses = new List<Course>();
        }

        [TearDown]
        public void TearDown()
        {
            courses = null;
            parser = null;
        }

        [Test]
        public void TestOne()
        {
            int a = 200;
            int b = 200;
            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestTwo()
        {
            //form.AddField("access_token", "1278a819-72d3-4a1c-a68b-6ecce9684311");
                   
            string jsonString = "[{\"coursecode\":\"HEJ\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"Är solen varm\",\"answer\":\" ? \"},{\"coursecode\":\"HEJ\",\"momentcode\":\"260\",\"questionid\":\"264\",\"question\":\"Glass är gott\",\"answer\":\".\"},{\"coursecode\":\"HEJ\",\"momentcode\":\"260\",\"questionid\":\"267\",\"question\":\"Hjälp mig\",\"answer\":\"!\"}]";
            jsonString = "[{\"coursecode\":\"HEJ\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"Är solen varm\",\"answer\":\" ? \"}]"; //"1";
            //jsonString = "1";
            parser = new Parser(jsonString, courses, 2);
            Assert.AreEqual(courses.Count, 1);

        }
        

        


        /*[Test]
        [Category("Failing Tests")]
        public void ExceptionTest()
        {
            throw new Exception("Exception throwing test");
        }

        [Test]
        [Ignore("Ignored test")]
        public void IgnoredTest()
        {
            throw new Exception("Ignored this test");
        }

        [Test]
        [MaxTime(100)]
        [Category("Failing Tests")]
        public void SlowTest()
        {
            Thread.Sleep(200);
        }

        [Test]
        [Category("Failing Tests")]
        public void FailingTest()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Failing Tests")]
        public void InconclusiveTest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void PassingTest()
        {
            Assert.Pass();
        }

        [Test]
        public void ParameterizedTest([Values(1, 2, 3)] int a)
        {
            Assert.Pass();
        }

        [Test]
        public void RangeTest([NUnit.Framework.Range(1, 10, 3)] int x)
        {
            Assert.Pass();
        }

        [Test]
        [Culture("pl-PL")]
        public void CultureSpecificTest()
        {
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "expected message")]
        public void ExpectedExceptionTest()
        {
            throw new ArgumentException("expected message");
        }

        [Datapoint]
        public double zero = 0;
        [Datapoint]
        public double positive = 1;
        [Datapoint]
        public double negative = -1;
        [Datapoint]
        public double max = double.MaxValue;
        [Datapoint]
        public double infinity = double.PositiveInfinity;

        [Theory]
        public void SquareRootDefinition(double num)
        {
            Assume.That(num >= 0.0 && num < double.MaxValue);

            var sqrt = Math.Sqrt(num);

            Assert.That(sqrt >= 0.0);
            Assert.That(sqrt * sqrt, Is.EqualTo(num).Within(0.000001));
        }*/
    }
}
