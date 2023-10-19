namespace AuthLib.DataContracts.ReponseUtils
{
    public class AuthResponse
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public AuthResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }

    public class AuthResponse<T> : AuthResponse
    {
        public T Data { get; set; }
        public AuthResponse(T data) : base(false, "")
        {
            Data = data;
        }
        public AuthResponse(bool isSuccess, string message, T data) : base(isSuccess, message) {
            Data = data;
        }


    }
}
