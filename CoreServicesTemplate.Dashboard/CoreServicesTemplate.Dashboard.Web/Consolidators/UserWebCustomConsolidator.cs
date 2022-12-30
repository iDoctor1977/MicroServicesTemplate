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
        public UserWebCustomConsolidator(ICustomMapper customMapper) : base(customMapper) { }

        public override IConsolidatorToResolve<UserViewModel, UserModel> ToData(UserViewModel @in)
        {
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
    }
}