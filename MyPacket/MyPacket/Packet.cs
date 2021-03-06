using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    public class Packet
    {
        public short type { get; set; }
        public byte[] data { get; set; }

        public Packet() { }

        public void SetData(PacketType packetType, byte[] data, int len)
        {
            type = ((short)packetType);
            this.data = new byte[len];
            Array.Copy(data, this.data, len);
        }

        public void SetData(PacketType packetType, byte[] data)
        {
            type = ((short)packetType);
            this.data = new byte[data.Length];
            Array.Copy(data, this.data, this.data.Length);
        }

        public byte[] GetSendBytes()
        {
            byte[] type_bytes = BitConverter.GetBytes(type);
            int header_size = data.Length;
            byte[] header_bytes = BitConverter.GetBytes(header_size);
            byte[] send_bytes = new byte[header_bytes.Length + type_bytes.Length + data.Length];

            Array.Copy(header_bytes, 0, send_bytes, 0, header_bytes.Length);
            Array.Copy(type_bytes, 0, send_bytes, header_bytes.Length, type_bytes.Length);
            Array.Copy(data, 0, send_bytes, header_bytes.Length + type_bytes.Length, data.Length);

            return send_bytes;
        }

        public T GetPacket<T>()
            where T : new()
            => Data<T>.Deserialize(data);
    }
}
