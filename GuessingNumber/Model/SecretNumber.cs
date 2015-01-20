using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace GuessingNumber.Model
{
    public enum Outcome
    {
        Indefinite,
        Low,
        High,
        Correct,
        NoMoreGuesses,
        PreviousGuess
    }

    public class SecretNumber
    {
        //Fält
        private int? _number;
        private List<int> _previousGuesses;

        //Konstant
        public const int MaxNumberOfGuesses = 7;

        //Egenskaper
        public bool CanMakeGuess { get; set; }
        public int Count { get { return _previousGuesses.Count; } }
        public int? Number { get; set; }
        public Outcome Outcome { get; set; }
        public IEnumerable<int> PreviousGuesses { get { return new ReadOnlyCollection<int>(_previousGuesses); } }

        //Konstruktor
        public SecretNumber()
        {
            _previousGuesses = new List<int>(MaxNumberOfGuesses);
        }

        //Metoder
        public void Initialize()
        {
            //Tilldelar _number ett slumptal mellan 1 och 100
            Random random = new Random();
            {
                this._number = random.Next(1, 101);
            }

            //Tar bort eventuella element i _previousGuesses
            if (_previousGuesses.Any())
            {
                _previousGuesses.Clear();
            }

            //Tilldelar Outcome värdet Indefinite
            Outcome = Outcome.Indefinite;

            CanMakeGuess = true;
        }

        public Outcome MakeGuess(int guess)
        {
            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException("Ange ett tal mellan 1 och 100.");
            }
            else
            {
                if (_previousGuesses.Count == (MaxNumberOfGuesses - 1))
                {
                    if (guess == _number)
                    {
                        Outcome = Outcome.Correct;
                    }
                    else
                    {
                        Outcome = Outcome.NoMoreGuesses;
                        Number = _number;
                    }

                    CanMakeGuess = false;
                }
                else
                {
                    if (_previousGuesses.Contains(guess))
                    {
                        Outcome = Outcome.PreviousGuess;
                    }
                    else
                    {
                        if (guess == _number)
                        {
                            Outcome = Outcome.Correct;
                            CanMakeGuess = false;
                        }
                        if (guess > _number)
                        {
                            Outcome = Outcome.High;
                        }
                        if (guess < _number)
                        {
                            Outcome = Outcome.Low;
                        }
                    }
                }

                //Lägger till gissningen i _previousGuesses-listan
                _previousGuesses.Add(guess);

                return Outcome;
            }
        }
    }
}