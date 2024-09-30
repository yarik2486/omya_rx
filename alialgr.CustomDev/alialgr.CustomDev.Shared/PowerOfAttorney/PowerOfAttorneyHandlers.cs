using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.PowerOfAttorney;

namespace alialgr.CustomDev
{
  partial class PowerOfAttorneySharedHandlers
  {

    public override void RepresentativeChanged(Sungero.Docflow.Shared.PowerOfAttorneyBaseRepresentativeChangedEventArgs e)
    {
      base.RepresentativeChanged(e);
      this.FillName();
    }

  }
}