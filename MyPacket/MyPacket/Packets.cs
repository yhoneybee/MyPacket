using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    [Serializable]
    public enum PacketType
    {
        LOGIN_PACKET,
        GAMESTART_PACKET,
        CHAT_PACKET,
        CHARACTOR_PACKET,
        LOGOUT_PACKET,
        END,
    }

    [Serializable]
    public enum ChatType : short
    {
        ALL,
        PERSON,
        END,
    }

    [Serializable]
    public enum CharactorState : short
    {
        IDLE,
        CROUCH,
        WALK,
        RUN,
        JUMP,
        FALL,
        ATTACK,
        DEFENCE,
        HIT,
        FLY,
        DIE,
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class ChatPacket : Data<ChatPacket>
    {
        [MarshalAs(UnmanagedType.I2)]
        public ChatType chatType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string to;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public string chat;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class LoginPacket : Data<LoginPacket>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class DisconnectPacket : Data<DisconnectPacket>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class LogoutPacket : Data<LogoutPacket>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CharactorPacket : Data<CharactorPacket>
    {
        [MarshalAs(UnmanagedType.I2)]
        public CharactorState charactorState;

        public float dir;

        public (float, float) pos;

        public float hp;
        public float mana;
    }

    //[Serializable]
    //[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //public class CharactorActionPacket : Data<CharactorActionPacket>
    //{
    //    [MarshalAs(UnmanagedType.I2)]
    //    public CharactorState charactorState;

    //    public float dir;

    //    public (float, float) pos;
    //}

    //[Serializable]
    //[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //public class CharactorStatPacket : Data<CharactorStatPacket>
    //{
    //    public float hp;
    //    public float mana;
    //}
}
