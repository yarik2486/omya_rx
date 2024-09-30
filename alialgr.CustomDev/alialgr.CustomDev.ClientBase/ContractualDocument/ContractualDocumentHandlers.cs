using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ContractualDocument;

namespace alialgr.CustomDev
{
  partial class ContractualDocumentClientHandlers
  {

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      if(_obj.DocumentKind != null)
      {
        var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);
        
        #region Обязательность
        _obj.State.Properties.ValidFrom.IsRequired = docKind.ValidFromReqalialgr.GetValueOrDefault();
        _obj.State.Properties.ValidTill.IsRequired = docKind.ValidTillReqalialgr.GetValueOrDefault();
        _obj.State.Properties.Currency.IsRequired = docKind.CurrencyReqalialgr.GetValueOrDefault();
        _obj.State.Properties.TotalAmount.IsRequired = docKind.TotalAmountReqalialgr.GetValueOrDefault();
        _obj.State.Properties.OurSignatory.IsRequired = docKind.OurSignatoryReqalialgr.GetValueOrDefault();
        _obj.State.Properties.CounterpartySignatory.IsRequired = docKind.CounterpartySignatoryReqalialgr.GetValueOrDefault();
        _obj.State.Properties.VatRate.IsRequired = docKind.VatRateReqalialgr.GetValueOrDefault();
        _obj.State.Properties.InitDalialgr.IsRequired = docKind.InitDocReqalialgr.GetValueOrDefault();
        #endregion
        
        #region Видимость
        _obj.State.Properties.WithResalealialgr.IsVisible = docKind.WithResaleVisalialgr.GetValueOrDefault();
        #endregion
      }
    }

  }
}