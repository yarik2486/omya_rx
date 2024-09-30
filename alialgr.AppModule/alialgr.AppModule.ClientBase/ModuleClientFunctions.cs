using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace alialgr.AppModule.Client
{
  public class ModuleFunctions
  {

   /// <summary>
    /// 
    /// </summary>
    public virtual void ContractualDocFix()
    {
      var contracts = Sungero.Contracts.Contracts.GetAll();
      var Cnt=contracts.Count();
      contracts = Sungero.Contracts.Contracts.GetAll(d=> d.LifeCycleState==null);
      var CntSel=contracts.Count();
      
      foreach(var c in contracts)
      {
        //c.Note= "ok";
        c.LifeCycleState =   Sungero.Contracts.Contract.LifeCycleState.Active;
        c.ValidFrom=null;
        c.ValidTill=null;
        c.Save();
      }
      
      
      Dialogs.ShowMessage("Всего документов: "+ Cnt.ToString() ,

                          "Выбрано для корректировки: " +CntSel.ToString(),

                          MessageType.Information, "Завершено");
      
    }
  }
}