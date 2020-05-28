using Exam2020_AA.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam2020_AA
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
