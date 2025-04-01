using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSelecterDemo
{
    internal class Stater
    {
        [STAThread]
        static void Main()
        {
            App app = new();
            app.Run();
        }
    }
}
