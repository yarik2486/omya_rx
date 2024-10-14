using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace OMYA.CounterpartyApproval.Server
{
  public class ModuleAsyncHandlers
  {

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