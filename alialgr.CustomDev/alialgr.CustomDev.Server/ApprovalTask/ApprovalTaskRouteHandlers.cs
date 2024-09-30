using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Workflow;
using alialgr.CustomDev.ApprovalTask;

namespace alialgr.CustomDev.Server
{
  partial class ApprovalTaskRouteHandlers
  {

    public override void StartBlock6(Sungero.Docflow.Server.ApprovalAssignmentArguments e)
    {
      base.StartBlock6(e);
      
      var stage = Functions.ApprovalTask.GetStage(_obj, Sungero.Docflow.ApprovalStage.StageType.Approvers);
      if (stage == null)
        return;
      if (stage != null)
      {
        if(stage.Stage.Name.Contains(alialgr.AppModule.PublicConstants.Module.PreSign)
           || stage.Stage.Name.Contains(alialgr.AppModule.PublicConstants.Module.PreSignRes)
           || stage.Stage.Name.Contains(alialgr.AppModule.PublicConstants.Module.PreSignSaleKarat)
           || stage.Stage.Name.Contains(alialgr.AppModule.PublicConstants.Module.PreSignSaleMramorecs)
           || stage.Stage.Name.Contains(alialgr.AppModule.PublicConstants.Module.PreSignSaleOMIYA))
        {
          var document = _obj.DocumentGroup.OfficialDocuments.FirstOrDefault();
          if (document != null)
          {
            e.Block.Subject = "Подпишите: " + document.Name;
            var approvers = Sungero.Docflow.PublicFunctions.ApprovalStage.Remote.GetStagePerformers(Sungero.Docflow.ApprovalTasks.As(_obj), stage.Stage);
            foreach (var approver in approvers)
            {
              if(!e.Block.Performers.Contains(approver))
                e.Block.Performers.Add(approver);
            }
            
          }
          
        }
      }
    }

  }
}