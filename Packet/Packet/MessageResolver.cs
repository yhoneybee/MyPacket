using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    public class MessageResolver
    {
        public delegate void CompletedMessageCallback(Packet packet);

        int messageSize;
        byte[] messageBuffer = new byte[Defines.MESSAGE_BUFFER_SIZE];
        byte[] headerBuffer = new byte[Defines.HEADER_BUFFER_SIZE];
        byte[] typeBuffer = new byte[Defines.TYPE_BUFFER_SIZE];

        PacketType preType;

        int headPos;
        int typePos;
        int currPos;

        short messageType;
        int remainBytes;

        bool headCompleted;
        bool typeCompleted;
        bool completed;

        CompletedMessageCallback completedCallback;

        public MessageResolver()
        {

        }

        public void OnReceive(byte[] buffer, int offset, int transffered, CompletedMessageCallback callback)
        {
            int srcPos = offset;

            completedCallback = callback;

            remainBytes = transffered;

            if (!headCompleted)
            {
                headCompleted = ReadHead(buffer, ref srcPos);
                if (!headCompleted) return;

                messageSize = GetBodySize();

                if (messageSize < 0 || Defines.COMPLETE_MESSAGE_SIZE_CLIENT < messageSize) return;
            }
            if (!typeCompleted)
            {
                typeCompleted = ReadType(buffer, ref srcPos);
                if (!typeCompleted) return;

                messageType = BitConverter.ToInt16(typeBuffer, 0);

                if (messageType < 0 || ((int)PacketType.END) < messageType) return;

                preType = (PacketType)messageType;
            }
            if (!completed)
            {
                completed = ReadBody(buffer, ref srcPos);
                if (!completed) return;
            }

            Packet packet = new Packet();
            packet.type = messageType;
            packet.SetData(messageBuffer, messageSize);

            completedCallback(packet);

            ClearBuffer();
        }

        private void ClearBuffer()
        {
            Array.Clear(messageBuffer, 0, messageBuffer.Length);
            Array.Clear(headerBuffer, 0, headerBuffer.Length);
            Array.Clear(typeBuffer, 0, typeBuffer.Length);

            messageSize = 0;
            headPos = 0;
            typePos = 0;
            currPos = 0;
            messageType = 0;

            headCompleted = false;
            typeCompleted = false;
            completed = false;
        }

        private int GetBodySize() => BitConverter.ToInt32(headerBuffer, 0);

        private bool ReadBody(byte[] buffer, ref int srcPos) => ReadUntil(buffer, ref srcPos, headerBuffer, ref headPos, Defines.HEADER_BUFFER_SIZE);

        private bool ReadType(byte[] buffer, ref int srcPos) => ReadUntil(buffer, ref srcPos, typeBuffer, ref typePos, Defines.TYPE_BUFFER_SIZE);

        private bool ReadHead(byte[] buffer, ref int srcPos) => ReadUntil(buffer, ref srcPos, messageBuffer, ref currPos, messageSize);

        private bool ReadUntil(byte[] buffer, ref int srcPos, byte[] destBuffer, ref int destPos, int toSize)
        {
            if (remainBytes < 0) return false;

            int copySize = toSize - destPos;
            if (remainBytes < copySize) copySize = remainBytes;

            Array.Copy(buffer, srcPos, destBuffer, destPos, copySize);

            srcPos += copySize;
            destPos += copySize;
            remainBytes -= copySize;

            return !(destPos < toSize);
        }
    }
}
