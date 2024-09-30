using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RegDocument;

namespace alialgr.AppModule
{
  partial class RegDocumentClientHandlers
  {

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      if(_obj.DocumentKind != null)
      {
        var docKind = alialgr.CustomDev.DocumentKinds.As(_obj.DocumentKind);
        
        #region Обязательность
        
        _obj.State.Properties.AreasApplication.IsRequired = docKind.RDAreasAppalialgr.GetValueOrDefault();
        _obj.State.Properties.Function.IsRequired = docKind.RDFunctionalialgr.GetValueOrDefault();
        _obj.State.Properties.Confidentiality.IsRequired = docKind.RDConfidentalialgr.GetValueOrDefault();
        _obj.State.Properties.Language.IsRequired = docKind.RDLanguagealialgr.GetValueOrDefault();
        _obj.State.Properties.Version.IsRequired = docKind.RDVersionalialgr.GetValueOrDefault();
        _obj.State.Properties.Subject.IsRequired = docKind.RDSubjectalialgr.GetValueOrDefault();
        _obj.State.Properties.OurSignatory.IsRequired = docKind.RDOurSignatoryalialgr.GetValueOrDefault();
        _obj.State.Properties.ValidFrom.IsRequired = docKind.RDValidFromalialgr.GetValueOrDefault();
        #endregion
        
        #region Видимость
         _obj.State.Properties.LeadingDocument.IsVisible = docKind.RDLeadDocValialgr.GetValueOrDefault();
         _obj.State.Properties.ValidFrom.IsVisible = docKind.RDValidFromValialgr.GetValueOrDefault();
          _obj.State.Properties.ValidTill.IsVisible = docKind.RDValidTillValialgr.GetValueOrDefault();
        #endregion
      }
    }

  }
}