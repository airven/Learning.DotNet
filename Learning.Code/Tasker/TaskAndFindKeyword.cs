using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.Tasker
{
    public class TaskAndFindKeyword : ITask
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        string[] filenames = { "D:\\MyConfiguration\\user\\Desktop\\local.txt", "chapter2.txt", "chapter3.txt", "chapter4.txt", "chapter5.txt" };
        string pattern = @"\b\w+\b";
        int totalWords = 0;
        public void Run()
        {
            var a = filenames.Select(async t =>
            {
                Console.WriteLine("fjdkfjd");
                if (!File.Exists(t))
                    throw new FileNotFoundException("{0} does not exist.", t);
                StreamReader sr = new StreamReader(t);
                String content = await sr.ReadToEndAsync();
                sr.Close();
                int words = Regex.Matches(content, pattern).Count;
                Interlocked.Add(ref totalWords, words);
                Console.WriteLine("{0,-25} {1,6:N0} words", t, words);
            });
        }


        public void TestRun()
        {
            var task1 = filenames.Select(async t =>
            {
                Console.WriteLine("fjdkfjd");
                if (!File.Exists(t))
                    throw new FileNotFoundException("{0} does not exist.", t);
                StreamReader sr = new StreamReader(t);
                String content = await sr.ReadToEndAsync();
                sr.Close();
                int words = Regex.Matches(content, pattern).Count;
                Interlocked.Add(ref totalWords, words);
                Console.WriteLine("{0,-25} {1,6:N0} words", t, words);
            });

            var finalTask = Task.Factory.ContinueWhenAll(task1.ToArray(), wordCountTasks =>
            {
                int nSuccessfulTasks = 0;
                int nFailed = 0;
                int nFileNotFound = 0;
                foreach (var t in wordCountTasks)
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                        nSuccessfulTasks++;

                    if (t.Status == TaskStatus.Faulted)
                    {
                        nFailed++;
                        t.Exception.Handle((e) =>
                        {
                            if (e is FileNotFoundException)
                            {
                                nFileNotFound++;
                                Console.WriteLine(e.ToString());
                            }
                            return true;
                        });
                    }
                }
                Console.WriteLine("\n{0,-25} {1,6} total words\n",
                                  String.Format("{0} files", nSuccessfulTasks),
                                  totalWords);
                if (nFailed > 0)
                {
                    Console.WriteLine("{0} tasks failed for the following reasons:", nFailed);
                    Console.WriteLine("   File not found:    {0}", nFileNotFound);
                    if (nFailed != nFileNotFound)
                        Console.WriteLine("   Other:          {0}", nFailed - nFileNotFound);
                }
            });
            finalTask.Wait();
        }
    }
}
