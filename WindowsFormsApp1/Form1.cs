using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cts;
        private CancellationToken token;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = !(button2.Enabled = true);

            cts?.Cancel();
            token = (cts = new CancellationTokenSource()).Token;

            var progress = new Progress<int>(n => label1.Text = n.ToString());

            var result1 = Task.Run(() => Fib(42, progress));
            var result2 = Task.Run(() => Fib(41, progress), cts.Token);

            try
            {
                var first = await await Task.WhenAny(result1, result2);
                label1.Text = first.ToString();
            }
            catch (OperationCanceledException)
            {
                label1.Text = "Operation was cancelled";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                button1.Enabled = !(button2.Enabled = false);
            }
        }

        public int Fib(int n, IProgress<int> progress)
        {
            token.ThrowIfCancellationRequested();

            progress?.Report(n);

            if (n <= 1)
                return n;

            return Fib(n - 1, null) + Fib(n - 2, progress);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }
    }
}
