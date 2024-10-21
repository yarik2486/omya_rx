using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.Checklist;

namespace OMYA.CounterpartyApproval
{
  partial class ChecklistServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      var leadingDocumentChanged = _obj.LeadingDocument != null && !Equals(_obj.LeadingDocument, _obj.State.Properties.LeadingDocument.OriginalValue);
      if (leadingDocumentChanged)
      {
        // Проверить, доступен ли для изменения ведущий документ.
        var isLeadingDocumentDisabled = Sungero.Docflow.PublicFunctions.OfficialDocument.NeedDisableLeadingDocument(_obj);
        if (isLeadingDocumentDisabled)
          e.AddError(Sungero.Docflow.OfficialDocuments.Resources.RelationPropertyDisabled);
      }
      
      base.BeforeSave(e);
      
      if (_obj.LeadingDocument != null && leadingDocumentChanged && _obj.AccessRights.StrictMode != AccessRightsStrictMode.Enhanced)
      {
        var accessRightsLimit = Sungero.Docflow.PublicFunctions.OfficialDocument.GetAvailableAccessRights(_obj);
        if (accessRightsLimit != Guid.Empty)
          Sungero.Docflow.PublicFunctions.OfficialDocument.CopyAccessRightsToDocument(_obj.LeadingDocument, _obj, accessRightsLimit);
      }
      
      if (_obj.LeadingDocument != null && _obj.LeadingDocument.AccessRights.CanRead() &&
          !_obj.Relations.GetRelatedFromDocuments(Sungero.Docflow.Constants.Module.AddendumRelationName).Any(x => x.Id == _obj.LeadingDocument.Id))
        _obj.Relations.AddFromOrUpdate(Sungero.Docflow.Constants.Module.AddendumRelationName, _obj.State.Properties.LeadingDocument.OriginalValue, _obj.LeadingDocument);
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      _obj.JobTitle = _obj.PreparedBy?.JobTitle;
      _obj.InitiatorEmail = _obj.PreparedBy?.Email;
    }
  }

}