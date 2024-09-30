using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RDLanguage;

namespace alialgr.AppModule
{
  partial class RDLanguageSharedHandlers
  {

    public virtual void LanguageChanged(Sungero.Domain.Shared.StringPropertyChangedEventArgs e)
    {
      FillNamne();
    }

    public virtual void CodeChanged(Sungero.Domain.Shared.StringPropertyChangedEventArgs e)
    {
      FillNamne();
    }

    /// <summary>
    /// Запонение наименования
    /// </summary>       
    public void FillNamne()
    {
      _obj.Name =  _obj.Code +" - "+ _obj.Language ; 
    
    }
    
  }
}