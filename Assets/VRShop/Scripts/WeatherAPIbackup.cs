/*using SimpleJSON;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;
using UnityEngine.UI;

public class WeatherAPIbackup : MonoBehaviour
{

    public string url1 = "https://www.dropbox.com/s/yzhjx9opzjl1qmp/tbl_articles.json?dl=1";
    public List<VRShopArticle> tester = null;
    public WeatherAPIbackup()
    {
        tester = new List<VRShopArticle>();
        GlobalControl.vrShopArticle = tester;
    }
    *//*GameObject tempObject = new GameObject();
    List<VRShopArticle> queriedArticles = tempObject.AddComponent<List<VRShopArticle>>();*//*

    // Use this for initialization
    public IEnumerator getRequest()
    {
        *//*WWW request = new WWW(url1);*//*
        UnityWebRequest request = UnityWebRequest.Get(url1);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Network error" + request.error);
        }
        else
        {
            Debug.Log("<< ---------------------------------------------------- >>");
            GlobalControl.sendValFunc = request;
            print("GlobalControl val :" + GlobalControl.sendValFunc);
        }

    }

    public IEnumerator getImgRequest(string getLink)
    {
        Debug.Log("Image link received in imageCoroutine :" + getLink);
        UnityWebRequest request2 = UnityWebRequestTexture.GetTexture(getLink);
        yield return request2.SendWebRequest();

        if (request2.isNetworkError || request2.isHttpError)
        {
            Debug.Log("Network error for image :" + request2.error);
        }
        else
        {
            GlobalControl2.sendValFunc = request2;
        }
    }


    public List<VRShopArticle> setWeatherAttributes(string searchStr)
    {

        StartCoroutine(getRequest());
        UnityWebRequest jsonString = GlobalControl.sendValFunc;

        //List<VRShopArticle> queriedArticles = AddComponents<List<VRShopArticle>>(tempObject);

        if (jsonString.isDone)
        {

            //string jsonString = GlobalControl.sendValFunc;
            Debug.Log("Global Control Value below");
            //Debug.Log(jsonString);
            if (jsonString == null)
            {
                Debug.Log("globalcontrol value is not preserved");
            }
            else
            {
                Debug.Log("Value is Preserved");
            }

            //string jsonString = "";

            var weatherJson = JSON.Parse(jsonString.downloadHandler.text);
            Debug.Log("WeatherJson value below");
            Debug.Log(weatherJson);



            //var strList = new List<string>();
            *//*string searchStr = "Fox";*//*
            int id;
            string articleName;
            decimal price;
            string description = "";
            string img = null;
            float? size = null;

            if (string.Equals(searchStr, "a"))
            {
                Debug.Log("You typed everything :" + searchStr);
                for (int i = 0; i < weatherJson.Count; i++)
                {
                    id = Convert.ToInt32(weatherJson[i]["id"]);
                    articleName = weatherJson[i]["name"];
                    price = Convert.ToDecimal(weatherJson[i]["price"]);
                    description = weatherJson[i]["description"];
                    img = weatherJson[i]["thumbnail"];
                    size = float.Parse(weatherJson[i]["scale_factor"]);

                    StartCoroutine(getImgRequest(img));
                    UnityWebRequest getMe = GlobalControl2.sendValFunc;
                    if (getMe.isDone)
                    {
                        Texture reqToTexture = ((DownloadHandlerTexture)getMe.downloadHandler).texture;

                        try
                        {
                            VRShopArticle article = new VRShopArticle(id, price, articleName, description, reqToTexture, size);
                            *//*var maketempObj = new GameObject();
                            VRShopArticle article = maketempObj.AddComponent<VRShopArticle(id, price, articleName, description, img, size)>();*//*
                            tester.Add(article);
                            Debug.Log(weatherJson[i]);
                        }

                        catch (Exception)
                        {

                        }
                    }
                }
                Debug.Log("These are queried :" + tester);
                return tester;
            }

            else
            {
                Debug.Log("You typed " + searchStr);
                for (int i = 0; i < weatherJson.Count; i++)
                {
                    *//*Debug.Log("Total count :" + weatherJson.Count);*/

                    /*strList[0] = (weatherJson[i]["url"]);*/
                    /*strList.Add(weatherJson[i]["title"]);*/
                    /*Debug.Log(weatherJson[i]["url"]);*//*
                    id = Convert.ToInt32(weatherJson[i]["id"]);
                    articleName = weatherJson[i]["name"];
                    price = Convert.ToDecimal(weatherJson[i]["price"]);
                    description = weatherJson[i]["description"];
                    //img = Byte.TryParse(weatherJson[i]["thumbnail"]);
                    //img = Encoding.ASCII.GetBytes(weatherJson[i]["thumbnail"]);
                    //img = (byte[])weatherJson[i]["thumbnail"];
                    img = weatherJson[i]["thumbnail"];
                    size = float.Parse(weatherJson[i]["scale_factor"]);
                    *//*UnityWebRequest getMe;*//*
                    StartCoroutine(getImgRequest(img));
                    UnityWebRequest getMe = GlobalControl2.sendValFunc;

                    Texture reqToTexture = ((DownloadHandlerTexture)getMe.downloadHandler).texture;
                    GameObject rawImage = GameObject.Find("RawImage");
                    rawImage.GetComponent<RawImage>().texture = reqToTexture;

                    *//*if (getMe.isDone)
                    {*/
                    /*Texture reqToTexture = ((DownloadHandlerTexture)getMe.downloadHandler).texture;
                    GameObject rawImage = GameObject.Find("RawImage");
                    rawImage.GetComponent<RawImage>().texture = reqToTexture;*//*
                    try
                    {
                        if ((articleName.Length > 0) && (description.Length > 0))
                        {
                            Debug.Log("<******************************************************>");
                            if ((articleName.Contains(searchStr)) || (description.Contains(searchStr)))
                            {
                                Debug.Log("<&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&>");

                                //strList.Add(val);
                                VRShopArticle article = new VRShopArticle(id, price, articleName, description, reqToTexture, size);
                                tester.Add(article);
                                Debug.Log(weatherJson[i]);
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }
                    *//*}*//*
                }
                Debug.Log("Total queried values:" + tester.Count);
                Debug.Log("return value:" + tester);
                return tester;
            }
        }
        else
        {
            print("Request is not complete....Wait sometime");
        }

        return null;
    }
}
*/