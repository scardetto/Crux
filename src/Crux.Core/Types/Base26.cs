using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Crux.Core.Types
{
    //Hexavigesimal number system A = 1 ... Z = 26, AA = 27 ... 
    public class Base26
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string AlphaNumber { get; }
        public int DecimalNumber { get; }

        public Base26(int decimalNumber)
        {
            if (decimalNumber <= 0) throw new ArgumentException("decimalNumber cannot be 0 or negative");

            AlphaNumber = ToAlphaNumber(decimalNumber);
            DecimalNumber = decimalNumber;
        }

        public Base26(string alphaNumber)
        {
            alphaNumber = alphaNumber.ToUpper();

            Validate(alphaNumber);

            DecimalNumber = ToDecimal(alphaNumber);
            AlphaNumber = alphaNumber;
        }

        private static void Validate(string alphaNumber)
        {
            var conditions = new Func<string, bool>[] {
                string.IsNullOrWhiteSpace,
                s => !Regex.IsMatch(s, "^[a-zA-Z]+$")
            };

            if (conditions.Any(c => c(alphaNumber))) {
                throw new ArgumentException($"{alphaNumber} is not a valid alphaNumber");
            }
        }

        private static int ToDecimal(string alphaNumber)
        {
            try {
                return alphaNumber
                    .Reverse()
                    .Select((c, i) => (int)(ToDecimal(c) * Math.Pow(26, i)))
                    .Sum();
            }
            catch (OverflowException) {
                throw new ArgumentException("alphaNumber given is outside of the range of the integer type");
            }
        }

        private static string ToAlphaNumber(int decimalNumber)
        {
            var remainders = new List<int>();
            var remainder = 0;

            for (var counter = 1; remainder != decimalNumber; counter++)
            {
                remainder = decimalNumber % (int)(Math.Pow(26, counter));
                remainders.Add(remainder);
            }

            var characters = remainders
                .Select((r, i) => ToChar((int)Math.Floor(r / (Math.Pow(26, i)))))
                .Reverse();

            return string.Join(string.Empty, characters);
        }

        private static int ToDecimal(char c)
        {
            return Alphabet.IndexOf(c) + 1;
        }

        private static char ToChar(int decimalNumber)
        {
            return Alphabet[decimalNumber - 1];
        }

        #region Overrides and Overloads
        public static explicit operator string(Base26 base26)
        {
            return base26.AlphaNumber;
        }

        public static explicit operator int (Base26 base26)
        {
            return base26.DecimalNumber;
        }

        public static explicit operator Base26(int decimalNumber)
        {
            return new Base26(decimalNumber);
        }

        public static explicit operator Base26(string alphaNumber)
        {
            return new Base26(alphaNumber);
        }

        public static Base26 operator + (Base26 a, Base26 b)
        {
            return new Base26(a.DecimalNumber + b.DecimalNumber);
        }

        public static Base26 operator - (Base26 a, Base26 b)
        {
            return new Base26(a.DecimalNumber - b.DecimalNumber);
        }

        public static Base26 operator * (Base26 a, Base26 b)
        {
            return new Base26(a.DecimalNumber * b.DecimalNumber);
        }

        public static Base26 operator / (Base26 a, Base26 b)
        {
            return new Base26(a.DecimalNumber / b.DecimalNumber);
        }

        protected bool Equals(Base26 other)
        {
            return string.Equals(AlphaNumber, other.AlphaNumber) && DecimalNumber == other.DecimalNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Base26)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((AlphaNumber != null ? AlphaNumber.GetHashCode() : 0) * 397) ^ DecimalNumber;
            }
        }
        #endregion
    }
}
