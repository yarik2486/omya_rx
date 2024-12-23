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
       
       // Вид документа "Чек-лист для одобрения контрагента (государственные предприятия".
       [Sungero.Core.Public]
       public static readonly Guid ChecklistStateEnterprisesKind = Guid.Parse("4DD883B3-31E4-4B6D-8BDF-B8AD6B0ECA7B");
         
       // Вид документа "Заявка на изменение реквизитов контрагента".
       [Sungero.Core.Public]
       public static readonly Guid CounterpartyChangeRequestKind = Guid.Parse("25B80240-9B75-4859-B989-558196102B4A"); 
       
       // Вид документа "Заявка на блокировку контрагента".
       [Sungero.Core.Public]
       public static readonly Guid CounterpartyBlockingRequestKind = Guid.Parse("40ADD802-B12A-4B3A-94E5-43023D3F2142");
       
       // GUID роли "Специалист по мастер-данным".
       [Sungero.Core.Public]
       public static readonly Guid MasterDataSpecialist = Guid.Parse("B39244B8-3337-440A-A931-E866A6D053E6");
     }
     
     // Guid типа документа "Заявка на одобрение контрагента".
     [Sungero.Core.Public]
     public const string CounterpartyApprovalRequestTypeGuid = "bb2e947a-d482-439b-88ce-3a4746ebdaef";
       
     // Guid типа документа "Заявка на изменение реквизитов контрагента".
     [Sungero.Core.Public]
     public const string CounterpartyChangeRequestTypeGuid = "889b5ebf-a7c1-4ede-83b4-6e0cd92f8000";
     
     // Наименование шаблона "Форма анкеты для нерезидента".
     [Sungero.Core.Public]
     public const string TemplateApplicationFormNonresident = "Форма анкеты для нерезидента";
  }
}