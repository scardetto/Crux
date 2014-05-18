namespace Crux.Core.KeyGen
{
    public class PasswordGeneratorOptions
    {
        private const int DEFAULT_MINIMUM_LENGTH = 6;
        private const int DEFAULT_MAXIMUM_LENGTH = 10;
        private const int DEFAULT_REQUIRED_SETS = 10;

        private int _minimumLength;
        private int _maximumLength;
        private int _requiredSets;
   
        public bool AllowConsecutiveCharacters { get; set; }
        public bool AllowRepeatingCharacters { get; set; }

        public PasswordCharacterSet[] CharacterSets { get; set; }

        public PasswordGeneratorOptions()
        {
            MinimumLength = DEFAULT_MINIMUM_LENGTH;
            MaximumLength = DEFAULT_MAXIMUM_LENGTH;
            RequiredSets = DEFAULT_REQUIRED_SETS;
            AllowConsecutiveCharacters = false;
            AllowRepeatingCharacters = true;

            CharacterSets = new[] {
                PasswordCharacterSet.UPPERCASE, 
                PasswordCharacterSet.LOWERCASE, 
                PasswordCharacterSet.NUMERIC
            };
        }

        public int MinimumLength
        {
            get { return _minimumLength; }
            set
            {
                _minimumLength = (value > DEFAULT_MINIMUM_LENGTH)
                    ? value
                    : DEFAULT_MINIMUM_LENGTH;
            }
        }

        public int MaximumLength
        {
            get { return _maximumLength; }
            set
            {
                _maximumLength = (value > _minimumLength)
                    ? value
                    : DEFAULT_MAXIMUM_LENGTH;
            }
        }

        public int RequiredSets
        {
            get { return _requiredSets; }
            set
            {
                _requiredSets = (value >= 1)
                    ? value
                    : 1;
            }
        }

        public bool HasMultipleSets 
        { 
            get { return RequiredSets > 1; } 
        }
    }
}