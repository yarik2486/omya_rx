using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyApprovalRequestClientHandlers
  {

    public virtual void TRRCValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      if (_obj.Nonresident != true)
      {
        var errorMessage = Functions.CounterpartyApprovalRequest.CheckTRRC(e.NewValue);
        if (!string.IsNullOrEmpty(errorMessage))
          e.AddError(errorMessage);
      }
    }

    public virtual void AccountValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      var errorMessage = Sungero.Parties.PublicFunctions.Counterparty.CheckAccountLength(e.NewValue);
      if (!string.IsNullOrEmpty(errorMessage))
        e.AddError(errorMessage);
    }

    public virtual void NCEOValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      if (_obj.Nonresident != true)
      {
        var errorMessage = Functions.CounterpartyApprovalRequest.CheckNceoLength(e.NewValue);
        if (!string.IsNullOrEmpty(errorMessage))
          e.AddError(errorMessage);
      }
    }

    public virtual void PSRNValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      if (_obj.Nonresident != true)
      {
        var errorMessage = Functions.CounterpartyApprovalRequest.CheckPsrnLength(e.NewValue);
        if (!string.IsNullOrEmpty(errorMessage))
          e.AddError(errorMessage);
      }
    }

    public virtual void EmailValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(e.NewValue) && !Sungero.Parties.PublicFunctions.Module.EmailIsValid(e.NewValue))
        e.AddWarning(Sungero.Parties.Resources.WrongEmailFormat);
    }

    public virtual void TINValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      if (_obj.Nonresident != true)
      {
        var errorMessage = Sungero.Parties.PublicFunctions.Counterparty.CheckTin(e.NewValue, true);
        if (!string.IsNullOrEmpty(errorMessage))
          e.AddError(_obj.Info.Properties.TIN, errorMessage, _obj.Info.Properties.Nonresident);
      }
    }

    public virtual void CounterpartyKindValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      
    }

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      var prop = _obj.State.Properties;
      prop.BusinessUnit.IsRequired = true;
      prop.PreparedBy.IsRequired = true;
      prop.CounterpartyType.IsRequired = true;
      prop.CounterpartyKind.IsRequired = true;
      prop.PaymentTerms.IsRequired = true;
      prop.ShortName.IsRequired = true;
      prop.LegalName.IsRequired = true;
      prop.City.IsRequired = true;
      prop.Region.IsRequired = true;
      prop.LegalAddress.IsRequired = true;
      prop.PostalAddress.IsRequired = true;
      prop.Phones.IsRequired = true;
      prop.Email.IsRequired = true;
      prop.Homepage.IsRequired = true;
      prop.Account.IsRequired = true;
      prop.CurrentAccount.IsRequired = true;
      prop.Bank.IsRequired = true;
      prop.BankAddress.IsRequired = true;
      prop.DocumentsForApproval.IsRequired = true;
      prop.DocumentsForApproval.Properties.DocumentName.IsRequired = true;
    }

  }
}