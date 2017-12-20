namespace Dev.SDK
{
    public class ResponseData<TResult> : ResponseData
    {
        public new TResult Content { get; set; }
    }
}