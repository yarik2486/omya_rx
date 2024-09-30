using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace alialgr.CARandOEC.Client
{
  public class ModuleFunctions
  {

    /// <summary>
    /// 
    /// </summary>
    public virtual void CreateRequest()
    {
      alialgr.AppModule.RequestCARandOECs.Create().Show();
    }

  }
}