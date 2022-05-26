using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    public enum ChatType
    {
        ALL,
        ROOM,
        END,
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ChatPacket : Data<ChatPacket>
    {
        public ChatType ChatType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string? id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public string? chat;
    }
}
