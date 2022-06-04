using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    [Serializable]
    public enum ChatType : short
    {
        ALL,
        ROOM,
        END,
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class ChatPacket : Data<ChatPacket>
    {
        [MarshalAs(UnmanagedType.I2)]
        public ChatType chatType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string? id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public string? chat;
    }
}
