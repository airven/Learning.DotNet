using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using DeletegateTest.Redis;

namespace DeletegateTest
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //委托的简单使用
            //DeletegateStep1 step1 = new DeletegateStep1();
            //step1.Show();

            //多播委托
            //DeletegateStep2 step2 = new DeletegateStep2();
            //step2.Show();

            //事件
            //DeletegateStep3 step3 = new DeletegateStep3();
            //step3.Show2();

            //使用事件在窗体间传值
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DeletegateTest.EventInWinForm.From1());

            //在线程中过程通过委托来更新UI变化
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DeletegateTest.ProgressBar.FileReadForm());

            DeletegateStep4 step4 = new DeletegateStep4();
            step4.Print();
            Console.Read();
        }
    }
}
