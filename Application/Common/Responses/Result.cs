namespace Application.Common.Responses
{
    public class Result<T>
    {
        public T Object { get; set; }
        public bool IsSuccess { get; set; }
    }
}
