using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using static Xunit.Assert;

namespace cnetprog
{
    public class UnitTest1
    {
        private static bool _uitgevoerd;
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test1()
        {
            double a = 0.1d;
            double b = 0.2d;
            double c = a + b;

            Assert.NotEqual(0.3d, c);
        }

        [Fact]
        public void ZelfdeVoorbeeldMaarDanMetDecimals()
        {
            decimal a = 0.1m;
            decimal b = 0.2m;
            decimal c = a + b;

            Assert.Equal(0.3m, c);
        }

        [Fact]
        public void ToVarOrNotToFar()
        {
            var i = 10;
            var b = InitializeThisVariable();

            int[] items1 = { 1, 2, 3, 4 };
            var items2 = new int[] { 1, 2, 3, 4 };

            Func<int> f = () => 3;
            var f2 = new Func<int>(() => 3);

            Expression<Func<int>> f3 = () => 3;

        }

        private bool InitializeThisVariable()
        {
            return true;
        }

        [Fact]

        public void StringNaarInteger()
        {
            string input = "1324";

            int i1 = int.Parse(input);
            int i2 = Convert.ToInt32(input);
        }

        [Fact]

        public void StringNaarIntegerDieNietAltijdLukt()
        {
            string input = "1324b";

            int i1;
            try
            {
                i1 = int.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine("input niet correct formaat");
            }
        }

        [Fact]

        public void StringNaarIntegerDieNietAltijdLuktZonderTryCatch()
        {
            string input = "1324b";


            if (!int.TryParse(input, out int i1))
            {
                Console.WriteLine("input niet correct formaat");
            }
            else
            {
            }
        }

        [Fact]
        public void WatIsEenNullable()
        {
            int? i = Initializeer();
            Nullable<int> i2 = Initializeer();

            Assert.Equal(null, i);

            if (i != null)
            {
            }

            if (i.HasValue)
            {
            }

            int i3 = i ?? i2 ?? -1;
        }

        private int? Initializeer()
        {
            return null;
        }

        [Fact]
        public void StringFormatDemo()
        {
            int age = 45;

            string eenbeetjefout = "De leeftijd is: " + age;
            var sb = new StringBuilder("De leeftijd is: ").Append(age);


            string result = string.Format("De leeftijd is {0}", age);
            string inerpolated = $"De leeftijd is {age}";
        }

        [Fact]
        public void WatZitErOnderDeEnum()
        {
            var modus = Beweging.StaatStil;
            Assert.Equal(Beweging.StaatStil, modus);

            modus = (Beweging)1234;

            switch (modus)
            {
                default:
                    break;
                case Beweging.StaatStil:
                    break;
            }
        }

        [Fact]
        public void WatIsDanEenFlagsEnum()
        {
            var modus = Beweging.StaatStil | Beweging.Rennen;
            Assert.True(Beweging.StaatStil == (modus & Beweging.StaatStil));

            Assert.Equal("StaatStil, Rennen", modus.ToString());
        }

        [Fact]
        public void KunnenWeInCSharp6AlEenSwitchOpTypeDoen()
        {
            Shape shape = new Rectangle();

            switch (shape)
            {
                case Rectangle r:
                    break;
                default:
                    break;
            }
        }

        private class Rectangle : Shape
        {
            public Rectangle()
            {
            }
        }

        private class Shape
        {
        }

        [Fact]
        public void MultidimensionalVsJaggedArray()
        {
            var flat = new int[5];
            flat[0] = 1;

            var multi = new int[5, 3];
            multi[0, 0] = 1;

            var jagged = new int[4][];
            jagged[0] = new int[2];
            jagged[0][0] = 1;
            jagged[1] = flat;

            Assert.Equal(1, flat.Rank);
            Assert.Equal(5, flat.Length);

            Assert.Equal(2, multi.Rank);
            Assert.Equal(15, multi.Length);
            Assert.Equal(5, multi.GetLength(0));
            Assert.Equal(3, multi.GetLength(1));

            Assert.Equal(1, jagged.Rank);
        }

        public void OmgaanMetNamespaces()
        {
            var writer1 = new System.IO.StringWriter();
            var writer2 = new StringWriter();

            Equal(1, 1);

            int[] items = { 1, 2, 3, 4 };
            var query = items.Where(i => i % 2 == 0);
            var query2 = Enumerable.Where(items, i => i % 2 == 0);
        }


        [Fact]
        public void IntelliTraceDemo()
        {
            int i = Initializeer(4);
        }


        private int Initializeer(int v)
        {
            var day = DateTime.Today;
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Thursday:
                    return 5;
                case DayOfWeek.Saturday:
                case DayOfWeek.Friday:
                    return 4;
                default:
                    return -1;
            }
        }

        [Fact]
        public void StructVsClass()
        {
            var s = new MyStruct();
            var c = new MyClass();

            MyStruct s2;
            s2.getal = 3;

            Assert.Equal(3, s2.getal);

            MyClass c2 = new MyClass();
            c2.Getal = 3;
        }

        private struct MyStruct
        {
            public int getal;
        }

        private class MyClass
        {
            private int getal;
            public int Getal { set { getal = value; } get { return getal; } }

            public int GetalMaarDanAutoImplemented { get; set; }

            public override string ToString()
            {
                return GetalMaarDanAutoImplemented.ToString();
            }
        }

        [Fact]
        public void SlimmeCompilerEnStrings()
        {
            string a = "asdf";
            string b = "asdf";

            Assert.True(Object.ReferenceEquals(a, b));

            string c = "as" + "df";
            Assert.True(Object.ReferenceEquals(a, c));

            string d = "as";
            d += "df";
            Assert.False(Object.ReferenceEquals(a, d));
        }

        [Fact]
        public void OptionalParameters()
        {
            var result = DoeIets(true);
            Assert.True(result.b);
            Assert.Equal(0, result.i);

            var result2 = DoeIets(i: 3);
            Assert.False(result2.b);
            Assert.Equal(3, result2.i);
        }

        private (bool b, int i) DoeIets(bool b = false, int i = 0)
        {
            return (b, i);
        }

        [Fact]
        public void ThrowExceptionsDemo()
        {
            try
            {
                throw new Exception("hoi");
            }
            catch (Exception ex) when (ex.Message != "hoi")
            {
                Assert.NotEqual("hoi", ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Equal("hoi", ex.Message);
            }
        }

        [Fact]
        public void WordtDeFinallyAtijdUitgevoerdZelfsBijEenVoortijdigeReturn()
        {
            DoeEenTryFinally();
            Assert.True(_uitgevoerd);
        }

        private static void DoeEenTryFinally()
        {
            try
            {
                return;
            }
            finally
            {
                _uitgevoerd = true;
            }
        }

        [Fact]
        public void WatHeeftUsingMetEenTryFinallyTeMaken()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                DieIetsDoetMetExternalResources resource = new DieIetsDoetMetExternalResources();
                try
                {
                    resource.Write();
                }
                finally
                {
                    resource?.CleanUp();
                }
            });
        }

        [Fact]
        public void WatHeeftUsingMetEenTryFinallyTeMakenMaarDanMetUsing()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                using (var resource = new DieIetsDoetMetExternalResources())
                {
                    resource.Write();
                }
            });
        }

        class DieIetsDoetMetExternalResources : IDisposable
        {
            private IntPtr externalResource;

            public void Write()
            {
                throw new NotImplementedException();
            }

            public void CleanUp()
            {
                externalResource = IntPtr.Zero;
            }

            public void Dispose()
            {
                CleanUp();
            }
        }

        [Fact]
        public void WaaromEenThrowInEenFinallyEenSlechtIdeeIs()
        {
            // Want er is nog een andere exception
            // die nu opgegeten wordt door de OutOfMemoryException
            // De oorspronkelijke exception is helaas verloren gegaan.
            Assert.Throws<OutOfMemoryException>(() => ThrowSomething("asdf"));
        }

        private int ThrowSomething(string input)
        {
            try
            {
                int.Parse(input);
            }
            finally
            {
                throw new OutOfMemoryException();
            }
        }

        [Fact]
        public void RethrowVanException()
        {
            try
            {
                RethrowException(true);
            }
            catch (Exception ex)
            {
                _output.WriteLine(ex.StackTrace);
            }

            _output.WriteLine("");

            try
            {
                RethrowException(false);
            }
            catch (Exception ex)
            {
                _output.WriteLine(ex.StackTrace);
            }
        }

        private void RethrowException(bool zoalshethoort)
        {
            try
            {
                int.Parse("asdf");
            }
            catch (Exception ex)
            {
                if (zoalshethoort)
                {
                    throw;
                }

                throw ex;
            }
        }

        [Fact]
        public void DebugWrite()
        {
            Debug.WriteLine("hoi");
        }

        [Fact]
        public void EvenCheckenOfDatMetDeStructsNogSteedsZoIs()
        {
            var s = new MyStruct2(3);
        }

        struct MyStruct2
        {
            public MyStruct2(int v) : this()
            {
                MyProperty = 3;
            }

            public int MyProperty { get; set; }
        }

        [Fact]
        public void IndexerDemo()
        {
            int[] items = { 1, 2, 3, 4 };
            var getal = items[0];

            var demo = new ClassMetIndexer();
            var i = demo[3];
            var s = demo["input"];

            demo["input"] = 6;

            demo
                .GetType()
                .GetTypeInfo()
                .GetMethods()
                .ToList()
                .ForEach(m => _output.WriteLine(m.ToString()));
        }

        [Fact]
        public void LinqDemo()
        {
            var people = new List<Person>
            {
                new Person
                {
                    Name = "Pietje",
                    Age = 31
                },
                new Person
                {
                    Name = "Langdraad",
                    Age = 54
                }
            };

            var query = people
                .Where(p => p.Age > 35)
                .OrderBy(p => p.Name)
                .ThenByDescending(p => p.Age);

            var query2 = from p in people
                         where p.Age > 35
                         orderby p.Name, p.Age descending
                         select p;

            var dict = people.ToDictionary(p => p.Name);
            Assert.Equal(31, dict["Pietje"].Age);

            var lookup = people.ToLookup(p => p.Name);
            Assert.Equal(1, lookup["Pietje"].Count());
        }


        private class ClassMetIndexer
        {
            private int _backingfield = 3;

            public int this[int index]
            {
                get { return _backingfield; }
            }

            public int this[string index]
            {
                get { return _backingfield; }
                set { _backingfield = value; }
            }

            private int myVar;

            public int MyProperty
            {
                get { return myVar; }
                set { myVar = value; }
            }

        }

        private class Person
        {
            public string Name { get; internal set; }
            public int Age { get; internal set; }
        }


        [Fact]
        public void NullableStructInLinqDemo()
        {
            int[] items = { 1, 2, 3, 4 };

            var item = items
                .Cast<int?>()
                .FirstOrDefault(i => i > 13);
            Assert.Null(item);

            var item2 = items
                .Where(i => i > 13)
                .DefaultIfEmpty(-1)
                .First();
            Assert.Equal(-1, item2);
        }

        [Fact]
        public void SubtleStruct()
        {
            MyStruct s1;
            s1.getal = 3;

            MyStruct s2;
            s2.getal = 3;

            Assert.Equal(s1, s2);
        }
    }

    namespace Nested
    {
        namespace NogDieper.ZoOpschrijven
        {
            class MyClass
            {
            }
        }
    }

    [Flags]
    enum Beweging
    {
        StaatStil = 0b0001,
        Rennen = 0b0010,
        Lopen = 0b0100,
        AllesBijElkaar = 0b11111111111
    }


}
