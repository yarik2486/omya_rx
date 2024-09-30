using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.ApprovalTask;

namespace alialgr.CustomDev.Server
{
  partial class ApprovalTaskFunctions
  {
        /// <summary>
    /// Определить текущий этап.
    /// </summary>
    /// <param name="task">Задача.</param>
    /// <param name="stageType">Тип этапа.</param>
    /// <returns>Текущий этап, либо null, если этапа нет (или это не тот этап).</returns>
    public static Sungero.Docflow.Structures.Module.DefinedApprovalStageLite GetStage(IApprovalTask task, Enumeration stageType)
    {
      var stage = task.ApprovalRule.Stages
        .Where(s => s.Stage != null)
        .Where(s => s.Stage.StageType == stageType)
        .FirstOrDefault(s => s.Number == task.StageNumber);
      
      if (stage != null)
        return Sungero.Docflow.Structures.Module.DefinedApprovalStageLite.Create(stage.Stage, stage.Number, stage.StageType);
      
      return null;
    }
  }
}