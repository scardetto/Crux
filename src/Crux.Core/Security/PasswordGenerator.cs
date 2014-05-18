using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Crux.Core.KeyGen
{
    public class PasswordGenerator
    {
        private readonly PasswordGeneratorOptions _options;
        private readonly RangePicker _rangePicker;

        public PasswordGenerator()
            : this(new PasswordGeneratorOptions()) { }

        public PasswordGenerator(PasswordGeneratorOptions options)
        {
            _options = options;
            _rangePicker = new RangePicker();
        }

        public string Generate()
        {
            int length = _rangePicker.GetPasswordLength(_options);

            var password = new char[length];
            IList<int> remainingPositions = GetCharacterPositions(length);

            if (_options.HasMultipleSets) {
                EnsureRequiredSets(remainingPositions, password);
            }

            FillRemainingPositions(remainingPositions, password);

            return new String(password);
        }

        private void FillRemainingPositions(IList<int> remainingPositions, char[] password)
        {
            var allCharacters = PasswordCharacterSet.Join(_options.CharacterSets);

            for (var i = 0; i < remainingPositions.Count;) {
                var position = remainingPositions[i];
                var newCharacter = allCharacters.GetRandomCharacter(_rangePicker);

                // Add the new char to the password if it passes validation,
                // otherwise keep trying.
                if (IsValidNewCharacter(newCharacter, position, password)) {
                    password.SetValue(newCharacter, position);
                    i++;
                }
            }
        }

        private void EnsureRequiredSets(IList<int> remainingPositions, char[] password)
        {
            // If strong passwords are required, generate a character for the required number 
            // of sets and place them at random points within the generated password. This 
            // will ensure that the resulting password will pass validation.
            var availableSets = new List<PasswordCharacterSet>(_options.CharacterSets);

            for (var i = 0; i < _options.RequiredSets; i++) {
                var characterSet = _rangePicker.GetRandomFrom(availableSets);
                var position = _rangePicker.GetRandomFrom(remainingPositions);

                var randomChar = characterSet.GetRandomCharacter(_rangePicker);
                password.SetValue(randomChar, position);

                availableSets.Remove(characterSet);
                remainingPositions.Remove(position);
            }
        }

        private IList<int> GetCharacterPositions(int length)
        {
            return Enumerable.Range(0, length).Select(i => i).ToList();
        }

        private bool IsValidNewCharacter(char newCharacter, int index, char[] password)
        {
            return (_options.AllowConsecutiveCharacters || IsConsecutiveCharacter(newCharacter, index, password))
                && (_options.AllowRepeatingCharacters || IsRepeatingCharacter(newCharacter, password));
        }

        private bool IsConsecutiveCharacter(char newCharacter, int index, char[] password)
        {
            return newCharacter != GetCharAtIndex(password, index - 1)
                && newCharacter != GetCharAtIndex(password, index + 1);
        }

        private char GetCharAtIndex(char[] password, int index)
        {
            return index < password.GetLowerBound(0) || index > password.GetUpperBound(0) 
                ? '\0' 
                : password[index];
        }

        private bool IsRepeatingCharacter(char newCharacter, IEnumerable<char> password)
        {
            return _options.AllowRepeatingCharacters 
                || password.All(t => t != newCharacter);
        }
    }

    public class RangePicker
    {
        private readonly RNGCryptoServiceProvider _randomNumberGenerator;

        public RangePicker()
        {
            _randomNumberGenerator = new RNGCryptoServiceProvider();
        }

        public int GetPasswordLength(PasswordGeneratorOptions options)
        {
            return GetRandomNumberInRange(options.MinimumLength, options.MaximumLength + 1);
        }

        private int GetRandomNumberInRange(int lowerBound, int upperBound)
        {
            // Assumes lowerBoundary >= 0 && lowerBoundary < upperBoundary
            // returns an int >= lowerBoundary and < upperBoundary
            if (lowerBound == upperBound - 1) {
                // test for degenerate case where only lBound can be returned
                return lowerBound;
            }

            uint xcludeRndBase = (UInt32.MaxValue -
                                  (UInt32.MaxValue % (uint)(upperBound - lowerBound)));

            uint randomNumber;
            var buffer = new Byte[4];
            do {
                _randomNumberGenerator.GetBytes(buffer);
                randomNumber = BitConverter.ToUInt32(buffer, 0);
            } while (randomNumber >= xcludeRndBase);

            return (int)(randomNumber % (upperBound - lowerBound)) + lowerBound;
        }

        public T GetRandomFrom<T>(IList<T> list)
        {
            return list[GetRandomNumberInRange(0, list.Count)];
        }
    }
}