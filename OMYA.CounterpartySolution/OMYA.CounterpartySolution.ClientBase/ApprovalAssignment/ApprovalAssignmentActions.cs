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
      
    }

    public virtual bool CanCancelOMYA(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}