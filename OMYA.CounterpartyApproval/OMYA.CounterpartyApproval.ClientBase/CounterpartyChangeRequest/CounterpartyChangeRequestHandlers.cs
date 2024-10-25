using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyChangeRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyChangeRequestClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      var prop = _obj.State.Properties;
      prop.Counterparty.IsRequired = true;
      
      // Для роли "Специалист по мастер-данным".
      var isMasterDataSpecialist = PublicFunctions.Module.IncludedInMasterDataSpecialist();
      if (!isMasterDataSpecialist)
        e.HideAction(_obj.Info.Actions.MakeChanges);
    }

  }
}