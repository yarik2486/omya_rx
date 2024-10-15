using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.Condition;

namespace OMYA.CounterpartySolution.Server
{
  partial class ConditionFunctions
  {

    public override string GetConditionName()
    {
      using (TenantInfo.Culture.SwitchTo())
      {
        if (_obj.ConditionType == ConditionType.CheckEDIOMYA)
          return OMYA.CounterpartySolution.Conditions.Resources.CheckEDO;
      }
      
      return base.GetConditionName();
    }
  }
}