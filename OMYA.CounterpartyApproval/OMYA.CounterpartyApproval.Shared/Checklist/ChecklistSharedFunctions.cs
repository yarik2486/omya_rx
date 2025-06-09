using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.Checklist;

namespace OMYA.CounterpartyApproval.Shared
{
  partial class ChecklistFunctions
  {

    /// <summary>
    /// Установить обязательность и видимость свойст.
    /// </summary>
    public void SetPropertiesAccess(Sungero.Docflow.IDocumentKind documentKind)
    {
      var checklistStateEnterprisesKind = Sungero.Docflow.PublicFunctions.DocumentKind.GetNativeDocumentKind(Constants.Module.Initialize.ChecklistStateEnterprisesKind);
      var notChecklistStateEnterprisesKind = !Equals(documentKind, checklistStateEnterprisesKind);
      
      var prop = _obj.State.Properties;
      prop.FoundationDate.IsRequired = notChecklistStateEnterprisesKind;
      prop.DocumentsReceivedFromSupplier.IsRequired = notChecklistStateEnterprisesKind;
      prop.SupplierRegistered.IsRequired = notChecklistStateEnterprisesKind;
      prop.SupplierRegistered18Months.IsRequired = notChecklistStateEnterprisesKind;
      prop.HaveWebsite.IsRequired = notChecklistStateEnterprisesKind;
      prop.CEOLeast5Companies.IsRequired = notChecklistStateEnterprisesKind;
      prop.ResultsCheckTurnover.IsRequired = notChecklistStateEnterprisesKind;
      prop.SupplierHasPersonnel.IsRequired = notChecklistStateEnterprisesKind;
      prop.CheckHeadCount.IsRequired = notChecklistStateEnterprisesKind;
      prop.IdentifiedRiskFactors.IsRequired = notChecklistStateEnterprisesKind;
      prop.CollectionOfReviews.IsRequired = notChecklistStateEnterprisesKind;
      prop.ContactDetailsSaved.IsRequired = notChecklistStateEnterprisesKind;
      
      prop.SupplierServices.IsRequired = notChecklistStateEnterprisesKind;
      prop.AttractingReasons.IsRequired = notChecklistStateEnterprisesKind;
      prop.MonthlyPurchase.IsRequired = notChecklistStateEnterprisesKind;
      prop.HowSupplierFound.IsRequired = notChecklistStateEnterprisesKind;
      prop.UseSubcontracting.IsRequired = notChecklistStateEnterprisesKind;
      prop.LicensedActivities.IsRequired = notChecklistStateEnterprisesKind;
      
      prop.AttractingReasons.IsVisible = notChecklistStateEnterprisesKind;
      prop.MonthlyPurchase.IsVisible = notChecklistStateEnterprisesKind;
      prop.HowSupplierFound.IsVisible = notChecklistStateEnterprisesKind;
      prop.UseSubcontracting.IsVisible = notChecklistStateEnterprisesKind;
      
      prop.DocumentsReceivedFromSupplier.IsVisible = notChecklistStateEnterprisesKind;
      prop.DocumentsReceivedFromSupplierOther.IsVisible = notChecklistStateEnterprisesKind;
      prop.SupplierRegistered.IsVisible = notChecklistStateEnterprisesKind;
      prop.SupplierRegistered18Months.IsVisible = notChecklistStateEnterprisesKind;
      prop.HaveWebsite.IsVisible = notChecklistStateEnterprisesKind;
      prop.CEOLeast5Companies.IsVisible = notChecklistStateEnterprisesKind;
      prop.ResultsCheckTurnover.IsVisible = notChecklistStateEnterprisesKind;
      prop.SupplierHasPersonnel.IsVisible = notChecklistStateEnterprisesKind;
      prop.CheckHeadCount.IsVisible = notChecklistStateEnterprisesKind;
      prop.IdentifiedRiskFactors.IsVisible = notChecklistStateEnterprisesKind;
      prop.CollectionOfReviews.IsVisible = notChecklistStateEnterprisesKind;
      prop.ContactDetailsSaved.IsVisible = notChecklistStateEnterprisesKind;
      prop.FoundationDate.IsVisible = notChecklistStateEnterprisesKind;
    }
    
    public override void ChangeDocumentPropertiesAccess(bool isEnabled, bool repeatRegister)
    {
      base.ChangeDocumentPropertiesAccess(isEnabled, repeatRegister);
    }
    
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
        <Вид документа> "<Полное наименование подрядчика (с указанием организационно-правовой формы)>" №<номер> от <дата> .
       */
      using (TenantInfo.Culture.SwitchTo())
      {
        if (!string.IsNullOrWhiteSpace(_obj.FullNameCompany))
          name += " \"" + _obj.FullNameCompany + "\"";
        
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
  }
}