﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    }
}