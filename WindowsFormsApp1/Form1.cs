using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Task.Run(() => Fib(42)).ContinueWith(t => label1.Text = t.Result.ToString(), TaskScheduler.FromCurrentSynchronizationContext());

            var result1 = Task.Run(() => Fib(42));
            var result2 = Task.Run(() => Fib(41));
            var result3 = Task.Run<int>(async () => {
                await Task.Delay(500);
                throw new OutOfMemoryException();
            });

            try
            {
                var first = await await Task.WhenAny(result1, result2, result3);
                label1.Text = first.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static int Fib(int n)
        {
            if (n <= 1)
                return n;

            return Fib(n - 1) + Fib(n - 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
