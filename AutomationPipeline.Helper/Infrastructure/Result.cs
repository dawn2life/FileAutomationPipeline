namespace AutomationPipeline.Helper.Infrastructure
{
    public class Result<T> : IError
    {
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
    }
}
