﻿using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

public interface IQueryHandler<in TIn, TOut>
{
    Task<OperationResult<TOut>> ExecuteAsync(TIn model);
}

public interface IQueryHandler<TOut>
{
    Task<OperationResult<TOut>> ExecuteAsync();
}