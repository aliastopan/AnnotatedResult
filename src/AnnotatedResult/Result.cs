using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnnotatedResult.Internal;

namespace AnnotatedResult
{
    /// <summary>
    /// Represents the result of an operation with additional metadata and error handling.
    /// </summary>
    public class Result
    {
        private readonly List<Error> _errors;
        private readonly Dictionary<string, object> _metadata;

        internal Result(ResultStatus status)
        {
            Status = status;
            _errors = new List<Error>();
            _metadata = new Dictionary<string, object>();
        }

        internal Result(ResultStatus status, params Error[] errors)
        {
            Status = status;
            _errors = new List<Error>(errors);
            _metadata = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets the status of the result.
        /// </summary>
        /// <seealso cref="ResultStatus"/>
        public ResultStatus Status { get; protected set; }

        /// <summary>
        /// Gets the errors associated with the result.
        /// </summary>
        public ReadOnlyCollection<Error> Errors => _errors.AsReadOnly();

        /// <summary>
        /// Gets the metadata associated with the result.
        /// </summary>
        public IReadOnlyDictionary<string, object> Metadata => _metadata;

        /// <summary>
        /// Gets a value indicating whether the result is a success.
        /// </summary>
        public bool IsSuccess()
        {
            return Status == ResultStatus.Ok;
        }

        /// <summary>
        /// Gets a value indicating whether the result is a failure.
        /// </summary>
        public bool IsFailure()
        {
            return !IsSuccess();
        }

        /// <summary>
        /// Gets a value indicating whether the result has metadata.
        /// </summary>
        public bool HasMetadata()
        {
            return _metadata.Count > 0;
        }

        /// <summary>
        /// Executes the specified actions based on the result status.
        /// </summary>
        /// <param name="onValue">The action to execute if the result is successful.</param>
        /// <param name="onError">The action to execute if the result is faulty, providing status and errors.</param>
        public void Match(Action onValue, Action<(ResultStatus status, ReadOnlyCollection<Error> errors)> onError)
        {
            if(IsFailure())
            {
                onError((Status, Errors));
            }

            onValue();
        }

        /// <summary>
        /// Executes the specified functions based on the result status.
        /// </summary>
        /// <typeparam name="U">The type of the value to return.</typeparam>
        /// <param name="onValue">The function to execute if the result is successful.</param>
        /// <param name="onError">The function to execute if the result is faulty, providing status and errors.</param>
        /// <returns>The result of the executed function.</returns>
        public U Match<U>(Func<U> onValue, Func<(ResultStatus status, ReadOnlyCollection<Error> errors), U> onError)
        {
            if(IsFailure())
            {
                return onError((Status, Errors));
            }

            return onValue();
        }

        /// <summary>
        /// Adds metadata to the result.
        /// </summary>
        /// <param name="metadata">The metadata to add.</param>
        public void AddMetadata(Dictionary<string, object> metadata)
        {
            for(int i = 0; i < metadata.Count; i++)
            {
                var key = metadata.ElementAt(i).Key;
                var value = metadata.ElementAt(i).Value;
                _metadata.Add(key, value);
            }
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance by inheriting the status and errors from another result.
        /// </summary>
        /// <param name="result">The result to inherit from.</param>
        /// <returns>A new <see cref="Result"/> instance.</returns>
        public static Result Inherit(Result result)
        {
            return new Result(result.Status, result.Errors.ToArray());
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a successful operation.
        /// </summary>
        /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.Ok"/>.</returns>
        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a general error in the operation.
        /// </summary>
        /// <param name="errorCode">The error code representing the specific error.</param>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.Error"/>.</returns>
        public static Result Error(int errorCode, params Error[] errors)
        {
            return new Result((ResultStatus)errorCode, errors);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a general error in the operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.Error"/>.</returns>
        public static Result Error(params Error[] errors)
        {
            return new Result(ResultStatus.Error, errors);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing an invalid operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.Invalid"/>.</returns>
        public static Result Invalid(params Error[] errors)
        {
            return new Result(ResultStatus.Invalid, errors);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing an unauthorized operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.Unauthorized"/>.</returns>
        public static Result Unauthorized(params Error[] errors)
        {
            return new Result(ResultStatus.Unauthorized, ErrorResult.Unauthorized(errors));
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a forbidden operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.Forbidden"/>.</returns>
        public static Result Forbidden(params Error[] errors)
        {
            return new Result(ResultStatus.Forbidden, ErrorResult.Forbidden(errors));
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a conflict in the operation.
        /// </summary>
        /// <param name="errors">Additional errors associated with the result.</param>
        /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.Conflict"/>.</returns>
        public static Result Conflict(params Error[] errors)
        {
            return new Result(ResultStatus.Conflict, ErrorResult.Conflict(errors));
        }

    /// <summary>
    /// Creates a new <see cref="Result"/> instance representing that the requested resource was not found.
    /// </summary>
    /// <param name="errors">Additional errors associated with the result.</param>
    /// <returns>A new <see cref="Result"/> instance with a status of <see cref="ResultStatus.NotFound"/>.</returns>
        public static Result NotFound(params Error[] errors)
        {
            return new Result(ResultStatus.NotFound, ErrorResult.NotFound(errors));
        }
    }
}