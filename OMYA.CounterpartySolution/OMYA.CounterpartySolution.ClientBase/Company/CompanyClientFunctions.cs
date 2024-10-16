using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.Company;

namespace OMYA.CounterpartySolution.Client
{
  partial class CompanyFunctions
  {

    /// <summary>
    /// Проверки перед отправкой приглашения и проверкой доступности КА в сервисах.
    /// </summary>
    /// <param name="args">Аргументы действия.</param>
    /// <returns>Возвращает всю полученную информацию.</returns>
    /// <remarks>Только когда свойство CanDoAction = true, можно выполнять действие.</remarks>
    public override Sungero.Parties.Structures.Counterparty.InvitationInfo ValidateExchangeAction(Sungero.Domain.Client.ExecuteActionArgs args)
    {
      return base.ValidateExchangeAction(args);
    }
  }
}