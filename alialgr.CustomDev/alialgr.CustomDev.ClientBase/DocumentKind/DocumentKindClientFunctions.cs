using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.DocumentKind;
using Sungero.Metadata;
using Sungero.Domain.Shared;

namespace alialgr.CustomDev.Client
{
  partial class DocumentKindFunctions
  {
    [Public]
    public virtual void SetRequirementAndVisibility()
    {
      #region Проверка на то, что вид документа от наследника договорного документа 
      var contractualDocumentGuid = Guid.Parse("59079f7f-a326-4947-bbd6-0ae6dfb5f59b"); //IncomingDocumentBase
      var contractualDocumentMetadata = Sungero.Metadata.Services.MetadataSearcher.FindEntityMetadata(contractualDocumentGuid);
      
      var contractualDocument = (_obj.DocumentType != null &&
                                 contractualDocumentMetadata
                                 .IsAncestorFor(Sungero.Domain.Shared.TypeExtension.GetTypeByGuid(Guid.Parse(_obj.DocumentType.DocumentTypeGuid)).GetEntityMetadata()));
      
      
        #endregion
      
      #region Проверка на то, что документ "Нормативный документ"   
      
      var regDocument =(_obj.DocumentType?.DocumentTypeGuid == "116129f8-bc91-4327-b04f-f36346148666" );
      
      #endregion
      
      #region Проверка на то, что документ Запрос CAR&OEC
      var isCARandOEC = (_obj.DocumentType?.DocumentTypeGuid == "33344524-7745-49b7-8e96-a92e185793e4" );
      #endregion

      #region Проверка на то, что документ Доп. соглашение
      var isSupAgr = (_obj.DocumentType?.DocumentTypeGuid == "9d8924c3-6eb0-475f-aabe-5aa81e173cd1" || _obj.DocumentType?.DocumentTypeGuid =="265f2c57-6a8a-4a15-833b-ca00e8047fa5" );
      #endregion

      


      #region Настройки для договорных документов
      _obj.State.Properties.CurrencyReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.InitDocReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.CounterpartySignatoryReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.TotalAmountReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.ValidFromReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.ValidTillReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.VatRateReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.OurSignatoryReqalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.WithResaleVisalialgr.IsVisible = contractualDocument;
      _obj.State.Properties.IsFrameworkalialgr.IsVisible = contractualDocument;
      #endregion
      
      #region Настройки для "Нормативный документ"
      //      _obj.State.Properties.RDAreasAppalialgr.IsVisible = regDocument;
      //      _obj.State.Properties.RDLeadDocValialgr.IsVisible = regDocument;

      _obj.State.Properties.RDAreasAppalialgr.IsVisible = regDocument;
      _obj.State.Properties.RDFunctionalialgr.IsVisible = regDocument;
      _obj.State.Properties.RDConfidentalialgr.IsVisible = regDocument;
      _obj.State.Properties.RDLanguagealialgr.IsVisible = regDocument;
      _obj.State.Properties.RDVersionalialgr.IsVisible = regDocument;
      _obj.State.Properties.RDSubjectalialgr.IsVisible = regDocument;
      _obj.State.Properties.RDOurSignatoryalialgr.IsVisible = regDocument;
      _obj.State.Properties.RDValidFromalialgr.IsVisible = regDocument;

      _obj.State.Properties.RDLeadDocValialgr.IsVisible = regDocument;
      _obj.State.Properties.RDValidFromValialgr.IsVisible = regDocument;
      _obj.State.Properties.RDValidTillValialgr.IsVisible = regDocument;
      
      _obj.State.Properties.RDSendRegalialgr.IsVisible = regDocument;
      _obj.State.Properties.RDRegTempalialgr.IsVisible = regDocument;

      #endregion
      
      #region Настройки для "Запрос CAR&OEC"
      _obj.State.Properties.COCountryalialgr.IsVisible = isCARandOEC;
      _obj.State.Properties.COAreaAppalialgr.IsVisible = isCARandOEC;
      _obj.State.Properties.COProjAmountalialgr.IsVisible = isCARandOEC;
      _obj.State.Properties.COCuralialgr.IsVisible = isCARandOEC;

      _obj.State.Properties.COOurSignalialgr.IsVisible = isCARandOEC;

      _obj.State.Properties.CODepartalialgr.IsVisible = isCARandOEC;
      _obj.State.Properties.COPrepByalialgr.IsVisible = isCARandOEC;
      #endregion
      
      #region Настройки для Доп. соглашение
      _obj.State.Properties.SelRegOnLDalialgr.IsVisible =isSupAgr;
        #endregion
        

    }
  }
}