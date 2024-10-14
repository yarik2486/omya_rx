using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval.Client
{
  partial class CounterpartyApprovalRequestActions
  {
    public virtual void CreateChecklist(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var checklist = Functions.CounterpartyApprovalRequest.Remote.CreateChecklist();
      checklist.LeadingDocument = _obj;
      checklist.ShowModal();
    }

    public virtual bool CanCreateChecklist(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}