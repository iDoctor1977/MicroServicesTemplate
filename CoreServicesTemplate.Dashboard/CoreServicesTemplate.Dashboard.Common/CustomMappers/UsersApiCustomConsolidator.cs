using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Common.CustomMappers
{
    public class UsersApiCustomConsolidator : ACustomMapperBase<UsersApiModel, UsersAppModel>
    {
        private readonly IMapperService<UserApiModel, UserAppModel> _userMapper;

        public UsersApiCustomConsolidator(IMapperWrap mapperWrap, IMapperService<UserApiModel, UserAppModel> userMapper) : base(mapperWrap)
        {
            _userMapper = userMapper;
        }

        public override UsersAppModel Map(UsersApiModel @in)
        {
            var appModel = DataInToDataOut(@in);

            var modelList = new List<UserAppModel>();
            foreach (var modelIn in @in.UsersApiModelList)
            {
                modelList.Add(_userMapper.Map(modelIn));
            }

            appModel.UsersModelList = modelList;

            return appModel;
        }

        public override UsersApiModel Map(UsersAppModel @out)
        {
            var apiModel = DataOutToDataIn(@out);

            var modelList = new List<UserApiModel>();
            foreach (var userModel in @out.UsersModelList)
            {
                modelList.Add(_userMapper.Map(userModel));
            }

            apiModel.UsersApiModelList = modelList;

            return apiModel;
        }

        public override UsersAppModel Map(UsersApiModel @in, UsersAppModel @out)
        {
            var appModel = ToDataOut(@in, @out);

            var modelList = new List<UserAppModel>();
            foreach (var modelIn in @in.UsersApiModelList)
            {
                modelList.Add(_userMapper.Map(modelIn));
            }

            appModel.UsersModelList = modelList;

            return appModel;
        }
    }
}