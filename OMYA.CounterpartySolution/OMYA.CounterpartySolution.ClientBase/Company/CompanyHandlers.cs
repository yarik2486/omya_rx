using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using OMYA.CounterpartySolution.Company;

namespace OMYA.CounterpartySolution
{
  partial class CompanyClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      var prop = _obj.State.Properties;
      prop.CounterpartyTypeOMYA.IsRequired = true;
      prop.CounterpartyKindOMYA.IsRequired = true;
    }

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      var nonresident = _obj.Nonresident == true;
      var prop = _obj.State.Properties;
      
      // Резидент.
      prop.TIN.IsRequired = !nonresident;
      prop.TIN.IsVisible = !nonresident;
      prop.TRRC.IsRequired = !nonresident;
      prop.TRRC.IsVisible = !nonresident;
      prop.PSRN.IsRequired = !nonresident;
      prop.PSRN.IsVisible = !nonresident;
      prop.NCEO.IsVisible = !nonresident;
      prop.NCEA.IsVisible = !nonresident;
      
      // Нерезидент.
      prop.CompanyRegistrationNumberOMYA.IsRequired = nonresident;
      prop.CompanyRegistrationNumberOMYA.IsVisible = nonresident;
      prop.TaxNumberOMYA.IsRequired = nonresident;
      prop.TaxNumberOMYA.IsVisible = nonresident;
    }

  }
}