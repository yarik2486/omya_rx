using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.DocumentKind;

namespace alialgr.CustomDev
{
  partial class DocumentKindSharedHandlers
  {

    public virtual void RDValidFromalialgrChanged(Sungero.Domain.Shared.BooleanPropertyChangedEventArgs e)
    {
      if (_obj.RDValidFromalialgr==true)
        _obj.RDValidFromValialgr=true;
    }

  }
}