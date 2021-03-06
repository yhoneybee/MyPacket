using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Data<T>
        where T : new()
    {
        public Data() { }

        public static byte[] Serialize(T data)
        {
            var size = Marshal.SizeOf<T>();
            var array = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(data, ptr, false);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);
            return array;
        }

        public static T Deserialize(byte[] array)
        {
            var size = Marshal.SizeOf<T>();
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(array, 0, ptr, size);
            var s = Marshal.PtrToStructure<T>(ptr);
            Marshal.FreeHGlobal(ptr);
            return s;
        }
    }
}
