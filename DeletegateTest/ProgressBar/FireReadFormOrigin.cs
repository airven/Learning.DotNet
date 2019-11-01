using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeletegateTest.ProgressBar
{
    public partial class FireReadFormOrigin : Form
    {
        public FireReadFormOrigin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            if (dr == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                FileInfo fileSize = new FileInfo(filename);
                long size = fileSize.Length;

                // Next we need to know where we are at and what defines a milestone in the
                // progress. So take the size of the file and divide it into 100 milestones
                // (which will match our 100 marks on the progress bar.

                long currentSize = 0;
                long incrementSize = (size / 100);

                // Open the big text file with open filemode access.
                StreamReader stream = new StreamReader(new FileStream(filename, FileMode.Open));

                // This buffer is only 10 characters long so we process the file in 10 char chunks.
                // We could have boosted this up, but we want a slow process to show the slow progress.
                char[] buff = new char[200];
                // Read through the file until end of file
                while (!stream.EndOfStream)
                {
                    currentSize += stream.Read(buff, 0, buff.Length);
                    string str = new string(buff);
                    textBox1.AppendText(str);
                }
                stream.Close();
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("no file choosed!");
            }
        }
    }
}
