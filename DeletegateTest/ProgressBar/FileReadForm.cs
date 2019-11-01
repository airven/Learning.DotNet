using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeletegateTest.ProgressBar
{
    /*
     * 1：通过开启线程，与主线程进行隔离，然后将委托的实例通过控件的invoke方法回到UI线程
     * 2：多线程传参的情况，可以使用以下情况进行定义，当然也可以自定义委托，因为ThreadStart本身就是委托类型
     * ParameterizedThreadStart pts=new ParameterizedThreadStart(方法名);
     * Thread t=new Thread(pts);
     * 3：如果不采用control.invoke方法进行UI线程的更新，也可以采用同步上下文的方式进行，可以参照用法：SynchronizationContext SyncContext=SynchronizationContext.Current
     * 然后在激发线程中通过post/send方法将消息发送到主线程上来，SyncContext.Post(delegate(){ },messagestring);
     * 4:异常处理：https://src-bin.com/es/q/610f2c
     */
    public partial class FileReadForm : Form
    {
        private bool invokeInProgress = false;
        private bool stopInvoking = false;

        public delegate void updatebar();
        public delegate void updatetext(string content);
        public FileReadForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            if (dr == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                ThreadStart theprogress = new ThreadStart(() => ReadFile(filename));
                Thread startprogress = new Thread(theprogress);
                startprogress.Name = "Update ProgressBar";
                startprogress.Start();
            }
            else
            {
                MessageBox.Show("no file choosed!");
            }
        }
        private void UpdateProgress()
        {
            progressBar1.Value += 1;
            label1.Text = Convert.ToString(Convert.ToInt64(label1.Text) + 1);
        }
        private void UpdateTextBox(string str)
        {
            textBox1.AppendText(str);
        }
        private void ReadFile(string filename)
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
                // Add to the current position in the file

                if (textBox1.InvokeRequired)
                {
                    if (stopInvoking != true) // don't start new invokes if the flag is set
                    {
                        invokeInProgress = true;  // let the form know if an invoke has started
                        string str = new string(buff);
                        textBox1.Invoke(new updatetext(this.UpdateTextBox), str);
                        invokeInProgress = false;  // the invoke is complete
                    }

                }

                // Once we hit a milestone, subtract the milestone value and
                // call our delegate we defined above.
                // We must do this through invoke since progressbar was defined in the other
                // thread.
                if (progressBar1.InvokeRequired)
                {
                    if (stopInvoking != true) // don't start new invokes if the flag is set
                    {
                        invokeInProgress = true;  // let the form know if an invoke has started
                        if (currentSize >= incrementSize)
                        {
                            currentSize -= incrementSize;
                            progressBar1.Invoke(new updatebar(this.UpdateProgress));
                        }
                        invokeInProgress = false;  // the invoke is complete
                    }

                }
            }
            stream.Close();
            MessageBox.Show("Done");
        }

        private async void FileReadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (invokeInProgress)
            {
                e.Cancel = true;  // cancel the original event 

                stopInvoking = true; // advise to stop taking new work

                //// now wait until current invoke finishes
                await Task.Factory.StartNew(() =>
                {
                    while (invokeInProgress) ;
                });

                // now close the form
                this.Close();
            }
        }
    }
}
