namespace Post.Dtos
{
    public class ResultViewModel
    {
        public dynamic Data { get; set; }
        public dynamic Error { get; set; }
        public int HttpCode { get; set; }
        public string Message { get; set; }
    }
}
