using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ApprovalAssignment;

namespace alialgr.CustomDev.Client
{
  partial class ApprovalAssignmentActions
  {
    public override void Approved(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      base.Approved(e);
    }

    public override bool CanApproved(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return !Functions.ApprovalAssignment.Cansign(_obj);
    }

    public virtual void Signalialgr(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var action = new Sungero.Workflow.Client.ExecuteResultActionArgs(e.FormType, e.Entity, e.Action);
      this.Approved(action);
      e.CloseFormAfterAction = true;
      _obj.Complete(Result.Approved);
    }

    public virtual bool CanSignalialgr(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return Functions.ApprovalAssignment.Cansign(_obj);
    }

  }

}