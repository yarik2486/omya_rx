using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace OMYA.CounterpartyApproval.Server
{
  public class ModuleAsyncHandlers
  {

    /// <summary>
    /// Присвоить статус согласования для контрагента относительно документа.
    /// </summary>
    /// <param name="args"></param>
    public virtual void SetApprovalStatusForCompany(OMYA.CounterpartyApproval.Server.AsyncHandlerInvokeArgs.SetApprovalStatusForCompanyInvokeArgs args)
    {
      try
      {
        Logger.DebugFormat("SetApprovalStatus. Start, CompanyId = {0}, DocumentId = {1}", args.CompanyId, args.DocumentId);
        
        var company = OMYA.CounterpartySolution.Companies.GetAll(x => x.Id == args.CompanyId).FirstOrDefault();
        if (company == null)
        {
          throw AppliedCodeException.Create("Don't have company.");
        }
        
        var document = CounterpartyApprovalRequests.GetAll(x => x.Id == args.DocumentId).FirstOrDefault();
        if (document == null)
        {
          throw AppliedCodeException.Create("Don't have document.");
        }
        
        if (Locks.GetLockInfo(company).IsLocked)
        {
          Logger.DebugFormat("Company is locked, id = {0}", company.Id);
          args.Retry = true;
          return;
        }
        
        if (document.Status == OMYA.CounterpartyApproval.CounterpartyApprovalRequest.Status.OnApproval)
        {
          company.ApprovalStatusOMYA = OMYA.CounterpartySolution.Company.ApprovalStatusOMYA.OnApproval;
          company.Save();
        }
        else if (document.Status == OMYA.CounterpartyApproval.CounterpartyApprovalRequest.Status.Approved)
        {
          company.ApprovalStatusOMYA = OMYA.CounterpartySolution.Company.ApprovalStatusOMYA.Approved;
          company.Save();
        }
        
        Logger.DebugFormat("SetApprovalStatus. End, CompanyId = {0}, DocumentId = {1}", args.CompanyId, args.DocumentId);
      }
      catch (Exception exp)
      {
        Logger.Error("AddApprovalResult", exp);
        return;
      }
    }

    /// <summary>
    /// Добавить результат согласования в заявку на одобрение контрагента.
    /// </summary>
    /// <param name="args"></param>
    public virtual void AddApprovalResult(OMYA.CounterpartyApproval.Server.AsyncHandlerInvokeArgs.AddApprovalResultInvokeArgs args)
    {
      try
      {
        Logger.DebugFormat("AddApprovalResult. Start, DocumentId = {0}", args.DocumentId);
        
        var document = CounterpartyApprovalRequests.GetAll(x => x.Id == args.DocumentId).FirstOrDefault();
        if (document == null)
        {
          throw AppliedCodeException.Create("Don't have document.");
        }
        
        var approver = Sungero.Company.Employees.GetAll(x => x.Id == args.ApproverId).FirstOrDefault();
        if (approver == null)
        {
          throw AppliedCodeException.Create("Don't have approver.");
        }
        
        if (Locks.GetLockInfo(document).IsLocked)
        {
          Logger.DebugFormat("Document is locked, id = {0}", document.Id);
          args.Retry = true;
          return;
        }
        
        var item = document.ApprovalResults.AddNew();
        item.Approver = approver;
        item.Result = args.Result;
        item.Comment = args.Comment;
        document.Save();
        
        Logger.DebugFormat("AddApprovalResult. End, DocumentId = {0}", args.DocumentId);
      }
      catch (Exception exp)
      {
        Logger.Error("AddApprovalResult", exp);
        return;
      }
    }

    /// <summary>
    /// Прекратить задачу.
    /// </summary>
    /// <param name="args"></param>
    public virtual void CancelTask(OMYA.CounterpartyApproval.Server.AsyncHandlerInvokeArgs.CancelTaskInvokeArgs args)
    {
      var task = Sungero.Workflow.Tasks.GetAll(x => x.Id == args.TaskId).FirstOrDefault();
      if (task == null)
      {
        Logger.Error("CancelTask. Don't have task.");
        return;
      }
      
      task.Abort();
    }

  }
}