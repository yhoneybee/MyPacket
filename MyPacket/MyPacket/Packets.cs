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
        // 연결
        REQ_CONNECTED,
        RES_CONNECTED,

        // 회원가입, 로그인
        REQ_SIGNIN_PACKET,
        RES_SIGNIN_PACKET,

        REQ_LOGIN_PACKET,
        RES_LOGIN_PACKET,

        // 방 생성, 접속, 퇴장
        REQ_CREATE_ROOM_PACKET,
        RES_CREATE_ROOM_PACKET,
        REQ_ENTER_ROOM_PACKET,
        RES_ENTER_ROOM_PACKET,

        RES_OTHER_USER_ENTER_ROOM_PACKET,

        REQ_LEAVE_ROOM_PACKET,
        RES_LEAVE_ROOM_PACKET,

        RES_OTHER_USER_LEAVE_ROOM_PACKET,

        // 방 갱신
        REQ_ROOMS_PACKET,
        RES_ROOMS_PACKET,

        // 원하는 유저의 대한 정보
        REQ_USER_PACKET,
        RES_USER_PACKET,

        // 레디, 시작
        REQ_READY_GAME_PACKET,
        RES_READY_GAME_PACKET,
        REQ_START_GAME_PACKET,
        RES_START_GAME_PACKET,

        // 3판2선에 대한 승패
        REQ_SET_WIN_PACKET,
        REQ_SET_LOSE_PACKET,

        // 게임에 대한 승패
        RES_GAME_WIN_PACKET,
        RES_GAME_LOSE_PACKET,

        // 채팅
        REQ_CHAT_PACKET,
        RES_CHAT_PACKET,

        // 캐릭터
        REQ_CHARACTOR_PACKET,
        RES_CHARACTOR_PACKET,

        // 로그아웃
        REQ_LOGOUT_PACKET,
        RES_LOGOUT_PACKET,

        // 연결 끊김
        REQ_DISCONNECTED,
        RES_DISCONNECTED,
        END,
    }

    [Serializable]
    public enum CharactorState : short
    {
        IDLE,
        CROUCHING,
        CROUCH,
        WALK,
        RUN,
        JUMP,
        FALL,
        ATTACK_WEAK,
        ATTACK_STRONG,
        ATTACK_CROUCH,
        ATTACK_JUMP,
        ATTACK_COMMAND_WEAK,
        ATTACK_COMMAND_STRONG,
        DEFENCE,
        HIT,
        CORUCH_HIT,
        FLY,
        DIE,
    }

    [Serializable]
    public enum CharactorType : short
    {
        Samdae,
        Kanzi,
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class RES : Data<RES>
    {
        public bool completed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string reason;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct RoomInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string player1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string player2;
        public bool player1Ready;
        public bool player2Ready;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class RES_Rooms : Data<RES_Rooms>
    {
        public bool completed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string reason;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public RoomInfo[] roomInfos;
    }

    [Serializable]
    public struct UserInfo
    {
        public string id;
        public bool isLogined;
        public bool isPlayingGame;
        public int win;
        public int lose;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class RES_User : Data<RES_User>
    {
        public bool completed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string reason;
        public UserInfo userInfo;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class RES_CreateRoom : Data<RES_CreateRoom>
    {
        public bool completed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string reason;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string roomName;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class RES_EnterRoom : Data<RES_EnterRoom>
    {
        public bool completed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string reason;
        public RoomInfo roomInfo;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class RES_StartGame : Data<RES_StartGame>
    {
        public bool completed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string reason;
        public int playerNum;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ : Data<REQ>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string what;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ_Signin : Data<REQ_Signin>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string pw;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string pwAgain;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ_Login : Data<REQ_Login>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string pw;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ_CreateEnterRoom : Data<REQ_CreateEnterRoom>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string roomName;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ_User : Data<REQ_User>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ_Rooms : Data<REQ_Rooms>
    {
        public int startIndex;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ_RES_Chat : Data<REQ_RES_Chat>
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string id;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string to;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public string chat;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class REQ_RES_Charactor : Data<REQ_RES_Charactor>
    {
        [MarshalAs(UnmanagedType.I2)]
        public CharactorState charactorState;

        public float dir;

        public float posX;
        public float posY;

        public float hp;
        public float mana;
    }
}
