using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ApprovalStage;

namespace alialgr.CustomDev.Shared
{
  partial class ApprovalStageFunctions
  {
public override List<Enumeration?> GetPossibleRoles()
{
  var baseRoles = base.GetPossibleRoles();
      
  if (_obj.StageType == Sungero.Docflow.ApprovalStage.StageType.Approvers ||
      _obj.StageType == Sungero.Docflow.ApprovalStage.StageType.SimpleAgr ||
      _obj.StageType == Sungero.Docflow.ApprovalStage.StageType.Notice)
  {
    baseRoles.Add(alialgr.AppModule.CalculateRole.Type.InitiatorInCard);// инициатор из карточки документа
    baseRoles.Add(alialgr.AppModule.CalculateRole.Type.SignCat);//подписывающий по категории
    baseRoles.Add(alialgr.AppModule.CalculateRole.Type.FinController);//Финансовый контролер 
  }
  
      
  return baseRoles;
}
  }
}