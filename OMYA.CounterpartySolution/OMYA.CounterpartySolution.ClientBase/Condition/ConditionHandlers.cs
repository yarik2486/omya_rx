using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.Condition;

namespace OMYA.CounterpartySolution
{
  partial class ConditionClientHandlers
  {

    public override void ConditionTypeValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      base.ConditionTypeValueInput(e);
      
      if (e.NewValue == ConditionType.CheckEDIOMYA)
        _obj.Note = OMYA.CounterpartySolution.Conditions.Resources.CheckEDINote;
      else
        _obj.Note = string.Empty;
    }

  }
}