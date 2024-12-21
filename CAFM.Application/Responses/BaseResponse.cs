namespace CAFM.Application.Responses
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
