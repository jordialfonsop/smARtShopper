using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskUIRender : MonoBehaviour
{
    [SerializeField] private GameObject orderObjectElementPrefab;
    [SerializeField] private GameObject orderElementPrefab;
    [SerializeField] private GameObject HMD;

    [SerializeField] private Pathfinder pathfinder;

    [SerializeField] private UseTTSManager useTTSManager;
    private Transform originalTransform;

    // Start is called before the first frame update
    void Start()
    {
        originalTransform = transform;
    }

    public void RenderUI(List<TaskManager.OrderObject> orderObjectsList, List<TaskManager.Order> orderList, TaskManager.OrderObject selectedObject, string task)
    {
        transform.parent = null;
        transform.position = originalTransform.position;
        transform.rotation = originalTransform.rotation;

        for (int i = transform.childCount - 1; i > 1; i--)
        {
            Destroy(transform.GetChild(i).gameObject);

        }

        switch (task)
        {
            case "task 1":
                transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = orderObjectsList[0].orderID;
                transform.GetChild(0).gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 180, 0, 0);
                foreach (TaskManager.OrderObject orderObject in orderObjectsList)
                {
                    GameObject objectUI1 = Instantiate(orderObjectElementPrefab);
                    objectUI1.transform.parent = transform;
                    objectUI1.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    objectUI1.GetComponent<RectTransform>().localRotation = new Quaternion(0,0,0,0);

                    objectUI1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Object Name: " + orderObject.objectName;
                    objectUI1.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "EAN: " + orderObject.ean;
                    objectUI1.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = "Position: " + orderObject.positionString;
                    objectUI1.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().text = "Stock: " + orderObject.stock.ToString();

                }
                break;
            case "task 2":

                transform.GetChild(0).gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 180, 0, 0);

                useTTSManager.StartSynthesis(selectedObject.orderID);

                break;
            case "task 3":
                
                transform.GetChild(0).gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 180, 0, 0);

                pathfinder.target = selectedObject.position;
                useTTSManager.StartSynthesis(selectedObject.orderID);
                
                break;
            case "task 4":
                transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Order List";
                transform.GetChild(0).gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 180, 0, 0);
                foreach (TaskManager.Order order in orderList)
                {
                    GameObject objectUI4 = Instantiate(orderElementPrefab);
                    objectUI4.transform.parent = transform;
                    objectUI4.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    objectUI4.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);

                    objectUI4.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "OrderID: " + order.orderID;
                    objectUI4.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Description: " + order.description;

                }
                break;
        }
        

        
        transform.parent = HMD.transform;
        transform.localPosition = new Vector3((float)-0.1929898, (float)-0.072, (float)0.967);
        transform.localRotation = HMD.transform.localRotation;
        transform.gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 180, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
