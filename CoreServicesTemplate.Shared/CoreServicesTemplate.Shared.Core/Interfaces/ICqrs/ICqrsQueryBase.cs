namespace CoreServicesTemplate.Shared.Core.Interfaces.ICqrs
{
    public interface ICqrsQueryBase
    {
        public PagingData PagingData { get; set; }
    }

    public class PagingData
    {
        public int MaxRecords { get; set; } = 100000;
        public int PageNumber { get; set; } = 1000;
    }
}