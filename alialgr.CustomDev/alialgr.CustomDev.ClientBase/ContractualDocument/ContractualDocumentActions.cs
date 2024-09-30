using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ContractualDocument;

namespace alialgr.CustomDev.Client
{
  partial class ContractualDocumentActions
  {
    public override void CreateFromFile(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      if (string.IsNullOrEmpty(_obj.Subject))
      {
        base.CreateFromFile(e);
        _obj.Subject = string.Empty;
      }
      else
      {
        base.CreateFromFile(e);
      }
    }

    public override bool CanCreateFromFile(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanCreateFromFile(e);
    }

  }

}