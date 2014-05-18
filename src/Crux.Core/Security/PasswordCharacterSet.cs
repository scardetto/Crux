using System;
using System.Linq;
using Crux.Core.Extensions;

namespace Crux.Core.KeyGen
{
    public class PasswordCharacterSet
    {
        public static readonly PasswordCharacterSet UPPERCASE = 
            new PasswordCharacterSet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        public static readonly PasswordCharacterSet LOWERCASE = 
            new PasswordCharacterSet("abcdefghijklmnopqrstuvwxyz");

        public static readonly PasswordCharacterSet NUMERIC = 
            new PasswordCharacterSet("0123456789");

        public static readonly PasswordCharacterSet SYMBOLS =
            new PasswordCharacterSet("`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?");

        public static readonly PasswordCharacterSet XML_SAFE_SYMBOLS =
            new PasswordCharacterSet("`~!@#$%^*()-_=+[]{}\\|;:,./?");

        private readonly char[] _characters;

        public PasswordCharacterSet(string characters)
            : this(characters.ToCharArray()) { }

        public PasswordCharacterSet(char[] characters)
        {
            _characters = characters;
        }

        public int Length
        {
            get { return _characters.Length; }
        }

        public char GetRandomCharacter(RangePicker rangePicker)
        {
            return rangePicker.GetRandomFrom(_characters);
        }

        public static PasswordCharacterSet Join(params PasswordCharacterSet[] characterSets)
        {
            // If only one passed in, then nothing to do.
            if (characterSets.Length == 1) {
                return characterSets[0];
            }

            // Allocate a buffer large enough to 
            // fit all of the joined character sets
            var totalLength = characterSets.Sum(characterSet => characterSet.Length);
            var newSet = new char[totalLength];

            // Block copy all characters in to the final set
            var offset = 0;

            characterSets.Each(cs => {
                Buffer.BlockCopy(cs._characters, 0, newSet, offset, cs.Length);
                offset += cs.Length;
            });

            return new PasswordCharacterSet(newSet);
        }
    }
}