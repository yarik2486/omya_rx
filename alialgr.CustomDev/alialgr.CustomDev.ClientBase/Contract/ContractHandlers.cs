using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.Contract;

namespace alialgr.CustomDev
{
  partial class ContractClientHandlers
  {

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      if(_obj.DocumentKind != null)
      {
        var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);
        
        
        #region Видимость
        _obj.State.Properties.IsFrameworkContract.IsVisible = docKind.IsFrameworkalialgr.GetValueOrDefault();
        #endregion
      }
    }
  }
}