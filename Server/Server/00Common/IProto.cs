using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IProto
{
    //协议编号
    ushort ProtoCode { get; }
    string ProtoEnName { get; }
    byte[] ToArray();
}

