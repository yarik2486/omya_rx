using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.PersonPassportData;

namespace alialgr.AppModule
{
  partial class PersonPassportDataServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
    _obj.Name="Паспорт "+ _obj.Person.Name;  
    }
  }

}