using System;
namespace MyBlackJack
{	
	public class GameController
	{
		public Deck d;//список карт
		public Hand h, dealer;
		public bool state;

		public GameController()
		{
			d = new Deck();
			h = new Hand();
			dealer = new Hand();
			state = false;
		}

        //в список карт игрока или дилера добавляется карта и она же уходит из колоды
		public void getCard()
		{
			h.Add(d.Get()); 
		}

		public void getCardDealer()
		{
			dealer.Add(d.Get());
		}

        //Разбор полётов
		public int Finish()
		{
			int sum = h.Check();
			sum += h.countOfAces();
			if (sum + 10 <= 21 && h.countOfAces() != 0) return sum += 10;
			else return sum;
		}

		public int FinishDealer()
		{
			int sum = dealer.Check();
			sum += dealer.countOfAces();
			if (sum + 10 <= 21 && dealer.countOfAces() != 0) return sum += 10;
			else return sum;
		}

		public void Turn()
		{
			getCard();
			if (!checkHand()) state = true;
		}

		public bool checkHand()
		{
			int sum = h.Check();
			if (sum >= 21) return false;
			else
			{
				int aces = h.countOfAces();
				if (aces == 0) return true;
				else
				{
					sum += aces;
					if (sum >= 21 || sum + 10 >= 21) return false;
					else return true;
				}
			}
		}

        //Раздача первых двух карт 
		public void Start()
		{
			Turn();
			Turn();
			getCardDealer();
			getCardDealer();
		}
	}
}