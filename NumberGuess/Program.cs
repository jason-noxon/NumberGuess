using System;
using System.Collections.Generic;

namespace NumberGuess
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			Game game = new Game();

		}
	}

	class Game
	{
		int numberToGuess = 0;
		bool correct = false;
		string name = string.Empty;
		int allowedGuesses = 5;
		int numberOfGuessesLeft = 0;

		public Game()
		{
			this.Setup();
		}

		private void Setup()
		{
			this.correct = false;

			if (string.IsNullOrEmpty(name))
			{
				Console.WriteLine("Hello! What is your name, human?");
				this.name = UppercaseFirst(Console.ReadLine());
			}

			numberOfGuessesLeft = allowedGuesses;

			Console.WriteLine(string.Format("Hello, {0}!", name));
			this.numberToGuess = new Random().Next(1, 10);
			Console.WriteLine("I have just thought of a number between 1 and 10");
			Console.WriteLine("Try to guess it in {0} guesses", allowedGuesses);

			this.Start();
		}

		private void Start()
		{
			while (this.numberOfGuessesLeft != 0)
			{
				int guess = 0;

				if (int.TryParse(Console.ReadLine(), out guess))
				{

					if (IsBetweenOneAndTen(guess))
					{
						if (guess == numberToGuess)
						{
							correct = true;
							break;
						}
						else
						{
							numberOfGuessesLeft--;

							if (numberOfGuessesLeft > 0)
							{
								string text = numberOfGuessesLeft == 1 ? "guess" : "guesses";
								Console.WriteLine("Sorry. That wasn't it. You have {0} {1} left", numberOfGuessesLeft, text);

								if (numberOfGuessesLeft <= 3)
								{
									GenerateHint();
								}
							}
						}
					}
					else
					{
						Console.WriteLine("The number has to be between 1 and 10");
					}

				}
				else
				{
					Console.WriteLine("{0}. I said a NUMBER between 1 and 10. A NUMBER!", name);
				}

			}

			End();
		}

		public void End()
		{
			if (correct)
			{
				Console.WriteLine("Amazing, {0}! You guessed it!", name);
			}
			else
			{
				Console.WriteLine("Sorry, {0}! You ran out of turns.", name);
				Console.WriteLine("The number was {0}", numberToGuess);
			}

			Console.Write("Would you like to play again? (y/n)");
			string result = Console.ReadLine();

			if (result.ToLower() == "y")
			{
				Console.Clear();
				this.Setup();
			}
			else
			{
				Console.Write("Thanks for playing with me!");
			}

		}

		private static string UppercaseFirst(string s)
		{
			// Check for empty string.
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			// Return char and concat substring.
			return char.ToUpper(s[0]) + s.Substring(1);
		}

		private static bool IsBetweenOneAndTen(int value)
		{
			if (value >= 1 && value <= 10)
			{
				return true;
			}

			else
			{
				return false;
			}
		}

		private void GenerateHint()
		{

			int twoGreater =  numberToGuess + 2 <= 10 ? numberToGuess + 2 : 10;
			int twoLessThan = numberToGuess - 2 >= 0 ? numberToGuess - 2 : 0;

			string[] hints = {
				"It's not one of the numbers you already picked! :)",
				string.Format("The number is greater than {0}", twoLessThan),
				string.Format("The number is less than {0}", twoGreater)
			};

			int index = new Random().Next(0, hints.Length);

			Console.WriteLine(string.Format("HINT: {0}", hints[index])); 
		}
	}
}