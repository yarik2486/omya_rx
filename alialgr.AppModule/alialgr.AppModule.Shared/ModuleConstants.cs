using System;
using Sungero.Core;

namespace alialgr.AppModule.Constants
{
  public static class Module
  {
    public const string ActiveCounterpartyStateInService = "Действующее";//константа для фонового процесса заполнения  данных по огрн инн
    
    //Названия этапов
    [Sungero.Core.Public]
    public const string PreSignRes = "Предварительное подписание(НецелевыеРес)";
    [Sungero.Core.Public]
    public const string PreSign = "Предварительное подписание";
    [Sungero.Core.Public]
    public const string PreSignSaleOMIYA = "Предварительное подписание_Продажи_ОМИА УРАЛ";
    [Sungero.Core.Public]
    public const string PreSignSaleMramorecs = "Предварительное подписание_Продажи_Мраморэкс";
    [Sungero.Core.Public]
    public const string PreSignSaleKarat = "Предварительное подписание_Продажи_Карат";    
  }
}