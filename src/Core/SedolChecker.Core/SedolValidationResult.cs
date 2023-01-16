using SedolChecker.Core.Interfaces;

namespace SedolChecker.Core
{
    public class SedolValidationResult : ISedolValidationResult
    {
        private  string _inputString;

        private  bool _isValidSedol;

        private  bool _isUserDefined;

        private  string _validationDetails;

        public SedolValidationResult(string inputString,bool isValidSedol,
            bool isUserDefined, string validationDetails)
        {
            _inputString= inputString;
            _isValidSedol= isValidSedol;
            _isUserDefined= isUserDefined;
            _validationDetails= validationDetails;
        }

        public string InputString => _inputString;

        public bool IsValidSedol => _isValidSedol;

        public bool IsUserDefined => _isUserDefined;

        public string ValidationDetails => _validationDetails;
    }
}
