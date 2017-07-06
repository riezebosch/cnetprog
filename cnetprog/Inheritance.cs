using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace cnetprog
{
    public class InheritanceDemo
    {
        interface ISpelen
        {
            string Spelen();
        }

        interface IGaming
        {
            void Spelen();
        }

        [Fact]
        public void WatIsHetProbleemVanEenDiamondShape()
        {
            var opa = new Opa();
            var kind = new Zoon();
            var kind2 = new Dochter();
            var kleinkind = new KleinKind();

            kleinkind.Spelen();
            ((IGaming)kleinkind).Spelen();
        }

        private class Opa : ISpelen
        {
            public virtual int MyProperty { get; set; }

            public virtual string Spelen()
            {
                return "schaken";
            }
        }

        private class Zoon : Opa
        {
            public override int MyProperty { get => base.MyProperty; set => base.MyProperty = value; }
            public override string Spelen()
            {
                return "voetbal";
            }
        }

        private class Dochter : ISpelen
        {
            public string Spelen()
            {
                return "paardrijden";
            }
        }

        private class KleinKind : Zoon, IGaming
        {
            public override string Spelen()
            {
                return base.Spelen();
            }

            void IGaming.Spelen()
            {

            }
        }

        [Fact]
        public void GenericsDemo()
        {
            var wrapper = new Wrapper<int>();
            wrapper.Value = 3;

            Assert.Equal(3, (int)wrapper.Value);

            var wrapper2 = new Wrapper<string>();
            wrapper2.Value = "input";

            Assert.Equal("input", wrapper2.Value);
        }

        private class Wrapper<T>
        {
            internal static int AantalInstanties;

            public T Value { get; set; }

            public Wrapper()
            {
                AantalInstanties++;
            }
        }

        [Fact]
        public void MetGenericsKrijgIkRuntimeDaadwerkelijkVerschillendeTypes()
        {
            var wrapper1 = new Wrapper<int>();
            var wrapper2 = new Wrapper<int>();
            var wrapper3 = new Wrapper<string>();

            Assert.Equal(Wrapper<int>.AantalInstanties, 2);
            Assert.Equal(Wrapper<string>.AantalInstanties, 1);
        }

        [Fact]
        public void GenericTypeConstraints()
        {
            DoeIetsMetEenGeneric(3, 2);
            //DoeIetsMetEenGeneric(new Opa(), new Zoon());
        }

        event EventHandler<StudentEventArgs> Graduated;

        private bool DoeIetsMetEenGeneric<T>(T v1, T v2)
            where T : IComparable

        {
            T v3 = default(T);
            return v1.CompareTo(v2) > 0;
        }

        private class StudentEventArgs : EventArgs
        {
        }

        [Fact]
        public void EnumeratorDemo()
        {
            var getallen = Fibonacci().Take(10);
            Assert.Equal(new[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 }, getallen);
        }

        [Fact]
        public void EnumeratorDemoYield()
        {
            var getallen = FibonacciYield().Take(10);
            Assert.Equal(new[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 }, getallen);
        }

        private IEnumerable<int> FibonacciYield()
        {
            yield return 0;
            yield return 1;

            int a = 0, b = 1;
            while (true)
            {
                b = a + b;
                a = b - a;

                yield return b;
            }
        }

        private IEnumerable<int> Fibonacci()
        {
            return new FibonacciEnumerable();
        }

        private class FibonacciEnumerable : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                return new FibonacciEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private class FibonacciEnumerator : IEnumerator<int>
            {
                private int a;
                private int b;
                private int state;

                public int Current { get; private set; }

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                }

                public FibonacciEnumerator()
                {
                    Reset();
                }

                public bool MoveNext()
                {
                    switch (state)
                    {
                        case 0:
                            Current = a;
                            state++;
                            break;
                        case 1:
                            Current = b;
                            state++;
                            break;
                        default:
                            Current = a + b;

                            a = b;
                            b = Current;
                            break;
                    }

                    return true;
                }

                public void Reset()
                {
                    state = 0;
                    a = 0;
                    b = 1;
                }
            }
        }

        abstract class VirtualEnAbstractDemoBase
        {
            public VirtualEnAbstractDemoBase(int getal)
            {
            }

            public abstract void DoeIets();

            protected internal virtual int HalfFabrikaat()
            {
                return 5;
            }
        }

        class VirtualEnAbstractDemoDerived : VirtualEnAbstractDemoBase
        {
            public VirtualEnAbstractDemoDerived() : this(5)
            {

            }

            public VirtualEnAbstractDemoDerived(int getal) : base(getal)
            {

            }

            public override void DoeIets()
            {
                this.HalfFabrikaat();
                HalfFabrikaat();

                base.HalfFabrikaat();
            }

            protected internal sealed override int HalfFabrikaat()
            {
                return base.HalfFabrikaat() * 3;
            }
        }

        [Fact]
        public void DemoVanInheritance()
        {
            new VirtualEnAbstractDemoDerived();
        }


        [Fact]
        public void ExtensionMethodDemo()
        {
            string input = "pietje puk";
            string result1 = StringHelpers.RemoveVowels(input);
            string result2 = input.RemoveVowels();

            Assert.Equal("ptj pk", result1);
            Assert.Equal("ptj pk", result2);
        }

        [Fact]
        public void WatDoetDeleteAlsEenFileNietBestaat()
        {
            File.Delete("nietbestaandefile.nogiets");
            Path.Combine("first", "second", "CasIngMoetOokNogMee");
        }

        [Fact]
        public void ImmutableStructDemo()
        {
            var s = new ImmutableStruct(data: 5);
            var field = s.GetType().GetTypeInfo().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Single();
            Assert.True(field.IsInitOnly);
        }

        private struct ImmutableStruct
        {
            public int Data { get; }

            public ImmutableStruct(int data)
            {
                this.Data = data;
            }
        }

        [Fact]
        public void NameHidingDemo()
        {
            var b = new Base();
            Assert.Equal("base", b.Waarde);
            Assert.Equal("base", b.Override);

            var d = new DerivedMetMethodHiding();
            Assert.Equal("derived", d.Waarde);
            Assert.Equal("derived", d.Override);

            Base cast = d;
            Assert.Equal("base", cast.Waarde);
            Assert.Equal("derived", cast.Override);
        }

        private class Base
        {
            public string Waarde => "base";

            public virtual string Override => "base";
        }

        private class DerivedMetMethodHiding : Base
        {
            public new string Waarde => "derived";

            public override string Override => "derived";
        }
    }

    static class StringHelpers
    {
        public static string RemoveVowels(this string input)
        {
            return input
                .Replace("a", "")
                .Replace("e", "")
                .Replace("i", "")
                .Replace("o", "")
                .Replace("u", "");
        }
    }
}
