using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.SupAgreement;

namespace alialgr.CustomDev.Server
{
  partial class SupAgreementFunctions
  {
    public override List<Sungero.Docflow.IApprovalRuleBase> GetApprovalRules()
    {
      
      var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);//Определяем вид документа
       var selectRuleAtLeadDoc=docKind?.SelRegOnLDalialgr; // Из настроек вида берём "Выбирать регламент по ведущему документу"
      
      if(_obj.LeadingDocument != null && _obj.LeadingDocument.DocumentKind != null && selectRuleAtLeadDoc==true) // добавил настройку
      {
        var rules = base.GetApprovalRules();
        return rules.Where(x=>x.DocumentKinds.Any(y=>Equals(y.DocumentKind, _obj.LeadingDocument.DocumentKind ))).ToList();
      }
      else
        return base.GetApprovalRules();
    }
    
    [Remote(IsPure = true)]
    public virtual bool SignatorySettingWithAllUsersExistCustom(Sungero.Docflow.IOfficialDocument contract)
    {
      var settings = Sungero.Docflow.PublicFunctions.OfficialDocument.Remote.GetSignatureSettingsQuery(contract);
      return settings.Any(s => s.Recipient.Sid == Sungero.Docflow.Constants.OfficialDocument.AllUsersSid);
    }
    
    [Remote(IsPure = true)]
    public virtual List<long> GetSignatoriesIdsCustom(Sungero.Docflow.IOfficialDocument contract)
    {
      if (_obj == null)
        return new List<long>();

      var settings = Sungero.Docflow.PublicFunctions.OfficialDocument.Remote.GetSignatureSettingsQuery(contract);

      return this.ExpandSignatoriesBySignatureSettings(settings);
      
    }
  }
}