using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletegateTest.EventInWinForm
{
    public class Person: EventArgs
    {
        public Person()
        {

        }
        public Person(string Name="",string Address="",string CompanyName="")
        {
            this.Name = Name;
            this.Address = Address;
            this.CompanyName = CompanyName;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
    }
}
