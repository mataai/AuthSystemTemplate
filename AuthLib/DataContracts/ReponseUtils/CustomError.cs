using System.Net;
using System.Reflection;

namespace AuthLib.DataContracts.ReponseUtils
{
    public class CustomErrorException : Exception, IError
    {
        public string SystemCode { get; }
        public string ErrorCode { get; }
        public HttpStatusCode HttpErrorCode { get; }
        public string ErrorMessage { get; }
        public override string? StackTrace { get; }

        public CustomErrorException(HttpStatusCode HttpErrorCode, string ErrorCode, string ErrorMessage, string StackTrace)
        {
            SystemCode = Assembly.GetEntryAssembly()?.GetName().Name ?? "CORE";
            this.ErrorCode = ErrorCode;
            this.HttpErrorCode = HttpErrorCode;
            this.ErrorMessage = ErrorMessage;
            this.StackTrace = StackTrace;
        }
        public CustomErrorException(string systemCode, HttpStatusCode errorCode, string ErrorCode, string errorMessage, string stackTrace)
            : base(errorMessage)
        {
            this.ErrorCode = ErrorCode;
            SystemCode = systemCode;
            HttpErrorCode = errorCode;
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
        }
    }
}
