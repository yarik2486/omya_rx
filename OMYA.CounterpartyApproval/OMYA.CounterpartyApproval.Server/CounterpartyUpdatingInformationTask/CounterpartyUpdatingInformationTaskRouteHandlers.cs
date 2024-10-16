using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Workflow;
using OMYA.CounterpartyApproval.CounterpartyUpdatingInformationTask;

namespace OMYA.CounterpartyApproval.Server
{
  partial class CounterpartyUpdatingInformationTaskRouteHandlers
  {

    public virtual void StartBlock3(OMYA.CounterpartyApproval.Server.CounterpartyUpdatingInformationAssignmentArguments e)
    {
      e.Block.RelativeDeadlineDays = 1;
      e.Block.Subject = OMYA.CounterpartyApproval.CounterpartyUpdatingInformationTasks.Resources.CheckCounterpartyDetailsAndVerificationDate;
      e.Block.Performers.Add(_obj.Performer);
    }

  }
}