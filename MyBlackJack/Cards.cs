
using System;
using System.Collections.Generic;

namespace MyBlackJack
{
	//список карт
	public class Cards
	{
		public Card this[int index]     
		{
			get { return cardList[index];}
		    			
		}  
  
		public List<Card> cardList;
		
		public Cards() 
		{
			cardList = new List<Card>();
		}

		
		public void Add(Card c)
		{
			cardList.Add(c);
		}
		
	}
}
