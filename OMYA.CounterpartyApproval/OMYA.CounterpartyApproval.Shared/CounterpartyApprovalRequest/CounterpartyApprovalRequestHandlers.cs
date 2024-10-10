using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;

namespace OMYA.CounterpartyApproval
{
  partial class CounterpartyApprovalRequestSharedHandlers
  {

    public virtual void CityChanged(OMYA.CounterpartyApproval.Shared.CounterpartyApprovalRequestCityChangedEventArgs e)
    {
      _obj.Region = e.NewValue?.Region;
    }

    public virtual void CounterpartyTypeChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      if (e.NewValue != null)
      {
        var setting = SettingDocumentsLists.GetAll(x => Equals(x.CounterpartyType, e.NewValue) && x.DocumentsForApproval.Any()).FirstOrDefault();
        if (setting != null)
        {
          _obj.DocumentsForApproval.Clear();
          foreach (var item in setting.DocumentsForApproval)
          {
            var documentsForApproval = _obj.DocumentsForApproval.AddNew();
            documentsForApproval.DocumentName = item.DocumentName;
            documentsForApproval.Document = item.Document;
            documentsForApproval.Absent = item.Absent;
            documentsForApproval.ReasonForAbsence = item.ReasonForAbsence;
          }
        }
      }
    }

    public virtual void StatusChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      if (e.NewValue == Status.Draft) // Черновик.
      {
        _obj.LifeCycleState = LifeCycleState.Draft; // В разработке.
        _obj.InternalApprovalState = null;
      }
      else if (e.NewValue == Status.OnApproval) // На согласовании.
      {
        _obj.LifeCycleState = LifeCycleState.Draft; // В разработке.
        _obj.InternalApprovalState = InternalApprovalState.OnApproval; // На согласовании.
      }
      else if (e.NewValue == Status.OnRework) // На доработке.
      {
        _obj.LifeCycleState = LifeCycleState.Draft; // В разработке.
        _obj.InternalApprovalState = InternalApprovalState.OnRework; // На доработке.
      }
      else if (e.NewValue == Status.Approved) // Согласовано.
      {
        _obj.LifeCycleState = LifeCycleState.Active; // Действующий.
        _obj.InternalApprovalState = InternalApprovalState.OnApproval; // На согласовании.
      }
      else if (e.NewValue == Status.NotApproved) // Не согласовано.
      {
        _obj.LifeCycleState = LifeCycleState.Draft; // В разработке.
        _obj.InternalApprovalState = InternalApprovalState.Aborted; // Прекращено.
      }
    }

    public virtual void BankChanged(OMYA.CounterpartyApproval.Shared.CounterpartyApprovalRequestBankChangedEventArgs e)
    {
      _obj.CorrespondentAccount = e.NewValue?.CorrespondentAccount;
      _obj.BIC = e.NewValue?.BIC;
      _obj.BankAddress = e.NewValue?.LegalAddress;
      _obj.SWIFT = e.NewValue?.SWIFT;
    }

    public virtual void NonresidentChanged(Sungero.Domain.Shared.BooleanPropertyChangedEventArgs e)
    {
      if (e.NewValue == true)
        _obj.CounterpartyType = CounterpartyType.ForeignOrg;
      else
        _obj.CounterpartyType = null;
    }

  }
}