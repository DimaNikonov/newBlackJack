using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBlackJack
{
    class Program
    {
        static int GetMoreCardsC(List<Card> Player, ref int Index, Card[] Cards, string WhoseCards)
        {
            int sumC;
            Console.WriteLine("computer make choice.");
            do
            {
                sumC = 0;
                for (int i = 0; i < Player.Count; i++)
                {
                    sumC += (int)Player[i].Face;
                }
                if (sumC >= 16) break;
                Player.Add(Cards[Index]);
                Index++;
                PrintCards(Player, WhoseCards);
            } while (true);
            return sumC;
        }

        static int GetMoreCardsP(List<Card> Player, ref int Index, Card[] Cards, string WhoseCards)
        {
            int sumP;
            Console.WriteLine("player1 make choice.");
            do
            {
                sumP = 0;
                foreach (Card item in Player)
                {
                    sumP += (int)item.Face;
                }
                Console.WriteLine("get more card? if yes put 'y', if no - 'n' ");
                var choice = char.Parse(Console.ReadLine());
                if (choice == 'n') break;
                Player.Add(Cards[Index]);
                Index++;
                PrintCards(Player, WhoseCards);
            } while (true);
            return sumP;
        }

        static void PrintCards(List<Card> Player, string WhoseCards)
        {
            Console.WriteLine(WhoseCards);
            Print(Player);
        }

        static int WhoFirst()
        {
            Console.WriteLine("pres 0 if first go player1, pres 1 if first go computer ");
            int First = int.Parse(Console.ReadLine());
            return First;
        }

        static void MixCards(Card[] Cards)
        {
            Random rnd = new Random();
            Card temp;
            int rndIndex = 0;
            for (int i = 0; i < Cards.Length; i++)
            {
                rndIndex = rnd.Next(0, Cards.Length);
                temp = Cards[i];
                Cards[i] = Cards[rndIndex];
                Cards[rndIndex] = temp;
            }
        }

        static void FillCards(Card[] Cards)
        {
            const int QuantitySuite = 4;
            const int QuantityFace = 11;
            const int ValueFirstCard = 2;
            const int OutCardValue = 5;
            int index = 0;
            for (int i = 0; i < QuantitySuite; i++)
            {
                for (int j = ValueFirstCard; j <= QuantityFace; j++)
                {
                    if (j == OutCardValue) continue;
                    else
                    {
                        Cards[index] = new Card()
                        {
                            Suite = (Suite)i,
                            Face = (Face)j
                        };
                        index++;
                    }
                }
            }
        }

        static void Print(IEnumerable<Card> Cards)
        {
            foreach (var item in Cards)
            {
                Console.WriteLine("{0,6} {1,7}", item.Face, item.Suite);
            }
        }

        static List<Card> PlayerGetFirstCards(List<Card> Player, Card[] Cards)
        {
            Player = new List<Card>
                        {
                            Cards[0],
                            Cards[1]
                        };
            return Player;
        }

        static List<Card> PlayerGetsecondCards(List<Card> Player, Card[] Cards)
        {
            Player = new List<Card>
                        {
                            Cards[2],
                            Cards[3]
                        };
            return Player;
        }

        static bool EqualAce(List<Card> Player)
        {
            bool equalAce;
            if (Player[0].Face == Face.Ace && Player[1].Face == Face.Ace) equalAce = true;
            else equalAce = false;
            return equalAce;
        }

        static bool NotEqualAce(List<Card> Player)
        {
            bool equalAce;
            if (Player[0].Face != Face.Ace && Player[1].Face != Face.Ace) equalAce = true;
            else equalAce = false;
            return equalAce;
        }

        static int CountWinn(int[] CountWin, int IndexPlayer, string WhoWin)
        {
            Console.WriteLine(WhoWin);
            CountWin[IndexPlayer]++;
            return CountWin[IndexPlayer];
        }

        static int[] Scoring(int[] CountWin, int SumP, int SumC, int[] IndexPlayer, string[] WhoWin)
        {

            if (SumP > 21 && SumC <= 21)
            {
                CountWinn(CountWin, IndexPlayer[1], WhoWin[1]);
            }
            if (SumP <= 21 && SumC > 21)
            {
                CountWinn(CountWin, IndexPlayer[0], WhoWin[0]);
            }
            if (SumC > 21 && SumP > 21 && SumP > SumC)
            {
                CountWinn(CountWin, IndexPlayer[1], WhoWin[1]);
            }
            if (SumC > 21 && SumP > 21 && SumP < SumC)
            {
                CountWinn(CountWin, IndexPlayer[0], WhoWin[0]);
            }
            if (SumC < 21 && SumP < 21 && SumC > SumP)
            {
                CountWinn(CountWin, IndexPlayer[1], WhoWin[1]);
            }
            if (SumC < 21 && SumP < 21 && SumC < SumP)
            {
                CountWinn(CountWin, IndexPlayer[0], WhoWin[0]);
            }
            if (SumP == SumC)
            {
                Console.WriteLine("Draw");
            }
            return CountWin;
        }

        struct Card
        {
            public Face Face;
            public Suite Suite;
        }

        enum Face
        {
            Jack = 2,
            Queen = 3,
            King = 4,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Ace = 11
        }

        enum Suite
        {
            Spade,
            Club,
            Diamond,
            Heart
        }

        static void Main(string[] args)
        {
            const int IndexPlayer1 = 0;
            const int IndexComputer = 1;
            int[] IndexPlayer = { IndexPlayer1, IndexComputer };
            const string ComputerWin = "Computer win";
            const string Player1Win = "Player1 win";
            string[] WhoWin = { Player1Win, ComputerWin };
            const string CardsComputer = "Computer cards:";
            const string CardsPlayer1 = "Player1 cards:";
            int[] countWin = new int[2];
            Card[] Cards = new Card[36];
            FillCards(Cards);
            Print(Cards);
            Console.WriteLine();
            do
            {
                MixCards(Cards);
                Print(Cards);
                Console.WriteLine();
                int goFirst = WhoFirst();
                List<Card> player1 = null;
                List<Card> computer = null;
                int index = 4;
                int sumP = 0;
                int sumC = 0;
                switch (goFirst)
                {
                    case 0:
                        player1 = PlayerGetFirstCards(player1, Cards);
                        computer = PlayerGetsecondCards(computer, Cards);
                        if (EqualAce(player1) && EqualAce(computer))
                        {
                            Console.WriteLine("Draw");
                            continue;
                        }
                        else if (EqualAce(player1) && NotEqualAce(computer))
                        {
                            PrintCards(player1, CardsPlayer1);
                            CountWinn(countWin, IndexPlayer1, Player1Win);
                            continue;
                        }
                        else if (NotEqualAce(player1) && EqualAce(computer))
                        {
                            PrintCards(computer, CardsComputer);
                            CountWinn(countWin, IndexComputer, ComputerWin);
                            continue;
                        }
                        PrintCards(player1, CardsPlayer1);
                        sumP = GetMoreCardsP(player1, ref index, Cards, CardsPlayer1);
                        PrintCards(computer, CardsComputer);
                        sumC = GetMoreCardsC(computer, ref index, Cards, CardsComputer);
                        Scoring(countWin, sumP, sumC, IndexPlayer, WhoWin);
                        break;
                    case 1:
                        computer = PlayerGetFirstCards(computer, Cards);
                        player1 = PlayerGetsecondCards(player1, Cards);
                        if (EqualAce(player1) && EqualAce(computer))
                        {
                            Console.WriteLine("Draw");
                            continue;
                        }
                        else if (EqualAce(player1) && NotEqualAce(computer))
                        {
                            PrintCards(player1, CardsPlayer1);
                            CountWinn(countWin, IndexPlayer1, Player1Win);
                            continue;
                        }
                        else if (NotEqualAce(player1) && EqualAce(computer))
                        {
                            PrintCards(computer, CardsComputer);
                            CountWinn(countWin, IndexComputer, ComputerWin);
                            continue;
                        }
                        PrintCards(computer, CardsComputer);
                        sumC = GetMoreCardsC(computer, ref index, Cards, CardsComputer);
                        PrintCards(player1, CardsPlayer1);
                        sumP = GetMoreCardsP(player1, ref index, Cards, CardsPlayer1);
                        Scoring(countWin, sumP, sumC, IndexPlayer, WhoWin);
                        break;
                }
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("do you won to continue? pres 'y' if yes, 'n' - no ");
                char choice = char.Parse(Console.ReadLine());
                if (choice == 'n') break;
            } while (true);
            Console.WriteLine($"player1 win:{countWin[0]}, computer win:{countWin[1]}");
            Console.ReadLine();
        }
    }
}
