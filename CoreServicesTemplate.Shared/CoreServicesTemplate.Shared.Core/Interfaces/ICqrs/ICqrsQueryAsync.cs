using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.ICqrs
{
    public interface ICqrsQueryAsync<in TIn, TOut> where TIn : ICqrsQueryBase
    {
        Task<TOut> ExecuteAsync(TIn model);
    }

    public interface ICqrsQueryBase
    {
        public PagingData PagingData { get; set; }
    }

    public class PagingData
    {
        public int MaxRecords { get; set; }
        public int PageNumber { get; set; }
    }
}