using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testConnection : MonoBehaviour {

 private Dictionary<string, string> headers = new Dictionary<string, string>();

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

    public void UploadJSON()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "data065");

        WWW www = new WWW("192.168.254.169:8080/greeting", form);
        Debug.Log("Nu försöker jag skicka: " + form + " till greeting");
                    
        testJson();
        
    }

    public void testJson()
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("coursecode", "iugu");
        form1.AddField("momentcode", "9845u");
        form1.AddField("question", "1 + 2");
        form1.AddField("answer", "3");

        string url = string.Format("192.168.254.169:8080/school");
        var www = new WWW(url, form1);
        Debug.Log("Nu försöker jag skicka: " + form1 + " till school");
        StartCoroutine(WaitForRequest(www));

    }

    private IEnumerator WaitForRequest(/*Action<EricssonApiParser> fn,*/ WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            if (www.text.Length > 0)
            {
                var parse = new Parser(www.text);
                Debug.Log(www.text + " was received");
                //if (parse.HasResult)
                //fn(parse);
            }
            else
            {
                Debug.Log("no data received");
            }
        }
        else
        {
            Debug.LogError("Failed to fetch data: " + www.error);
        }

    }

   
}





