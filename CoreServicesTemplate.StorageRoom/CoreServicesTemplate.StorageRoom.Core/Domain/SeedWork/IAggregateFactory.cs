﻿using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;

public interface IAggregateFactory
{
    TOut GenerateAggregate<TIn, TOut>(TIn model) where TIn : IAggModel where TOut : IAggregate;
}