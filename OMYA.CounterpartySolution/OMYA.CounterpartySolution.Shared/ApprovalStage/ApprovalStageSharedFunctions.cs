using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalStage;

namespace OMYA.CounterpartySolution.Shared
{
  partial class ApprovalStageFunctions
  {

    /// <summary>
    /// Установить видимость свойств.
    /// </summary>
    public override void SetPropertiesVisibility()
    {
      base.SetPropertiesVisibility();
      
      _obj.State.Properties.StepForSendingCounterpartyOMYA.IsVisible = _obj.StageType == StageType.SimpleAgr;
    }
  }
}