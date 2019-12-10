
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestSocket : MonoBehaviour
{
    void Start()
    {
        GameEntry.Event.SocketEvent.AddEventListener(1002,OnTestRsp);
    }

    private void OnTestRsp(byte[] userData)
    {
        TestProto2 p2 = TestProto2.GetProto(userData);
        Debug.Log("接收到 服务的消息: "+p2.msg);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            GameEntry.Socket.ConnectToMainSocket("127.0.0.1", 17666);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {

           //Task_SearchTaskProto proto = new Task_SearchTaskProto();
           TestProto proto = new TestProto();
           proto.msg = "你好我是客户端，这是我发的消息";
           GameEntry.Socket.SendMsg(proto);


           Debug.Log("发送消息完毕");
           //for (int i = 0; i < 100; i++)
           //{
           //    System_SendLocalTimeProto proto = new System_SendLocalTimeProto();

           //    GameEntry.Socket.SendMsg(proto);
           //}
        }

    }

    private void OnDestroy() {
        GameEntry.Event.SocketEvent.RemoveEventListener(1002,OnTestRsp);
    }
}