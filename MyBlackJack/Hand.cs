using System;
using System.Collections.Generic;

namespace MyBlackJack
{

	public class Hand : Cards
	{
		
		public int Count
		{
			get{ return cardList.Count;
			}
		}
		
        //Определение значения туза
		public int countOfAces()
		{
			int c = 0;
			for (int i = 0; i < cardList.Count; i++)
				if (cardList[i].value == 0) c += 1;
			return c;
			
		}
		
        //Суммируем очки, перебирая каждую карту из списка
		public int Check()
		{
			int Sum=0;
			foreach( Card c in cardList ) 
			{
				Sum+=c.points;
			}
			return Sum;
		}
		
		public Hand() : base()
		{
		}
	}
}
