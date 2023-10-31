using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IStringValidationOption
    {
        bool Validate(string input);
    }

    public class LengthValidation : IStringValidationOption
    {
        private int _minLength;
        private int _maxLength;

        public LengthValidation(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public bool Validate(string input)
        {
            return input.Length >= _minLength && input.Length <= _maxLength;
        }
    }

    public class HasDigitValidation : IStringValidationOption
    {
        public bool Validate(string input)
        {
            return input.Any(char.IsDigit);
        }
    }

    public class HasLowercaseValidation : IStringValidationOption
    {
        public bool Validate(string input)
        {
            return input.Any(char.IsLower);
        }
    }

    public class HasUppercaseValidation : IStringValidationOption
    {
        public bool Validate(string input)
        {
            return input.Any(char.IsUpper);
        }
    }

    public class HasSpecialCharacterValidation : IStringValidationOption
    {
        public bool Validate(string input)
        {
            return input.Any(c => !char.IsLetterOrDigit(c));
        }
    }

    public class StringValidationBuilder
    {
        private List<IStringValidationOption> _options = new List<IStringValidationOption>();
        public StringValidationBuilder AddValidationOption(IStringValidationOption option)
        {
            _options.Add(option);
            return this;
        }
        public bool Validate(string input)
        {
            return _options.All(o => o.Validate(input));
        }
    }
}
