using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class TestMD5 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetDir(@"E:\BaiduNetdiskDownload\从云舒哪里来的\UU教程\高级教程");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDir(string folderPath)
    {
        string[] pathArr = System.IO.Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

        string str = string.Empty;

        for (int i = 0; i < pathArr.Length; i++)
        {
            FileInfo info = new FileInfo(pathArr[i]);


            str += info.Name + "\r\n";
            str += GetMD5HashFromFile(pathArr[i]) + "\r\n";
            str += "\r\n\r\n";
            
        }

        //Debug.LogError(str);

        IOUtil.CreateTextFile(@"E:\2.txt", str);
    }

    public string GetMD5HashFromFile(string fileName)
    {
        try
        {
            FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }
}