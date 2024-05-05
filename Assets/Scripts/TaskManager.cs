using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class TaskManager : MonoBehaviour
{

    [SerializeField] private TextAsset JSONFile;
    [SerializeField] private JSONNode JSONParse;

    [SerializeField] private GameObject Task1UIRender;

    [SerializeField] public Task task = new Task();

    public static TaskManager _instance;

    public static TaskManager Instance
    {
        get { return _instance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(JSONFile.text);
        //GetTaskInformation(JSONFile.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class Task
    {
        public string taskName;
    }

    [System.Serializable]
    public class Order
    {
        public string description;
        public string orderID;
    }

    [System.Serializable]
    public class OrderObject
    {
        public string orderID;
        public string objectName;
        public string ean;
        public Vector3 position;
        public string positionString;
        public int stock;
    }

    

    private string requestText = "";

    public Vector3 Vector3FromString(string Vector3string)
    {
        //get first number (x)
        int startChar = 1;
        int endChar = Vector3string.IndexOf(",");
        int  lastEnd = endChar;
        decimal x = System.Convert.ToDecimal(Vector3string.Substring(startChar, endChar - 1));
        //get second number (y)
        startChar = lastEnd + 1;
        endChar = Vector3string.IndexOf(",", lastEnd);
        lastEnd = endChar;
        decimal y = System.Convert.ToDecimal(Vector3string.Substring(startChar, endChar));
        //get third number (z)
        startChar = lastEnd + 1;
        endChar = Vector3string.IndexOf(",", lastEnd);
        lastEnd = endChar;
        decimal z = System.Convert.ToDecimal(Vector3string.Substring(startChar, endChar));
        //build a new vector3 object using the values we've parsed
        Vector3 returnvector3 = new Vector3((float)x, (float)y, (float)z);
        //pass back a vector3 type
        return returnvector3;

    }
    IEnumerator GetRequest(string uri, string task, string argument)
    {
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                requestText = webRequest.downloadHandler.text;
            }
        }

        List<Order> orderList = new List<Order>();
        List<OrderObject> orderObjectsList = new List<OrderObject>();
        OrderObject selectedObject = new OrderObject();

        switch (task){
            case "task 1":
                orderObjectsList.Clear();
                JSONNode objectsParse = JSONNode.Parse(requestText);
                foreach (JSONNode node in objectsParse)
                {

                    OrderObject tmp = new OrderObject();
                    tmp.orderID = argument;
                    tmp.objectName = node["objectName"];
                    tmp.stock = node["stock"];
                    tmp.ean = node["ean"];
                    string positionstring = node["position"].ToString();
                    tmp.positionString = positionstring;
                    tmp.position = Vector3FromString(positionstring);
                    orderObjectsList.Add(tmp);

                }
                Task1UIRender.GetComponent<TaskUIRender>().RenderUI(orderObjectsList,orderList,selectedObject, "task 1");
                break;
            case "task 2":
                JSONNode objectStockParse = JSONNode.Parse(requestText);
                selectedObject.stock = objectStockParse["stock"];
                selectedObject.orderID = objectStockParse["text"];
                Task1UIRender.GetComponent<TaskUIRender>().RenderUI(orderObjectsList, orderList, selectedObject, "task 2");
                break;
            case "task 3":
                JSONNode objectParse = JSONNode.Parse(requestText);
                string locationstring = objectParse["location"].ToString();
                selectedObject.orderID = objectParse["text"];
                selectedObject.position = Vector3FromString(locationstring);
                Task1UIRender.GetComponent<TaskUIRender>().RenderUI(orderObjectsList, orderList, selectedObject, "task 3");
                break;
            case "task 4":
                orderList.Clear();
                JSONNode ordersParse = JSONNode.Parse(requestText);
                foreach (JSONNode node in ordersParse)
                {
                    Order tmp = new Order();
                    tmp.description = node["description"];
                    tmp.orderID = node["orderID"];
                    orderList.Add(tmp);
                }
                Task1UIRender.GetComponent<TaskUIRender>().RenderUI(orderObjectsList, orderList,selectedObject, "task 4");
                break;
            default: break;

        }
        
    }

    public void GetTaskInformation(string info)
    {
        JSONParse = JSONNode.Parse(info);
        task.taskName = JSONParse["taskName"];

        switch (task.taskName)
        {
            case "task 1":

                StartCoroutine(GetRequest("https://smart-shopper-api.azurewebsites.net/api/getObjectsOrder?" + "orderID=" + JSONParse["taskArgument"], "task 1", JSONParse["taskArgument"]));

                break;
            case "task 2":
                StartCoroutine(GetRequest("https://smart-shopper-api.azurewebsites.net/api/getStockObject?" + "objectName=" + JSONParse["taskArgument"], "task 2", JSONParse["taskArgument"]));
                break;
            case "task 3":
                StartCoroutine(GetRequest("https://smart-shopper-api.azurewebsites.net/api/getLocation?" + "objectName=" + JSONParse["taskArgument"], "task 3", JSONParse["taskArgument"]));
                break;
            case "task 4":
                        
                StartCoroutine(GetRequest("https://smart-shopper-api.azurewebsites.net/api/getOrders?", "task 4", JSONParse["taskArgument"]));

                break;
            default: break;
        }
    }
}
