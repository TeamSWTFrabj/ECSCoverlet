using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    class Window : IWindow
    {
        public bool RunSelfTest()
        {
            return true;
        }

        public void Close()
        {
            System.Console.WriteLine("Window is closed");
        }

        public void Open()
        {
            System.Console.WriteLine("Window is open");
        }

    }
}
