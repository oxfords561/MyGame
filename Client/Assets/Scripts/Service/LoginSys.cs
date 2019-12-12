/****************************************************
    文件：LoginSys.cs
	功能：登录注册业务系统
*****************************************************/

using PEProtocol;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSys : SystemRoot
{

    // public LoginWnd loginWnd;
    // public CreateWnd createWnd;
    private ResLoader loader;

    public override void InitSys()
    {
        base.InitSys();

        loader = ResLoader.Allocate();
        loader.Add2Load(QAssetBundle.Scenelogin_unity.SCENELOGIN);
        loader.LoadAsync(()=>{
            Log.I("加载场景成功");
            SceneManager.LoadScene("SceneLogin");
        });

        // loader.LoadSync(QAssetBundle.Scenelogin_unity.SCENELOGIN);

        PECommon.Log("Init LoginSys...");
    }

    /// <summary>
    /// 进入登录场景
    /// </summary>
    public void EnterLogin()
    {
        //异步的加载登录场景
        //并显示加载的进度
        resSvc.AsyncLoadScene(Constants.SceneLogin, () =>
        {
            //加载完成以后再打开注册登录界面
            //loginWnd.SetWndState();

            UIMgr.OpenPanel(QAssetBundle.Loginwnd_prefab.LOGINWND);
            //audioSvc.PlayBGMusic(Constants.BGLogin);
        });

    }

    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg.rspLogin);

        if (msg.rspLogin.playerData.name == "")
        {
            // createWnd.SetWndState();
        }
        else
        {
            // MainCitySys.Instance.EnterMainCity();
        }
        //关闭登录界面
        // loginWnd.SetWndState(false);
    }

    public void RspRename(GameMsg msg)
    {
        GameRoot.Instance.SetPlayerName(msg.rspRename.name);

        //跳转场景进入主城
        // MainCitySys.Instance.EnterMainCity();
        //关闭创建界面
        // createWnd.SetWndState(false);
    }
}