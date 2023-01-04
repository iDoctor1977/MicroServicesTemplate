using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.Consolidators
{
    public class UserWebCustomConsolidator : ACustomConsolidatorBase<UserViewModel, UserAppModel>
    {
        private readonly IConsolidator<AddressViewModel, AddressAppModel> _addressConsolidator;

        public UserWebCustomConsolidator(ICustomMapper customMapper, IConsolidator<AddressViewModel, AddressAppModel> addressConsolidator) : base(customMapper)
        {
            _addressConsolidator = addressConsolidator;
        }

        public override IConsolidatorToResolve<UserViewModel, UserAppModel> ToData(UserViewModel @in)
        {
            ModelOut = InDataToOutData(@in);
            ModelOut.Birth = DateTime.ParseExact(@in.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            ModelOut.AddressAppModel = _addressConsolidator.ToData(@in.AddressViewModel).Resolve();

            return this;
        }

        public override IConsolidatorToResolveReversing<UserViewModel, UserAppModel> ToDataReverse(UserAppModel @out)
        {
            ModelIn = OutDataToInData(@out);
            ModelIn.Birth = @out.Birth.ToStandardString();
            ModelIn.AddressViewModel = _addressConsolidator.ToDataReverse(@out.AddressAppModel).Resolve();

            return this;
        }
    }
}