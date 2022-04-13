using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDev.IO
{
    // You may not copy this library. This is a custom written library only to be used in this source code.
    // If you see this, I am so terribly sorry for this

    class Loading
    {
        public static string LoadFile(string filePath, string fileName)
        {
            return File.ReadAllText(filePath + "/" + fileName);
        }

        public static string GetCurrentDir()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}
