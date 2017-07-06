using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace ouderwets
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RethrowVanException()
        {
            try
            {
                RethrowException(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();
            
            try
            {
                RethrowException(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
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

        [TestMethod]
        public void ImmutableStructDemo()
        {
            var s = new ImmutableStruct(data: 5);
            var field = s.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Single();
            Assert.IsTrue(field.IsInitOnly);
        }

        private struct ImmutableStruct
        {
            public int Data { get; }

            public ImmutableStruct(int data)
            {
                this.Data = data;
            }
        }

        [TestMethod]
        public async Task IetsMetAsync()
        {
            var result = await Task.Run(() => 3);
            Assert.AreEqual(3, result);
        }
    }
}
