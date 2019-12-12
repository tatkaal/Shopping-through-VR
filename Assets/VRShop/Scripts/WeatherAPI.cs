using SimpleJSON;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*using System.Threading;
using UnityEngine.UI;*/

public class WeatherAPI : MonoBehaviour
{
    public string url1 = "https://www.dropbox.com/s/yzhjx9opzjl1qmp/tbl_articles.json?dl=1";
    public List<VRShopArticle> tester = null;

    public WeatherAPI()
    {
        tester = new List<VRShopArticle>();
        GlobalControl.vrShopArticle = tester;
    }
    //GameObject tempObject = new GameObject();
    //List<VRShopArticle> queriedArticles = tempObject.AddComponent<List<VRShopArticle>>();

    // Use this for initialization
    public IEnumerator getRequest()
    {
        //WWW request = new WWW(url1);
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

    public List<VRShopArticle> setWeatherAttributes(string searchStr)
    {

        StartCoroutine(getRequest());
        UnityWebRequest jsonString = GlobalControl.sendValFunc;

        //List<VRShopArticle> queriedArticles = AddComponents<List<VRShopArticle>>(tempObject);

        if (jsonString.isDone)
        {
            StopCoroutine(getRequest());
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

                    try
                    {
                        VRShopArticle article = new VRShopArticle(id, price, articleName, description, img, size);                       
                        tester.Add(article);
                    }

                    catch (Exception)
                    {

                    }
                    
                }
                /*Debug.Log("These are queried :" + tester);*/
                return tester;
            }

            else
            {
                Debug.Log("You typed " + searchStr);
                for (int i = 0; i < weatherJson.Count; i++)
                {
                    id = Convert.ToInt32(weatherJson[i]["id"]);
                    articleName = weatherJson[i]["name"];
                    price = Convert.ToDecimal(weatherJson[i]["price"]);
                    description = weatherJson[i]["description"];
                    img = weatherJson[i]["thumbnail"];
                    size = float.Parse(weatherJson[i]["scale_factor"]);

                    try
                    {
                        if ((articleName.Length > 0) && (description.Length > 0))
                        {
                            Debug.Log("<******************************************************>");
                            if ((articleName.Contains(searchStr)) || (description.Contains(searchStr)))
                            {
                                Debug.Log("<&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&>");

                                VRShopArticle article = new VRShopArticle(id, price, articleName, description, img, size);
                                tester.Add(article);
                                Debug.Log(weatherJson[i]);
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }
                }
/*                Debug.Log("Total queried values:" + tester.Count);
                Debug.Log("return value:" + tester);*/
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
