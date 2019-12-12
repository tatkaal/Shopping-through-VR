using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class GlobalControl
{
    public static UnityWebRequest sendValFunc
    {
        get;set;
    }
    public static List<VRShopArticle> vrShopArticle
    {
        get;set;
    }
}
