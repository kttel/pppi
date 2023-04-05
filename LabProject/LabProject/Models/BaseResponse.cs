namespace LabProject
{
    public class BaseResponse<T>
    {
        public string Description { get; set; } = string.Empty;
        public int StatusCode { get; set; } = 0;
        public List<T> Values { get; set; } = new List<T>();
    }
}
