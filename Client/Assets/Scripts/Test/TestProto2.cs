using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestProto2 : IProto
{
    public ushort ProtoCode { get { return 1002; } }
    public string ProtoEnName { get { return "TestProto2"; } }

    public string msg;
    public byte[] ToArray()
    {
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteUTF8String(msg);
        return ms.ToArray();
    }

    public static TestProto2 GetProto(byte[] buffer)
    {
        TestProto2 proto = new TestProto2();
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.msg = ms.ReadUTF8String();

        return proto;
    }
}