using Microsoft.Extensions.Options;
using SedolChecker.Core.Interfaces;
using SedolChecker.Core.Models;
using System.Text.RegularExpressions;

namespace SedolChecker.Core
{
    public class SedolValidator : ISedolValidator
    {
        private readonly WeighingFactorConfig _config;
        private const int _alphaPosition = 9;
        public SedolValidator(IOptions<WeighingFactorConfig> config)
        {
            _config = config.Value;
        }
        public ISedolValidationResult ValidateSedol(string input)
        {
            string validationDetails = string.Empty;
            if (IsInputLenghtCorrect(input))
            {
                if (IsAlphaNumeric(input))
                {
                    var isInputUserDefined = IsInputUserDefined(input);
                    var isInputValidSedole = IsInputValidSedol(input);

                    if (!isInputValidSedole)
                    {
                        validationDetails = "Checksum digit does not agree with the rest of the input";
                    }
                    return new SedolValidationResult(input, isInputValidSedole, isInputUserDefined,
                        validationDetails);
                }
                else
                    validationDetails = "SEDOL contains invalid characters";
            }
            else
                validationDetails = "Input string was not 7-characters long";
            return new SedolValidationResult(input, false, false, validationDetails);
        }

        private bool IsAlphaNumeric(string input)
        {
            string strRegex = @"^[a-zA-Z0-9 ]*$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(input);
        }

        private bool IsInputLenghtCorrect(string input)
        {            
            return input.ToCharArray().Length ==7 ;
        }
        private bool IsInputUserDefined(string input)
        {
            return input != null ? input.StartsWith("9") : false;
        }

        private bool IsInputValidSedol(string input)
        {
            var inputCharacters = input.ToCharArray();
            int checkSum = 0;
            for (int i = 0; i < inputCharacters.Length - 1; i++)
            {
                if (char.IsLetter(inputCharacters[i]))
                {
                    var inputNumericPosition = GetAlphaPosition(inputCharacters[i]) + _alphaPosition;
                    checkSum = checkSum + (_config.WeighingFactorPairs[Convert.ToString(i + 1)] * inputNumericPosition);
                }
                else
                    checkSum = checkSum + (_config.WeighingFactorPairs[Convert.ToString(i + 1)] * GetCheckDigit(inputCharacters[i]));
            }
            int checkDigit = GetCheckDigit(inputCharacters[inputCharacters.Length-1]);

            int checkNumber = (10 - (checkSum % 10)) % 10;

            return checkDigit == checkNumber;
        }

        private int GetCheckDigit(char inputCharacter)
        {
            return Convert.ToInt32(inputCharacter.ToString());
        }

        private int GetAlphaPosition(char inputCharacter)
        {
            return inputCharacter - 'A' + 1;
        }
    }
}
