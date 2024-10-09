using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.SettingDocumentsList;

namespace OMYA.CounterpartyApproval
{
  partial class SettingDocumentsListServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      var setting = SettingDocumentsLists.GetAll(x => x.CounterpartyType == _obj.CounterpartyType && x.Status == Status.Active).FirstOrDefault();
      if (setting != null)
      {
        e.AddError(OMYA.CounterpartyApproval.SettingDocumentsLists.Resources.SettingHasAlready);
        return;
      }
    }
  }

}