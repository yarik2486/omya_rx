using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyChangeRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyChangeRequestServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      
      if (_obj.CounterpartyDetails == false && _obj.BankDetails == false)
      {
        e.AddError(OMYA.CounterpartyApproval.CounterpartyChangeRequests.Resources.SelectTypeDataChange);
        return;
      }
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.CounterpartyDetails = false;
      _obj.BankDetails = false;
      _obj.Status = Status.Draft;
    }
  }

}