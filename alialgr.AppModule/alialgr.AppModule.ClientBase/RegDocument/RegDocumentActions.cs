using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RegDocument;

namespace alialgr.AppModule.Client
{
  partial class RegDocumentActions
  {
    public override void SendForFreeApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      if(_obj.DocumentKind != null)
      {
        var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);
        if (docKind.RDSendRegalialgr.GetValueOrDefault() && _obj.HasVersions==false )
        {
          e.AddWarning("Приложите версию ! ");
          return;
        }
      }

      base.SendForFreeApproval(e);
    }

    public override bool CanSendForFreeApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanSendForFreeApproval(e);
    }

    public override void SendForApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      
      if(_obj.DocumentKind != null)
      {
        var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);
        if (docKind.RDSendRegalialgr.GetValueOrDefault() && _obj.HasVersions==false )
        {
          e.AddWarning("Приложите версию ! ");
          return;
        }
        
      }
      base.SendForApproval(e);
    }

    public override bool CanSendForApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanSendForApproval(e);
    }

    public override void CreateFromTemplate(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      
      if(_obj.DocumentKind != null)
      {
        var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);
        if (docKind.RDRegTempalialgr.GetValueOrDefault() && _obj.RegistrationNumber==null )
        {
          e.AddWarning("Не заполнен «Рег. №» !");
          return;
        }
        
      }
      
      base.CreateFromTemplate(e);
    }

    public override bool CanCreateFromTemplate(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanCreateFromTemplate(e);
    }

  }

}