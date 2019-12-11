
using UnityEngine;
using YouYou;

public class ProcedureRoot : ProcedureBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEntry.Log(LogCategory.Procedure, "OnEnter ProcedureRoot");

        // 加载配置文件
        GameEntry.DataTable.LoadDataTableAsync();

        TimeAction action = GameEntry.Time.CreateTimeAction();
        action.Init(0.1f, 0f, 1, null, null, () =>
        {
            GameEntry.Procedure.ChangeState(ProcedureState.Load);
        }).Run();
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        GameEntry.Log(LogCategory.Procedure, "OnLeave ProcedureRoot");
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
