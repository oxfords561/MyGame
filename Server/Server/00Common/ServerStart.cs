/****************************************************
	文件：ServerStart.cs
	功能：服务器入口
*****************************************************/

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

class ServerStart {
    private static string m_ServerIP = "127.0.0.1";
    private static int m_Port = 17666;

    private static Socket m_ServerSocket;

    static void Main(string[] args) {
        //ServerRoot.Instance.Init();

        // 实例化socket
        m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //向操作系统申请一个可用的ip和端口用来通讯
        m_ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(m_ServerIP), m_Port));

        //设置最多3000个排队连接请求
        m_ServerSocket.Listen(3000);

        Console.WriteLine("启动监听{0}成功", m_ServerSocket.LocalEndPoint.ToString());

        Thread mThread = new Thread(ListenClientCallBack);
        mThread.Start();

        while (true) {
            ServerRoot.Instance.Update();
            Thread.Sleep(20);
        }
    }

    /// <summary>
    /// 监听客户端连接
    /// </summary>
    private static void ListenClientCallBack()
    {
        while (true)
        {
            //接收客户端请求
            Socket socket = m_ServerSocket.Accept();

            Console.WriteLine("客户端{0}已经连接", socket.RemoteEndPoint.ToString());

            //一个角色 就相当于一个客户端
            //Role role = new Role();
            //ClientSocket clientSocket = new ClientSocket(socket, role);
            SocketHelper helper = new SocketHelper(socket);

            //把角色添加到角色管理
            //RoleMgr.Instance.AllRole.Add(role);
        }
    }
}