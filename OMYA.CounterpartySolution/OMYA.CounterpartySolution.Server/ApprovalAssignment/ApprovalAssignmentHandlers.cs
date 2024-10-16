using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalAssignment;
using Sungero.Company;
using OMYA.CounterpartyApproval;

namespace OMYA.CounterpartySolution
{
  partial class ApprovalAssignmentServerHandlers
  {

    public override void BeforeComplete(Sungero.Workflow.Server.BeforeCompleteEventArgs e)
    {
      base.BeforeComplete(e);
      
      var document = CounterpartyApprovalRequests.As(_obj.DocumentGroup.OfficialDocuments.FirstOrDefault());
      if (document != null && Employees.Current != null)
      {
        var stringResult = string.Empty;
        if (_obj.Result == Result.Approved)
          stringResult = OMYA.CounterpartySolution.ApprovalAssignments.Resources.ApprovedResult;
        else if (_obj.Result == Result.ForRevision)
          stringResult = OMYA.CounterpartySolution.ApprovalAssignments.Resources.ForRevisionResult;
        else if (_obj.Result == Result.Forward)
          stringResult = OMYA.CounterpartySolution.ApprovalAssignments.Resources.ForwardResult;
        
        // Добавить результат согласования в заявку на одобрение контрагента.
        OMYA.CounterpartyApproval.PublicFunctions.Module.AddApprovalResult(document.Id, Employees.Current.Id, stringResult, _obj.ActiveText);
      }
    }
  }

}