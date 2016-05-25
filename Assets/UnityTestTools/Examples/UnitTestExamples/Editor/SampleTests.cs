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
            courses = new List<Course>();                   //Före varje test skapas en ny tom lista för kurser. 
        }

        [TearDown]
        public void TearDown()
        {
            courses = null;                                 // Efter varje test blir både kurslistan och parsern null.
            parser = null;                                  // Detta för att ingen gammal information ska sparas av misstag.
        }

        [Test]
        public void TestNumberOfCourses()
        {
            // Ett jsonobjekt skapas på “konstgjord” väg:
            string jsonString = 
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"katt\",\"answer\":\" cat \"}]";

            // Ett nytt jsonobjekt med samma kurskod, men inte samma fråga, skapas
            string jsonString2 = 
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"264\",\"question\":\"hund\",\"answer\":\"dog\"}]";

            parser = new Parser(jsonString, courses, 2);     // Första objektet skickas in för bearbetning    
            parser = new Parser(jsonString2, courses, 2);    // Andra objektet skickas in på samma sätt

            Assert.AreEqual(courses.Count, 1);               // Förväntat resultat är att kurslistan "courses" har en kurs
        }

        [Test]
        public void TestTwoCourses()
        {
            // Ett jsonobjekt skapas på “konstgjord” väg:
            string jsonString =
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"katt\",\"answer\":\" cat \"}]";

            // Ett nytt jsonobjekt med en annan kurskod skapas
            string jsonString2 =
                "[{\"coursecode\":\"EN\",\"momentcode\":\"260\",\"questionid\":\"264\",\"question\":\"hund\",\"answer\":\"dog\"}]";

            parser = new Parser(jsonString, courses, 2);     // Första objektet skickas in för bearbetning    
            parser = new Parser(jsonString2, courses, 2);    // Andra objektet skickas in på samma sätt

            Assert.AreEqual(courses.Count, 2);               // Förväntat resultat är att kurslistan "courses" har två kurser
        }


        [Test]
        public void TestOfOneMoment()
        {
            // Ett jsonobjekt skapas på “konstgjord” väg:
            string jsonString =
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"katt\",\"answer\":\" cat \"}]";

            // Ett nytt jsonobjekt med samma kurskod och samma momentkod skapas
            string jsonString2 =
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"264\",\"question\":\"hund\",\"answer\":\"dog\"}]";

            parser = new Parser(jsonString, courses, 2);     // Första objektet skickas in för bearbetning    
            parser = new Parser(jsonString2, courses, 2);    // Andra objektet skickas in på samma sätt

            Assert.AreEqual(courses[0].levels.Count, 1);     // Förväntat resultat är att kurslistans första kurs har ett moment

        }

        [Test]
        public void TestOfTwoMoments()
        {
            // Ett jsonobjekt skapas på “konstgjord” väg:
            string jsonString =
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"katt\",\"answer\":\" cat \"}]";

            // Ett nytt jsonobjekt med samma kurskod, men inte samma momentkod, skapas
            string jsonString2 =
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"261\",\"questionid\":\"264\",\"question\":\"hund\",\"answer\":\"dog\"}]";

            parser = new Parser(jsonString, courses, 2);     // Första objektet skickas in för bearbetning    
            parser = new Parser(jsonString2, courses, 2);    // Andra objektet skickas in på samma sätt

            Assert.AreEqual(courses[0].levels.Count, 2);               // Förväntat resultat är att kurslistans första kurs har 2 moment

        }
        [Test]
        public void TestOneQuestion()
        {
            // Ett jsonobjekt skapas på “konstgjord” väg:
            string jsonString =
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"katt\",\"answer\":\"cat\"}]";

            // Ett nytt jsonobjekt med samma kurskod, samma momentkod och samma fråga skapas(men med olika svar)
            string jsonString2 =
                "[{\"coursecode\":\"ENG\",\"momentcode\":\"260\",\"questionid\":\"262\",\"question\":\"katt\",\"answer\":\"dog\"}]";

            parser = new Parser(jsonString, courses, 2);     // Första objektet skickas in för bearbetning    
            parser = new Parser(jsonString2, courses, 2);    // Andra objektet skickas in på samma sätt

            Assert.AreEqual(courses[0].questions[0].Count, 1);               // Förväntat resultat är att kurslistans frågelista har en fråga 
                                                                             // för det här momentet

            Assert.AreEqual(courses[0].answers["katt"], "cat");              // Förväntat resultat är att motsvaraden frågas svar är "cat"
        }

       /* [Test]
        public void TestAccessToken()
        {
            // Ett jsonobjekt inehållande ett access token skapas på “konstgjord” väg:
            string jsonString =
                "{\"access_token\":\"caefab42-8113-45d7-9cd9-bc3d0e902754\"}";
            

            parser = new Parser(jsonString, courses, 2);     // Objektet skickas in för bearbetning    
            

            Assert.AreEqual(parser.access_token, "caefab42-8113-45d7-9cd9-bc3d0e902754");               // Förväntat resultat är att samma access token som skickades in sparas
                                                                             

           
        }*/






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
