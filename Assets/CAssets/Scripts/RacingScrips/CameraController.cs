using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour
{

    public GameObject player;
    //public GameObject camera;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}