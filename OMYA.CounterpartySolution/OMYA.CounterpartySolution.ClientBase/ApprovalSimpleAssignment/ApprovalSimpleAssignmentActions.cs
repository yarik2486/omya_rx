using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalSimpleAssignment;

namespace OMYA.CounterpartySolution.Client
{
  partial class ApprovalSimpleAssignmentActions
  {
    public virtual void SendInvitationOMYA(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var document = _obj.DocumentGroup.OfficialDocuments.FirstOrDefault();
      if (document == null)
      {
        e.AddError(OMYA.CounterpartySolution.ApprovalSimpleAssignments.Resources.NotContainsDocument);
        return;
      }
      
      var request = CounterpartyApproval.CounterpartyApprovalRequests.As(document);
      if (request == null)
      {
        e.AddError(OMYA.CounterpartySolution.ApprovalSimpleAssignments.Resources.DocumentNotRequest);
        return;
      }
      
      if (request.Counterparty == null)
      {
        e.AddError(OMYA.CounterpartySolution.ApprovalSimpleAssignments.Resources.NoCounterpartyInRequest);
        return;
      }
      
      
    }

    public virtual bool CanSendInvitationOMYA(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}