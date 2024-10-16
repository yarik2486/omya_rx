using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalSimpleAssignment;

namespace OMYA.CounterpartySolution
{
  partial class ApprovalSimpleAssignmentClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      var stage = Functions.ApprovalSimpleAssignment.Remote.GetStage(_obj, Sungero.Docflow.ApprovalStage.StageType.SimpleAgr);
      
      // Для роли "Специалист по мастер-данным".
      var isMasterDataSpecialist = OMYA.CounterpartyApproval.PublicFunctions.Module.IncludedInMasterDataSpecialist();
      var isStepForSendingCounterparty = stage != null && stage.StepForSendingCounterpartyOMYA == true && isMasterDataSpecialist;
      if (!isStepForSendingCounterparty)
      {
        e.HideAction(_obj.Info.Actions.SendInvitationOMYA);
      }
    }

  }
}