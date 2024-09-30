using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RequestCARandOEC;

namespace alialgr.AppModule
{
  partial class RequestCARandOECSharedHandlers
  {

    public virtual void FinControllerChanged(alialgr.AppModule.Shared.RequestCARandOECFinControllerChangedEventArgs e)
    {
     _obj.OurSignatory = _obj.FinController; 
    }

  }
}