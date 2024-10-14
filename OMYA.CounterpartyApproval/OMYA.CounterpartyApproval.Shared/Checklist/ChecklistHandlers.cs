using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.Checklist;

namespace OMYA.CounterpartyApproval
{
  partial class ChecklistSharedHandlers
  {

    public override void LeadingDocumentChanged(Sungero.Docflow.Shared.OfficialDocumentLeadingDocumentChangedEventArgs e)
    {
      base.LeadingDocumentChanged(e);
      
      if (CounterpartyApprovalRequests.Is(e.NewValue))
      {
        var leadingDocument = CounterpartyApprovalRequests.As(e.NewValue);
        _obj.FullNameCompany = leadingDocument.LegalName;
        _obj.TIN = leadingDocument.TIN;
        _obj.PSRN = leadingDocument.PSRN;
        _obj.Homepage = leadingDocument.Homepage;
      }
    }

  }
}