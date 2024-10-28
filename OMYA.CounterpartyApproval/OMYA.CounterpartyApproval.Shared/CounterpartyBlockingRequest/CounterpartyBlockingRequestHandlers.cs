using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyBlockingRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyBlockingRequestSharedHandlers
  {

    public override void InternalApprovalStateChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      base.InternalApprovalStateChanged(e);
      
      Functions.CounterpartyBlockingRequest.UpdateStatus(_obj, _obj.LifeCycleState, e.NewValue);
    }

    public override void LifeCycleStateChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      base.LifeCycleStateChanged(e);
      
      Functions.CounterpartyBlockingRequest.UpdateStatus(_obj, e.NewValue, _obj.InternalApprovalState);
    }

  }
}