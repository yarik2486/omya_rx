using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;
using OMYA.CounterpartySolution;
using System.Net;
using Sungero.Parties;

namespace OMYA.CounterpartyApproval.Server
{
  partial class CounterpartyApprovalRequestFunctions
  {

    /// <summary>
    /// Создать чек-лист.
    /// </summary>
    /// <returns>Чек-лист.</returns>
    [Remote]
    public static IChecklist CreateChecklist()
    {
      return Checklists.Create();
    }
    
    /// <summary>
    /// Создать контрагента и заполнить поля из заявки на одобрение контрагента.
    /// </summary>
    /// <returns>Контрагент.</returns>
    [Remote]
    public OMYA.CounterpartySolution.ICompany CreateCompany()
    {
      var company = OMYA.CounterpartySolution.Companies.Create();
      company.Nonresident = _obj.Nonresident;
      company.CounterpartyTypeOMYA = _obj.CounterpartyType;
      company.CounterpartyKindOMYA = _obj.CounterpartyKind;
      company.Name = _obj.ShortName;
      company.LegalName = _obj.LegalName;
      company.TIN = _obj.TIN;
      company.TRRC = _obj.TRRC;
      company.PSRN = _obj.PSRN;
      company.NCEO = _obj.NCEO;
      company.NCEA = _obj.NCEA;
      company.SAPNumOMYA = _obj.SAPNum;
      company.CompanyRegistrationNumberOMYA = _obj.CompanyRegistrationNumber;
      company.TaxNumberOMYA = _obj.TaxNumber;
      company.Region = _obj.Region;
      company.City = _obj.City;
      company.LegalAddress = _obj.LegalAddress;
      company.PostalAddress = _obj.PostalAddress;
      company.Phones = _obj.Phones;
      company.Email = _obj.Email;
      company.Homepage = _obj.Homepage;
      company.Account = _obj.Account;
      company.CurrentAccountOMYA = _obj.CurrentAccount;
      company.Bank = _obj.Bank;
      company.BankAddressOMYA = _obj.BankAddress;
      company.CorrespondentAccountOMYA = _obj.CorrespondentAccount;
      company.BICOMYA = _obj.BIC;
      company.IBANOMYA = _obj.IBAN;
      company.SWIFTOMYA = _obj.SWIFT;
      
      return company;
    }
    
    /// <summary>
    /// Заполнить реквизиты контрагента из сервиса.
    /// </summary>
    /// <param name="specifiedPSRN">ОГРН выбранной организации.</param>
    /// <returns>Статус запроса, список представлений организаций, список контактов.</returns>
    [Remote]
    public Structures.CounterpartyApprovalRequest.FoundCompanies FillFromServiceCustom(string specifiedPSRN)
    {
      var url = Functions.Module.GetCompanyDataServiceURL();
      if (string.IsNullOrEmpty(url))
        return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorNotFound, null, null, 0);
      
      // Указать ОГРН выбранного контрагента.
      var psrn = _obj.PSRN;
      if (!string.IsNullOrWhiteSpace(specifiedPSRN))
        psrn = specifiedPSRN;

      // Запрос в сервис.
      var searchResult = Sungero.CompanyData.Client.Search(psrn, _obj.TIN, _obj.ShortName, url);
      switch (searchResult.StatusCode)
      {
        case HttpStatusCode.OK:
          break;
        case HttpStatusCode.Unauthorized:
          return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorUnauthorized, null, null, 0);
        case HttpStatusCode.Forbidden:
          return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorForbidden, null, null, 0);
        case (HttpStatusCode)429:
          return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorTooManyRequests, null, null, 0);
        case HttpStatusCode.ServiceUnavailable:
        case HttpStatusCode.BadGateway:
        case HttpStatusCode.NotFound:
          return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorNotFound, null, null, 0);
        case HttpStatusCode.PaymentRequired:
          return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorNoLicense, null, null, 0);
        default:
          return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorInService, null, null, 0);
      }
      
      Logger.DebugFormat("FillFromServiceCustom. {0} counterparties found in service", searchResult.Count.ToString());

      // Ничего не нашли.
      if (searchResult.Count < 1)
      {
        // Пустая структура, чтобы можно было отделить результат, когда ничего не найдено, от случая, когда сервис вернул ошибку.
        var emptyListOfCompanies = new List<Structures.CounterpartyApprovalRequest.CompanyDisplayValue>();
        return Structures.CounterpartyApprovalRequest.FoundCompanies.Create(CompanyBases.Resources.ErrorCompanyNotFoundInService, emptyListOfCompanies, null, 0);
      }
      
      // Подготовить ответ.
      var result = Structures.CounterpartyApprovalRequest.FoundCompanies.Create();
      result.CompanyDisplayValues = searchResult.Companies
        .Select(r =>
                {
                  var dialogText = string.IsNullOrWhiteSpace(r.Kpp)
                    ? CompanyBases.Resources.CompanySelectDialogTextFormat(r.ShortName, r.Inn)
                    : CompanyBases.Resources.CompanySelectDialogTextWithTRRCFormat(r.ShortName, r.Inn, r.Kpp);
                  return Structures.CounterpartyApprovalRequest.CompanyDisplayValue.Create(dialogText, r.Ogrn);
                })
        .ToList();
      
      result.Amount = searchResult.Total;
      
      // Нашли ровно один. Сразу заполняем реквизиты.
      if (searchResult.Count == 1)
      {
        var company = searchResult.Companies.First();
        _obj.ShortName = company.ShortName;
        _obj.LegalName = company.LegalName;
        _obj.PSRN = company.Ogrn;
        _obj.TIN = company.Inn;
        _obj.TRRC = company.Kpp;
        _obj.NCEO = company.Okpo;
        _obj.NCEA = Functions.CounterpartyApprovalRequest.GetFormatOkved(company);
        _obj.LegalAddress = company.Address;
        _obj.Region = Sungero.Commons.PublicFunctions.Region.GetRegionFromAddress(company.Address);
        _obj.City = Sungero.Commons.PublicFunctions.City.GetCityFromAddress(company.Address);
        if (!Equals(company.State, Constants.CounterpartyApprovalRequest.ActiveCounterpartyStateInService))
        {
          if (string.IsNullOrEmpty(_obj.Note))
            _obj.Note = company.State;
          else if (!_obj.Note.Contains(company.State))
            _obj.Note = string.Format("{0}\r\n{1}", _obj.Note, company.State);
        }
        result.FoundContacts = company.Managers
          .Select(t => Structures.CounterpartyApprovalRequest.FoundContact.Create(t.FullName, t.JobTitle, t.Phone))
          .ToList();
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
  }
}