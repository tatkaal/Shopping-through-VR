using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class ArticleMonitorWrapper : MonoBehaviour {

    public bool selectScreen;
    public GameObject colorObject;

    public GameObject frontObject;
    public GameObject nameObjectFront;
    public GameObject imageObjectFront;
    public GameObject priceObjectFront;

    public GameObject backObject;
    public GameObject nameObjectBack;
    public GameObject imageObjectBack;
    public GameObject priceObjectBack;
    public GameObject descriptionObjectBack;
    public GameObject cartObjectBack;

    public GameObject cartIncreaseObjectBack;
    public GameObject cartDecreaseObjectBack;
    public GameObject cartAddToCartObjectBack;

    public IList<GameObject> allChildren;

    private ShopExplorerBehavior shopExplorer;
    private const string COLOR_OBJECT = "Color";
    private const string FRONT_OBJECT = "Front";
    private const string BACK_OBJECT  = "Back";
    private const string NAME_OBJECT  = "Name";
    private const string IMAGE_OBJECT = "Image";
    private const string PRICE_OBJECT = "Price";
    private const string DESCRIPTION_OBJECT = "Description";
    private const string CART_OBJECT = "Cart";
    private const string CART_INCREASE_OBJECT = "IncreaseCart";
    private const string CART_DECREASE_OBJECT = "DecreaseCart";
    private const string CART_ADDTOCART_OBJECT = "AddToCart";

    private const string TINT_COLOR = "_TintColor";

    public int wallPositionId;
    public int articleLoadIndexId;

    private VRShopArticle assignedArticle;
    private string articleName;
    private decimal articlePrice;
    private string articleDescription;
    public Texture articleImage;
    private string getImageLinkStr;

    private const int DEFAULT_QUANTITY = 1;
    public int cartQuanity = DEFAULT_QUANTITY;
    private const char CURRENCY_SYMBOL = '€';

    private Texture defaultTexture;
    private Vector3 imgScaleFront;
    private Vector3 imgScaleBack;

    void Awake() {
        selectScreen = false;

        shopExplorer = transform.parent.GetComponent<ShopExplorerBehavior>();

        // Find all the objects from the children
        colorObject = transform.Find(COLOR_OBJECT).gameObject;

        frontObject = transform.Find(FRONT_OBJECT).gameObject;
        nameObjectFront = frontObject.transform.Find(NAME_OBJECT).gameObject;
        imageObjectFront = frontObject.transform.Find(IMAGE_OBJECT).gameObject;
        priceObjectFront = frontObject.transform.Find(PRICE_OBJECT).gameObject;

        backObject = transform.Find(BACK_OBJECT).gameObject;
        nameObjectBack = backObject.transform.Find(NAME_OBJECT).gameObject;
        imageObjectBack = backObject.transform.Find(IMAGE_OBJECT).gameObject;
        priceObjectBack = backObject.transform.Find(PRICE_OBJECT).gameObject;
        descriptionObjectBack = backObject.transform.Find(DESCRIPTION_OBJECT).gameObject;

        cartObjectBack = backObject.transform.Find(CART_OBJECT).gameObject;
        cartIncreaseObjectBack = cartObjectBack.transform.Find(CART_INCREASE_OBJECT).gameObject;
        cartDecreaseObjectBack = cartObjectBack.transform.Find(CART_DECREASE_OBJECT).gameObject;
        cartAddToCartObjectBack = cartObjectBack.transform.Find(CART_ADDTOCART_OBJECT).gameObject;

        // Memorize a couple values for the thumbnail
        defaultTexture = imageObjectFront.GetComponent<Renderer>().material.mainTexture;
        imgScaleFront = imageObjectFront.transform.localScale;
        imgScaleBack = imageObjectBack.transform.localScale;
    }

    void Update() {
        if (selectScreen) {
            // Debug to select the own screen from inspector
            selectScreen = false;
            Select();
        }    
    }

    public void Select() {
        // Tell the ShopExplorer that this article monitor has been selected
        shopExplorer.SelectScreen(gameObject);

        // Spawn the shop item
        shopExplorer.SpawnShopItem(gameObject);
    }

    public Color GetMonitorColor() {
        if (colorObject != null) {
            return transform.GetComponent<Renderer>().material.GetColor(TINT_COLOR);
        }
        return Color.black;
    }

    private void SetMonitorColor(Color color) {
        if (colorObject != null) {
            colorObject.GetComponent<Renderer>().material.SetColor(TINT_COLOR, color);
        }
    }

    public void SetBacksideActive(bool active) {
        if (backObject != null) {
            backObject.SetActive(active);
        }
    }

    private void UpdateName() {
        // Front
        TextMeshPro frontName = nameObjectFront.transform.GetComponent<TextMeshPro>();
        frontName.SetText(articleName);
        frontName.ForceMeshUpdate(true);

        // Back
        TextMeshPro backName = nameObjectBack.transform.GetComponent<TextMeshPro>();
        backName.SetText(articleName);
        backName.ForceMeshUpdate(true);
    }

    private void UpdateDescription() {
        // Back
        TextMeshPro backDescription = descriptionObjectBack.transform.GetComponent<TextMeshPro>();
        backDescription.SetText(articleDescription);
        backDescription.ForceMeshUpdate(true);
    }

    private void UpdatePrice() {
        // Front
        string newPriceFront = string.Format("{0:0.00} {1}", articlePrice.ToString(), CURRENCY_SYMBOL);
        TextMeshPro frontPrice = priceObjectFront.transform.GetComponent<TextMeshPro>();
        frontPrice.SetText(newPriceFront);
        frontPrice.ForceMeshUpdate(true);

        // Back
        string newPriceBack = string.Format("{2}x {0:0.00} {1} = {3:0.00} {1}", articlePrice.ToString(), CURRENCY_SYMBOL, cartQuanity, (articlePrice*cartQuanity).ToString());
        TextMeshPro backPrice = priceObjectBack.transform.GetComponent<TextMeshPro>();
        backPrice.SetText(newPriceBack);
        backPrice.ForceMeshUpdate(true);
    }

    private void UpdateImage() {
        
        if (articleImage != null) {
            Debug.Log("articleImage is not null");
            //Texture2D thumbnail = new Texture2D(1, 1);
            Texture thumbnail = articleImage;
            //thumbnail.LoadImage(myTextureTest);
            imageObjectFront.GetComponent<Renderer>().material.mainTexture = thumbnail;
            imageObjectBack.GetComponent<Renderer>().material.mainTexture = thumbnail;

            // Preserve asect ratio
            int width = thumbnail.width;
            int height = thumbnail.height;
            float aspect = (float)width / (float)height;

            Vector3 scaleFront = imgScaleFront;
            if (width >= height) {
                scaleFront.x = scaleFront.y * aspect;
            } else {
                scaleFront.y = scaleFront.x * aspect;
            }
            imageObjectFront.transform.localScale = scaleFront;

            Vector3 scaleBack = imgScaleBack;
            if (width >= height) {
                scaleBack.x = scaleBack.y * aspect;
            } else {
                scaleBack.y = scaleBack.x * aspect;
            }
            imageObjectBack.transform.localScale = imgScaleBack;
        } else {
            imageObjectFront.GetComponent<Renderer>().material.mainTexture = defaultTexture;
            imageObjectBack.GetComponent<Renderer>().material.mainTexture = defaultTexture;
        }
    }

    private void UpdateColor() {
        // Visually indicate whether the article has a model (the absence of a scale factor implies that, even if it isn't 100% accurate)
        if (assignedArticle.ScaleFactor != null) {
            SetMonitorColor(shopExplorer.colorActive);
        } else {
            SetMonitorColor(shopExplorer.colorInactive);
        }
    }

    public IEnumerator getImgRequest(string getLink)
    {
        Debug.Log("Image link received in imageCoroutine :" + getLink);
        UnityWebRequest request2 = UnityWebRequestTexture.GetTexture(getLink);
        yield return request2.SendWebRequest();
        /*while (!request2.isDone)
        {
            yield return request2.SendWebRequest();
        }*/

        if (request2.isNetworkError || request2.isHttpError)
        {
            Debug.Log("Network error for image :" + request2.error);
        }
        else
        {
            if (request2.isDone)
            {
                GlobalControl2.sendValFunc = request2;
                articleImage = ((DownloadHandlerTexture)request2.downloadHandler).texture;
                UpdateImage();
            }
            
            //StopCoroutine(getLink);
        }
    }

    public void SetArticle(VRShopArticle article) 
    {
        /*counter++;
        if (counter > 1)
        {
            
        }*/
        Debug.Log("Articles received here :"+article);
        Debug.Log("Article Image Location :"+article.Thumbnail);
        Debug.Log("Assined Article values :"+assignedArticle);

        //Start debugging/looking from here : when not commented image is not displayed
        if (article == assignedArticle)
        {
            Debug.Log("article==assignedArticle is true");
            if (GlobalControl2.sendValFunc.isDone)
            {
                /*                StopCoroutine(getImgRequest(article.Thumbnail));
                                Debug.Log("Coroutines stopped");*/
                GlobalControl2.runOrNot = 1;
            }
            
            return;
        }
        GlobalControl2.runOrNot = 0;
        Debug.Log("Ayo hai ayo <@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@>");

        //getImageLinkStr = article.Thumbnail;

        //var objTest = new GameObject();
        //WeatherAPI testOBJ = objTest.AddComponent<WeatherAPI>();

        //WeatherAPI testOBJ = new WeatherAPI();

        cartQuanity = DEFAULT_QUANTITY;

        articleName = article.Name;
        articlePrice = article.Price;
        articleDescription = article.Description;
        //articleImage = article.Thumbnail;
        //Debug.Log("Image Bytes to display"+articleImage);
        
        assignedArticle = article;

        UpdateName();
        UpdatePrice();
        UpdateDescription();
        StartCoroutine(getImgRequest(article.Thumbnail));

        /*if ((GlobalControl2.sendValFunc).isDone)
        {
            StopCoroutine(getImgRequest(article.Thumbnail));
        }*/
        /*UpdateImage();*/
        UpdateColor();
    }

    public VRShopArticle GetArticle() {
        return assignedArticle;
    }

    public void HandleCartSelection(GameObject targetObject) {
        if (targetObject == cartIncreaseObjectBack) {
            cartQuanity += 1;
            cartDecreaseObjectBack.SetActive(true);
        } else if (targetObject == cartDecreaseObjectBack) {
            if (cartQuanity > 1) {
                cartQuanity -= 1;
                if (cartQuanity <= 1) {
                    cartDecreaseObjectBack.SetActive(false);
                }
            }
        } else if (targetObject = cartAddToCartObjectBack) {
            shopExplorer.AddToCart(cartQuanity);
        }

        UpdatePrice();
    }
}
