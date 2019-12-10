//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// 启动流程
    /// </summary>
    public class ProcedureLaunch : ProcedureBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
            GameEntry.Log(LogCategory.Procedure, "OnEnter ProcedureLaunch");

            //访问账号服务器
            string url = GameEntry.Http.RealWebAccountUrl + "/api/init";

            Dictionary<string, object> dic = GameEntry.Pool.DequeueClassObject<Dictionary<string, object>>();
            dic.Clear();

            GameEntry.Data.SysDataManager.CurrChannelConfig.ChannelId = 0;
            GameEntry.Data.SysDataManager.CurrChannelConfig.InnerVersion = 1001;

            dic["ChannelId"] = GameEntry.Data.SysDataManager.CurrChannelConfig.ChannelId;
            dic["InnerVersion"] = GameEntry.Data.SysDataManager.CurrChannelConfig.InnerVersion;

            GameEntry.Http.SendData(url, OnWebAccountInit, true, false, dic);
        }

        private void OnWebAccountInit(HttpCallBackArgs args)
        {
            if (!args.HasError)
            {
                LitJson.JsonData data = LitJson.JsonMapper.ToObject(args.Value);
                LitJson.JsonData config = LitJson.JsonMapper.ToObject(data["Value"].ToString());

                GameEntry.Data.SysDataManager.CurrChannelConfig.ServerTime = long.Parse(config["ServerTime"].ToString());
                GameEntry.Data.SysDataManager.CurrChannelConfig.SourceVersion = config["SourceVersion"].ToString();
                GameEntry.Data.SysDataManager.CurrChannelConfig.SourceUrl = config["SourceUrl"].ToString();
                GameEntry.Data.SysDataManager.CurrChannelConfig.RechargeUrl = config["RechargeUrl"].ToString();
                GameEntry.Data.SysDataManager.CurrChannelConfig.TDAppId = config["TDAppId"].ToString();
                GameEntry.Data.SysDataManager.CurrChannelConfig.IsOpenTD = int.Parse(config["IsOpenTD"].ToString()) == 1;

                GameEntry.Log(LogCategory.Resource, "RealSourceUrl=>" + GameEntry.Data.SysDataManager.CurrChannelConfig.RealSourceUrl);
                GameEntry.Procedure.ChangeState(ProcedureState.CheckVersion);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnLeave()
        {
            base.OnLeave();
            GameEntry.Log(LogCategory.Procedure, "OnLeave ProcedureLaunch");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}