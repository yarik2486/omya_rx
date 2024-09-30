using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RegDocument;

namespace alialgr.AppModule.Shared
{
  partial class RegDocumentFunctions
  {

   /// <summary>
    /// Получить номер ведущего документа.
    /// </summary>
    /// <returns>Номер документа либо пустая строка.</returns>
    public override string GetLeadDocumentNumber()
    {
      // Виртуальная функция. Переопределено в потомках.
      //base.GetLeadDocumentNumber()
      
      return _obj.LeadingDocument?.RegistrationNumber;
    } 
   
    
    
    
    
  }
}