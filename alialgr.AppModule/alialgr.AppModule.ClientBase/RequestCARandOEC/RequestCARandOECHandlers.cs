using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RequestCARandOEC;

namespace alialgr.AppModule
{
  partial class RequestCARandOECClientHandlers
  {

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      
      // Скрыть поля (не стал удалять просто скрыл)
      _obj.State.Properties.DocumentRegister.IsVisible=false;
      _obj.State.Properties.DeliveryMethod.IsVisible=false;
      
      _obj.State.Properties.CaseFile.IsVisible=false;
      _obj.State.Properties.PlacedToCaseFileDate.IsVisible=false;
      _obj.State.Properties.PaperCount.IsVisible=false;
      _obj.State.Properties.StoredIn.IsVisible=false;

      _obj.State.Properties.VerificationState.IsVisible=false;
      _obj.State.Properties.RegistrationState.IsVisible=false;
      _obj.State.Properties.ExecutionState.IsVisible=false;
      _obj.State.Properties.ControlExecutionState.IsVisible=false;
      _obj.State.Properties.ExchangeState.IsVisible=false;
      
      
      if(_obj.DocumentKind != null)
      {
        var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);
        
        #region Обязательность
        _obj.State.Properties.Country.IsRequired = docKind.COCountryalialgr.GetValueOrDefault();
        _obj.State.Properties.AreaApp.IsRequired = docKind.COAreaAppalialgr.GetValueOrDefault();
        _obj.State.Properties.ProjAmount.IsRequired = docKind.COProjAmountalialgr.GetValueOrDefault();
        _obj.State.Properties.Currency.IsRequired = docKind.COCuralialgr.GetValueOrDefault();
        _obj.State.Properties.OurSignatory.IsRequired = docKind.COOurSignalialgr.GetValueOrDefault();
        #endregion
        
        #region Видимость
        _obj.State.Properties.OurSignatory.IsVisible = docKind.COOurSignalialgr.GetValueOrDefault();
        #endregion
        
        #region Ограничить редактирование
        _obj.State.Properties.Department.IsEnabled =!docKind.CODepartalialgr.GetValueOrDefault();
        _obj.State.Properties.PreparedBy.IsEnabled =!docKind.COPrepByalialgr.GetValueOrDefault();
        #endregion
        
        
      }
    }
  }
}