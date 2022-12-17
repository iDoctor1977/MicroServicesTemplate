﻿using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Presenters
{
    public class UsersApiCustomPresenter : AConsolidatorBase<UsersModel, UsersApiModel>
    {
        private readonly IConsolidators<UserModel, UserApiModel> _userConsolidator;

        public UsersApiCustomPresenter(ICustomMapper customMapper, IConsolidators<UserModel, UserApiModel> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;
        }

        public override UsersApiModel ToData(UsersModel modelIn)
        {
            var viewModel = ToExternalData(modelIn);
            viewModel.UsersApiModelList = _userConsolidator.ToData(modelIn.UsersModelList);

            return viewModel;
        }
    }
}