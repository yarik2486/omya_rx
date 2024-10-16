using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalAssignment;
using OMYA.CounterpartyApproval;

namespace OMYA.CounterpartySolution.Client
{
  partial class ApprovalAssignmentActions
  {
    public virtual void CancelOMYA(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      e.CloseFormAfterAction = true;
      
      // Добавить результат согласования в заявку на одобрение контрагента.
      var document = CounterpartyApprovalRequests.As(_obj.DocumentGroup.OfficialDocuments.FirstOrDefault());
      var item = document.ApprovalResults.AddNew();
      item.Approver = Sungero.Company.Employees.Current;
      item.Result = OMYA.CounterpartySolution.ApprovalAssignments.Resources.Aborted;
      item.Comment = _obj.ActiveText;
      document.Save();
      
      var cancelTask = CounterpartyApproval.AsyncHandlers.CancelTask.Create();
      cancelTask.TaskId = _obj.Task.Id;
      cancelTask.ExecuteAsync();
    }

    public virtual bool CanCancelOMYA(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      var isCounterpartyApprovalRequest = CounterpartyApprovalRequests.Is(_obj.DocumentGroup.OfficialDocuments.FirstOrDefault());
      return _obj.Status == Status.InProcess && isCounterpartyApprovalRequest;
    }

  }

}