using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ContractualDocument;

namespace alialgr.CustomDev
{
  partial class ContractualDocumentServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      if(!_obj.WithResalealialgr.GetValueOrDefault())
        _obj.WithResalealialgr = false;
    }
  }

}