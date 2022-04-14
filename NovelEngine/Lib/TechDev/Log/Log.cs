using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDev.IO;

namespace TechDev.Log
{
    // You may not copy this library. This is a custom written library only to be used in this source code.

    // Put into a standalone library because I don't want the renderer class messy
    // If you see this adaxiik, I am so fucking terribly sorry for this

    class Log
    {
        public static void LogError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR {0}] - {1}. See traceback.txt for more info", DateTime.Now.ToString("HH:mm:ss:fff"), msg);
            Writing wr = new Writing();
            wr.AppendToFile(IO.Loading.GetCurrentDir(),"traceback.txt",string.Format("[ERR {0}] - {1}." + Environment.NewLine, DateTime.Now.ToString("HH:mm:ss:fff"), msg));
        }

        public static void LogMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("[MESSAGE {0}] - {1}. See traceback.txt for more info", DateTime.Now.ToString("HH:mm:ss:fff"), msg);
            Writing wr = new Writing();
            wr.AppendToFile(IO.Loading.GetCurrentDir(), "traceback.txt", string.Format("[LOG {0}] - {1}." + Environment.NewLine, DateTime.Now.ToString("HH:mm:ss:fff"), msg));
        }

        public static void LogWarning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING {0}] - {1}. See traceback.txt for more info", DateTime.Now.ToString("HH:mm:ss:fff"), msg);
            Writing wr = new Writing();
            wr.AppendToFile(IO.Loading.GetCurrentDir(), "traceback.txt", string.Format("[WARN {0}] - {1}." + Environment.NewLine, DateTime.Now.ToString("HH:mm:ss:fff"), msg));
        }
    }
}
