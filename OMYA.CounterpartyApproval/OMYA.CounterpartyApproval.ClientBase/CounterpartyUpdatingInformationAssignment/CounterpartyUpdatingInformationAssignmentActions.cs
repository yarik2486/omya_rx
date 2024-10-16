using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyUpdatingInformationAssignment;

namespace OMYA.CounterpartyApproval.Client
{
  partial class CounterpartyUpdatingInformationAssignmentActions
  {
    public virtual void Complete(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var company = OMYA.CounterpartySolution.Companies.As(_obj.AttachmentGroup.All.FirstOrDefault());
      if (company != null && company.LastUpdatedDateOMYA < Calendar.Today)
      {
        e.AddError(OMYA.CounterpartyApproval.CounterpartyUpdatingInformationAssignments.Resources.LastUpdateDateLessCurrentDate);
        return;
      }
    }

    public virtual bool CanComplete(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return true;
    }

  }

}