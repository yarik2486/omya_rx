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
      prop.SAPNum.IsRequired = true;
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