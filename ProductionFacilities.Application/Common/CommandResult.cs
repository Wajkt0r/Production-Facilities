using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionFacilities.Application.Common
{
    public record CommandResult(bool IsSuccess, string Message, int StatusCode)
    {
        public static CommandResult Success(string message = "Success", int statusCode = 200)
            => new CommandResult(true, message, statusCode);

        public static CommandResult Failure(string message, int statusCode = 400)
            => new CommandResult(false, message, statusCode);
    }
}
