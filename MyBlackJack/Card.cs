using System;
using System.Collections.Generic;

namespace MyBlackJack
{	
	public class Card
	{		
		public static string[] ColourName = {"Крести","Пики","Черви","Буби"};
		public static string[] ValueName = {"Туз","","2","3","4","5","6","7","8","9","10","Валет","Дама","Король"};
		
		public string title;
		public int value;
		public int colour; // масть
		public int points;
			
		public Card(int value, int colour)
		{
			this.value = value;
			this.colour = colour;
			title = ValueName[value] + " " + ColourName[colour];
			if ( value >= 11 ) points = 10;//валет, дама, король
			else points = value;//или значение по номиналу
				
		}
		//для img
		public override string ToString()
		{
			return title;
		}
	}
}
