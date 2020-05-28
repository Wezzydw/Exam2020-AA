
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsListenerForLinux
{
    public class SocketManager
    {
        List<SocketListener> connections;
        public SocketManager()
        {
            connections = new List<SocketListener>();
        }
    }
}
