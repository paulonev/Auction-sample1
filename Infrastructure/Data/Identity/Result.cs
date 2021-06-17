using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Identity
{
    public class Result : IdentityResult
    {
        private Result(bool succeeded, string error) : base()
        {
            Succeeded = succeeded;
            Error = error;
        }

        public new bool Succeeded { get; }
        public string Error { get; }

        public new static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }
    }
}