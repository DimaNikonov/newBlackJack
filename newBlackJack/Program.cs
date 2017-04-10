using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newBlackJack
{
    

    class Program
    {
        static int getMoreCardsC(List<card> player,ref int index, card[] cards)
        {
            int sumC ;
            Console.WriteLine("computer make choice.");
            do
            {
                sumC = 0;
                for (int i=0;i<player.Count;i++)
                {
                    sumC += (int)player[i].znachenie;
                }
                if (sumC >= 16) break;
                player.Add(cards[index]);
                index++;
                printCardsC(player);
            } while (true);
            return sumC;
        }
        static int getMoreCardsP(List<card> player, ref int index,card[] cards)
        {
            int sumP;
            Console.WriteLine("player1 make choice.");
            do
            {
                sumP = 0;
                foreach (card item in player)
                {
                    sumP += (int)item.znachenie;
                }

                Console.WriteLine("get more card? if yes put 'y' if no - 'n' ");
                var choice = char.Parse(Console.ReadLine());
                if (choice == 'n') break;
                player.Add(cards[index]);
                index++;
                printCardsP(player);
            } while (true);
            return sumP;
        }
        static void printCardsC(List<card> player)
        {
            Console.WriteLine("cards computer:");
            foreach (card item in player)
            {
                Console.WriteLine("{0,6} {1,7}", item.znachenie, item.mast);
            }
        }
        static void printCardsP(List<card> player)
        {
            Console.WriteLine("cards player1:");
            foreach (card item in player)
            {
                Console.WriteLine("{0,6} {1,7}", item.znachenie, item.mast);
            }
        }
        static int ktoPerviy()
        {
            int perviy = 0;
            Console.WriteLine("put 0 if first go player, put 1 if first go computer ");
            perviy = int.Parse(Console.ReadLine());
            return perviy;
        }
        static void mixCards(card[] cards)
        {
            Random rnd = new Random();
            card temp;
            int tempIndex = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                tempIndex = rnd.Next(0, cards.Length);
                temp = cards[i];
                cards[i] = cards[tempIndex];
                cards[tempIndex] = temp;
            }
        }
        static void fillCards(card[] cards)
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j <= 11; j++)
                {
                    if (j == 5) continue;
                    else
                    {
                        cards[index] = new card() { mast = (mast)i, znachenie = (znachenie)j };
                        index++;
                    }

                }
            }
        }
        static void print(card[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                Console.WriteLine("{0,6} {1,7}", cards[i].znachenie, cards[i].mast);
            }
        }
        struct card
        {
            public znachenie znachenie;
            public mast mast;
        }
        enum znachenie
        {
            valet=2,
            dama=3,
            korol=4,
            six =6,
            seven = 7,
            eight=8,
            nine= 9,
            ten =10,
            tuz= 11
        }
        enum mast
        {
            pika,
            trefa,
            bubna,
            cherva
        }

        static void Main(string[] args)
        {
            int countWinP = 0, countWinC = 0;
            card[] cards = new card[36];
            fillCards(cards);
            print(cards);
            Console.WriteLine();
            do
            {


                mixCards(cards);
                print(cards);
                Console.WriteLine();
                int goFirst = ktoPerviy();

                List<card> player1; List<card> computer;
                int index = 4, sumP = 0, sumC = 0;
                switch (goFirst)
                {
                    case 0:

                        player1 = new List<card> { cards[0], cards[1] };
                        computer = new List<card> { cards[2], cards[3] };

                        if (player1[0].znachenie == znachenie.tuz && player1[1].znachenie == znachenie.tuz && computer[0].znachenie == znachenie.tuz && computer[1].znachenie == znachenie.tuz)
                        { Console.WriteLine("nichya"); continue; }
                        else if (player1[0].znachenie == znachenie.tuz && player1[1].znachenie == znachenie.tuz)
                        {
                            printCardsP(player1);
                            Console.WriteLine("Player1 win");
                            countWinP++;continue;
                        }
                        else if (computer[0].znachenie == znachenie.tuz && computer[1].znachenie == znachenie.tuz)
                        {
                            printCardsC(computer);
                            Console.WriteLine("computer win");
                            countWinC++;continue;
                        }
                        printCardsP(player1);
                        sumP = getMoreCardsP(player1, ref index, cards);

                        printCardsC(computer);
                        sumC = getMoreCardsC(computer, ref index, cards);

                        if (sumP > 21 && sumC <= 21) { Console.WriteLine("computer win"); countWinC++; }
                        if (sumP <= 21 && sumC > 21) { Console.WriteLine("player1 win"); countWinP++; }
                        if (sumC > 21 && sumP > 21 && sumP > sumC) { Console.WriteLine("computer win"); countWinC++; }
                        if (sumC > 21 && sumP > 21 && sumP < sumC) { Console.WriteLine("player1 win"); countWinP++; }
                        if (sumC < 21 && sumP < 21 && sumC > sumP) { Console.WriteLine("computer win"); countWinC++; }
                        if (sumC < 21 && sumP < 21 && sumC < sumP) { Console.WriteLine("player1 win"); countWinP++; }
                        if (sumP == sumC) { Console.WriteLine("nichya"); }
                        break;

                    case 1:
                        computer = new List<card> { cards[0], cards[1] };
                        player1 = new List<card> { cards[2], cards[3] };

                        if (player1[0].znachenie == znachenie.tuz && player1[1].znachenie == znachenie.tuz && computer[0].znachenie == znachenie.tuz && computer[1].znachenie == znachenie.tuz)
                        { Console.WriteLine("nichya"); continue; }
                        else if (player1[0].znachenie == znachenie.tuz && player1[1].znachenie == znachenie.tuz && computer[0].znachenie != znachenie.tuz && computer[1].znachenie != znachenie.tuz)
                        {
                            printCardsP(player1);
                            Console.WriteLine("Player1 win");
                            countWinP++;continue;
                        }
                        else if (player1[0].znachenie != znachenie.tuz && player1[1].znachenie != znachenie.tuz && computer[0].znachenie == znachenie.tuz && computer[1].znachenie == znachenie.tuz)
                        {
                            printCardsC(computer);
                            Console.WriteLine("computer win");
                            countWinC++;continue;
                        }

                        printCardsC(computer);
                        sumC = getMoreCardsC(computer,ref index, cards);

                        printCardsP(player1);
                        sumP = getMoreCardsP(player1, ref index, cards);

                        if (sumP > 21 && sumC <= 21) { Console.WriteLine("computer win"); countWinC++; }
                        if (sumP <= 21 && sumC > 21) { Console.WriteLine("player1 win"); countWinP++; }
                        if (sumC > 21 && sumP > 21 && sumP > sumC) { Console.WriteLine("computer win"); countWinC++; }
                        if (sumC > 21 && sumP > 21 && sumP < sumC) { Console.WriteLine("player1 win"); countWinP++; }
                        if (sumC < 21 && sumP < 21 && sumC > sumP) { Console.WriteLine("computer win"); countWinC++; }
                        if (sumC < 21 && sumP < 21 && sumC < sumP) { Console.WriteLine("player1 win"); countWinP++; }
                        if (sumP == sumC) { Console.WriteLine("nichya"); }
                        break;
                }
                Console.ReadLine();


                Console.Clear();
                Console.WriteLine("do you won to continue? put 'y' if yes, 'n' - no ");
                char choice = char.Parse(Console.ReadLine());
                if (choice == 'n') break;
            } while (true);
            Console.WriteLine($"plaer1 win:{countWinP}, computer win:{countWinC}");
            Console.ReadLine();
        }
    }
}
