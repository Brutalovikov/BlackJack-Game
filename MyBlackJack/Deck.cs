

using System;

namespace MyBlackJack
{	
	public class Deck : Cards
	{		
		public int Count
		{
			get { return cardList.Count; }
		}
        
        public Card Get()//когда вызывается метод, выбрасивается случайное число от (1 до 52)
		{
            Random R = new Random();
			int NumOfCard = R.Next(Count-1); //выбирается карта
			Card c = cardList[NumOfCard];
			cardList.Remove(c);//карта уходит из колоды
			return c;	
		}
			
		public Deck()//составление карт
		{

			for(int i=0; i < 4; i++)
			{
					Add(new Card(0,i)); 
					for(int j=2; j < 14; j++)
					Add(new Card(j,i));
				
			}
			
		}
	}
}
