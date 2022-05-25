using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    public static class Defines
    {
        public static readonly int SOCKET_BUFFER_SIZE = 1024 * 20;
        public static readonly int SOCKET_DIVISION_SIZE = 1024 * 4;
        public static readonly int HEADER_SIZE = 32;
    }
}
