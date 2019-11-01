using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generics.example
{
    public class Student:BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string Email { get; set; }
    }
}
