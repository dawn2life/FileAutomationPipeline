namespace AutomationPipeline.Helper.Infrastructure
{
    public class Result<T> : IError
    {
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        private Result(T? data, string? errorMessage, bool isSuccess)
        {
            Data = data;
            ErrorMessage = errorMessage;
            IsSuccess = isSuccess;
        }

        public static Result<T> Ok<T>(T data)
        {
            return new Result<T>(data, null, true);
        }

        public static Result<T> Fail<T>(string errorMessage)
        {
            return new Result<T>(default, errorMessage, false);
        }
    }
}
