using System;
using YouYou;


public class ReqLoginProto :IProto
{
    public string acct;
    public string pass;

    public ushort ProtoCode { get { return 1001; } }
    public string ProtoEnName { get { return "ReqLoginProto"; } }

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteUTF8String(acct);
        ms.WriteUTF8String(pass);
        return ms.ToArray();
    }

    public static ReqLoginProto GetProto(byte[] buffer)
    {
        ReqLoginProto proto = new ReqLoginProto();
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.acct = ms.ReadUTF8String();
        proto.pass = ms.ReadUTF8String();

        return proto;
    }
}

