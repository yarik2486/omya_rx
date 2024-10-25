using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.Condition;

namespace OMYA.CounterpartySolution.Shared
{
  partial class ConditionFunctions
  {

    /// <summary>
    /// Получить словарь поддерживаемых типов условий.
    /// </summary>
    /// <returns>
    /// Словарь.
    /// Ключ - GUID типа документа.
    /// Значение - список поддерживаемых условий.
    /// </returns>
    public override System.Collections.Generic.Dictionary<string, List<Nullable<Enumeration>>> GetSupportedConditions()
    {
      var baseConditions = base.GetSupportedConditions();
      
      // Заявка на одобрение контрагента.
      baseConditions[OMYA.CounterpartyApproval.PublicConstants.Module.CounterpartyApprovalRequestTypeGuid].Add(ConditionType.CheckEDIOMYA);
      
      // Заявка на изменение реквизитов контрагента.
      baseConditions[OMYA.CounterpartyApproval.PublicConstants.Module.CounterpartyChangeRequestTypeGuid].Add(ConditionType.CheckCPDataType);
      
      return baseConditions;
    }
    
    public override Sungero.Docflow.Structures.ConditionBase.ConditionResult CheckCondition(Sungero.Docflow.IOfficialDocument document, Sungero.Docflow.IApprovalTask task)
    {
      if (_obj.ConditionType == ConditionType.CheckEDIOMYA)
        return this.CheckEDO(document);
      else if (_obj.ConditionType == ConditionType.CheckCPDataType)
        return this.CheckCPDataType(document);
      
      return base.CheckCondition(document, task);
    }
    
    /// <summary>
    /// Проверить, указано ли значение «СЭД Диадок» в поле «Оператор ЭДО» в карточке чек-листа, связанного с заявкой.
    /// </summary>
    /// <param name="document">Документ.</param>
    /// <returns>True, если указано.</returns>
    private Sungero.Docflow.Structures.ConditionBase.ConditionResult CheckEDO(Sungero.Docflow.IOfficialDocument document)
    {
      var checkList = OMYA.CounterpartyApproval.Checklists.GetAll(x => x.LeadingDocument != null && Equals(x.LeadingDocument, document)).FirstOrDefault();
      var isEDI = checkList != null && checkList.EDIOperator == OMYA.CounterpartyApproval.Checklist.EDIOperator.Diadoc;
      
      return Sungero.Docflow.Structures.ConditionBase.ConditionResult.Create(isEDI, string.Empty);
    }
    
    /// <summary>
    /// Проверяет заполнение признака «Банковские реквизиты» в карточке заявки на изменение контрагента.
    /// </summary>
    /// <param name="document">Документ.</param>
    /// <returns>True, если заполнено.</returns>
    private Sungero.Docflow.Structures.ConditionBase.ConditionResult CheckCPDataType(Sungero.Docflow.IOfficialDocument document)
    {
      var counterpartyChangeRequest = CounterpartyApproval.CounterpartyChangeRequests.As(document);
      var isBankDetails = counterpartyChangeRequest != null && counterpartyChangeRequest.BankDetails == true;
      
      return Sungero.Docflow.Structures.ConditionBase.ConditionResult.Create(isBankDetails, string.Empty);
    }
  }
}