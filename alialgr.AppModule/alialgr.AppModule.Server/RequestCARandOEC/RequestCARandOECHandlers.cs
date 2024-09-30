using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RequestCARandOEC;

namespace alialgr.AppModule
{
  partial class RequestCARandOECServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
// Значения по умолчанию
_obj.Department = Sungero.Company.Employees.Current?.Department;
_obj.PreparedBy = Sungero.Company.Employees.Current;
  
    }
  }

  partial class RequestCARandOECFinControllerPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> FinControllerFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      var role = Sungero.CoreEntities.Roles.GetAll().Where(r => Equals(r.Name, "Финансовый контролер")).FirstOrDefault();
      var usersinGroup =  Sungero.CoreEntities.Roles.GetAllUsersInGroup(role);  
      return query.Where( x =>  usersinGroup.Contains(x) );
    }
  }


}