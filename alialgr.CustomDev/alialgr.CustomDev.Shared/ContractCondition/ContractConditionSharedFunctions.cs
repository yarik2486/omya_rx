using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ContractCondition;

namespace alialgr.CustomDev.Shared
{
  partial class ContractConditionFunctions
  {
    public override System.Collections.Generic.Dictionary<string, List<Nullable<Enumeration>>> GetSupportedConditions()
    {
      var baseSupport = base.GetSupportedConditions();
      
      var contractualDocuments = Sungero.Docflow.PublicFunctions.DocumentKind.GetDocumentGuids(typeof(Sungero.Contracts.IContractualDocument));
      foreach(var contractualDoc in contractualDocuments)
      {
        baseSupport[contractualDoc].Add(ConditionType.WithResale);
      }
      
      return baseSupport;
    }
    
    public override Sungero.Docflow.Structures.ConditionBase.ConditionResult CheckCondition(Sungero.Docflow.IOfficialDocument document, Sungero.Docflow.IApprovalTask task)
    {
      if (_obj.ConditionType == ConditionType.WithResale)
        return this.CheckWithResale(document, task);
      
      if(_obj.ConditionType == ConditionType.DocumentKind)
        return this.CheckDocKind(document, task);
      
      return base.CheckCondition(document, task);
    }
    
    private Sungero.Docflow.Structures.ConditionBase.ConditionResult CheckWithResale(Sungero.Docflow.IOfficialDocument document, Sungero.Docflow.IApprovalTask task)
    {
      if (alialgr.CustomDev.ContractualDocuments.Is(document))
      {
        var supAgr = alialgr.CustomDev.SupAgreements.As(document);
        if(supAgr != null && supAgr.LeadingDocument != null)
        {
          return Sungero.Docflow.Structures.ConditionBase.ConditionResult.Create(alialgr.CustomDev.ContractualDocuments.As(supAgr.LeadingDocument).WithResalealialgr.GetValueOrDefault(), string.Empty);
        }
        
        return Sungero.Docflow.Structures.ConditionBase.ConditionResult.Create(alialgr.CustomDev.ContractualDocuments.As(document).WithResalealialgr.GetValueOrDefault(), string.Empty);
      }

      return Sungero.Docflow.Structures.ConditionBase.ConditionResult.Create(false, string.Empty);
    }
    
    private Sungero.Docflow.Structures.ConditionBase.ConditionResult CheckDocKind(Sungero.Docflow.IOfficialDocument document, Sungero.Docflow.IApprovalTask task)
    {
      var supAgr = alialgr.CustomDev.SupAgreements.As(document);
      if(supAgr != null)
      {
        var leadDoc = supAgr.LeadingDocument;
        if(leadDoc != null)
          return base.CheckDocumentKind(leadDoc);
      }
      return base.CheckDocumentKind(document);
    }
  }
}