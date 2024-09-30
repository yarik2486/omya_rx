using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RequestCARandOEC;

namespace alialgr.AppModule.Client
{
  partial class RequestCARandOECActions
  {
    public override bool CanSendForFreeApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanSendForFreeApproval(e);
    }

    public override void SendForFreeApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      if (_obj.HasVersions==false )
      {
        e.AddWarning("Приложите версию ! ");
        return;
      }
      base.SendForFreeApproval(e);
    }

    public override void SendForApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      if (_obj.HasVersions==false )
      {
        e.AddWarning("Приложите версию ! ");
        return;
      }
      base.SendForApproval(e);
    }

    public override bool CanSendForApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanSendForApproval(e);
    }

  }

}