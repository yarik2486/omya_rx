using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyApprovalRequestDocumentsForApprovalDocumentPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> DocumentsForApprovalDocumentFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      return query.Where(x => Equals(x.LeadingDocument, _root));
    }
  }

  partial class CounterpartyApprovalRequestCityPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> CityFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      // Фильтровать населенные пункты по региону.
      if (_obj.Region != null)
        query = query.Where(settlement => Equals(settlement.Region, _obj.Region));
      
      return query;
    }
  }

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

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      
      // Обновить статус согласования контрагента.
      if (_obj.Counterparty != null && _obj.State.Properties.Status.IsChanged)
      {
        var setApprovalStatusForCompany = AsyncHandlers.SetApprovalStatusForCompany.Create();
        setApprovalStatusForCompany.CompanyId = _obj.Counterparty.Id;
        setApprovalStatusForCompany.DocumentId = _obj.Id;
        setApprovalStatusForCompany.ExecuteAsync();
      }
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.Nonresident = false;
    }
  }

}