
namespace SedolChecker.Core.Interfaces
{
    public interface ISedolValidator
    {
        ISedolValidationResult ValidateSedol(string input);
    }
}
