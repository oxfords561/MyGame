//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YouYou;

public class TestAessetBundle : MonoBehaviour
{
    //private AssetBundle bundleRes;

    //private AssetBundle bundleUI;

    //public string LocalFilePath;

    //List<Object> lstOBj = new List<Object>();

    ///// <summary>
    ///// 读取本地文件到byte数组
    ///// </summary>
    ///// <param name="path"></param>
    ///// <returns></returns>
    //public byte[] GetBuffer(string path)
    //{
    //    byte[] buffer = null;

    //    using (FileStream fs = new FileStream(path, FileMode.Open))
    //    {
    //        buffer = new byte[fs.Length];
    //        fs.Read(buffer, 0, buffer.Length);
    //    }
    //    return buffer;
    //}

    void Start()
    {
        //LocalFilePath = @"F:\UnityProject\youyouMMO\trunk\NewMMO\AssetBundles\1.0.6\Windows\";

        //AssetBundleLoaderUIRes("download/ui/uires/uicommon.assetbundle");

        //AssetBundleLoaderUI("download/ui/uiprefab.assetbundle");
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.M))
        //{
        //    Instantiate(bundleUI.LoadAsset("UI_Task"));
        //}
        ////if (Input.GetKeyUp(KeyCode.N))
        ////{
        ////    Instantiate(bundleUI.LoadAsset("UI_Loading1"));
        ////}
        //if (Input.GetKeyUp(KeyCode.L))
        //{
        //    //Destroy(GameObject.Find("UI_Task(Clone)"));
        //    //bundleUI.Unload(true);
        //    bundleRes.LoadAssetAsync("Btn_2");
        //}

        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.LogError("卸载分类资源池中资源");
            GameEntry.Pool.PoolManager.ReleaseAssetPool();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.LogError("卸载不用的资源");
            Resources.UnloadUnusedAssets();
        }

        //if (Input.GetKeyUp(KeyCode.B))
        //{
        //    //lstOBj.Clear();

        //    
        //}
    }

    //public void AssetBundleLoaderUIRes(string assetBundlePath)
    //{
    //    string fullPath = LocalFilePath + assetBundlePath;

    //    byte[] buffer = GetBuffer(fullPath);

    //    bundleRes = AssetBundle.LoadFromMemory(buffer);


    //    //Object[] arr = bundleRes.LoadAllAssets();

        

    //    //for (int i = 0; i < arr.Length; i++)
    //    //{
    //    //    Debug.LogError("==>" + arr[i].name);
    //    //    if (arr[i].name == "Btn_2")
    //    //    {
    //    //        lstOBj.Add(arr[i]);
    //    //    }
    //    //}

    //}

    //public void AssetBundleLoaderUI(string assetBundlePath)
    //{
    //    string fullPath = LocalFilePath + assetBundlePath;

    //    byte[] buffer = GetBuffer(fullPath);

    //    bundleUI = AssetBundle.LoadFromMemory(buffer);
    //}
}