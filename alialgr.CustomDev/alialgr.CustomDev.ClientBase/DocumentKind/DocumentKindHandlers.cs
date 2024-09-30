using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.DocumentKind;

namespace alialgr.CustomDev
{
  partial class DocumentKindClientHandlers
  {

    public override void DocumentTypeValueInput(Sungero.Docflow.Client.DocumentKindDocumentTypeValueInputEventArgs e)
    {
      base.DocumentTypeValueInput(e);
      PublicFunctions.DocumentKind.SetRequirementAndVisibility(_obj);
    }

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      PublicFunctions.DocumentKind.SetRequirementAndVisibility(_obj);
    }

  }
}