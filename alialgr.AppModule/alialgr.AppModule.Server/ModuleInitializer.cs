using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;

namespace alialgr.AppModule.Server
{
  public partial class ModuleInitializer
  {

    public override bool IsModuleVisible()
    {
      //INK Обложка видна только системным пользователям
      if (Users.Current.IsSystem==true)
        return true;
      
      return false;
    }

    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      CreateRole("Финансовый контролер");
      CreateRole("Инициатор запроса CAR&OEC");
      CreateDocumentTypes();
      CreateCalculateRole(AppModule.CalculateRole.Type.InitiatorInCard , "Инициатор документа");
      CreateCalculateRole(AppModule.CalculateRole.Type.SignCat , "Подписывающий по категории");
      CreateCalculateRole(AppModule.CalculateRole.Type.FinController , "Финансовый контролер");
    }
    
    /// <summary>
    /// Создание Роли.
    /// </summary>
    public static void CreateRole(string name)
    {
      var role = Sungero.CoreEntities.Roles.GetAll().Where(r => Equals(r.Name, name)).FirstOrDefault();
      if (role == null)
      {
        role =Sungero.CoreEntities.Roles.Create();
        role.Name = name;
        role.Save();
      }
    }
    
    
    /// <summary>
    /// Создание вычисляемой роли.
    /// </summary>
    public static void CreateCalculateRole(Enumeration roleType, string description)
    {

      var role = CalculateRoles.GetAll().Where(r => Equals(r.Type, roleType)).FirstOrDefault();
      // Проверяет наличие роли.
      if (role == null)
      {
        role = CalculateRoles.Create();
        role.Type = roleType;
      }
      role.Description = description;
      role.Save();
      InitializationLogger.Debug("Создана роль '"+description+"'");
    }
    
    /// <summary>
    /// Создание в служебном справочнике "Типы документов".
    /// </summary>
    public static void CreateDocumentTypes()
    {
      ///тип документа "Нормативный документ"
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentType("Нормативный документ", RegDocument.ClassTypeGuid, Sungero.Docflow.DocumentType.DocumentFlow.Inner, true);
      
      ///тип документа "Запрос CAR&OEC"
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentType("Запрос CAR&OEC", RequestCARandOEC.ClassTypeGuid, Sungero.Docflow.DocumentType.DocumentFlow.Inner, true);
      
      
      
    }
    
    
    
  }
}
