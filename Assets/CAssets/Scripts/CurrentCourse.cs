using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class CurrentCourse : Course {

    public string coursecode { get; private set; }

    public CurrentCourse(string coursecode)
    {
        this.coursecode = coursecode;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
