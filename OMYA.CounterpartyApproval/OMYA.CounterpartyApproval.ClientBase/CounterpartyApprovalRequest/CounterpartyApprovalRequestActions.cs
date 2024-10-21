using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;
using Sungero.Parties;

namespace OMYA.CounterpartyApproval.Client
{
  partial class CounterpartyApprovalRequestActions
  {
    public virtual void FillFromService(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      if (string.IsNullOrWhiteSpace(_obj.PSRN) && string.IsNullOrWhiteSpace(_obj.TIN) && string.IsNullOrWhiteSpace(_obj.ShortName))
      {
        e.AddError(CompanyBases.Resources.ErrorNeedFillTinPsrnNameForService);
        return;
      }
      
      if (!string.IsNullOrEmpty(_obj.PSRN))
        _obj.PSRN = _obj.PSRN.Trim();
      
      if (!string.IsNullOrEmpty(_obj.TIN))
        _obj.TIN = _obj.TIN.Trim();
      
      var response = Functions.CounterpartyApprovalRequest.Remote.FillFromServiceCustom(_obj, string.Empty);
      var error = response.Message;
      
      var companyDisplayValues = response.CompanyDisplayValues;
      if (companyDisplayValues != null && companyDisplayValues.Count < 1)
      {
        Dialogs.ShowMessage(CompanyBases.Resources.ErrorCompanyNotFoundInService, MessageType.Information);
        return;
      }
      
      if (!string.IsNullOrWhiteSpace(error))
      {
        Dialogs.ShowMessage(error, CompanyBases.Resources.ContactAdministrator, MessageType.Error);
        return;
      }
      
      if (response.Amount > 1)
      {
        const int MaxCompaniesCount = 25;
        var dialogText = response.Amount > MaxCompaniesCount
          ? CompanyBases.Resources.FoundMoreThanNCompaniesInServiceFormat(MaxCompaniesCount, response.Amount)
          : CompanyBases.Resources.FoundSeveralCompaniesInServiceFormat(response.Amount);
        var dialog = Dialogs.CreateInputDialog(CompanyBases.Resources.ChoseCompanyDialogTitle, dialogText);
        dialog.Buttons.AddOkCancel();
        var companyShortLabels = companyDisplayValues.Select(x => x.DisplayValue).Take(MaxCompaniesCount);
        var companyShortLabel = dialog.AddSelect(CompanyBases.Resources.FillFrom, true).From(companyShortLabels.ToArray());
        
        var result = dialog.Show();
        if (result == DialogButtons.Ok)
        {
          var companyDisplayValue = companyDisplayValues.Where(x => x.DisplayValue == companyShortLabel.Value).First();
          response = Functions.CounterpartyApprovalRequest.Remote.FillFromServiceCustom(_obj, companyDisplayValue.PSRN);
          error = response.Message;
          if (!string.IsNullOrWhiteSpace(error))
          {
            Dialogs.ShowMessage(error, MessageType.Error);
            return;
          }
        }
        else
          return;
      }
      
      if (string.IsNullOrWhiteSpace(error) && response.CompanyDisplayValues != null)
      {
        Dialogs.NotifyMessage(CompanyBases.Resources.FillFromServiceSuccess);
      }
    }

    public virtual bool CanFillFromService(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.AccessRights.CanUpdate();
    }

    public virtual void CreateCompany(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      _obj.Save();
      var company = Functions.CounterpartyApprovalRequest.Remote.CreateCompany(_obj);
      company.ShowModal();
      
      if (!company.State.IsInserted)
      {
        _obj.Counterparty = company;
        _obj.Save();
      }
    }

    public virtual bool CanCreateCompany(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

    public virtual void CreateChecklist(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var checklist = Functions.CounterpartyApprovalRequest.Remote.CreateChecklist();
      checklist.LeadingDocument = _obj;
      checklist.ShowModal();
    }

    public virtual bool CanCreateChecklist(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}