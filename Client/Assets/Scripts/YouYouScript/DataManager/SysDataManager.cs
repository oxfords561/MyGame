//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 系统相关数据
/// </summary>
public class SysDataManager
{
    /// <summary>
    /// 当前的服务器时间
    /// </summary>
    public long CurrServerTime;

    /// <summary>
    /// 当前的渠道设置
    /// </summary>
    public ChannelConfigEntity CurrChannelConfig
    {
        get;
        private set;
    }

    public SysDataManager()
    {
        CurrChannelConfig = new ChannelConfigEntity();
    }

    public void Clear()
    {

    }

    public void Dispose()
    {

    }
}