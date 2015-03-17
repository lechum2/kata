using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem01
{
    public class Calculator
    {
        private readonly string[] defaultSeparators = { ",", "\n" };

        public int Add(string input)
        {
            if (String.IsNullOrEmpty(input)) return 0;

            string[] separators;

            var numbersString 
                = ReadCustomSeparatorAndRemoveItFromInput(input, out separators);

            var numbersList = new List<int>();
            var notNumbers = new List<string>();
            var negativeNumbers = new List<int>();

            var numberStrings = numbersString.Split(separators, StringSplitOptions.None);

            foreach (var numberString in numberStrings)
            {
                ProcessSingleNumber(numbersList, notNumbers, negativeNumbers, numberString);
            }

            HandleInvalidInputs(notNumbers, negativeNumbers);

            return numbersList.Sum();
        }

        private string ReadCustomSeparatorAndRemoveItFromInput(
            string input,
            out string[] separators)
        {
            separators = defaultSeparators;
            var numbersString = input;

            if (input.StartsWith("//"))
            {
                numbersString = ReadCustomSeparator(input.Substring(2), ref separators);
            }

            return numbersString;
        }

        private static void HandleInvalidInputs(List<string> notNumbers, List<int> negativeNumbers)
        {
            if (negativeNumbers.Any())
            {
                throw new ArgumentException(
                    String.Format(
                        "Negatives not allowed: {0}.",
                        String.Join(",", negativeNumbers)));
            }

            if (notNumbers.Any())
            {
                throw new ArgumentException(
                    String.Format(
                        "Some input values are not numbers: {0}.",
                        String.Join(",", notNumbers)));
            }
        }

        private static string ReadCustomSeparator(string input, ref string[] separators)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("No separator provided.");

            var separatorsList = new List<string>();

            var openingBracket = input.IndexOf("[");

            int separatorStartIndex = 0;
            int separatorLength = 1;

            if (openingBracket != -1)
            {
                var truncatedInput = input;
                while (openingBracket != -1)
                {
                    var closingBracket = truncatedInput.IndexOf("]");
                    separatorStartIndex = openingBracket + 1;
                    separatorLength = closingBracket - separatorStartIndex;
                    var separator = truncatedInput.Substring(separatorStartIndex, separatorLength);
                    separatorsList.Add(separator);
                    truncatedInput = truncatedInput.Substring(closingBracket + 1);
                    openingBracket = truncatedInput.IndexOf("[");
                }
            }
            else
            {
                var separator = input.Substring(separatorStartIndex, separatorLength);
                separatorsList.Add(separator);
            }

            separators = separatorsList.ToArray();
            var numbersSectionStartIndex = input.IndexOf("\n") + 1;
            return input.Substring(numbersSectionStartIndex);
        }

        private static void ProcessSingleNumber(
            List<int> numbersList,
            List<string> notNumbers,
            List<int> negativeNumbers,
            string numberString)
        {
            int number;
            if (!Int32.TryParse(numberString, out number))
            {
                var notNumber = numberString;
                if (String.IsNullOrWhiteSpace(numberString))
                {
                    notNumber = "empty";
                }

                notNumbers.Add(notNumber);
            }
            else
            {
                if (number < 0)
                {
                    negativeNumbers.Add(number);
                }
                else if (number <= 1000)
                {
                    numbersList.Add(number);
                }
            }
        }
    }
}
