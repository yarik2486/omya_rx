using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ApprovalAssignment;

namespace alialgr.CustomDev.Shared
{
  partial class ApprovalAssignmentFunctions
  {
    public bool Cansign()
    {
      bool result = false;
      if (_obj.Stage.Name == alialgr.AppModule.PublicConstants.Module.PreSign || _obj.Stage.Name == alialgr.AppModule.PublicConstants.Module.PreSignRes
          || _obj.Stage.Name == alialgr.AppModule.PublicConstants.Module.PreSignSaleKarat
          ||_obj.Stage.Name == alialgr.AppModule.PublicConstants.Module.PreSignSaleMramorecs
          ||_obj.Stage.Name == alialgr.AppModule.PublicConstants.Module.PreSignSaleOMIYA)
        result = true;
      return result;
    }
  }
}