﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class testScript2 : MonoBehaviour
{
    private string url1 = "https://www.dropbox.com/s/yzhjx9opzjl1qmp/tbl_articles.json?dl=1";
    public Texture articleImage;
    //public string getTheUrl;
    public List<string> imgUrl = new List<string>();

    void Start()
    {

        StartCoroutine(getRequest());

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
                    imgUrl.Add(items[i]["thumbnail"]);
                    Debug.Log("..........." + imgUrl[i]);
                }

                Debug.Log("About to StartCoroutine");

                StartCoroutine(TestCoroutine(imgUrl));

                Debug.Log("Back from StartCoroutine");
            }

        }
    }

    IEnumerator TestCoroutine(List<string> getListOfUrl)
    {
        Debug.Log("inside coroutine............");

        for (int i = 0; i < getListOfUrl.Count; i++)
        {
            UnityWebRequest request2 = UnityWebRequestTexture.GetTexture(getListOfUrl[i]);
            yield return request2.SendWebRequest();


            if (request2.isNetworkError || request2.isHttpError)
            {
                Debug.Log("Network error for image :" + request2.error);
            }
            else
            {
                /*GlobalControl2.sendValFunc = request2;*/
                articleImage = ((DownloadHandlerTexture)request2.downloadHandler).texture;

                GameObject rawImage = GameObject.Find("RawImage" + i);
                rawImage.GetComponent<RawImage>().texture = articleImage;
            }
        }
        /*Debug.Log("Just waited 1 second");
        yield return new WaitForSeconds(1);*/
        /* Debug.Log("Just waited another second");
         yield break;
         Debug.Log("You'll never see this"); // produces a dead code warning*/
    }
}