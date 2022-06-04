using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    public class SocketAsyncEventArgsPool : Singleton<SocketAsyncEventArgsPool>
    {
        Stack<SocketAsyncEventArgs>? pool;

        override protected void Init()
        {
            pool = new Stack<SocketAsyncEventArgs>(Defines.MAX_CONNECTION);

            for (int i = 0; i < Defines.MAX_CONNECTION; i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                pool.Push(args);
            }
        }

        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item is null");
            }

            lock (pool!)
            {
                if (pool.Count >= Defines.MAX_CONNECTION)
                {
                    item.Dispose();
                    return;
                }
                pool.Push(item);
            }
        }

        public SocketAsyncEventArgs Pop()
        {
            lock (pool)
            {
                if (pool.Count > 0)
                    return pool.Pop();
                else
                {
                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    return args;
                }
            }
        }

        public int Count => pool.Count;
    }
}
