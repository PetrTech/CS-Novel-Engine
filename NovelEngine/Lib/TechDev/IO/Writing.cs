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

    class Writing
    {
        public void WriteToFile(string filePath, string fileName, string text)
        {
            switch (TechDev.IO.DirectoryAndFileChecker.directoryExists(filePath)) // Check if folder exists, if not, create a new one.
            {
                case true:
                    // move on
                    File.WriteAllTextAsync(filePath + "/" + fileName, text); // Let's hope that I don't need to close the writer 💀 (spoiler: no I don't, don't worry)
                    break;

                case false:
                    // REAL SHIT (aka make a folder)
                    Directory.CreateDirectory(filePath);
                    WriteToFile(filePath, fileName, text); // Let's repeat the function and also let's hope nothing breaks because we know I am an awesome programmer /j
                    break;
            }
        }

        public void AppendToFile(string filePath, string fileName, string text)
        {
            switch (TechDev.IO.DirectoryAndFileChecker.directoryExists(filePath)) // Check if folder exists, if not, create a new one.
            {
                case true:
                    // move on
                    File.AppendAllText(filePath + "/" + fileName, text);
                    break;

                case false:
                    // REAL SHIT (aka make a folder)
                    Directory.CreateDirectory(filePath);
                    AppendToFile(filePath, fileName, text);
                    break;
            }
        }

        public void DestroyFile(string filePath, string fileName)
        {
            File.Delete(filePath + "/" + fileName);
        }
    }
}
