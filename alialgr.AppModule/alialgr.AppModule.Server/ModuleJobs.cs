using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace alialgr.AppModule.Server
{
  public class ModuleJobs
  {

    /// <summary>
    /// 
    /// </summary>
    public virtual void FillCPartybyTIN()
    {

      Logger.Debug("Start  fill counterparty proccess.");
      var companyList = Functions.Module.GetCompanyForFillJob();
      Logger.Debug(" Try to fill " + companyList.Count.ToString() + " companies");
      var filledCompanyCount = 0;
      if (companyList.Any())
      {
        foreach (var item in companyList)
        {
          var name = item.Name;
          item.Note = string.Empty;
          Logger.Debug(" Trying to check info : " + item.Id.ToString());
          var response = Functions.Module.FillFromServicealialgr(item, string.Empty);
          Logger.Debug(". Fill counter party result : " + response);
          if (response.Contains("is filled"))
            filledCompanyCount++;
          item.Note = name +"; " + item.Note ;
          item.Save();
        }
        Logger.Debug(" Filled " + filledCompanyCount.ToString() + " from " + companyList.Count.ToString());
      }
         
      
      
    }

  }
}