namespace Web.Lib
{
    public class ReturnObject
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public dynamic Object { get; set; }
    }
}