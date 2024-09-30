using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using alialgr.CustomDev.PowerOfAttorney;

namespace alialgr.CustomDev.Shared
{
  partial class PowerOfAttorneyFunctions
  {
    public override void FillName()
    {
      base.FillName();
      
      FillPasportData();

    }
    
    /// <summary>
    /// Заполнения скрытого поля паспортные данные
    /// </summary>
    /// <param name="person"></param>
    [Public]
    public virtual void FillPasportData()//Sungero.Parties.IPerson person
    {
    var person =_obj.Representative;
      
      if (_obj.IssuedTo !=null)
        person=_obj.IssuedTo.Person;
      
      if (_obj.IssuedToParty!=null && person==null)
      {
        var per=Sungero.Parties.People.GetAll(p=> p.Name.Equals(_obj.IssuedToParty.Name)).FirstOrDefault();
        if (per!=null)
          person =per;
      }     

      var passportData = alialgr.AppModule.PersonPassportDatas.GetAll().Where(p=> p.Person.Equals(person)).FirstOrDefault();
        if (passportData==null)
      {
        if (_obj.Passportalialgr!=null)
          _obj.Passportalialgr=null;
      return;
      }
      
      if (passportData.Equals(_obj.Passportalialgr))
        return;
      
      _obj.Passportalialgr=passportData;
    
    
    }
  }
}