namespace CoreServicesTemplate.Shared.Core.Interfaces.ICqrs
{
    public interface ICqrsQueryBase
    {
        public PagingData PagingData { get; set; }
    }

    public class PagingData
    {
        public int MaxRecords { get; set; } = 10000;
        public int PageNumber { get; set; } = 10;
    }
}