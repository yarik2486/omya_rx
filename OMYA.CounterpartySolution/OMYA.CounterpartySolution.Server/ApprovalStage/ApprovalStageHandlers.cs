using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalStage;

namespace OMYA.CounterpartySolution
{
  partial class ApprovalStageServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.StepForSendingCounterpartyOMYA = false;
    }
  }

}