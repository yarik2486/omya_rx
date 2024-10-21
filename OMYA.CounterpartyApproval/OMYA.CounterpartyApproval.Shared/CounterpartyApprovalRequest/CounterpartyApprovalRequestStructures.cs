using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace OMYA.CounterpartyApproval.Structures.CounterpartyApprovalRequest
{

  partial class FoundCompanies
  {
    public string Message { get; set; }
    
    public List<OMYA.CounterpartyApproval.Structures.CounterpartyApprovalRequest.CompanyDisplayValue> CompanyDisplayValues { get; set; }
    
    public List<OMYA.CounterpartyApproval.Structures.CounterpartyApprovalRequest.FoundContact> FoundContacts { get; set; }
    
    public int Amount { get; set; }
  }
  
  partial class CompanyDisplayValue
  {
    public string DisplayValue { get; set; }
    
    public string PSRN { get; set; }
  }
  
  partial class FoundContact
  {
    public string FullName { get; set; }
    
    public string JobTitle { get; set; }
    
    public string Phone { get; set; }
  }
}