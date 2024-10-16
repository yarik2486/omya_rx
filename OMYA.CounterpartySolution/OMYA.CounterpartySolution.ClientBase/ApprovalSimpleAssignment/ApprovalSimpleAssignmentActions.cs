using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalSimpleAssignment;

namespace OMYA.CounterpartySolution.Client
{
  partial class ApprovalSimpleAssignmentActions
  {
    public virtual void SendInvitationOMYA(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var document = _obj.DocumentGroup.OfficialDocuments.FirstOrDefault();
      if (document == null)
      {
        e.AddError(OMYA.CounterpartySolution.ApprovalSimpleAssignments.Resources.NotContainsDocument);
        return;
      }
      
      var request = CounterpartyApproval.CounterpartyApprovalRequests.As(document);
      if (request == null)
      {
        e.AddError(OMYA.CounterpartySolution.ApprovalSimpleAssignments.Resources.DocumentNotRequest);
        return;
      }
      
      if (request.Counterparty == null)
      {
        e.AddError(OMYA.CounterpartySolution.ApprovalSimpleAssignments.Resources.NoCounterpartyInRequest);
        return;
      }
      
      var counterparty = request.Counterparty;
      var info = Functions.Company.ValidateExchangeAction(Companies.As(counterparty), e);
      if (!info.CanDoAction)
        return;

      var dialog = Dialogs.CreateInputDialog(Sungero.Parties.Counterparties.Resources.InvitationTitle);
      
      var box = dialog.AddSelect(counterparty.Info.Properties.ExchangeBoxes.Properties.Box.LocalizedName, true, info.DefaultBox).From(info.Boxes);
      
      var company = Sungero.Parties.CompanyBases.As(counterparty);
      var counterpartyInfo = dialog.AddString(Sungero.Parties.Counterparties.Resources.ExchangeDialogCounterpartyInfo, false);
      counterpartyInfo.IsEnabled = false;
      counterpartyInfo.Value = string.Format("{0}/{1}", counterparty.TIN, company != null ? company.TRRC : string.Empty);
      
      var counterpartyBranchId = dialog.AddString(Sungero.Parties.Counterparties.Resources.ExchangeDialogCounterpartyBranchId, false);
      var isSbis = Sungero.ExchangeCore.ExchangeService.ExchangeProvider.Sbis == box.Value?.ExchangeService.ExchangeProvider;
      counterpartyBranchId.IsEnabled = isSbis;

      var operatorCode = dialog.AddString(Sungero.Parties.Counterparties.Resources.ExchangeDialogOperatorCode, false);
      operatorCode.IsEnabled = isSbis;
      
      var comment = dialog.AddMultilineString(Sungero.Parties.Counterparties.Resources.InvitationMessageHeader, false,
                                              Sungero.Parties.Counterparties.Resources.InvitationMessageDefault);

      box.SetOnValueChanged((args) => {
                              isSbis = Sungero.ExchangeCore.ExchangeService.ExchangeProvider.Sbis == args.NewValue?.ExchangeService.ExchangeProvider;
                              counterpartyBranchId.IsEnabled = isSbis;
                              operatorCode.IsEnabled = isSbis;
                              
                              counterpartyBranchId.Value = string.Empty;
                              operatorCode.Value = string.Empty;
                            });
      
      dialog.HelpCode = Sungero.Parties.Constants.Counterparty.HelpCodes.SendInvitation;
      var sendButton = dialog.Buttons.AddCustom(Sungero.Parties.Counterparties.Resources.ExchangeDialogButtonSend);
      dialog.Buttons.AddCancel();
      dialog.Buttons.Default = sendButton;
      dialog.SetOnButtonClick(x =>
                              {
                                if (x.Button == sendButton && x.IsValid && e.Validate())
                                {
                                  var operatorCodeValue = !string.IsNullOrEmpty(operatorCode.Value) ? operatorCode.Value.Trim() : operatorCode.Value;
                                  
                                  if (!string.IsNullOrEmpty(operatorCodeValue) && operatorCodeValue.Length != 3)
                                    x.AddError(Sungero.Parties.Counterparties.Resources.ExchangeDialogOperatorCodeError);
                                  else
                                  {
                                    var result = Sungero.ExchangeCore.PublicFunctions.BusinessUnitBox.Remote.SendInvitation(box.Value, counterparty, operatorCodeValue, counterpartyBranchId.Value, comment.Value);
                                    if (!string.IsNullOrWhiteSpace(result))
                                      x.AddError(result);
                                    else
                                    {
                                      var counterpartyExchangeBox = counterparty.ExchangeBoxes.FirstOrDefault(b => Equals(b.Box, box.Value));
                                      Dialogs.NotifyMessage(counterpartyExchangeBox.Info.Properties.Status.GetLocalizedValue(counterpartyExchangeBox.Status));
                                    }
                                  }
                                }
                              });
      dialog.Show();
    }

    public virtual bool CanSendInvitationOMYA(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}