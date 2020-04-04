using System;

namespace SimpulBlog.Core.Exceptions
{
    public class BlogException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public BlogException(ErrorCode errorCode)
            : this(errorCode, string.Empty)
        { }

        public BlogException(ErrorCode errorCode, string message)
            : this(errorCode, message, null)
        { }

        public BlogException(ErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
