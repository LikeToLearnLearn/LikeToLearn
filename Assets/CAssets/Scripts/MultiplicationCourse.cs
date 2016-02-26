using System.Collections;
using System;

[Serializable]
public class MultiplicationCourse : Course {

	public MultiplicationCourse()
	{
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				AddQuestion(i, ""+i+" * "+j, ""+(i*j));
				if (i != j) {
					AddQuestion(i, ""+j+" * "+i, ""+(j*i));
				}
			}
		}
	}
	// todo
	/*
	public override Question GetQuestion(int alternatives)
	{		
		return base.GetQuestion(alternatives);
	}
	*/
}
