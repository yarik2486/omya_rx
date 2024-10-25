using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalTask;

namespace OMYA.CounterpartySolution
{
  partial class ApprovalTaskServerHandlers
  {

    public override void BeforeStart(Sungero.Workflow.Server.BeforeStartEventArgs e)
    {
      base.BeforeStart(e);
      
      var document = _obj.DocumentGroup.OfficialDocuments.FirstOrDefault();
      if (document == null)
        return;
      
      if (CounterpartyApproval.CounterpartyApprovalRequests.Is(document))
      {
        var request = CounterpartyApproval.CounterpartyApprovalRequests.As(document);
        request.Status = CounterpartyApproval.CounterpartyApprovalRequest.Status.OnApproval;
        request.LifeCycleState = CounterpartyApproval.CounterpartyApprovalRequest.LifeCycleState.Draft;
      }
      else if (CounterpartyApproval.CounterpartyChangeRequests.Is(document))
      {
        var request = CounterpartyApproval.CounterpartyChangeRequests.As(document);
        request.Status = CounterpartyApproval.CounterpartyChangeRequest.Status.OnApproval;
        request.LifeCycleState = CounterpartyApproval.CounterpartyChangeRequest.LifeCycleState.Draft;
      }
    }
  }

}