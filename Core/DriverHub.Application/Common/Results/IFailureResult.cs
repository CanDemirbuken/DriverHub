using DriverHub.Application.Common.Errors;

namespace DriverHub.Application.Common.Results;

public interface IFailureResult<TSelf> where TSelf : IFailureResult<TSelf>
{
    static abstract TSelf Failure(IEnumerable<Error> errors);
}