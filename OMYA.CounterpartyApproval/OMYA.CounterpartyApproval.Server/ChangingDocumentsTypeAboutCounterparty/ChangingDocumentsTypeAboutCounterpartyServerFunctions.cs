using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.ChangingDocumentsTypeAboutCounterparty;
using Sungero.Docflow;

namespace OMYA.CounterpartyApproval.Server
{
  partial class ChangingDocumentsTypeAboutCounterpartyFunctions
  {
    /// <summary>
    /// Выполнить сценарий.
    /// </summary>
    /// <param name="approvalTask">Задача на согласование по регламенту.</param>
    /// <returns>Результат выполнения сценария.</returns>
    public override Sungero.Docflow.Structures.ApprovalFunctionStageBase.ExecutionResult Execute(Sungero.Docflow.IApprovalTask approvalTask)
    {
      Logger.DebugFormat("ChangingDocumentsTypeAboutCounterparty. Start for task id: {0}, start id: {1}.", approvalTask.Id, approvalTask.StartId);
      
      var result = base.Execute(approvalTask);
      
      var document = approvalTask.DocumentGroup.OfficialDocuments.SingleOrDefault();
      if (document == null)
      {
        Logger.ErrorFormat("ChangingDocumentsTypeAboutCounterparty. Primary document not found. task id: {0}, start id: {1}", approvalTask.Id, approvalTask.StartId);
        return this.GetErrorResult(Sungero.Docflow.Resources.PrimaryDocumentNotFoundError);
      }
      
      var request = CounterpartyApprovalRequests.As(document);
      if (request == null)
      {
        Logger.ErrorFormat("ChangingDocumentsTypeAboutCounterparty. Primary document not request for counterparty approval. task id: {0}, start id: {1}", approvalTask.Id, approvalTask.StartId);
        return this.GetErrorResult(OMYA.CounterpartyApproval.CreatingCounterpartyForRequests.Resources.DocumentNotRequestCounterpartyApproval);
      }
      
      try
      {
        var relations = request.Relations.GetRelatedDocuments();
        foreach (var relation in relations)
        {
          var lockInfo = Locks.GetLockInfo(relation);
          if (lockInfo.IsLocked)
          {
            Logger.DebugFormat("ChangingDocumentsTypeAboutCounterparty. Document with Id {0} locked {1}.", document.Id, lockInfo.OwnerName);
            return this.GetRetryResult(string.Format(Sungero.Docflow.ApprovalConvertPdfStages.Resources.ConvertPdfLockError, relation.Name, relation.Id, lockInfo.OwnerName));
          }
      
          if (!CounterpartyDocuments.Is(relation))
          {
            var convertedDocument = CounterpartyDocuments.As(relation.ConvertTo(CounterpartyDocuments.Info));
            convertedDocument.Counterparty = request.Counterparty;
            convertedDocument.State.Properties.Counterparty.IsRequired = false;
            convertedDocument.Save();
          }
        }
      }
      catch (Exception ex)
      {
        Logger.ErrorFormat("ChangingDocumentsTypeAboutCounterparty. Document Id {0}", ex, document.Id);
        result = this.GetErrorResult(ex.Message);
      }
      
      return result;
    }
  }
}