using System.Collections.Generic;

namespace WaitLess.Core.Application.DTOs.Common
{
    public interface IErrorResponse
    {
        //Errors that we need to show to the user
        List<string> APIErrors { get; set; }
        //Application specific errors
        public List<string> Errors { get; set; }
        int ErrorID { get; set; }
        Exception OriginalException { get; set; }
        bool IsSuccess { get; set; }
        public string CustomErrorMessage { get; set; }
        public int StatusCode { get; set; }

    }
}