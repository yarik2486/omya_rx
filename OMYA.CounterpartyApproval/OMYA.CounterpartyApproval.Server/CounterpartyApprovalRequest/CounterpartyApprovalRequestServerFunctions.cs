using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartyApproval.CounterpartyApprovalRequest;
using OMYA.CounterpartySolution;

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
    public ICompany CreateCompany()
    {
      var company = Companies.Create();
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
  }
}