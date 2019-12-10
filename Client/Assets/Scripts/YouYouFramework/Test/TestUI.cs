//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestUI : MonoBehaviour
{
    //SafeInteger money = 0; //必须先赋值

    void Start()
    {
        //string str = Application.systemLanguage.ToString();

        //Debug.LogError("str=" + str);

        //int a = 100;


        //money++;

        //money += 20;

        //Debug.LogError("money=" + money);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            GameEntry.UI.OpenUIForm(UIFormId.UI_Task);

            //string str = GameEntry.Localization.GetString("Button.Receive");
            //Debug.LogError("str=" + str);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            GameEntry.UI.CloseUIForm(UIFormId.UI_Task);
        }
    }
}