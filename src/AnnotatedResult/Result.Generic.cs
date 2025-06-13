using System;
using System.Collections.ObjectModel;
using System.Linq;
using AnnotatedResult.Internal;

namespace AnnotatedResult
{
    /// <summary>
    /// Represents the result of an operation with a specific value of type <typeparamref name="T"/>,
    /// additional metadata, and error handling.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value and status.
        /// </summary>
        /// <param name="value">The value of the result.</param>
        /// <param name="status">The status of the result.</param>
        internal protected Result(T value, ResultStatus status)
            : base(status)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value, status, and errors.
        /// </summary>
        /// <param name="value">The value of the result.</param>
        /// <param name="status">The status of the result.</param>
        /// <param name="errors">The errors associated with the result.</param>
        internal protected Result(T value, ResultStatus status, params Error[] errors)
            : base(status, errors)
        {
            Value = value;
        }

        /// <summary>
        /// Implicitly converts a <see cref="Result{T}"/> instance to its value.
        /// </summary>
        /// <param name="result">The <see cref="Result{T}"/> instance to convert.</param>
        public static implicit operator T(Result<T> result) => result.Value;

        /// <summary>
        /// Gets the value of the result.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets the type of the result value.
        /// </summary>
        public Type ValueType => Value.GetType();

        /// <summary>
        /// Executes one of the provided actions depending on whether the result is successful or a failure.
        /// </summary>
        /// <param name="onPass">
        /// The action to execute if the result is successful. Receives the result value as a parameter.
        /// </param>
        /// <param name="onFail">
        /// The action to execute if the result is a failure. Receives a tuple containing the result status and a read-only collection of errors.
        /// </param>
        public void Match(Action<T> onPass, Action<(ResultStatus status, ReadOnlyCollection<Error> errors)> onFail)
        {
            if (IsFailure())
            {
                onFail((Status, Errors));
            }

            onPass(Value);
        }

        /// <summary>
        /// Executes one of the provided functions depending on whether the result is successful or a failure, and returns the function's result.
        /// </summary>
        /// <typeparam name="U">The type of the value to return from the function.</typeparam>
        /// <param name="onPass">
        /// The function to execute if the result is successful. Receives the result value as a parameter.
        /// </param>
        /// <param name="onFail">
        /// The function to execute if the result is a failure. Receives a tuple containing the result status and a read-only collection of errors.
        /// </param>
        /// <returns>
        /// The value returned by either <paramref name="onPass"/> or <paramref name="onFail"/>, depending on the result status.
        /// </returns>
        public U Match<U>(Func<T, U> onPass, Func<(ResultStatus status, ReadOnlyCollection<Error> errors), U> onFail)
        {
            if(IsFailure())
            {
                return onFail((Status, Errors));
            }

            return onPass(Value);
        }

        /// <summary>
        /// Validates the specified value using the provided <see cref="IResultValidator"/>.
        /// If the value passes validation, returns a successful <see cref="Result{T}"/> with the value;
        /// otherwise, returns an invalid <see cref="Result{T}"/> with the validation errors.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validator">The validator to use for validation.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> instance with a successful status if the validation is successful,
        /// or an invalid status with validation errors if the validation fails.
        /// </returns>
        public static Result<T> Validate(T value, IResultValidator validator)
        {
            var isValid = validator.TryValidate(value, out Error[] errors);
            if(isValid)
            {
                return Result<T>.Ok(value);
            }

            return Result<T>.Invalid(errors);
        }

        /// <summary>
        /// Validates the specified value using the default <see cref="InternalValidator"/>.
        /// If the value passes validation, returns a successful <see cref="Result{T}"/> with the value;
        /// otherwise, returns an invalid <see cref="Result{T}"/> with the validation errors.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> instance with a successful status if the validation is successful,
        /// or an invalid status with validation errors if the validation fails.
        /// </returns>
        public static Result<T> Validate(T value)
        {
            return Result<T>.Validate(value, new InternalValidator());
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing an empty result with a status of <see cref="ResultStatus.Error"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="Result{T}"/> instance with a default value and a status of <see cref="ResultStatus.Error"/>.
        /// </returns>
        public static new Result<T> CreateEmpty()
        {
            return new Result<T>(default, ResultStatus.Error);
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing an empty result with a status of <see cref="ResultStatus.Error"/> and an error message.
        /// </summary>
        /// <param name="message">The error message to include in the result.</param>
        /// <returns>
        /// A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Error"/> and an error containing the specified message.
        /// </returns>
        public static new Result<T> CreateEmpty(string message)
        {
            return new Result<T>(default, ResultStatus.Error, new Error(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance by inheriting the status and errors from another result.
        /// </summary>
        /// <param name="result">The result to inherit from.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with the same status and errors.</returns>
        public static new Result<T> Inherit(Result result)
        {
            return new Result<T>(default, result.Status, result.Errors.ToArray());
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing a successful operation with the specified value.
        /// </summary>
        /// <param name="value">The value of the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Ok"/>.</returns>
        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, ResultStatus.Ok);
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing a general error in the operation.
        /// </summary>
        /// <param name="errorCode">The error code representing the specific error.</param>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Error"/>.</returns>
        public static new Result<T> Error(int errorCode, params Error[] errors)
        {
            return new Result<T>(default, (ResultStatus)errorCode, errors);
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing a general error in the operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Error"/>.</returns>
        public static new Result<T> Error(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Error, errors);
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing an invalid operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Invalid"/>.</returns>
        public static new Result<T> Invalid(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Invalid, errors);
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing an unauthorized operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Unauthorized"/>.</returns>
        public static new Result<T> Unauthorized(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Unauthorized, ErrorResult.Unauthorized(errors));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing a forbidden operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Forbidden"/>.</returns>
        public static new Result<T> Forbidden(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Forbidden, ErrorResult.Forbidden(errors));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing a conflict in the operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.Conflict"/>.</returns>
        public static new Result<T> Conflict(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Conflict, ErrorResult.Conflict(errors));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> instance representing that the requested resource was not found.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result{T}"/> instance with a status of <see cref="ResultStatus.NotFound"/>.</returns>
        public static new Result<T> NotFound(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.NotFound, ErrorResult.NotFound(errors));
        }
    }
}