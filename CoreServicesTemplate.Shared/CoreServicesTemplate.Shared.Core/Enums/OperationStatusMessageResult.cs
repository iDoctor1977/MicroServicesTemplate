namespace CoreServicesTemplate.Shared.Core.Enums;

public abstract class OperationStatusMessageResult : Enumeration
{
    public static readonly OperationStatusMessageResult Undefined = new UndefinedType();
    public static readonly OperationStatusMessageResult Error = new ErrorType();

    public static readonly OperationStatusMessageResult NoContent = new NoContentType();
    public static readonly OperationStatusMessageResult Success = new SuccessType();
    public static readonly OperationStatusMessageResult Created = new CreatedType();
    public static readonly OperationStatusMessageResult Deleted = new DeletedType();
    public static readonly OperationStatusMessageResult Updated = new UpdatedType();
    public static readonly OperationStatusMessageResult NotFound = new NotFoundType();

    private OperationStatusMessageResult(int id, string statusCode) : base(id, statusCode) { }

    public abstract int CodeResult { get; }
    public abstract string Message { get; }

    private class UndefinedType : OperationStatusMessageResult
    {
        public UndefinedType() : base(-2, "Undefined") { }

        public override int CodeResult => -2;
        public override string Message => "Operation undefined.";
    }
    private class ErrorType : OperationStatusMessageResult
    {
        public ErrorType() : base(-1, "Error") { }

        public override int CodeResult => -1;
        public override string Message => "Operation error.";
    }

    private class NoContentType : OperationStatusMessageResult
    {
        public NoContentType() : base(0, "NoContent") { }

        public override int CodeResult => 0;
        public override string Message => "No content value to return.";
    }

    private class SuccessType : OperationStatusMessageResult
    {
        public SuccessType() : base(1, "Success") { }

        public override int CodeResult => 1;
        public override string Message => "Operation succeeded.";
    }

    private class CreatedType : OperationStatusMessageResult
    {
        public CreatedType() : base(2, "Created") { }

        public override int CodeResult => 2;
        public override string Message => "Operation succeeded.";
    }

    private class DeletedType : OperationStatusMessageResult
    {
        public DeletedType() : base(3, "Deleted") { }

        public override int CodeResult => 3;
        public override string Message => "Operation succeeded.";
    }
    private class UpdatedType : OperationStatusMessageResult
    {
        public UpdatedType() : base(4, "Updated") { }

        public override int CodeResult => 4;
        public override string Message => "Operation succeeded.";
    }
    private class NotFoundType : OperationStatusMessageResult
    {
        public NotFoundType() : base(5, "NotFound") { }

        public override int CodeResult => 5;
        public override string Message => "Element not found.";
    }
}