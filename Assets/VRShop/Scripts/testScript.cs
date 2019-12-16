using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using System.Net;

public class testScript : MonoBehaviour
{
    private string url1 = "http://13.90.151.82:8000/api/products/";
    public Texture articleImage;
    public List<string> productUrl = new List<string>();
    public List<string> imgUrl = new List<string>();
    //public List<string> cat = new List<string>();
    public string categories,cat;

    public string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "Images");
        
        StartCoroutine(getRequest());
        Debug.Log("..............................Completed.............................");
    }

    public IEnumerator getRequest()
    {
        /*WWW request = new WWW(url1);*/
        UnityWebRequest request = UnityWebRequest.Get(url1);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Network error" + request.error);
        }
        else
        {
            /*Debug.Log("<< ---------------------------------------------------- >>");
            GlobalControl.sendValFunc = request;
            print("GlobalControl val :" + GlobalControl.sendValFunc);*/
            if (request.isDone)
            {
                var items = JSON.Parse(request.downloadHandler.text);

                for (int i = 0; i < items.Count; i++)
                {
                    productUrl.Add(items[i]["url"]);
                    Debug.Log("..........." + productUrl[i]);
                }

                Debug.Log("About to StartCoroutine");
                
                StartCoroutine(getImagesFromList(productUrl));
                StopCoroutine(getRequest());
                //TestCoroutine(imgUrl);

                Debug.Log("Back from StartCoroutine");
            }
        }
    }

    public IEnumerator getImagesFromList(List<string> myurl)
    {
        Debug.Log("inside coroutine............");

        for (int i = 0; i < myurl.Count; i++)
        {
            UnityWebRequest request2 = UnityWebRequest.Get(myurl[i]);
            yield return request2.SendWebRequest();


            if (request2.isNetworkError || request2.isHttpError)
            {
                Debug.Log("Network error for image :" + request2.error);
            }
            else
            {
                if (request2.isDone)
                {
                    /*GlobalControl2.sendValFunc = request2;*/
                    //articleImage = ((DownloadHandlerTexture)request2.downloadHandler).texture;

                    var receivedJson = JSON.Parse(request2.downloadHandler.text);
                    //categories.Add(receivedJson["categories"]);
                    
                    var catCheck = receivedJson["categories"];

                    if (catCheck.Count!=0)
                    {
                        categories = receivedJson["categories"][0];

                        if (categories.Contains("COATS & JACKETS"))
                        {
                            //cat.Add("COATS & JACKETS");
                            cat = "COATS & JACKETS";
                        }
                        else if (categories.Contains("ACCESSORIES"))
                        {
                            //cat.Add("ACCESSORIES");
                            cat = "ACCESSORIES";
                        }
                        else if (categories.Contains("PLUS SIZES"))
                        {
                            //cat.Add("PLUS SIZES");
                            cat = "PLUS SIZES";
                        }
                        else if (categories.Contains("LEATHER & FUR"))
                        {
                            //cat.Add("LEATHER & FUR");
                            cat = "LEATHER & FUR";
                        }
                        else if (categories.Contains("EVENING WEAR"))
                        {
                            //cat.Add("EVENING WEAR");
                            cat = "EVENING WEAR";
                        }
                        else if (categories.Contains("Rugs and Home"))
                        {
                            //cat.Add("RUGS AND HOME");
                            cat = "RUGS AND HOME";
                        }
                        else if (categories.Contains("CLEARANCE"))
                        {
                            //cat.Add("CLEARANCE");
                            cat = "CLEARANCE";
                        }
                        else if (categories.Contains("WHOLESALE"))
                        {
                            //cat.Add("CLEARANCE");
                            cat = "WHOLESALE";
                        }
                        else if (categories.Contains("NEW ARRIVALS"))
                        {
                            //cat.Add("NEW ARRIVALS");
                            cat = "NEW ARRIVALS";
                        }

                        var check = receivedJson["images"];

                        if (check.Count == 0)
                        {
                            Debug.Log("image url is null");
                        }
                        else
                        {
                            WebClient client = new WebClient();
                            string allUrl;
                            for (int j = 0; j < check.Count; j++)
                            {
                                allUrl = check[j]["original"];
                                client.DownloadFile(allUrl, savePath + "/" + cat + "/" + "imgID" + check[j]["id"] + "_proID" + check[j]["product"] + ".jpg");
                                Debug.Log("Downloaded file................ " + j + "of product id " + check[j]["product"] + " and image id " + check[j]["id"]);
                                //imgUrl.Add(receivedJson["images"][0]["original"]);
                                //Debug.Log("%%%%%%%%%% URL of actual images %%%%%%%%%%" + "      " + i + "     " + imgUrl[i]);
                            }

                        }
                    }
                    else
                    {
                        Debug.Log("categories is null");
                        cat = "Null";

                        var check = receivedJson["images"];

                        if (check.Count == 0)
                        {
                            Debug.Log("image url is null");
                        }
                        else
                        {
                            WebClient client = new WebClient();
                            string allUrl;
                            for (int j = 0; j < check.Count; j++)
                            {
                                allUrl = check[j]["original"];
                                client.DownloadFile(allUrl, savePath + "/" + cat + "/" + "imgID" + check[j]["id"] + "_proID" + check[j]["product"] + ".jpg");
                                Debug.Log("Downloaded file................ " + j + "of product id " + check[j]["product"] + " and image id " + check[j]["id"]);
                                //imgUrl.Add(receivedJson["images"][0]["original"]);
                                //Debug.Log("%%%%%%%%%% URL of actual images %%%%%%%%%%" + "      " + i + "     " + imgUrl[i]);
                            }

                        }
                    }

                }
            }
        }

        //TestCoroutine(imgUrl);
    }
/*
    public void TestCoroutine(List<string> getListOfUrl)
    {
        *//*Debug.Log("inside coroutine............");*//*

        for (int i = 0; i < getListOfUrl.Count; i++)
        {
            WebClient client = new WebClient();
            client.DownloadFile(getListOfUrl[i], savePath+i+".jpg");
            Debug.Log("Downloaded file "+i);
        }
    }*/
}