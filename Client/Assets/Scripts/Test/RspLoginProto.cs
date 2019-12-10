using System;
using YouYou;


public class RspLoginProto : IProto
{
    public string msg;

    public ushort ProtoCode { get { return 1002; } }
    public string ProtoEnName { get { return "RspLoginProto"; } }

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteUTF8String(msg);
        return ms.ToArray();
    }

    public static RspLoginProto GetProto(byte[] buffer)
    {
        RspLoginProto proto = new RspLoginProto();
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.msg = ms.ReadUTF8String();

        return proto;
    }
}

