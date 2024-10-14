using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval.Server
{
  partial class CounterpartyApprovalRequestFunctions
  {

    /// <summary>
    /// Создать чек-лист.
    /// </summary>
    /// <returns>Чек-лист.</returns>
    [Remote]
    public static IChecklist CreateChecklist()
    {
      return Checklists.Create();
    }
  }
}