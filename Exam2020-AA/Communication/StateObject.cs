﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Exam2020_AA.Communication
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
