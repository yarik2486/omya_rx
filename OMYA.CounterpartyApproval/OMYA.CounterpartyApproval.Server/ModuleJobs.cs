using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution;

namespace OMYA.CounterpartyApproval.Server
{
  public class ModuleJobs
  {

    /// <summary>
    /// Обновление сведений.
    /// </summary>
    public virtual void UpdatingInformation()
    {
      Logger.Debug("UpdatingInformation. Start");
      
      var requests = CounterpartyApprovalRequests.GetAll(x => x.Counterparty != null &&
                                                         Companies.As(x.Counterparty).LastUpdatedDateOMYA.HasValue &&
                                                         Companies.As(x.Counterparty).LastUpdatedDateOMYA.Value < Calendar.Today.AddYears(-2));
      
      Logger.DebugFormat("UpdatingInformation. RequestsCount = {0}", requests.Count());
      
      foreach (var request in requests)
      {
        try
        {
          Logger.DebugFormat("UpdatingInformation. StartTask RequestId = {0}", request.Id);
          var task = CounterpartyUpdatingInformationTasks.Create();
          task.Subject = OMYA.CounterpartyApproval.Resources.CheckTheCounterpartyDetails;
          task.AttachmentGroup.All.Add(request.Counterparty);
          task.Performer = request.PreparedBy;
          task.Start();
        }
        catch (Exception exp)
        {
          Logger.ErrorFormat("UpdatingInformation. ERORR RequestId = {0}", exp, request.Id);
        }
      }
      
      Logger.Debug("UpdatingInformation. End");
    }

  }
}