

using UnityEngine;
using YouYou;

public class ProcedureBattle : ProcedureBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEntry.Log(LogCategory.Procedure, "OnEnter ProcedureLogOn");

        GameEntry.Scene.LoadScene(1, onComplete: () =>
        {
            GameEntry.Event.CommonEvent.Dispatch(SysEventId.CloseCheckVersionUI);
        });
    }

    private void LoadRole()
    {
        GameEntry.Resource.ResourceLoaderManager.LoadMainAsset(AssetCategory.RolePrefab, string.Format("Assets/Download/Role/RolePrefab/Player/Tianshan_001/Zy_tianshan_002_yxt/Zy_tianshan_002_yxt.prefab"), (ResourceEntity resourceEntity) =>
        {
            Debug.LogError("加载角色完毕");

            GameObject obj = Object.Instantiate(resourceEntity.Target as GameObject);
            obj.transform.position = new Vector3(166.51f, 1.454f, 170.1f);
        });
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyUp(KeyCode.D))
        {
            LoadRole();
        }
    }

    public override void OnLeave()
    {
        base.OnLeave();
        GameEntry.Log(LogCategory.Procedure, "OnLeave ProcedureLogOn");
    }
}
