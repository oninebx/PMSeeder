// ${CopyrightHolder}
// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Core/NHIGenerator.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 2/09/2023
using System;
using System.Text;

namespace PMSeeder.Core
{
	public class NHIGenerator : IGenerator<string>
	{
        private readonly Random _random = new Random();
        public NHIGenerator()
		{
		}

        public string Generate()
        {
            //// Define the characters for the first three letters (excluding I and O)
            //const string letters = "ABCDEFGHJKLMNPQRSTUVWXYZ";

            //// Generate the first three letters randomly
            //StringBuilder nhiBuilder = new StringBuilder();
            //for (int i = 0; i < 3; i++)
            //{
            //    int randomIndex = _random.Next(letters.Length);
            //    nhiBuilder.Append(letters[randomIndex]);
            //}

            //// Generate the last three digits randomly
            //int randomNumber = _random.Next(100, 1000); // Between 100 and 999

            //// Calculate the checksum digit
            //int checksum = CalculateChecksum(nhiBuilder.ToString(), randomNumber);

            //return nhiBuilder.ToString() + randomNumber.ToString() + checksum.ToString();
            Random random = new Random();
            StringBuilder nhiBuilder = new StringBuilder();

            // Generate three random letters (A-Z, excluding I and O)
            for (int i = 0; i < 3; i++)
            {
                char randomChar;
                do
                {
                    randomChar = (char)('A' + random.Next(0, 24));
                } while (randomChar == 'I' || randomChar == 'O');

                nhiBuilder.Append(randomChar);
            }

            // Generate four random numbers (0-9)
            for (int i = 0; i < 4; i++)
            {
                nhiBuilder.Append(random.Next(0, 10));
            }

            // Calculate the checksum
            int checksum = CalculateChecksum(nhiBuilder.ToString());

            // Append the checksum as a letter
            nhiBuilder.Append((char)('A' + checksum));

            return nhiBuilder.ToString();

        }

        private int CalculateChecksum(string nhiWithoutChecksum)
        {
            int[] multipliers = { 7, 6, 5, 4, 3, 2 };
            int total = 0;

            for (int i = 0; i < nhiWithoutChecksum.Length; i++)
            {
                char c = nhiWithoutChecksum[i];
                int numericValue;

                // Assign numeric values to alphabetic characters based on your rules
                if (char.IsLetter(c))
                {
                    // Convert A-Z to numeric values (1-24, excluding I and O)
                    numericValue = char.ToUpper(c) - 'A' + 1;
                    if (numericValue >= 9) // Adjust for omitted letters I and O
                    {
                        numericValue -= 2;
                    }
                }
                else if (char.IsDigit(c))
                {
                    // Convert numeric characters to numeric values (0-9)
                    numericValue = c - '0';
                }
                else
                {
                    throw new ArgumentException("NHI must contain only alphabetic and numeric characters.");
                }

                total += numericValue * multipliers[i];
            }

            return (23 - (total % 23)) % 23;
        }

        //private int CalculateChecksum(string letters, int number)
        //{
        //    // Calculate the checksum based on the letters and numbers
        //    string nhi = letters + number.ToString();
        //    int total = nhi.Select((c, index) => (index % 2 == 0 ? 1 : 2) * (c - 'A')).Sum();
        //    return (10 - (total % 10)) % 10;
        //}

    }
}

