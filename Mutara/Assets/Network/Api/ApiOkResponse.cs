using System;

namespace Mutara.Network.Api
{
    [Serializable]
    public class ApiOkResponse<T> : ApiResponse
    {
        public T Content { get; set; } 

        public ApiOkResponse() : base(200)
        {
        }

        public ApiOkResponse(T content) : base(200)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return $"[ApiOkResponse: Status:{StatusCode} Message:{Message} Content: {(Content == null ? "Null" : Content.ToString())}]";
        }
    }
}