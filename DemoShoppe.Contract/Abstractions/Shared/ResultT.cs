namespace DemoShoppe.Contract.Abstractions.Shared
{
    public class Result<T> : Result
    {
        private readonly T? _value;

        protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
            => _value = value;

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access the value of a failed result.");
        public static implicit operator Result<T>(T value) => Success(value);
    }
}
