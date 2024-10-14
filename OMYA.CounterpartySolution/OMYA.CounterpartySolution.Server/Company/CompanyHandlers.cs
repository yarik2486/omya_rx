using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.Company;

namespace OMYA.CounterpartySolution
{
  partial class CompanyServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.LastUpdatedDateOMYA = Calendar.Today;
    }
  }

}