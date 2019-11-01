using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Code.ExceptionTEst
{
    public class CSharpException : ITask
    {
        public string Name
        {
            get { return GetType().Name; }
        }

        public void Run()
        {
            try
            {
                DoSomeUsefulWork();
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }


        private void DoSomeUsefulWork()
        {
            try
            {
                ICanThrowException();
                ICanThrowException();
            }
            catch (Exception ex)
            {
                Log(ex);
                throw ex;
            }
        }

        private void ICanThrowException()
        {
            throw new Exception("Bad thing happened");
        }

        private void Log(Exception ex)
        {
            // Intentionally left blank
        }
    }
}
