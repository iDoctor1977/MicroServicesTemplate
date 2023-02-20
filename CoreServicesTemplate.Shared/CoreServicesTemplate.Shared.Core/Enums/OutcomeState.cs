namespace CoreServicesTemplate.Shared.Core.Enums;

public class OutcomeState : Enumeration
{
    public static readonly OutcomeState Failure = new OutcomeState(-4, "Failure");
    public static readonly OutcomeState NotFound = new OutcomeState(-3, "NotFound");
    public static readonly OutcomeState Undefined = new OutcomeState(-2, "Undefined");
    public static readonly OutcomeState Error = new OutcomeState(-1, "Error");

    public static readonly OutcomeState NoContent = new OutcomeState(0, "NoContent");
    public static readonly OutcomeState Success = new OutcomeState(1, "Success");
    public static readonly OutcomeState Deleted = new OutcomeState(3, "Deleted");
    public static readonly OutcomeState Updated = new OutcomeState(4, "Updated");

    private OutcomeState(int id, string name) : base(id, name) {}
}