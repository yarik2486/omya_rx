using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RDAreasApplication;

namespace alialgr.AppModule
{
  partial class RDAreasApplicationServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      // Значение по дефолту
      _obj.Name = "Заполняется автоматически" ;
    }
  }

}