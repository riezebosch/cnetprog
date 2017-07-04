using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;
using static Xunit.Assert;

namespace cnetprog
{
    public class UnitTest1
    {
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
