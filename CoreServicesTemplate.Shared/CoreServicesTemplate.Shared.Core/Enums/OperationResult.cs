using System;

namespace CoreServicesTemplate.Shared.Core.Enums
{
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// Providing the outcome values to config the constructor.
        /// </summary>
        /// <param name="state">The state of operation to return <example>OutcomeState.Success</example>.</param>
        /// <param name="value">The value to return.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public OperationResult(OutcomeState state, T? value = default, string? message = null) : base(state, message)
        {
            Value = value;
            if (value == null && string.IsNullOrWhiteSpace(message) || Equals(state, OutcomeState.Undefined))
            {
                throw new InvalidOperationException(nameof(message));
            }
        }

        /// <summary>
        /// If a value is provided this constructor will assume the outcome is <see cref="OutcomeState.Success"/>.
        /// </summary>
        /// <param name="value"></param>
        public OperationResult(T value) : base(OutcomeState.Success)
        {
            Value = value;
        }

        /// <summary>
        /// For failed operations, providing the error message and eventually the inner object, will suffice.
        /// The state will be automatically set to the <see cref="OutcomeState.Failure"/> state.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <exception cref="InvalidOperationException">Throws an exception if the message is empty.</exception>
        public OperationResult(string message) : base(OutcomeState.Failure, message)
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
        public OperationResult(OutcomeState state, string? message = null)
        {
            State = state;
            Message = message;
        }

        public OutcomeState State { get; }
        public string? Message { get; }
    }
}
