using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyBlockingRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyBlockingRequestServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.Status = Status.Draft;
    }
  }

}