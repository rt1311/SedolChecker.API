
namespace SedolChecker.Core.Interfaces
{
    public interface ISedolValidationResult
    {
        string InputString { get; }

        bool IsValidSedol { get; }

        bool IsUserDefined { get; }

        string ValidationDetails { get; }
    }
}
