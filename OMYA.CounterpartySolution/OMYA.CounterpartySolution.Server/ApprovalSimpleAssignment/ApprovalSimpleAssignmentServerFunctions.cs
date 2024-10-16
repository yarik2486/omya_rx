using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.ApprovalSimpleAssignment;

namespace OMYA.CounterpartySolution.Server
{
  partial class ApprovalSimpleAssignmentFunctions
  {

    /// <summary>
    /// Получить этап задания.
    /// </summary>
    /// <param name="stageType">Тип этапа.</param>
    /// <returns>Этап, если подходит по типу.</returns>
    [Remote]
    public IApprovalStage GetStage(Enumeration stageType)
    {
      var stage = ApprovalTasks.As(_obj.Task).ApprovalRule.Stages
        .Where(s => s.Stage != null)
        .Where(s => s.Stage.StageType == stageType)
        .FirstOrDefault(s => s.Number == _obj.StageNumber);
      
      return ApprovalStages.As(stage);
    }
  }
}