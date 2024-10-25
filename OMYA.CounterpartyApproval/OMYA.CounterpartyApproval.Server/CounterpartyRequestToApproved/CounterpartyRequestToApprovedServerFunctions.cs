using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyRequestToApproved;

namespace OMYA.CounterpartyApproval.Server
{
  partial class CounterpartyRequestToApprovedFunctions
  {

    /// <summary>
    /// Выполнить сценарий.
    /// </summary>
    /// <param name="approvalTask">Задача на согласование по регламенту.</param>
    /// <returns>Результат выполнения сценария.</returns>
    public override Sungero.Docflow.Structures.ApprovalFunctionStageBase.ExecutionResult Execute(Sungero.Docflow.IApprovalTask approvalTask)
    {
      Logger.DebugFormat("CounterpartyRequestToApproved. Start for task id: {0}, start id: {1}.", approvalTask.Id, approvalTask.StartId);
      
      var result = base.Execute(approvalTask);
      
      var document = approvalTask.DocumentGroup.OfficialDocuments.SingleOrDefault();
      if (document == null)
      {
        Logger.ErrorFormat("CounterpartyRequestToApproved. Primary document not found. task id: {0}, start id: {1}", approvalTask.Id, approvalTask.StartId);
        return this.GetErrorResult(Sungero.Docflow.Resources.PrimaryDocumentNotFoundError);
      }
      
      var lockInfo = Locks.GetLockInfo(document);
      if (lockInfo.IsLocked)
      {
        Logger.DebugFormat("CounterpartyRequestToApproved. Document with Id {0} locked {1}.", document.Id, lockInfo.OwnerName);
        return this.GetRetryResult(string.Format(Sungero.Docflow.ApprovalConvertPdfStages.Resources.ConvertPdfLockError, document.Name, document.Id, lockInfo.OwnerName));
      }
        
      try
      {
        if (CounterpartyApprovalRequests.Is(document))
          CounterpartyApprovalRequests.As(document).Status = OMYA.CounterpartyApproval.CounterpartyApprovalRequest.Status.Approved;
        else if (CounterpartyChangeRequests.Is(document))
          CounterpartyChangeRequests.As(document).Status = OMYA.CounterpartyApproval.CounterpartyChangeRequest.Status.Approved;
        
        document.LifeCycleState = Sungero.Docflow.OfficialDocument.LifeCycleState.Active;
        document.Save();
      }
      catch (Exception ex)
      {
        Logger.ErrorFormat("CounterpartyRequestToApproved. Document Id {0}", ex, document.Id);
        result = this.GetErrorResult(ex.Message);
      }
      
      return result;
    }
  }
}