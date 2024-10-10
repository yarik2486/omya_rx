using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;
using Sungero.Docflow;

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
      
      _obj.State.Properties.Subject.IsRequired = false;
    }
  
    /// <summary>
    /// Сменить доступность реквизитов документа.
    /// </summary>
    /// <param name="isEnabled">True, если свойства должны быть доступны.</param>
    /// <param name="repeatRegister">Перерегистрация.</param>
    public override void ChangeDocumentPropertiesAccess(bool isEnabled, bool repeatRegister)
    {
      base.ChangeDocumentPropertiesAccess(isEnabled, repeatRegister);
      
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
      prop.MacroSegment.IsVisible = _obj.CounterpartyKind == CounterpartyKind.Supplier;
      prop.MarketSegment.IsRequired = _obj.CounterpartyKind == CounterpartyKind.Client;
      prop.MarketSegment.IsVisible = _obj.CounterpartyKind == CounterpartyKind.Client;
      
      prop.DocumentKind.IsEnabled = false;
      prop.Name.IsEnabled = false;
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
        <Вид документа> "<Краткое наименование>" №<номер> от <дата>.
       */
      using (TenantInfo.Culture.SwitchTo())
      {
        if (!string.IsNullOrWhiteSpace(_obj.ShortName))
          name += " \"" + _obj.ShortName + "\"";
        
        if (!string.IsNullOrWhiteSpace(_obj.RegistrationNumber))
          name += OfficialDocuments.Resources.Number + _obj.RegistrationNumber;
        
        if (_obj.RegistrationDate != null)
          name += OfficialDocuments.Resources.DateFrom + _obj.RegistrationDate.Value.ToString("d");
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
  }
}