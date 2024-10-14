using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ContractualDocument;

namespace OMYA.CounterpartySolution
{
  partial class ContractualDocumentCounterpartyPropertyFilteringServerHandler<T>
  {

    public override IQueryable<T> CounterpartyFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = base.CounterpartyFiltering(query, e);
      query = query.Cast<ICompany>().Where(x => Equals(x.ApprovalStatusOMYA, OMYA.CounterpartySolution.Company.ApprovalStatusOMYA.Approved)).Cast<T>();
      
      return query;
    }
  }

}