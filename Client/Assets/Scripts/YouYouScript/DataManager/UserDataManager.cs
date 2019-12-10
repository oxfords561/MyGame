//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用户数据
/// </summary>
public class UserDataManager : IDisposable
{
    /// <summary>
    /// 服务器返回的任务列表
    /// </summary>
    public List<ServerTaskEntity> ServerTaskList
    {
        get;
        private set;
    }


    public UserDataManager()
    {
        ServerTaskList = new List<ServerTaskEntity>();
    }

    public void Clear()
    {
        ServerTaskList.Clear();
    }

    public void Dispose()
    {
        ServerTaskList.Clear();
    }

    public void ReceiveTask(Task_SearchTaskReturnProto proto)
    {
        int len = proto.CurrTaskItemList.Count;
        for (int i = 0; i < len; i++)
        {
            Task_SearchTaskReturnProto.TaskItem item = proto.CurrTaskItemList[i];

            ServerTaskList.Add(new ServerTaskEntity()
            {
                Id = item.Id,
                Status = item.Status
            });
        }
    }
}