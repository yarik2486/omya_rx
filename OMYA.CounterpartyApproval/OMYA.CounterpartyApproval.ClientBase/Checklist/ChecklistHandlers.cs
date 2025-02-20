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
      
      _obj.State.Properties.EDIOperatorOther.IsVisible = _obj.EDIOperator == EDIOperator.Other;
      _obj.State.Properties.DocumentsReceivedFromSupplierOther.IsVisible = _obj.DocumentsReceivedFromSupplier == DocumentsReceivedFromSupplier.No;
    }

    public virtual void EDIOperatorValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      
    }

    public override void DocumentKindValueInput(Sungero.Docflow.Client.OfficialDocumentDocumentKindValueInputEventArgs e)
    {
      base.DocumentKindValueInput(e);
    }

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
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