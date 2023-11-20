using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPractice
{
    public interface IHuman
    {
        string Surname { get; }
        string Name { get; }
        int Age { get; }
    }
}
