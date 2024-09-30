using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;

namespace alialgr.CARandOEC.Server
{
  public partial class ModuleInitializer
  {

    public override bool IsModuleVisible()
    {
      if (Users.Current.IsSystem==true)
        return true;
      
      var role = Sungero.CoreEntities.Roles.GetAll().Where(r => Equals(r.Name, "Инициатор запроса CAR&OEC")).FirstOrDefault();
      if (role != null)
        if (Users.Current.IncludedIn( role ) )
          return true;
      return false;
      
    }

    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      
    }
  }
}
