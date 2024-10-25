using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyChangeRequest;

namespace OMYA.CounterpartyApproval.Client
{
  partial class CounterpartyChangeRequestActions
  {
    

    public virtual void MakeChanges(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      Hyperlinks.Open(Hyperlinks.Get(_obj.Counterparty));
    }

    public virtual bool CanMakeChanges(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.Counterparty != null;
    }

  }

}