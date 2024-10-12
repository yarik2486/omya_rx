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

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      var prop = _obj.State.Properties;
      prop.LeadingDocument.IsRequired = true;
      prop.PreparedBy.IsRequired = true;
      prop.JobTitle.IsRequired = true;
      prop.BusinessUnit.IsRequired = true;
      prop.InitiatorEmail.IsRequired = true;
      prop.FullNameCompany.IsRequired = true;
      prop.FoundationDate.IsRequired = true;
      prop.TIN.IsRequired = true;
      prop.PSRN.IsRequired = true;
      prop.PrimaryContact.IsRequired = true;
      prop.CompanyOwners.IsRequired = true;
      prop.EDIOperator.IsRequired = true;
      prop.SupplierServices.IsRequired = true;
      prop.AttractingReasons.IsRequired = true;
      prop.MonthlyPurchase.IsRequired = true;
      prop.HowSupplierFound.IsRequired = true;
      prop.UseSubcontracting.IsRequired = true;
      prop.LicensedActivities.IsRequired = true;
      prop.DocumentsReceivedFromSupplier.IsRequired = true;
      prop.SupplierRegistered.IsRequired = true;
      prop.SupplierRegistered18Months.IsRequired = true;
      prop.HaveWebsite.IsRequired = true;
      prop.CEOLeast5Companies.IsRequired = true;
      prop.ResultsCheckTurnover.IsRequired = true;
      prop.SupplierHasPersonnel.IsRequired = true;
      prop.CheckHeadCount.IsRequired = true;
      prop.IdentifiedRiskFactors.IsRequired = true;
      prop.CollectionOfReviews.IsRequired = true;
      prop.ContactDetailsSaved.IsRequired = true;
      prop.ResultsSentToManager.IsRequired = true;
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