using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharer;

namespace ElectronicArt1
{
    public class ArduinoSettings
    {
        public static SharerConnection Connection
        { get; set; }
        = null;

        public static int Baud = 115200;
        public static string COMPort = null;

        public static string ImageFolder = "\\images";
    }
}
