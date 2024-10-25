using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace OMYA.CounterpartyApproval.Client
{
  public class ModuleFunctions
  {

    /// <summary>
    /// Создать заявку.
    /// </summary>
    public virtual void CreateRequest()
    {
      Sungero.Docflow.IncomingDocumentBases.CreateDocumentWithCreationDialog(CounterpartyApprovalRequests.Info,
                                                                             CounterpartyChangeRequests.Info,
                                                                             Sungero.Docflow.CounterpartyDocuments.Info);
    }

  }
}