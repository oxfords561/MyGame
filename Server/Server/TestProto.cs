using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TestProto : IProto
{
    public ushort ProtoCode { get { return 1001; } }
    public string ProtoEnName { get { return "TestProto"; } }

    public string msg;
    public byte[] ToArray()
    {
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteUTF8String(msg);
        return ms.ToArray();
    }

    public static TestProto GetProto(byte[] buffer)
    {
        TestProto proto = new TestProto();
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.msg = ms.ReadUTF8String();

        return proto;
    }
}

