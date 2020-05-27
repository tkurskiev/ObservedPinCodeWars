using System;
using System.Collections.Generic;
using System.Linq;

namespace ObservedPinCodeWars
{
    class Program
    {
        public static string ObservedPin = "1357";
        
        //public static char[,] KeypadLayout = new char[,]
        //{
        //    {'1', '2', '3'},
        //    {'4', '5', '6'},
        //    {'7', '8', '9'},
        //    {'2'}
        //};

        public static readonly List<List<char>> KeypadLayout = new List<List<char>>
        {
            new List<char>{ '1', '2', '3' },
            new List<char>{ '4', '5', '6' },
            new List<char>{ '7', '8', '9' },
            new List<char>{ 'a', '0', 'a' },
        };

        static void Main(string[] args)
        {
            var result = GetPINs("11");

            foreach (var pinCode in result)
                Console.WriteLine(pinCode);
        }

        public static List<string> GetPINs(string observed)
        {
            // TODO: переименовать в possibleDigitChars
            var possibleDigitChars = new List<char>();

            foreach (var digitChar in observed)
            {
                possibleDigitChars.Add(digitChar);

                var currentRaw = KeypadLayout.FirstOrDefault(x => x.Contains(digitChar));

                // Индекс текущей цифры в ее ряду
                var digitCharIndex = currentRaw.IndexOf(digitChar);

                // Индекс того ряда, в котором находится текущая цифра, в клавиатуре
                var currentRawIndex = KeypadLayout.IndexOf(currentRaw);

                // Если имеется ряд выше...
                if (currentRawIndex > 0)
                {
                    possibleDigitChars.Add(KeypadLayout[currentRawIndex - 1][digitCharIndex]);
                }

                // Не в последнем ли мы ряду...
                if (currentRawIndex < KeypadLayout.Count - 1)
                {
                    var sameIndexChar = KeypadLayout[currentRawIndex + 1][digitCharIndex];

                    if(int.TryParse($"{sameIndexChar}", out var digit))
                        possibleDigitChars.Add(sameIndexChar);
                }

                // Если имеется предыдущая цифра в ряду с текущей цифрой...
                if (digitCharIndex > 0)
                {
                    var previousIndexChar = currentRaw[digitCharIndex - 1];

                    if (int.TryParse($"{previousIndexChar}", out var digit))
                        possibleDigitChars.Add(previousIndexChar);
                }

                // Если в ряду имеется цифра, следующая за текущей (если мы не на последней позиции)
                if (digitCharIndex < currentRaw.Count - 1)
                {
                    var nextIndexChar = currentRaw[digitCharIndex + 1];

                    if (int.TryParse($"{nextIndexChar}", out var digit))
                        possibleDigitChars.Add(nextIndexChar);
                }
            }

            possibleDigitChars = possibleDigitChars.Distinct().ToList();

            // TODO: Generate combinations...

            // Заглушка...
            return new List<string>();
        }
    }
}
