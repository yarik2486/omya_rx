using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.SettingDocumentsList;

namespace OMYA.CounterpartyApproval
{
  partial class SettingDocumentsListClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      var prop = _obj.State.Properties;
      prop.Name.IsRequired = true;
      prop.CounterpartyType.IsRequired = true;
      prop.DocumentsForApproval.IsRequired = true;
      prop.DocumentsForApproval.Properties.DocumentName.IsRequired = true;
    }

  }
}