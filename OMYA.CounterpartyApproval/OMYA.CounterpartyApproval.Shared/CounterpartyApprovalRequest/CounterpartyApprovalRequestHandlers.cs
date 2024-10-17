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

    public override void InternalApprovalStateChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      base.InternalApprovalStateChanged(e);
      
      Functions.CounterpartyApprovalRequest.UpdateStatus(_obj, _obj.LifeCycleState, e.NewValue);
    }

    public override void LifeCycleStateChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      base.LifeCycleStateChanged(e);
      
      Functions.CounterpartyApprovalRequest.UpdateStatus(_obj, e.NewValue, _obj.InternalApprovalState);
    }

    public virtual void LegalAddressChanged(Sungero.Domain.Shared.StringPropertyChangedEventArgs e)
    {
      // Заполнить почтовый адрес в соответствии с юрид. адресом.
      if (!string.IsNullOrWhiteSpace(e.NewValue) &&
          (string.IsNullOrWhiteSpace(_obj.PostalAddress) || _obj.PostalAddress == e.OldValue))
        _obj.PostalAddress = e.NewValue;
    }

    public virtual void RegionChanged(OMYA.CounterpartyApproval.Shared.CounterpartyApprovalRequestRegionChangedEventArgs e)
    {
      // Очистить город при смене региона.
      if (!Equals(e.NewValue, e.OldValue) && _obj.City != null && !_obj.City.Region.Equals(e.NewValue))
        _obj.City = null;
    }

    public virtual void CityChanged(OMYA.CounterpartyApproval.Shared.CounterpartyApprovalRequestCityChangedEventArgs e)
    {
      // Установить регион в соответствии с городом.
      if (e.NewValue != null)
        _obj.Region = e.NewValue.Region;
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