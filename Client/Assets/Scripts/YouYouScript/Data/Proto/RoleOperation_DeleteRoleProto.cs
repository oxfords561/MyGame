//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-13 12:55:27
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYou;

/// <summary>
/// 客户端发送删除角色消息
/// </summary>
public struct RoleOperation_DeleteRoleProto : IProto
{
    public ushort ProtoCode { get { return 10005; } }
    public string ProtoEnName { get { return "RoleOperation_DeleteRole"; } }

    public int RoleId; //角色ID

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteInt(RoleId);

        return ms.ToArray();
    }

    public static RoleOperation_DeleteRoleProto GetProto(byte[] buffer)
    {
        RoleOperation_DeleteRoleProto proto = new RoleOperation_DeleteRoleProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.RoleId = ms.ReadInt();

        return proto;
    }
}