using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyApprovalRequestMarketSegmentPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> MarketSegmentFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      return query.Where(x => Equals(x.CounterpartyKind, _obj.CounterpartyKind));
    }
  }

  partial class CounterpartyApprovalRequestMacroSegmentPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> MacroSegmentFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      return query.Where(x => Equals(x.CounterpartyKind, _obj.CounterpartyKind));
    }
  }

  partial class CounterpartyApprovalRequestServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.Nonresident = false;
      _obj.Status = Status.Draft;
    }
  }

}