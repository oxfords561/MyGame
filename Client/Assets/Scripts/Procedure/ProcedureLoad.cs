
using UnityEngine.SceneManagement;
using YouYou;

public class ProcedureLoad : ProcedureBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEntry.Log(LogCategory.Procedure, "OnEnter ProcedureLoad");

        // 打开 loading 界面
        // GameEntry.UI.OpenUIForm(UIFormId.LoadingWnd);
        GameEntry.Scene.LoadScene(1,true,()=>{
            // 打开了场景之后
            // SceneManager.LoadScene(1);
            // GameEntry.UI.OpenUIForm(UIFormId.LoginWnd);
        });


        //GameEntry.Procedure.ChangeState(ProcedureState.Login);
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        GameEntry.Log(LogCategory.Procedure, "OnLeave ProcedureLoad");
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}