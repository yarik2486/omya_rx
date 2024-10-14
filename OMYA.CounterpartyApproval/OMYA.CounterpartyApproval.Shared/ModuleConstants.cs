using System;
using Sungero.Core;

namespace OMYA.CounterpartyApproval.Constants
{
  public static class Module
  {

     public static class Initialize
     {
       // Вид документа "Заявка на одобрение контрагента".
       [Sungero.Core.Public]
       public static readonly Guid RequestForCounterpartyApprovalKind = Guid.Parse("BC8CF578-733A-46CD-B604-3263D069847F");
       
       // Вид документа "Чек-лист для одобрения контрагента".
       [Sungero.Core.Public]
       public static readonly Guid ChecklistKind = Guid.Parse("7B6C418B-9EB3-44E4-A0E1-4AA5022DA24B");
       
       // GUID роли "Специалист по мастер-данным".
       [Sungero.Core.Public]
       public static readonly Guid MasterDataSpecialist = Guid.Parse("B39244B8-3337-440A-A931-E866A6D053E6");
     }
  }
}