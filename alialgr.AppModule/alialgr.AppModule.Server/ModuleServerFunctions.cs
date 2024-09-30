//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Sungero.Core;
//using Sungero.CoreEntities;
//using Sungero.CompanyData;

using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using System.IO;
using Sungero.Domain.Shared;
using Sungero.CompanyData;
using System.Net;


namespace alialgr.AppModule.Server
{
  public class ModuleFunctions
  {
    #region Заполнение контрагента
    /// <summary>
    /// 
    /// Получить список организаций
    /// </summary>
    /// <returns>Список организаций.</returns>
    [Public, Remote(IsPure = true)]
    public  List<Sungero.Parties.ICompany> GetCompanyForFillJob()
    {
      return Sungero.Parties.Companies.GetAll().Where(a => a.Nonresident == false && a.IsCardReadOnly != true && (a.NCEA == null || a.NCEA == string.Empty) && (a.TIN != string.Empty || a.PSRN != string.Empty)).Take(130).ToList();  ////a.NCEA == string.Empty && (a.TIN != string.Empty || a.Name != string.Empty || a.PSRN != string.Empty)).Take(130).ToList();
    }
    
    
    /// <summary>
    /// Заполнить реквизиты контрагента из сервиса.
    /// </summary>
    /// <param name="specifiedPSRN">ОГРН выбранной организации.</param>
    /// <returns>Статус запроса, список представлений организаций, список контактов.</returns>
    [Public, Remote]
    public string FillFromServicealialgr(Sungero.Parties.ICompany currentCompany, string specifiedPSRN)
    {
      var result = string.Empty;
      
      
      var key = Sungero.Docflow.PublicConstants.Module.CompanyDataServiceKey; //ключ в базе по которому написан адрес сервиса
      var command = string.Format(Queries.Module.SelectCompanyDataService, key); //формируем команду на получение адреса сервиса
      var commandExecutionResult = Sungero.Docflow.PublicFunctions.Module.ExecuteScalarSQLCommand(command); //посылаем команду
      var serviceUrl = string.Empty;
      if (!(commandExecutionResult is DBNull) && commandExecutionResult != null)
        serviceUrl = commandExecutionResult.ToString(); //получаем ответ
      var url = serviceUrl; // адрес сервиса для поиска
      
      //INK Debug
      Logger.Debug("INK SericeUrl: " + serviceUrl);
      
      if (string.IsNullOrEmpty(url))
        return Sungero.Parties.CompanyBases.Resources.ErrorNotFound;
      
      // Указать ОГРН выбранного контрагента.
      var psrn = currentCompany.PSRN;
      if (!string.IsNullOrWhiteSpace(specifiedPSRN))
        psrn = specifiedPSRN;
      
      
      //INK Debug
      Logger.Debug("INK psrn: " + psrn);
      
      // Запрос в сервис.
      var searchResult = Sungero.CompanyData.Client.Search(psrn, currentCompany.TIN, currentCompany.Name, url);
      switch (searchResult.StatusCode)
      {
        case HttpStatusCode.OK:
          break;
        case HttpStatusCode.Unauthorized:
          return Sungero.Parties.CompanyBases.Resources.ErrorUnauthorized;
        case HttpStatusCode.Forbidden:
          return Sungero.Parties.CompanyBases.Resources.ErrorForbidden;
        case (HttpStatusCode)429:
          return Sungero.Parties.CompanyBases.Resources.ErrorTooManyRequests;
        case HttpStatusCode.ServiceUnavailable:
        case HttpStatusCode.BadGateway:
        case HttpStatusCode.NotFound:
          return Sungero.Parties.CompanyBases.Resources.ErrorNotFound;
        case HttpStatusCode.PaymentRequired:
          return Sungero.Parties.CompanyBases.Resources.ErrorNoLicense;
        default:
          return Sungero.Parties.CompanyBases.Resources.ErrorInService;
      }
      
      Logger.DebugFormat(" {0} counterparties found in service for {1}", searchResult.Count.ToString(), currentCompany.Id.ToString());
      
      // Нашли ровно один. Сразу заполняем реквизиты.
      if (searchResult.Count == 1)
      {
        var company = searchResult.Companies.FirstOrDefault();
        string calcOkved = Functions.Module.GetFormatOkved(company);
        var okved = string.IsNullOrWhiteSpace(calcOkved) ? "-" : calcOkved;
        Logger.DebugFormat(" {0} counterparty NCEA is {1}", currentCompany.Id.ToString(),  okved);
        if (string.IsNullOrWhiteSpace(currentCompany.TRRC)) //кпп пустой заполняем как обычно
        {
          currentCompany.Name = company.ShortName;
          currentCompany.LegalName = company.LegalName;
          currentCompany.PSRN = company.Ogrn;
          currentCompany.TIN = company.Inn;
          currentCompany.TRRC = company.Kpp;
          currentCompany.NCEO = company.Okpo;
          currentCompany.NCEA = okved;
          currentCompany.LegalAddress = company.Address;
          currentCompany.Region = Sungero.Commons.PublicFunctions.Region.GetRegionFromAddress(company.Address);
          currentCompany.City = Sungero.Commons.PublicFunctions.City.GetCityFromAddress(company.Address);
        }
        else //кпп не пустой
        {
          if (company.Kpp == currentCompany.TRRC) //но пришел такой же
          {
            currentCompany.Name = company.ShortName;
            currentCompany.LegalName = company.LegalName;
            currentCompany.PSRN = company.Ogrn;
            currentCompany.TIN = company.Inn;
            currentCompany.TRRC = company.Kpp;
            currentCompany.NCEO = company.Okpo;
            currentCompany.NCEA = okved;
            currentCompany.LegalAddress = company.Address;
            currentCompany.Region = Sungero.Commons.PublicFunctions.Region.GetRegionFromAddress(company.Address);
            currentCompany.City = Sungero.Commons.PublicFunctions.City.GetCityFromAddress(company.Address);
          }
          else //если КПП пришел другой
          {
            Logger.Debug(" Counter party with id "+ currentCompany.Id.ToString() +"  KPP is not equals.");
            currentCompany.NCEA = "-";
          }
        }
      
        if (!Equals(company.State, alialgr.AppModule.Constants.Module.ActiveCounterpartyStateInService))
        {
          if (string.IsNullOrEmpty(currentCompany.Note))
            currentCompany.Note = company.State;
          else if (!currentCompany.Note.Contains(company.State))
            currentCompany.Note = string.Format("{0}\r\n{1}", currentCompany.Note, company.State);
        }
        try
        {
          currentCompany.Save();
          result = "Counter party with id "+ currentCompany.Id.ToString() +" is filled from service.";
          var foundContacts = string.Empty;
          var companyContacts = Sungero.Parties.Contacts.GetAll(contact => Equals(currentCompany, contact.Company));
          var hasActiveCompanyContacts = companyContacts.Any(contact => contact.Status == Sungero.Parties.Contact.Status.Active);
          var hasContactsFromService = company.Managers.Any();
          if (!hasActiveCompanyContacts && hasContactsFromService && Sungero.Parties.Contacts.AccessRights.CanCreate())
          {
            foreach (var contact in company.Managers)
            {
              var name = contact.FullName;
              var contactsExist = companyContacts.Any(cont => cont.Name.ToLower() == name.ToLower());
              
              if (string.IsNullOrWhiteSpace(name) || contactsExist)
                continue;
              
              var companyContact = Sungero.Parties.Contacts.Create();
              companyContact.Company = currentCompany;
              companyContact.Name = name;
              companyContact.JobTitle = contact.JobTitle;
              companyContact.Phone = contact.Phone;
              companyContact.Save();
            }
          }
          result = result + " Find "+ company.Managers.Count.ToString() + " contacts.";
        }
        
        catch (Exception ex)
        {
          result = ex.Message;
        }
      }
      else
      {
        result = " Counter party with id "+ currentCompany.Id.ToString() +" have more then 1 search result.";
        currentCompany.NCEA = "-";
        try
        {
          currentCompany.Save();
        }
        catch (Exception ex)
        {
          result = ex.Message;
        }
      }
      
      return result;
    }
    /// <summary>
    /// Сформировать строку с ОКВЭД.
    /// </summary>
    /// <param name="company">Компания с сервиса.</param>
    /// <returns>Список ОКВЭД организации.</returns>
    public static string GetFormatOkved(Sungero.CompanyData.CompaniesDTO.CompanyDTO company)
    {
      if (company.AdditionalOkveds == null || !company.AdditionalOkveds.Any())
        return company.MainOkved.Code;
      
      var separator = "; ";
      var okveds = new List<string>() { company.MainOkved.Code };
      okveds.AddRange(company.AdditionalOkveds.Select(x => x.Code).Take(6));
      return string.Join(separator, okveds.ToArray());
    }
    #endregion
    
  }
}