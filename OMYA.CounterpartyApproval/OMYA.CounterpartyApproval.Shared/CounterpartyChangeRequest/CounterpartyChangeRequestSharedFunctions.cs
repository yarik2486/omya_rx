using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyChangeRequest;

namespace OMYA.CounterpartyApproval.Shared
{
  partial class CounterpartyChangeRequestFunctions
  {

    /// <summary>
    /// Установить обязательность свойств в зависимости от заполненных данных.
    /// </summary>
    public override void SetRequiredProperties()
    {
      base.SetRequiredProperties();
      
      _obj.State.Properties.Subject.IsRequired = false;
    }
    
    /// <summary>
    /// Получить автоматически сформированное имя документа.
    /// </summary>
    /// <returns>Имя документа.</returns>
    public override string GetGeneratedDocumentName()
    {
      var documentKind = _obj.DocumentKind;
      var name = string.Empty;
      
      /* Имя в формате:
        <Вид документа> "<Контрагент>" №<номер> от <дата>.
       */
      using (TenantInfo.Culture.SwitchTo())
      {
        var counterpartyName = _obj.Counterparty?.Name;
        if (!string.IsNullOrWhiteSpace(counterpartyName))
          name += " \"" + counterpartyName + "\"";
        
        if (!string.IsNullOrWhiteSpace(_obj.RegistrationNumber))
          name += Sungero.Docflow.OfficialDocuments.Resources.Number + _obj.RegistrationNumber;
        
        if (_obj.RegistrationDate != null)
          name += Sungero.Docflow.OfficialDocuments.Resources.DateFrom + _obj.RegistrationDate.Value.ToString("d");
      }
      
      if (string.IsNullOrWhiteSpace(name))
      {
        if (_obj.VerificationState == null)
          name = Sungero.Docflow.Resources.DocumentNameAutotext;
        else
          name = _obj.DocumentKind.ShortName;
      }
      else if (documentKind != null)
      {
        name = documentKind.ShortName + name;
      }
      
      name = Sungero.Docflow.PublicFunctions.Module.TrimSpecialSymbols(name);
      
      return Sungero.Docflow.PublicFunctions.OfficialDocument.AddClosingQuote(name, _obj);
    }
    
    /// <summary>
    /// Изменить статус запроса.
    /// </summary>
    /// <param name="lifeCycleState">Состояние.</param>
    /// <param name="internalApprovalState">Согласование.</param>
    public void UpdateStatus(Nullable<Enumeration> lifeCycleState, Nullable<Enumeration> internalApprovalState)
    {
      // Состояние - в разработке, Согласование - не заполнено. 
      if (lifeCycleState == LifeCycleState.Draft && internalApprovalState == null) 
      {
        _obj.Status = Status.Draft; // Черновик.
      }
      
      // Состояние - в разработке, Согласование - на согласовании. 
      else if (lifeCycleState == LifeCycleState.Draft && internalApprovalState == InternalApprovalState.OnApproval) 
      {
         _obj.Status = Status.OnApproval; // На согласовании.
      }
      
      // Состояние - в разработке, Согласование - на доработке. 
      else if (lifeCycleState == LifeCycleState.Draft && internalApprovalState == InternalApprovalState.OnRework)
      {
        _obj.Status = Status.OnRework; // На доработке.
      }
      
      // Состояние - действующий, Согласование - на согласовании. 
      else if (lifeCycleState == LifeCycleState.Active && internalApprovalState == InternalApprovalState.OnApproval)
      {
        _obj.Status = Status.Approved; // Согласовано.
      }
      
      // Состояние - в разработке, Согласование - прекращено. 
      else if (lifeCycleState == LifeCycleState.Draft && internalApprovalState == InternalApprovalState.Aborted)
      {
        _obj.Status = Status.NotApproved; // Не согласовано.
      }
    }
  }
}