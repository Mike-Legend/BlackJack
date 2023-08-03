using BlackjackClassLibrary;
using PG2Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FullSailCasino
{
    public class BlackjackGame : BlackjackHand
    {
        public int _playerWins;
        public int _dealerWins;
        public BlackjackHand dealer;
        public BlackjackHand player;
        public BlackjackDeck deck;

        public void PlayRound()
        {
            bool isDealer = true;
            BlackjackHand _dealer = new BlackjackHand(isDealer);
            dealer = _dealer;
            BlackjackHand _player = new BlackjackHand();
            player = _player;
            BlackjackDeck _deck = new BlackjackDeck();
            deck = _deck;
            _deck.Shuffle();
            DealInitialCards();
            if (Score == 21 || DScore == 21)
            {
                bool reveal = true;
                DrawTable(reveal);
                DeclareWinner();
                dealer.Clear();
                player.Clear();
            }
            else
            {
                PlayersTurn();
                DealersTurn();
                DeclareWinner();
                dealer.Clear();
                player.Clear();
            }
        }

        public void DrawTable(bool reveal = false)
        {
            Console.Clear();
            DrawWins();
            Console.CursorLeft = 5;
            Console.CursorTop = 3;
            Console.WriteLine("Player Hand");
            player.Write(5, 5, ConsoleColor.White);
            Console.CursorLeft = 5;
            Console.CursorTop = 18;
            Console.WriteLine("Dealer Hand");

            if (reveal == true)
            {
                dealer.Write(5, 20, ConsoleColor.White);
                dealer.Reveal(5, 20, ConsoleColor.White);
            }
            else
            {
                dealer.Write(5, 20, ConsoleColor.White);
            }
        }

        public void DealInitialCards()
        {
            player.AddCard(deck.NextCard());
            DrawTable();
            dealer.AddCard(deck.NextCard());
            DrawTable();
            player.AddCard(deck.NextCard());
            DrawTable();
            dealer.AddCard(deck.NextCard());
            DrawTable();
        }

        public void PlayersTurn()
        {
            while (Score < 21)
            {
                string speech = "";
                int menuSelection = 0;
                while (menuSelection != 2)
                {
                    if (Score < 21)
                    {
                        Console.CursorLeft = 5;
                        Console.CursorTop = 25;
                        Input.GetMenuChoice(speech, new string[] { "1. Hit", "2. Stand" }, out menuSelection);



                        switch (menuSelection)
                        {
                            case 1:
                                player.AddCard(deck.NextCard());
                                DrawTable();
                                break;
                            case 2:
                                DrawTable();
                                break;
                        }
                    }
                    else
                    {
                        menuSelection = 2;
                        break;
                    }
                }
                break;
            }
        }

        public void DealersTurn()
        {
            bool reveal = true;
            while (DScore < 17)
            {
                dealer.AddCard(deck.NextCard());
                DrawTable(reveal);
            }
            DrawTable(reveal);
        }

        public void DeclareWinner()
        {
            if (Score > 21)
            {
                Console.CursorLeft = 5;
                Console.CursorTop = 13;
                Console.WriteLine("Oh No! You bust and lost Blackjack!");
                _dealerWins += 1;
                DrawWins();
                Console.CursorLeft = 5;
                Console.CursorTop = 25;
                Console.WriteLine("\nPress any key to continue:\n");
                Console.ReadKey();
                Console.Clear();              
            }
            else if (DScore > 21)
            {
                Console.CursorLeft = 5;
                Console.CursorTop = 13;
                Console.WriteLine("Dealer bust! Congratulations! You have won Blackjack!");
                _playerWins += 1;
                DrawWins();
                Console.CursorLeft = 5;
                Console.CursorTop = 25;
                Console.WriteLine("\nPress any key to continue:\n");
                Console.ReadKey();
                Console.Clear();               
            }
            else if (Score > DScore)
            {
                Console.CursorLeft = 5;
                Console.CursorTop = 13;
                Console.WriteLine("Congratulations! You have won Blackjack!");
                _playerWins += 1;
                DrawWins();
                Console.CursorLeft = 5;
                Console.CursorTop = 25;
                Console.WriteLine("\nPress any key to continue:\n");
                Console.ReadKey();
                Console.Clear();               
            }
            else if (Score < DScore)
            {
                Console.CursorLeft = 5;
                Console.CursorTop = 13;
                Console.WriteLine("Oh No! You have lost Blackjack!");
                _dealerWins += 1;
                DrawWins();
                Console.CursorLeft = 5;
                Console.CursorTop = 25;
                Console.WriteLine("\nPress any key to continue:\n");
                Console.ReadKey();
                Console.Clear();               
            }
            else if (Score == DScore)
            {
                Console.CursorLeft = 5;
                Console.CursorTop = 13;
                Console.WriteLine("A tie? What are the odds...");
                DrawWins();
                Console.CursorLeft = 5;
                Console.CursorTop = 25;
                Console.WriteLine("\nPress any key to continue:\n");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void DrawWins()
        {
            Console.CursorLeft = 5;
            Console.CursorTop = 8;
            Console.WriteLine("\n\tYou have won " + _playerWins + " game(s) of Blackjack.\n\n\tDealer has won " + _dealerWins + " game(s) of Blackjack.");
        }
    }
}
