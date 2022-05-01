using System;
using AutoMapper;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Shared.Core.Mappers
{
    /// <summary>
    /// Wrapper class for external mapping tool.
    /// </summary>
    public class CustomMapper : ICustomMapper
    {
        private readonly IMapper _mapper;

        public CustomMapper(IServiceProvider service)
        {
            _mapper = service.GetRequiredService<IMapper>();
        }

        public TOut Map<TIn ,TOut>(TIn model)
        {
            return _mapper.Map<TOut>(model);
        }
    }
}