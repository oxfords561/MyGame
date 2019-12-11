
using UnityEngine;
using YouYou;

public class ProcedureLogin : ProcedureBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEntry.Log(LogCategory.Procedure, "OnEnter ProcedureLogin");

        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        GameEntry.Log(LogCategory.Procedure, "OnLeave ProcedureLogin");
    }
}
