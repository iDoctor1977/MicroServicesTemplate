namespace CoreServicesTemplate.Shared.Core.Enums;

public class OperationStatusResult : Enumeration
{
    public static readonly OperationStatusResult Undefined = new OperationStatusResult(-2, "Undefined");
    public static readonly OperationStatusResult Error = new OperationStatusResult(-1, "Error");

    public static readonly OperationStatusResult NoContent = new OperationStatusResult(0, "NoContent");
    public static readonly OperationStatusResult Success = new OperationStatusResult(1, "Success");
    public static readonly OperationStatusResult Created = new OperationStatusResult(2, "Created");
    public static readonly OperationStatusResult Deleted = new OperationStatusResult(3, "Deleted");
    public static readonly OperationStatusResult Updated = new OperationStatusResult(4, "Updated");
    public static readonly OperationStatusResult NotFound = new OperationStatusResult(5, "NotFound");

    private OperationStatusResult(int id, string name) : base(id, name) {}
}