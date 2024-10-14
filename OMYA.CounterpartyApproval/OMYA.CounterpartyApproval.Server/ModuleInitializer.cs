using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;
using Sungero.Domain;
using Sungero.Domain.Shared;

namespace OMYA.CounterpartyApproval.Server
{
  public partial class ModuleInitializer
  {

    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      CreateDocumentTypes();
      CreateDocumentKinds();
      CreateRoles();
      
      // Выдача прав всем пользователям.
      var allUsers = Roles.AllUsers;
      if (allUsers != null)
      {
        // Документы.
        InitializationLogger.Debug("Init CounterpartyApproval: Grant rights on documents to all users.");
        GrantRightsOnDocuments(allUsers);
      }
    }
    
    /// <summary>
    /// Создать типы документов.
    /// </summary>
    public static void CreateDocumentTypes()
    {
      InitializationLogger.Debug("Init CounterpartyApproval: Create document types");
      
      // Заявка на одобрение контрагента.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentType(Resources.RequestForCounterpartyApproval, 
                                                                              CounterpartyApprovalRequest.ClassTypeGuid, 
                                                                              Sungero.Docflow.DocumentType.DocumentFlow.Inner, 
                                                                              true);
      
      // Чек-лист для одобрения контрагента
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentType(Resources.CounterpartyApprovalChecklistKind, 
                                                                              Checklist.ClassTypeGuid, 
                                                                              Sungero.Docflow.DocumentType.DocumentFlow.Inner, 
                                                                              true);
    }
    
    /// <summary>
    /// Создать виды документов.
    /// </summary>
    public static void CreateDocumentKinds()
    {
      InitializationLogger.Debug("Init CounterpartyApproval: Create document kinds.");
      
      var registrable = Sungero.Docflow.DocumentKind.NumberingType.Registrable;
      var numerable = Sungero.Docflow.DocumentKind.NumberingType.Numerable;
      var notNumerable = Sungero.Docflow.DocumentKind.NumberingType.NotNumerable;
      
      var actions = new Sungero.Domain.Shared.IActionInfo[] {
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendActionItem,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForFreeApproval,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForDocumentFlow,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForAcquaintance,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForApproval };
      
      // Заявка на одобрение контрагента.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentKind(Resources.RequestForCounterpartyApproval,
                                                                              Resources.RequestForCounterpartyApproval,
                                                                              registrable,
                                                                              Sungero.Docflow.DocumentType.DocumentFlow.Inner, 
                                                                              true, 
                                                                              false,
                                                                              CounterpartyApprovalRequest.ClassTypeGuid,
                                                                              actions,
                                                                              Constants.Module.Initialize.RequestForCounterpartyApprovalKind, 
                                                                              true);
      
      // Чек-лист для одобрения контрагента
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentKind(Resources.CounterpartyApprovalChecklistKind,
                                                                              Resources.CounterpartyApprovalChecklistKind,
                                                                              registrable,
                                                                              Sungero.Docflow.DocumentType.DocumentFlow.Inner, 
                                                                              true, 
                                                                              false,
                                                                              Checklist.ClassTypeGuid,
                                                                              actions,
                                                                              Constants.Module.Initialize.ChecklistKind, 
                                                                              true);
    }
    
    /// <summary>
    /// Выдать права всем пользователям на документы.
    /// </summary>
    /// <param name="allUsers">Группа "Все пользователи".</param>
    public static void GrantRightsOnDocuments(IRole allUsers)
    {
      InitializationLogger.Debug("Init CounterpartyApproval: Grant rights on documents to all users.");
      
      CounterpartyApprovalRequests.AccessRights.Grant(allUsers, DefaultAccessRightsTypes.Create);
      CounterpartyApprovalRequests.AccessRights.Save();
      
      Checklists.AccessRights.Grant(allUsers, DefaultAccessRightsTypes.Create);
      Checklists.AccessRights.Save();
    }
    
    /// <summary>
    /// Создать предопределенные роли.
    /// </summary>
    public static void CreateRoles()
    {
      InitializationLogger.Debug("Init CounterpartyApproval: Create Default Roles");
      
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateRole(Resources.MasterDataSpecialistName, Resources.MasterDataSpecialistName, Constants.Module.Initialize.MasterDataSpecialist);
    }
  }
}
