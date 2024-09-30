using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace alialgr.CustomDev.Module.Company.Client
{
  partial class ModuleFunctions
  {

    /// <summary>
    /// Доступ к справочнику Паспотные данные персон
    /// </summary>
    public virtual void PasportData()
    {
      //INK
     var Su = Sungero.CoreEntities.Substitutions.ActiveSubstitutedUsers;
     var isdeny = Su.Where(u=> u.Name.Contains("Юридическая служба")).Any();
      
     if ((Sungero.CoreEntities.Users.Current.IsSystem ?? false) || isdeny)
        alialgr.AppModule.PersonPassportDatas.GetAll().Show();
      else
        Dialogs.ShowMessage("Доступно только для юридической службы!" ,MessageType.Information);
      
    }

  }
}