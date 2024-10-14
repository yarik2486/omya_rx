using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.Company;

namespace OMYA.CounterpartySolution
{
  partial class CompanySharedHandlers
  {

    public override void BankChanged(Sungero.Parties.Shared.CounterpartyBankChangedEventArgs e)
    {
      base.BankChanged(e);
      
      _obj.CorrespondentAccountOMYA = e.NewValue?.CorrespondentAccount;
      _obj.BICOMYA = e.NewValue?.BIC;
      _obj.BankAddressOMYA = e.NewValue?.LegalAddress;
      _obj.SWIFTOMYA = e.NewValue?.SWIFT;
    }

    public override void NonresidentChanged(Sungero.Domain.Shared.BooleanPropertyChangedEventArgs e)
    {
      base.NonresidentChanged(e);
      
      if (e.NewValue == true)
        _obj.CounterpartyTypeOMYA = CounterpartyTypeOMYA.ForeignOrg;
      else
        _obj.CounterpartyTypeOMYA = null;
    }

  }
}