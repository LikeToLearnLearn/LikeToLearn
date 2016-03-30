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
        form.AddField("name", "data065");
        string data = "from Unity";
       //WWW www = new WWW("192.168.254.169:8080/greeting?name=" + data);

        WWW www = new WWW("192.168.254.169:8080/greeting", form);
        Debug.Log("Nu försöker jag skicka: " + form + " till greeting");

        WWWForm form1 = new WWWForm();
        WWW www1 = new WWW("192.168.254.169:8080/school", form1);
        Debug.Log("Nu försöker jag skicka: " + form1 + " till school");

        form1.AddField("coursecode", "data065");
        form1.AddField("momentcode", "kurt");
        form1.AddField("question", "1 + 2");
        form1.AddField("question", "3");
        

        
        
        
        //Debug.Log("Nu försöker jag skicka: " + data);

        
    }

    /*public void parseJson()
    {

        StartCoroutine(www);

        for (int i = 0; i < data.Length; i++) // iterate over the characters
        {
            switch (st)
            {
                case State.open:
                    if (data[i] == '[') // look for opening JSON array
                        st = State.array; // switch to array state
                    break;
                case State.array:
                    if (data[i] == '{') // look for opening JSON object
                        st = State.obj; // switch to object state
                    save = false; // new object; reset save state
                    break;
                case State.obj:
                    if (data[i] == '}') // looking for end of JSON object
                    {
                        if (save) // the object read should replace the return values
                        {
                            Timestamp = timestampCand;
                            Value = valueCand;
                            HasResult = true; // we now have a result
                        }


                    }*/
    
}





