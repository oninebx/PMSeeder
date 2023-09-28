// ${CopyrightHolder}
// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/NHIGenerator.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 2/09/2023
using System;
using System.Text;
using System.Text.RegularExpressions;
using PMSFake.Core;

namespace PMSFake.Domain.Generators
{
	public class NHIGenerator : IGenerator<string>
	{
        private readonly Random _random = new Random();
        private bool _isNewFormat;
        private const string VALIDLETTERS = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        public NHIGenerator(IGeneratorConfiguration config)
		{
            _isNewFormat = config.IsNewFormat;
		}

        public string Generate()
        {
            // Define the characters for the first three letters (excluding I and O)
            //var isNewFormat = _supportNewFormat ? _random.Next(2) == 0 : false;
            StringBuilder nhiBuilder = new StringBuilder();
            int remain;
            do
            {
                nhiBuilder.Clear();
                // Generate the first three letters randomly
                for (int i = 0; i < 3; i++)
                {
                    int randomIndex = _random.Next(VALIDLETTERS.Length);
                    nhiBuilder.Append(VALIDLETTERS[randomIndex]);
                }

                // Generate the following 2(new format) or 4(old format) digits randomly
                int randomNumber = _isNewFormat ? _random.Next(10, 100) : _random.Next(100, 1000);
                nhiBuilder.Append(randomNumber.ToString());

                if (_isNewFormat)
                {
                    nhiBuilder.Append(VALIDLETTERS[_random.Next(VALIDLETTERS.Length)]);
                }

                // Compute checksum as the last character according to the generated ones.
                var toCheck = nhiBuilder.ToString();
                int sum = 0;
                for (int i = 7; i > 1; i--)
                {
                    var character = toCheck[7 - i];
                    if (IsEnglishLetter(character))
                    {
                        sum += (VALIDLETTERS.IndexOf(character) + 1) * i;
                    }
                    else
                    {
                        sum += (int)char.GetNumericValue(character) * i;
                    }
                }

                remain = _isNewFormat ? sum % 23 : sum % 11;
            } while (remain == 0);

            
            nhiBuilder.Append(_isNewFormat ? VALIDLETTERS[(23 - remain)].ToString() : 11 - remain == 10 ? 0 : 11 - remain);
            return nhiBuilder.ToString();

        }

        private bool IsNHI(string nhi)
        {
            nhi = nhi.ToUpper();    // we're going to accept nhis in lower case, but validate them as upper
            const string validNhiChars = "ABCDEFGHJKLMNPQRSTUVWXYZ";    // excludes I and O

            if (nhi.Length != 7) return false;

            // 1. Position 1,2 and 3 must be within the Alphabet Conversion Table(see above), that is, they are not ‘I’ or ‘O’.
            // suck it regex
            if (!nhi[..3].ToList().All(i => validNhiChars.Contains(i))) return false;

            // 2. Position 4 and 5 must be numeric
            if (!nhi[3..5].ToList().All(n => int.TryParse(n.ToString(), out _))) return false;

            // 3. Position 6 and 7 are either both numeric or both alphabetic
            var rule3 = nhi[5..7];
            var isInt = int.TryParse(rule3, out _);
            var IsChars = rule3.All(x => IsEnglishLetter(x));
            if (!isInt && !IsChars) return false;

            // 4. Assign first letter its corresponding value from the Alphabet Conversion Table and multiply value by 7.
            // 5. Assign second letter its corresponding value from the Alphabet Conversion Table and multiply value by 6.
            // 6. Assign third letter its corresponding value from the Alphabet Conversion Table and multiply value by 5.
            // 7. Multiply first number by 4
            // 8. Multiply second number by 3.
            // 9. Multiply third number by 2
            // If the position 6 is an alpha character assign its corresponding value from the Alphabet Conversion Table and multiply value by 2
            // 10. Total the results of steps 4 to 9.
            var result = 0;
            int counter = 7;
            foreach (var character in nhi[..6])
            {
                if (IsEnglishLetter(character))
                {
                    result += counter * (validNhiChars.IndexOf(character) + 1);
                }
                else
                {
                    result += counter * (int)char.GetNumericValue(character);
                }
                counter--;
            }

            // 11. Apply modulus 11 to create a checksum.NB: Excel has a modulus function MOD(n, d) where n is the number to be
            // converted(eg, the sum calculated in step 9), and d equals the modulus (in the case of the NHI this is 11).
            // [If the position 6 is an alpha character]
            // Divide by 24 and get the remainder(in Java/ C#/javascript/etc , this is the(mod) %operator)
            var newFormat = IsEnglishLetter(nhi[5]);
            int checkSum = result % (newFormat ? 24 : 11);

            // 12. If checksum is ‘0’ then the NHI number is incorrect.
            //[the document doesn't say, but this only applies to the old format]
            if (!newFormat && checkSum == 0) return false;

            // 13. Subtract checksum from 11 to create check digit.
            // [If the position 6 is an alpha character]
            // Subtract checksum from 24 and use the conversion table to create alpha check digit.
            string checkDigit = newFormat ? validNhiChars[(24 - checkSum - 1)].ToString() : (11 - checkSum).ToString();

            // 14. If the check digit equals ‘10’, convert to ‘0’.
            if (checkDigit == "10") checkDigit = "0";

            // 15. Fourth number or last character must equal the check digit.
            if (checkDigit != nhi[6].ToString()) return false;

            return true;
        }
        
        private bool IsEnglishLetter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }

    }
}

