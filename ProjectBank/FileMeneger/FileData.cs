using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.FileMeneger
{
    internal class FileData
    {
        public static void CreateBaseFolder()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path + "/AccountData");
                Directory.CreateDirectory(path + "/BankData");
            }
        }
    }
}
