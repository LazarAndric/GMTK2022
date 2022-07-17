using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesUtil
{
    public static T[] getResources<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }
}
