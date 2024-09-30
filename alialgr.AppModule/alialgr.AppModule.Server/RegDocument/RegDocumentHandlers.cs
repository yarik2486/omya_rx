using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.AppModule.RegDocument;

namespace alialgr.AppModule
{
  partial class RegDocumentLeadingDocumentPropertyFilteringServerHandler<T>
  {

    public override IQueryable<T> LeadingDocumentFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = base.LeadingDocumentFiltering(query, e);
      //return query;
      return query.Where(d => !Equals(d, _obj) &&  RegDocuments.Is(d) );
    }
  }


  partial class RegDocumentServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
     
    //Значения по дефолту      
      _obj.Version=1;
      
    }
  }

}