using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour {
    public Vector3 pointB, pointC;

    // Use this for initialization
    IEnumerator Start()
    {
        var pointA = transform.position;
        pointB = new Vector3(pointA.x + SetValue(5), pointA.y, pointA.z - SetValue(5));
        pointC = new Vector3(pointB.x + SetValue(5), pointB.y - SetValue(2), pointB.z - SetValue(5));
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointC, 3.0f));
            //turn fish
            transform.Rotate(Vector3.right * Time.deltaTime);
            yield return StartCoroutine(MoveObject(transform, pointC, pointA, 3.0f));
        }
    }

    // Update is called once per frame
    void Update () {

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

    float SetValue(float i)
    {
        return Mathf.Floor(Random.value * i);
    }
}
