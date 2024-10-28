using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyBlockingRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyBlockingRequestClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      var prop = _obj.State.Properties;
      prop.Counterparty.IsRequired = true;
      prop.ReasonForBlocking.IsRequired = true;
    }

  }
}