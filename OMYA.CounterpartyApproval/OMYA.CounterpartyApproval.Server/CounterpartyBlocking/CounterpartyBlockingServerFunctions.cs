using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyBlocking;

namespace OMYA.CounterpartyApproval.Server
{
  partial class CounterpartyBlockingFunctions
  {

    /// <summary>
    /// Выполнить сценарий.
    /// </summary>
    /// <param name="approvalTask">Задача на согласование по регламенту.</param>
    /// <returns>Результат выполнения сценария.</returns>
    public override Sungero.Docflow.Structures.ApprovalFunctionStageBase.ExecutionResult Execute(Sungero.Docflow.IApprovalTask approvalTask)
    {
      Logger.DebugFormat("CounterpartyBlocking. Start for task id: {0}, start id: {1}.", approvalTask.Id, approvalTask.StartId);
      
      var result = base.Execute(approvalTask);
      
      var document = approvalTask.DocumentGroup.OfficialDocuments.SingleOrDefault();
      if (document == null)
      {
        Logger.ErrorFormat("CounterpartyBlocking. Primary document not found. task id: {0}, start id: {1}", approvalTask.Id, approvalTask.StartId);
        return this.GetErrorResult(Sungero.Docflow.Resources.PrimaryDocumentNotFoundError);
      }
      
      var request = CounterpartyBlockingRequests.As(document);
      if (request == null || request.Counterparty == null)
      {
        Logger.ErrorFormat("CounterpartyBlocking. Primary document not request for blocking counterparty or counterparty is null. task id: {0}, start id: {1}", approvalTask.Id, approvalTask.StartId);
        return this.GetErrorResult(OMYA.CounterpartyApproval.CounterpartyBlockings.Resources.NotRequestToBlockCounterparty);
      }
      
      var lockInfo = Locks.GetLockInfo(request);
      if (lockInfo.IsLocked)
      {
        Logger.DebugFormat("CounterpartyBlocking. Document with Id {0} locked {1}.", document.Id, lockInfo.OwnerName);
        return this.GetRetryResult(string.Format(Sungero.Docflow.ApprovalConvertPdfStages.Resources.ConvertPdfLockError, document.Name, document.Id, lockInfo.OwnerName));
      }
      
      lockInfo = Locks.GetLockInfo(request.Counterparty);
      if (lockInfo.IsLocked)
      {
        Logger.DebugFormat("CounterpartyBlocking. Counterparty with Id {0} locked {1}.", request.Counterparty.Id, lockInfo.OwnerName);
        return this.GetRetryResult(CounterpartyBlockings.Resources.CounterpartyHasBeenBlockedFormat(request.Counterparty.Id, lockInfo.OwnerName));
      }
      
      try
      {
        var company = OMYA.CounterpartySolution.Companies.As(request.Counterparty);
        company.ApprovalStatusOMYA = OMYA.CounterpartySolution.Company.ApprovalStatusOMYA.Blocked;
        company.Status = Sungero.Parties.Company.Status.Closed;
        company.Save();
      }
      catch (Exception ex)
      {
        Logger.ErrorFormat("CounterpartyBlocking. Document Id {0}", ex, document.Id);
        result = this.GetErrorResult(ex.Message);
      }
      
      return result;
    }
  }
}