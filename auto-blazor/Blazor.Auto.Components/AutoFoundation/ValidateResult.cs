namespace Blazor.Auto.Components.AutoFoundation
{
    public struct ValidateResult
    {
        public bool IsSuccess { get;}

        public string Message { get;}

        public ValidateResult(bool isSuccess, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static ValidateResult Success() => new ValidateResult(true);

        public static ValidateResult Fail(string message) => new ValidateResult(false, message);
    }
}
