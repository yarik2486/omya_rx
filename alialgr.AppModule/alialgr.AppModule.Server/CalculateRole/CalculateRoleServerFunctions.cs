using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.CalculateRole;

namespace alialgr.AppModule.Server
{
  partial class CalculateRoleFunctions
  {

    /// <summary>
    /// Получение согласующего
    /// </summary>
    /// <returns>Сотрудник</returns>
    public override Sungero.Company.IEmployee GetRolePerformer(Sungero.Docflow.IApprovalTask task)
    {
      
      //INK вычисление Финансовый контролер
      if (_obj.Type == alialgr.AppModule.CalculateRole.Type.FinController)
      {
        var doc= alialgr.AppModule.RequestCARandOECs.As(task.DocumentGroup.OfficialDocuments.FirstOrDefault());
        if (doc!=null)
          return doc.FinController;
        
      }


      if (_obj.Type == alialgr.AppModule.CalculateRole.Type.InitiatorInCard)
      {
        //INK вычисление инициатора из карточки документа
        var doc= alialgr.CustomDev.ContractualDocuments.As(task.DocumentGroup.OfficialDocuments.FirstOrDefault());
        if (doc!=null)
          return doc.InitDalialgr;
        
      }
      
      if (_obj.Type == alialgr.AppModule.CalculateRole.Type.SignCat)
      {
        //INK вычисление подписывающего по категории
        //Logger.Debug("INK0 заача {id} успешно сохранен", _obj.Id);
        var doc = alialgr.CustomDev.ContractualDocuments.As(task.DocumentGroup.OfficialDocuments.FirstOrDefault());
        if (doc!=null)
        {
          if(alialgr.CustomDev.SupAgreements.Is(doc))
          {
            var supAgr = alialgr.CustomDev.SupAgreements.As(doc);
            var leadDoc = supAgr.LeadingDocument;
            if(leadDoc != null)
            {
              var cat = leadDoc.DocumentGroup;
              var res = Sungero.Docflow.SignatureSettings.GetAll().Where(r=> r.Categories.Any(c=> c.Category.Equals(cat))).FirstOrDefault();
              if (res!= null)
              {
                return Sungero.Company.Employees.As(res.Recipient);
              }
            }
          }
          else
          {
            var cat = doc.DocumentGroup;
            var res = Sungero.Docflow.SignatureSettings.GetAll().Where(r=> r.Categories.Any(c=> c.Category.Equals(cat))).FirstOrDefault();
            if (res!= null)
            {
              return Sungero.Company.Employees.As(res.Recipient);
            }
          }
          //          var cat = doc.DocumentGroup;
          //          //Logger.Debug("INK0 Категория {id} найдена", cat.Name);
          //          var res = Sungero.Docflow.SignatureSettings.GetAll().Where(r=> r.Categories.Any(c=> c.Category.Equals(cat))).FirstOrDefault();
//
          //          //x => x.ApprovalRole.Type == Purchases.PurchaseApprovalRole.Type.Experts
          //          if (res!= null)
          //          {
          //            //Logger.Debug("INK0 право подписи {id} найдена", res.Name);
          //            return Sungero.Company.Employees.As(res.Recipient);
          //          }
          //          // else
          //          //Logger.Debug("INK0 не найдена запись права подписи");
        }
      }

      
      
      return base.GetRolePerformer(task);
    }
    
    

  }
}