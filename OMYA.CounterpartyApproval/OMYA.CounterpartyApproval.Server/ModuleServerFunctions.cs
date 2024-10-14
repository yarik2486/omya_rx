using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace OMYA.CounterpartyApproval.Server
{
  public class ModuleFunctions
  {

    /// <summary>
    /// Текущий пользователь администратор или входит в роль "Специалист по мастер-данным".
    /// </summary>
    /// <returns>True - да, False - нет.</returns>
    [Public]
    public bool IncludedInMasterDataSpecialist()
    {
      var role = Roles.GetAll(x => x.Sid == Constants.Module.Initialize.MasterDataSpecialist).FirstOrDefault();
      return Users.Current.IncludedIn(Roles.Administrators) || Users.Current.IncludedIn(role);
    }
  }
}