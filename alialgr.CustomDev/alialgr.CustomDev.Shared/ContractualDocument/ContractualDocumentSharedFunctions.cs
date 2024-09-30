using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ContractualDocument;

namespace alialgr.CustomDev.Shared
{
  partial class ContractualDocumentFunctions
  {
    public override void SetRequiredProperties()
    {
      base.SetRequiredProperties();
      _obj.State.Properties.DocumentGroup.IsEnabled = true;
    }
  }
}