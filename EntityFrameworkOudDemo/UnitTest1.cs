using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EntityFrameworkOudDemo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new SchoolEntities())
            {
                Assert.IsTrue(context.People.Any(p => p.FirstName == "Kim"));
            }
        }

        [TestMethod]
        public void QuerySyntax()
        {
            using (var context = new SchoolEntities())
            {
                var query = from p in context.People
                            where p.FirstName == "Kim"
                            select p;

                Assert.IsTrue(query.Any());
            }
        }

        [TestMethod]
        public void TeacherOnderscheidInContext()
        {
            using (var context = new SchoolEntities())
            {
                Assert.IsInstanceOfType(context.People.First(p => p.FirstName == "Kim"), typeof(Instructor));
            }
        }

        [TestMethod]
        public void HoeMaakJeOnderscheidTussenInstructorsEnStudents()
        {
            using (var context = new SchoolEntities())
            {
                var instructors = context.People.OfType<Instructor>().ToList();
                
            }
        }

        [TestMethod]
        public void HoeMaakIkEenProjection()
        {
            using (var context = new SchoolEntities())
            {
                var query = from p in context.People.OfType<Instructor>()
                            //where p.FirstName == "Kim"
                            group p by p.HireDate.Year / 10;


                var group = query.Single(g => g.Key == 200);
                Assert.IsTrue(group.Any(p => p.FirstName == "Fadi"));
                Assert.IsTrue(group.Any(p => p.FirstName == "Roger"));
            }
        }

        [TestMethod]
        public void NavigationProperties()
        {
            using (var context = new SchoolEntities())
            {
                var kim = context
                    .People
                    .OfType<Instructor>()
                    .Include(p => p.Courses)
                    .Single(p => p.PersonID == 1);
                Assert.IsTrue(kim.Courses.Any());
            }
        }
        [TestMethod]
        public void LazyLoadingNadeel()
        {
            using (var context = new SchoolEntities())
            {
                foreach (var instructor in context
                    .People
                    .OfType<Instructor>()
                    .Include(i => i.Courses))
                {
                    Assert.IsTrue(instructor.Courses.Any()); 
                }
            }
        }

        [TestMethod]
        public void OptimisticConcurrency()
        {
            using (var context = new SchoolEntities())
            {
                context.People.Single(p => p.PersonID == 1).LastName = "Abercrombie";
                context.SaveChanges();
            }

            using (var context1 = new SchoolEntities())
            using (var context2 = new SchoolEntities())
            {
                var kima = context1.People.Single(p => p.PersonID == 1);
                var kimb = context2.People.Single(p => p.PersonID == 1);

                kima.LastName += "a";
                kimb.LastName += "b";

                context1.SaveChanges();
                Assert.ThrowsException<DbUpdateConcurrencyException>(() => context2.SaveChanges());
            }

            using (var context = new SchoolEntities())
            {
                Assert.AreEqual("Abercrombiea", context.People.Single(p => p.PersonID == 1).LastName);
            }
        }
    }
}
