using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace cnetprog
{
    public class TPL
    {
        [Fact]
        public void WatIsEenTask()
        {
            int f0 = Fib(0);
            Assert.Equal(0, f0);

            int f1 = Fib(1);
            Assert.Equal(1, f1);

            int f2 = Fib(2);
            Assert.Equal(1, f2);

            int f3 = Fib(3);
            Assert.Equal(2, f3);

            int f33 = Fib(42);
            
        }

        public static Task<int> FibAsync(int n)
        {
            return Task.Run(() => Fib(n));
        }

        public static int Fib(int n)
        {
            if (n <= 1)
                return n;

            return Fib(n - 1) + Fib(n - 2);
        }

        [Fact]
        public void ParallelLinq()
        {

            Parallel.ForEach(new int[] { }, i => { });
            var items = Enumerable.Range(0, 1234444);
            var query = from i in items.AsParallel()
                        where Math.Pow(i, 1.3) % 3 == 0
                        select i;


            Assert.NotEqual(0, query.Count());

        }

        [Fact]
        public async void IetsMetAsyncVoid()
        {
            var result = await Task.Run(() => 3);
            Assert.Equal(3, result);
        }
    }
}
