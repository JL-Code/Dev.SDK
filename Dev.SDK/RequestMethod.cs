namespace Dev.SDK
{
    /// <summary>
    /// HTTP请求方式
    /// </summary>
    public enum RequestMethod
    {

        GET = 1,

        /// <summary>
        /// 类似于get请求，只不过返回的响应中没有具体的内容，用于获取报头
        /// </summary>
        HEAD,
        POST,
        PUT,
        DELETE,

        /// <summary>
        /// HTTP/1.1协议中预留给能够将连接改为管道方式的代理服务器。
        /// </summary>
        CONNECT,

        /// <summary>
        /// 允许客户端查看服务器的性能。
        /// </summary>
        OPTIONS,

        /// <summary>
        /// 回显服务器收到的请求，主要用于测试或诊断。
        /// </summary>
        TRACE
    }
}