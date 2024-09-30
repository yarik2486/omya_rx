using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.DocumentKind;

namespace alialgr.CustomDev
{
  partial class DocumentKindServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      
      _obj.SelRegOnLDalialgr=false; //Выбирать регламент по ведущему документу
      
      #region Настройки для "Нормативный документ"
      
      _obj.RDAreasAppalialgr=false;
      _obj.RDFunctionalialgr=false;
      _obj.RDConfidentalialgr=false;
      _obj.RDLanguagealialgr=false;
      _obj.RDVersionalialgr=false;
      _obj.RDSubjectalialgr=false;
      _obj.RDOurSignatoryalialgr=false;
      _obj.RDValidFromalialgr=false;

      _obj.RDLeadDocValialgr=false;
      _obj.RDValidFromValialgr=false;
      _obj.RDValidTillValialgr=false;

      _obj.RDSendRegalialgr=false;
      _obj.RDRegTempalialgr=false;
      #endregion
      
      #region Настройки для Запрос CAR&OEC
      _obj.COCountryalialgr=false;
      _obj.COAreaAppalialgr=false;
      _obj.COProjAmountalialgr=false;
      _obj.COCuralialgr=false;

      _obj.COOurSignalialgr=false;

      _obj.CODepartalialgr=false;
      _obj.COPrepByalialgr=false;
      
      #endregion
      
    }
  }

}