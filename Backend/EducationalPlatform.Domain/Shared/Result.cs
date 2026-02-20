using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalPlatform.Domain.Shared
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException("Successful results cannot have an error.");

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException("Failed results must contain an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        protected internal Result(T? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

        public static Result<T> Success(T value) => new(value, true, Error.None);
        public static new Result<T> Failure(Error error) => new(default, false, error);
    }
}
