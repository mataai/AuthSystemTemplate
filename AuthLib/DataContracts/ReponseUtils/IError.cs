using System.Net;
using System.Reflection;

namespace AuthLib.DataContracts.ReponseUtils
{
    public interface IError
    {
        public string SystemCode { get; }
        public string ErrorCode { get; }
        public HttpStatusCode HttpErrorCode { get; }
        public string ErrorMessage { get; }
        public string StackTrace { get; }
    }
    
}
