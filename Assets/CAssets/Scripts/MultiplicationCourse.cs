using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class MultiplicationCourse : Course {

	static System.Random rnd = new System.Random();

    
	public MultiplicationCourse()
	{
        int id = 0;
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				AddQuestion(i, ""+id ,""+i+" * "+j, ""+(i*j));
                id++;
                //levelDictionary.Add(i, "" + i);
				/*if (i != j) {
					AddQuestion(i, "" + j + "" + i, "" +j+" * "+i, ""+(j*i));
				}*/
			}
		}

        setCoursecode("StartCourse");
	}

	public override int CurrentLevel()
	{
		int i = base.CurrentLevel();
		return i > 9 ? rnd.Next(0, 9) : i;
	}

    
}
