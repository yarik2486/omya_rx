using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CreatingCounterpartyForRequest;

namespace OMYA.CounterpartyApproval.Server
{
  partial class CreatingCounterpartyForRequestFunctions
  {

    /// <summary>
    /// Выполнить сценарий.
    /// </summary>
    /// <param name="approvalTask">Задача на согласование по регламенту.</param>
    /// <returns>Результат выполнения сценария.</returns>
    public override Sungero.Docflow.Structures.ApprovalFunctionStageBase.ExecutionResult Execute(Sungero.Docflow.IApprovalTask approvalTask)
    {
      Logger.DebugFormat("CreatingCounterpartyForRequest. Start for task id: {0}, start id: {1}.", approvalTask.Id, approvalTask.StartId);
      
      var result = base.Execute(approvalTask);
      
      var document = approvalTask.DocumentGroup.OfficialDocuments.SingleOrDefault();
      if (document == null)
      {
        Logger.ErrorFormat("CreatingCounterpartyForRequest. Primary document not found. task id: {0}, start id: {1}", approvalTask.Id, approvalTask.StartId);
        return this.GetErrorResult(Sungero.Docflow.Resources.PrimaryDocumentNotFoundError);
      }
      
      var request = CounterpartyApprovalRequests.As(document);
      if (request == null)
      {
        Logger.ErrorFormat("CreatingCounterpartyForRequest. Primary document not request for counterparty approval. task id: {0}, start id: {1}", approvalTask.Id, approvalTask.StartId);
        return this.GetErrorResult(OMYA.CounterpartyApproval.CreatingCounterpartyForRequests.Resources.DocumentNotRequestCounterpartyApproval);
      }
      
      try
      {
        var company = Functions.CounterpartyApprovalRequest.CreateCompany(request);
        company.Save();
        
        approvalTask.OtherGroup.All.Add(company);
        approvalTask.Save();
      }
      catch (Exception ex)
      {
        Logger.ErrorFormat("CreatingCounterpartyForRequest. Document Id {0}", ex, document.Id);
        result = this.GetErrorResult(ex.Message);
      }
      
      return result;
    }
  }
}