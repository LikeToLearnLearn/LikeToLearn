using UnityEngine;
using System.Collections;

public class testConnection : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //string url = String.Format("https://ece01.ericsson.net:4443/ecity?dgw=Ericsson${0}&sensorSpec=Ericsson${1}&t1={2}&t2={3}", bus, sensor, t1, t2);
        //var www = new WWW(url, null, headers);
        // StartCoroutine(WaitForRequest(action, www));

        //string productlist = File.ReadAllText(Application.dataPath + "/Resources/AssetBundles/" + "AssetBundleInfo.json");
        //UploadProductListJSON(productList);

        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

   
    void ConnectToServer()
    {
        Network.Connect("192.168.254.169", 8080);
    }

    public void UploadJSON(/*string data*/)
    {
        //Debug.Log(data);
        WWWForm form = new WWWForm();

        form.AddField("coursecode", "data065");
        form.AddField("momentcode", "kurt");
        form.AddField("question", "1 + 2");
        form.AddField("question", "3");

        WWW www = new WWW("192.168.254.157:8080/greeting", form);
        //WWW www = new WWW("192.168.254.157:8080/greeting?name=" + data);
    }
    
}





