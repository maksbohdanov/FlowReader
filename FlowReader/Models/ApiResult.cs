namespace FlowReader.Models
{
    public class ApiResult
    {
        public ApiResult()
        {
            
        }
        private ApiResult(bool succeeded, IEnumerable<string> errors, int errorCode = 0)
        {
            Succeeded = succeeded;
            Errors = errors;
            ErrorCode = errorCode;
        }

        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorName
        {
            get
            {
                switch (ErrorCode)
                {
                    case 400:
                        return "Bad Request";
                    case 404:
                        return "Not Found";
                    case 500:
                        return "Internal Server Error";
                    default:
                        return "Unknown Error";
                }
            }
        }

        public static ApiResult Failure(IEnumerable<string> errors, int code)
        {
            return new ApiResult(false, errors, code);
        }
    }
}
