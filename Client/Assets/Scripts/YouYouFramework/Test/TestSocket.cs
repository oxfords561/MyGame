//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestSocket : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            GameEntry.Socket.ConnectToMainSocket("192.168.0.111", 1038);
        }
        //else if (Input.GetKeyUp(KeyCode.C))
        //{

        //    Task_SearchTaskProto proto = new Task_SearchTaskProto();
        //    GameEntry.Socket.SendMsg(proto);
        //    //for (int i = 0; i < 100; i++)
        //    //{
        //    //    System_SendLocalTimeProto proto = new System_SendLocalTimeProto();

        //    //    GameEntry.Socket.SendMsg(proto);
        //    //}

        //}
        //else if (Input.GetKeyUp(KeyCode.C))
        //{
        //    TestA();
        //}
        //else if (Input.GetKeyUp(KeyCode.D))
        //{
        //    TestB();
        //}
    }

    void TestA()
    {
        
    }

    void TestB()
    {
        
    }
}