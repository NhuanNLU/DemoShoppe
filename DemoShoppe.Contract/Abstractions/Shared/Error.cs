namespace DemoShoppe.Contract.Abstractions.Shared
{
    public class Error
    {
        public string Message { get; }
        public int StatusCode { get; }
        private Error(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
        private Error(string message)
        {
            Message = message;
        }
        public static readonly Error None = new(string.Empty);
        public static readonly Error NullValue = new("Error.NullValue");
        public static Error NotFound(string message) => new Error(message, 404);
        public static Error BadRequest(string message) => new Error(message, 400);
        public static Error Unauthorized(string message) => new Error(message, 401);
        public static Error Forbidden(string message) => new Error(message, 403);
        public static Error InternalServerError(string message) => new Error(message, 500);
    }
}
