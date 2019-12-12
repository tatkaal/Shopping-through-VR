using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using SimpleJSON;


public class VRShopDBConnector : MonoBehaviour {

    private static string APPLICATION_PATH = Directory.GetCurrentDirectory();
    private static string ARTICLE_FOLDER_NAME = "Articles";
    public static string ARTICLE_FOLDER_PATH = Path.Combine(APPLICATION_PATH, ARTICLE_FOLDER_NAME);

    public List<VRShopArticle> SearchForArticle(string searchString)
    {
        // Prepare return list
        //List<VRShopArticle> queriedArticles = new List<VRShopArticle>();
        List<VRShopArticle> getMyVal = new List<VRShopArticle>();

        // Prevent empty searches
        if (searchString.Length > 0)
        {

            /*WeatherAPI var = new WeatherAPI();*/
            var tempGameObj = new GameObject();
            WeatherAPI getHere = tempGameObj.AddComponent<WeatherAPI>();

            // StartCoroutine(getHere.getRequest());

            getMyVal = getHere.setWeatherAttributes(searchString);

            Debug.Log("List data received from WeatherAPI"+getMyVal);
            return getMyVal;
        }
        return null;
    }
}
