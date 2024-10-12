using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.Checklist;

namespace OMYA.CounterpartyApproval
{
  partial class ChecklistServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.JobTitle = _obj.PreparedBy?.JobTitle;
      _obj.InitiatorEmail = _obj.PreparedBy?.Email;
    }
  }

}