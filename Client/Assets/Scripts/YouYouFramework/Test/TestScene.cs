//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestScene : MonoBehaviour 
{
	void Start () 
	{

	}
	
	void Update () 
	{
        if (Input.GetKeyUp(KeyCode.C))
        {
            GameEntry.Scene.LoadScene(1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            GameEntry.Scene.LoadScene(2);
        }
    }
}