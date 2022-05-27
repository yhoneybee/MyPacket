using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    public static class Defines
    {
        public static readonly int KILO = 1024;

        public static readonly int MESSAGE_BUFFER_SIZE = KILO * 20;
        public static readonly int HEADER_BUFFER_SIZE = 4;
        public static readonly int TYPE_BUFFER_SIZE = 2;
    }
}
