using System;
using CoreServicesTemplate.Shared.Core.Enums;

namespace CoreServicesTemplate.Shared.Core.Results
{
    public class OperationResult<T> : OperationResult
    {
        public OperationResult()
        {
            
        }

        public OperationResult(OutcomeState state, T? value = default,  string? message = null, OperationResult? innerResult = null) 
            : base(state, innerResult, message)
        {
            Value = value;

            if (value == null && string.IsNullOrWhiteSpace(message) || state == OutcomeState.Undefined)
            {
                throw new InvalidOperationException(nameof(message));
            }
        }

        /// <summary>
        /// If a value is provided this constructor will assume the outcome is <see cref="OutcomeState.Success"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="innerResult"></param>
        public OperationResult(T value, OperationResult? innerResult = null) : base(OutcomeState.Success, innerResult)
        {
            Value = value;
        }

        /// <summary>
        /// For failed operations, providing the error message and eventually the inner object, will suffice.
        /// The state will be automatically set to the <see cref="OutcomeState.Failure"/> state.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerResult">The inner object, if any.</param>
        /// <exception cref="InvalidOperationException">Throws an exception if the message is empty.</exception>
        public OperationResult(string message, OperationResult? innerResult = null) 
            : base(OutcomeState.Failure, innerResult, message)
        {

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException(nameof(message));
            }
        }

        public T? Value { get; }
    }

    public class OperationResult
    {
        public OperationResult()
        {
            
        }

        public OperationResult(OutcomeState state, OperationResult? innerResult = null, string? message = null)
        {
            State = state;
            Message = message;
            InnerResult = innerResult;
        }

        public OutcomeState State { get; }
        public string? Message { get; }
        public OperationResult? InnerResult { get; }
    }
}