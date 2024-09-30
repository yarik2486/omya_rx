using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.SupAgreement;

namespace alialgr.CustomDev
{
  partial class SupAgreementOurSignatoryPropertyFilteringServerHandler<T>
  {

    public override IQueryable<T> OurSignatoryFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      if(_obj.LeadingDocument == null)
      {
        query = base.OurSignatoryFiltering(query, e);
        return query;
      }
      else
      {
        if (Functions.SupAgreement.SignatorySettingWithAllUsersExistCustom(_obj, _obj.LeadingDocument))
          return query;
        
        var signatories = Functions.SupAgreement.GetSignatoriesIdsCustom(_obj, _obj.LeadingDocument);
        
        return query.Where(s => signatories.Contains(s.Id));
      }
    }
  }

}