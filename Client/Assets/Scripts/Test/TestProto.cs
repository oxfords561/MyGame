using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestProto : IProto

{
    public ushort ProtoCode { get { return 1001; } }
    public string ProtoEnName { get { return "TestProto"; } }

    public string msg;
    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteUTF8String(msg);
        return ms.ToArray();
    }
}
