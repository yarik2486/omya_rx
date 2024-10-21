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
    
    /// <summary>
    /// Добавить результат согласования в заявку на одобрение контрагента.
    /// </summary>
    /// <param name="documentId">ИД документа.</param>
    /// <param name="approverId">ИД согласующего.</param>
    /// <param name="result">Результат согласования.</param>
    /// <param name="comment">Комментарий.</param>
    [Public]
    public void AddApprovalResult(long documentId, long approverId, string result, string comment)
    {
      var addApprovalResult = OMYA.CounterpartyApproval.AsyncHandlers.AddApprovalResult.Create();
      addApprovalResult.DocumentId = documentId;
      addApprovalResult.ApproverId = approverId;
      addApprovalResult.Result = result;
      addApprovalResult.Comment = comment;
      addApprovalResult.ExecuteAsync();
    }
    
    /// <summary>
    /// Получить адрес нашего сервиса заполнения контрагентов.
    /// </summary>
    /// <returns>Адрес сервера, или пустую строку, если его нет.</returns>
    [Remote]
    public string GetCompanyDataServiceURL()
    {
      var key = Sungero.Docflow.PublicConstants.Module.CompanyDataServiceKey;
      var command = string.Format(Queries.Module.SelectCompanyDataService, key);
      var commandExecutionResult = Sungero.Docflow.PublicFunctions.Module.ExecuteScalarSQLCommand(command);
      var serviceUrl = string.Empty;
      if (!(commandExecutionResult is DBNull) && commandExecutionResult != null)
        serviceUrl = commandExecutionResult.ToString();
      
      return serviceUrl;
    }
  }
}