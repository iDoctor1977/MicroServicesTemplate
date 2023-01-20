namespace CoreServicesTemplate.Shared.Core.Enums;

public abstract class OperationStatusValueResult<T> : Enumeration
{
    public static readonly OperationStatusValueResult<T> Undefined = new UndefinedType();
    public static readonly OperationStatusValueResult<T> Error = new ErrorType();

    public static readonly OperationStatusValueResult<T> NoContent = new NoContentType();
    public static readonly OperationStatusValueResult<T> Success = new SuccessType();
    public static readonly OperationStatusValueResult<T> Created = new CreatedType();
    public static readonly OperationStatusValueResult<T> Deleted = new DeletedType();
    public static readonly OperationStatusValueResult<T> Updated = new UpdatedType();
    public static readonly OperationStatusValueResult<T> NotFound = new NotFoundType();

    private OperationStatusValueResult(int id, string statusCode) : base(id, statusCode) { }

    public abstract T ValueResult { get; set; }
    public abstract string Message { get; }

    private class UndefinedType : OperationStatusValueResult<T>
    {
        public UndefinedType() : base(-2, "Undefined") { }

        public override T ValueResult { get; set; }
        public override string Message => "Operation undefined.";
    }
    private class ErrorType : OperationStatusValueResult<T>
    {
        public ErrorType() : base(-1, "Error") { }

        public override T ValueResult { get; set; }
        public override string Message => "Operation error.";
    }

    private class NoContentType : OperationStatusValueResult<T>
    {
        public NoContentType() : base(0, "NoContent") { }

        public override T ValueResult { get; set; }
        public override string Message => "No content value to return.";
    }

    private class SuccessType : OperationStatusValueResult<T>
    {
        public SuccessType() : base(1, "Success") { }

        public override T ValueResult { get; set; }
        public override string Message => "Operation succeeded.";
    }

    private class CreatedType : OperationStatusValueResult<T>
    {
        public CreatedType() : base(2, "Created") { }

        public override T ValueResult { get; set; }
        public override string Message => "Operation succeeded.";
    }

    private class DeletedType : OperationStatusValueResult<T>
    {
        public DeletedType() : base(3, "Deleted") { }

        public override T ValueResult { get; set; }
        public override string Message => "Operation succeeded.";
    }
    private class UpdatedType : OperationStatusValueResult<T>
    {
        public UpdatedType() : base(4, "Updated") { }

        public override T ValueResult { get; set; }
        public override string Message => "Operation succeeded.";
    }
    private class NotFoundType : OperationStatusValueResult<T>
    {
        public NotFoundType() : base(5, "NotFound") { }

        public override T ValueResult { get; set; }
        public override string Message => "Element not found.";
    }
}