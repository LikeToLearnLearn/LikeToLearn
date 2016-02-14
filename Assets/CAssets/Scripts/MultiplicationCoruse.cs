using System.Collections;
using System;

[Serializable]
public class MultiplicationCoruse : Course {

	public MultiplicationCoruse()
	{
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				AddQuestion(i, ""+i+" * "+j, ""+(i*j));
				if (i != j)
					AddQuestion(i, ""+j+" * "+i, ""+(i*j));
				
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
