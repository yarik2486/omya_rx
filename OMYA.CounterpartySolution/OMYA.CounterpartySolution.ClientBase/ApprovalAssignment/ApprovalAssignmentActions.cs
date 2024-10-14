using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalAssignment;

namespace OMYA.CounterpartySolution.Client
{
  partial class ApprovalAssignmentActions
  {
    public virtual void CancelOMYA(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      e.CloseFormAfterAction = true;
      
      var cancelTask = CounterpartyApproval.AsyncHandlers.CancelTask.Create();
      cancelTask.TaskId = _obj.Task.Id;
      cancelTask.ExecuteAsync();
    }

    public virtual bool CanCancelOMYA(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      var isCounterpartyApprovalRequest = CounterpartyApproval.CounterpartyApprovalRequests.Is(_obj.DocumentGroup.OfficialDocuments.FirstOrDefault());
      return _obj.Status == Status.InProcess && isCounterpartyApprovalRequest;
    }

  }

}