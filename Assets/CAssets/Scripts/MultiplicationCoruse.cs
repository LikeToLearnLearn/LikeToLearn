using System.Collections;
using System;

[Serializable]
public class MultiplicationCoruse : Course {

	public MultiplicationCoruse()
	{
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				AddQuestion(i, ""+i+"X"+j, ""+(i*j));
			}
		}
	}

	public override Question GetQuestion(int alternatives)
	{
		Question q = base.GetQuestion(alternatives);
		for (int i = 1; i < alternatives; i++)
			q.AddAlternative("123"); // fixme
		return q;
	}
}
