using System;
using System.Collections.Generic;

namespace VigenereCipher
{
    public class VigenereCipher
    {
        public IEnumerable<string> Encrypt(IEnumerable<string> source, string key)
            => UseVigenereCipher(source, key, true);

        public IEnumerable<string> Decrypt(IEnumerable<string> encrypted, string key)
            => UseVigenereCipher(encrypted, key, false);

        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        private IEnumerable<string> UseVigenereCipher(IEnumerable<string> elements, string key, bool encrypt)
        {
            List<string> result = new List<string> { };

            for (int i = 0; i < key.Length; ++i)
                if (!char.IsLetter(key[i]))
                {
                    throw new Exception("Key can only contain letter");
                }

            foreach (var element in elements)
            {
                string outputElement = "";
                int nonAlphabetCharCount = 0;

                for (int n = 0; n < element.Length; n++)
                {
                    if(char.IsLetter(element[n]))
                    {
                        bool cIsUpper = char.IsUpper(element[n]);
                        char offset = cIsUpper ? 'A' : 'a';
                        int keyIndex = (n - nonAlphabetCharCount) % key.Length;
                        int symbolIndex = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;

                        symbolIndex = encrypt ? symbolIndex : -symbolIndex;
                        char symbol = (char)((Mod(((element[n] + symbolIndex) - offset), 26)) + offset);
                        outputElement += symbol;
                    }
                    else
                    {
                        outputElement += element[n];
                        ++nonAlphabetCharCount;
                    }
                }

                result.Add(outputElement);
            }

            return result;
        }
    }
}
