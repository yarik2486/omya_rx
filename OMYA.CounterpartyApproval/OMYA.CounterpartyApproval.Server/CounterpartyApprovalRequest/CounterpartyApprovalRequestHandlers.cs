using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyApprovalRequestServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.Nonresident = false;
      _obj.Status = Status.Draft;
    }
  }

}