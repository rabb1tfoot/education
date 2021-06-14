using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFunc
{
    static private CustomFunc instance;

    static public CustomFunc GetInstance()
    {
        if (instance == null)
            instance = new CustomFunc();

        return instance;
    }

    private CustomFunc()
    { }

    public void GetAssetPath(ref string _path)
    {
        _path = Application.dataPath;
        _path += "/Asset/";
    }
}
