using System;
using System.Collections.Generic;
using Xunit;

namespace VigenereCipher.Tests
{
    public class VigenereCipherTest
    {
        [Fact]
        public void TestEncrypt()
        {
            var vigenereCipher = new VigenereCipher();
            string[] elements = new string[] { "WORD", "StriNg", "result", "cipher test", "ReSult Test" };
            string key = "key";

            IEnumerable<string> result = vigenereCipher.Encrypt(elements, key);

            Assert.Equal(result, new string[] { "GSPN", "CxpsRe", "biqepr", "mmnrip diqd", "BiQepr Diqd" });
        }

        [Fact]
        public void TestDecrypt()
        {
            var vigenereCipher = new VigenereCipher();
            string[] elements = new string[] {"GSPN", "CxpsRe", "biqepr", "mmnrip diqd", "BiQepr Diqd" };

            string key = "key";

            IEnumerable<string> result = vigenereCipher.Decrypt(elements, key);

            Assert.Equal(result, new string[] { "WORD", "StriNg", "result", "cipher test", "ReSult Test" });
        }

        [Fact]
        public void TestLogicWithWrongKey()
        {
            var vigenereCipher = new VigenereCipher();

            var ex = Assert.Throws<Exception>(() => vigenereCipher.Encrypt(new string[] { "stub" }, "wr0ng"));
            Assert.Equal("Key can only contain letter", ex.Message);
        }
    }
}
