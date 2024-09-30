using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RDAreasApplication;

namespace alialgr.AppModule
{
  partial class RDAreasApplicationSharedHandlers
  {

    public virtual void CodeChanged(Sungero.Domain.Shared.StringPropertyChangedEventArgs e)
    {
      FillNamne();
    }

    public virtual void ScopeOfApplicationChanged(Sungero.Domain.Shared.StringPropertyChangedEventArgs e)
    {
      FillNamne();
    }
    
    
    /// <summary>
    /// Запонение наименования
    /// </summary>
    public void FillNamne()
    {
      _obj.Name =  _obj.Code +" - "+ _obj.ScopeOfApplication ;
      
    }
  }

}