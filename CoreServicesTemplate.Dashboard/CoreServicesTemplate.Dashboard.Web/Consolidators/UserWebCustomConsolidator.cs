using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Dashboard.Web.Consolidators
{
    public class UserWebCustomConsolidator : ACustomConsolidatorBase<UserViewModel, UserModel>
    {
        //private UserModel _userModel;
        //private UserViewModel _userViewModel;

        public UserWebCustomConsolidator(ICustomMapper customMapper) : base(customMapper)
        {
            //_userModel = new UserModel();
            //_userViewModel = new UserViewModel();
        }

        public override IConsolidatorToResolve<UserViewModel, UserModel> ToData(UserViewModel @in)
        {
            //_userModel = InDataToOutData(@in);
            //_userModel.Birth = DateTime.ParseExact(@in.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            ModelOut = InDataToOutData(@in);
            ModelOut.Birth = DateTime.ParseExact(@in.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            return this;
        }

        public override IConsolidatorToResolveReversing<UserViewModel, UserModel> ToDataReverse(UserModel @out)
        {
            ModelIn = OutDataToInData(@out);
            ModelIn.Birth = @out.Birth.ToStandardString();

            return this;
        }

        //UserModel IConsolidatorToResolve<UserViewModel, UserModel>.Resolve()
        //{
        //    return _userModel;
        //}

        //UserViewModel IConsolidatorToResolveReversing<UserViewModel, UserModel>.Resolve()
        //{
        //    return _userViewModel;
        //}
    }
}