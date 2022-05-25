using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyPacket
{
    public class SocketAsyncEventArgsPool
    {
        static SocketAsyncEventArgsPool? instance;
        public static SocketAsyncEventArgsPool Instance
        {
            get
            {
                if (instance == null)
                    instance = new SocketAsyncEventArgsPool(30);
                return instance;
            }
        }

        Stack<SocketAsyncEventArgs>? pool;

        public SocketAsyncEventArgsPool(int capacity)
        {
            SetCapacity(capacity);
        }

        public void SetCapacity(int capacity)
        {
            pool = new Stack<SocketAsyncEventArgs>(capacity);
            for (int i = 0; i < capacity; i++)
                pool.Push(new SocketAsyncEventArgs());
        }

        public int Count => pool!.Count;

        public void Push(SocketAsyncEventArgs args)
        {
            if (pool == null) return;
            lock (pool)
            {
                pool.Push(args);
            }
        }

        public SocketAsyncEventArgs Pop()
        {
            lock (pool!)
            {
                return pool!.Pop();
            }
        }
    }
}
