using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace UnityTest
{
    [TestFixture]
    internal class SampleTests
    {
        public class Employee
        {
            public string access_token { get; set; }
        }

        List<Course> courses;
        Parser parser;

        [SetUp]
        public void Setup()
        {
            courses = new List<Course>();                   //F�re varje test skapas en ny tom lista f�r kurser. 
        }

        [TearDown]
        public void TearDown()
        {
            courses = null;                                 // Efter varje test blir b�de kurslistan och parsern null.
            parser = null;                                  // Detta f�r att ingen gammal information ska sparas av misstag.
        }

        [Test]
        public void TestNumberOfCourses()
        {
            // Ett jsonobjekt skapas p� �konstgjord� v�g:
            string jsonString = "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"katt\",\"answer\":\" cat \"}]";

            // Ett nytt jsonobjekt med samma kurskod, men inte samma fr�ga, skapas
            string jsonString2 = "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"264\",\"question\":\"hund\",\"answer\":\"dog\"}]";

            parser = new Parser(jsonString, courses, 2);     // F�rsta objektet skickas in f�r bearbetning    
            parser = new Parser(jsonString2, courses, 2);    // Andra objektet skickas in p� samma s�tt

            Assert.AreEqual(courses.Count, 1);               // F�rv�ntat resultat �r att kurslistan har en kurs
        }
    }
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
   // }
//}
