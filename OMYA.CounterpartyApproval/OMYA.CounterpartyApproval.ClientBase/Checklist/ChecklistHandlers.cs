using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.Checklist;

namespace OMYA.CounterpartyApproval
{
  partial class ChecklistClientHandlers
  {

    public virtual void DocumentsReceivedFromSupplierValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      
    }

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      var checklistStateEnterprisesKind = Sungero.Docflow.PublicFunctions.DocumentKind.GetNativeDocumentKind(Constants.Module.Initialize.ChecklistStateEnterprisesKind);
      var notChecklistStateEnterprisesKind = !Equals(_obj.DocumentKind, checklistStateEnterprisesKind);
      
      var isEDIOperatorOther = _obj.EDIOperator == EDIOperator.Other;
      var DocumentsReceivedFromSupplierOther = _obj.DocumentsReceivedFromSupplier == DocumentsReceivedFromSupplier.No && notChecklistStateEnterprisesKind;
      _obj.State.Properties.EDIOperatorOther.IsVisible = isEDIOperatorOther;
      _obj.State.Properties.EDIOperatorOther.IsRequired = isEDIOperatorOther;
      _obj.State.Properties.DocumentsReceivedFromSupplierOther.IsVisible = DocumentsReceivedFromSupplierOther;
      _obj.State.Properties.DocumentsReceivedFromSupplierOther.IsRequired = DocumentsReceivedFromSupplierOther;
    }

    public virtual void EDIOperatorValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      
    }

    public override void DocumentKindValueInput(Sungero.Docflow.Client.OfficialDocumentDocumentKindValueInputEventArgs e)
    {
      base.DocumentKindValueInput(e);
      
      Functions.Checklist.SetPropertiesAccess(_obj, e.NewValue);
    }

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      var prop = _obj.State.Properties;
      prop.LeadingDocument.IsRequired = true;
      prop.PreparedBy.IsRequired = true;
      prop.JobTitle.IsRequired = true;
      prop.BusinessUnit.IsRequired = true;
      prop.FullNameCompany.IsRequired = true;
      prop.TIN.IsRequired = true;
      prop.PSRN.IsRequired = true;
      prop.PrimaryContact.IsRequired = true;
      prop.CompanyOwners.IsRequired = true;
      prop.EDIOperator.IsRequired = true;
      
      Functions.Checklist.SetPropertiesAccess(_obj, _obj.DocumentKind);
    }

    public virtual void PSRNValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      var errorMessage = Functions.CounterpartyApprovalRequest.CheckPsrnLength(e.NewValue);
      if (!string.IsNullOrEmpty(errorMessage))
        e.AddError(errorMessage);
    }

    public virtual void TINValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      var errorMessage = Sungero.Parties.PublicFunctions.Counterparty.CheckTin(e.NewValue, true);
      if (!string.IsNullOrEmpty(errorMessage))
        e.AddError(_obj.Info.Properties.TIN, errorMessage);
    }

  }
}