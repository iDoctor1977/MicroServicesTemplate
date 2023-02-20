namespace CoreServicesTemplate.Shared.Core.Enums;

public abstract class OutcomeStateAndMessage : Enumeration
{
    public static readonly OutcomeStateAndMessage Failure = new FailureType();
    public static readonly OutcomeStateAndMessage NotFound = new NotFoundType();
    public static readonly OutcomeStateAndMessage Undefined = new UndefinedType();
    public static readonly OutcomeStateAndMessage Error = new ErrorType();

    public static readonly OutcomeStateAndMessage NoContent = new NoContentType();
    public static readonly OutcomeStateAndMessage Success = new SuccessType();
    public static readonly OutcomeStateAndMessage Created = new CreatedType();
    public static readonly OutcomeStateAndMessage Deleted = new DeletedType();
    public static readonly OutcomeStateAndMessage Updated = new UpdatedType();

    private OutcomeStateAndMessage(int id, string statusCode) : base(id, statusCode) { }

    public abstract int CodeResult { get; }
    public abstract string Message { get; }

    private class FailureType : OutcomeStateAndMessage
    {
        public FailureType() : base(-4, "NotFound") { }

        public override int CodeResult => -4;
        public override string Message => "OperationFailure.";
    }

    private class NotFoundType : OutcomeStateAndMessage
    {
        public NotFoundType() : base(-3, "NotFound") { }

        public override int CodeResult => -3;
        public override string Message => "Element not found.";
    }

    private class UndefinedType : OutcomeStateAndMessage
    {
        public UndefinedType() : base(-2, "Undefined") { }

        public override int CodeResult => -2;
        public override string Message => "Operation undefined.";
    }

    private class ErrorType : OutcomeStateAndMessage
    {
        public ErrorType() : base(-1, "Error") { }

        public override int CodeResult => -1;
        public override string Message => "Operation error.";
    }

    private class NoContentType : OutcomeStateAndMessage
    {
        public NoContentType() : base(0, "NoContent") { }

        public override int CodeResult => 0;
        public override string Message => "No content value to return.";
    }

    private class SuccessType : OutcomeStateAndMessage
    {
        public SuccessType() : base(1, "Success") { }

        public override int CodeResult => 1;
        public override string Message => "Operation succeeded.";
    }

    private class CreatedType : OutcomeStateAndMessage
    {
        public CreatedType() : base(2, "Created") { }

        public override int CodeResult => 2;
        public override string Message => "Operation succeeded.";
    }

    private class DeletedType : OutcomeStateAndMessage
    {
        public DeletedType() : base(3, "Deleted") { }

        public override int CodeResult => 3;
        public override string Message => "Operation succeeded.";
    }
    private class UpdatedType : OutcomeStateAndMessage
    {
        public UpdatedType() : base(4, "Updated") { }

        public override int CodeResult => 4;
        public override string Message => "Operation succeeded.";
    }
}