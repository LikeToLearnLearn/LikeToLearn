using UnityEngine;
using System.Collections;

public class MathQGenerator : ScriptableObject {
    
    private enum operators
    {
        addition, subtraction, multiplication, division
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static int[] GenerateIntMultiplicationQ(int operations, int min, int max)
    {
        int i = 0;
        int[] question = new int[1+2*operations];
        question[i] = Random.Range(min, max + 1);
        
        while ( i < question.Length - 1)
        {
            question[++i] = (int) operators.multiplication;
            question[++i] = Random.Range(min, max + 1);
        }

        return question;
    }

    public static int[] Generate4RandomOperationsQ(int min, int max)
    {
        int operations = 4;
        int i = 0;
        int[] question = new int[1 + 2 * operations];
        question[i] = Random.Range(min, max + 1);

        while (i < question.Length - 1)
        {
            question[++i] = Random.Range(0,2); // See enum for operation numbers.
            question[++i] = Random.Range(min, max + 1);
        }

        return question;
    }

}
