using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour {
    public Vector3 pointB;

    // Use this for initialization
    IEnumerator Start()
    {
        var pointA = transform.position;
        pointB = new Vector3(1.89f, -0.049f, -9.02f);
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
        }
    }

    // Update is called once per frame
    void Update () {
        //Animation.Play();
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
