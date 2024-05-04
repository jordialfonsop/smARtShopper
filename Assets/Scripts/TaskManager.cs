using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class TaskManager : MonoBehaviour
{

    [SerializeField] private TextAsset JSONFile;
    [SerializeField] private JSONNode JSONParse;

    [SerializeField] public Task task = new Task();

    public static TaskManager _instance;

    public static TaskManager Instance
    {
        get { return _instance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetTaskInformation();
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

    [SerializeField] public List<Order> orderList = new List<Order>();

    private string ordersText = "";

    IEnumerator GetRequest(string uri)
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
                ordersText = webRequest.downloadHandler.text;
            }
        }

        JSONNode ordersParse = JSONNode.Parse(ordersText);
        foreach (JSONNode node in ordersParse)
        {
            Order tmp = new Order();
            tmp.description = node["description"];
            tmp.orderID = node["orderID"];
            orderList.Add(tmp);
        }
    }

    public void GetTaskInformation()
    {
        JSONParse = JSONNode.Parse(JSONFile.text);
        Debug.Log(JSONFile.text);
        task.taskName = JSONParse["taskName"];

        switch (task.taskName)
        {
            case "Task 1":
                
                /*foreach(JSONNode taskResult in JSONParse["taskResult"])
                {
                    task.taskResults.Add(new TaskResult1(taskResult["objectName"], new Vector3(0, 0, 0)));
                }*/
                break;
            case "Task 4":
                orderList.Clear();
                    
                StartCoroutine(GetRequest("https://smart-shopper-api.azurewebsites.net/api/getOrders?"));

                

                break;
            default: break;
        }
    }
}
