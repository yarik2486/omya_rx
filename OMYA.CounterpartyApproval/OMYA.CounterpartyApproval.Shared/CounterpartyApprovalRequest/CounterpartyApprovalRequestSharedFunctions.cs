using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval.Shared
{
  partial class CounterpartyApprovalRequestFunctions
  {

    /// <summary>
    /// Установить обязательность свойств в зависимости от заполненных данных.
    /// </summary>
    public override void SetRequiredProperties()
    {
      base.SetRequiredProperties();
      
      var nonresident = _obj.Nonresident == true;
      var prop = _obj.State.Properties;
      
      // Резидент.
      prop.TIN.IsRequired = !nonresident;
      prop.TIN.IsVisible = !nonresident;
      prop.TRRC.IsRequired = !nonresident;
      prop.TRRC.IsVisible = !nonresident;
      prop.PSRN.IsRequired = !nonresident;
      prop.PSRN.IsVisible = !nonresident;
      prop.NCEO.IsVisible = !nonresident;
      prop.NCEA.IsVisible = !nonresident;
      
      prop.CorrespondentAccount.IsRequired = !nonresident;
      prop.CorrespondentAccount.IsVisible = !nonresident;
      prop.BIC.IsRequired = !nonresident;
      prop.BIC.IsVisible = !nonresident;
      
      
      // Нерезидент.
      prop.CompanyRegistrationNumber.IsRequired = nonresident;
      prop.CompanyRegistrationNumber.IsVisible = nonresident;
      prop.TaxNumber.IsRequired = nonresident;
      prop.TaxNumber.IsVisible = nonresident;
      prop.Currency.IsRequired = nonresident;
      prop.Currency.IsVisible = nonresident;
      prop.DeliveryTerms.IsRequired = nonresident;
      prop.DeliveryTerms.IsVisible = nonresident;
      
      prop.IBAN.IsRequired = nonresident;
      prop.IBAN.IsVisible = nonresident;
      prop.SWIFT.IsRequired = nonresident;
      prop.SWIFT.IsVisible = nonresident;
      
      // Поставщик или клиент.
      prop.MacroSegment.IsRequired = _obj.CounterpartyKind == CounterpartyKind.Supplier;
      prop.MacroSegment.IsVisible = _obj.CounterpartyKind == CounterpartyKind.Supplier;;
      prop.MarketSegment.IsRequired = _obj.CounterpartyKind == CounterpartyKind.Client;;
      prop.MarketSegment.IsVisible = _obj.CounterpartyKind == CounterpartyKind.Client;;
    }
  }
}