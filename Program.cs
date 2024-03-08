// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

namespace Program
{
    public class Program
    {

        static void Main(string[] args)
        {
            string input = "Write a C# function that takes a string as input and returns a dictionary containing the frequency of each word in the string.";

            var wordFrequencies = CountWordFrequencies(input);

            foreach (var pair in wordFrequencies)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }

        }

        public static Dictionary<string, int> CountWordFrequencies(string input)
        {
            char[] punctuationChars = { ',', '.', '!', '?', ':', ';', '"', '\'', '(', ')' };
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim(punctuationChars);
            }

            Dictionary<string, int> res = new Dictionary<string, int>();

            foreach (var word in words)
            {
                res[word] = res.ContainsKey(word) ? res[word] + 1 : 1;
            }

            return res;
        }

        public static bool isPalindrome(string input)
        {
            // let's remove all non-alphanumeric characters
            input = Regex.Replace(input, "[^a-zA-Z0-9]", "").ToLower();
            int left = 0;
            int right = input.Length - 1;

            while (left < right)
            {
                if (input[left] != input[right])
                {
                    return false;
                }
                left++;
                right--;
            }

            return true;
        }
    }
}