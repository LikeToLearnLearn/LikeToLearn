using UnityEngine;
using System.Collections;

public class Parser : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void parseJson()
    {

        /*  for (int i = 0; i < data.Length; i++) // iterate over the characters
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


                     }

             }

         }*/

    }
}
