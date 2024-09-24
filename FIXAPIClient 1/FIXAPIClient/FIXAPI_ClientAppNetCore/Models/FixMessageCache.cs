using Apache.Ignite.Core.Cache.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXAPI_ClientAppNetCore.Models
{
     public class FixMessageCache
    {
        [QuerySqlField] public string ID { get; set; }
        [QuerySqlField] public string OrderID { get; set; }
        [QuerySqlField] public string ExecID { get; set; }
        [QuerySqlField] public string ExecType { get; set; }
        [QuerySqlField] public string OrdStatus { get; set; }
        [QuerySqlField] public string Side { get; set; }
        [QuerySqlField] public string LeavesQty { get; set; }
        [QuerySqlField] public string CumQty { get; set; }
        [QuerySqlField] public string AvgPx { get; set; }
        [QuerySqlField] public string Symbol { get; set; }
        [QuerySqlField] public string SecurityType { get; set; }
        [QuerySqlField] public string LastQty { get; set; }
        [QuerySqlField] public string LastPx { get; set; }
        [QuerySqlField] public string TransactTime { get; set; }
        [QuerySqlField] public string NoPartyIDs { get; set; }
        [QuerySqlField] public string PartyID { get; set; }
        [QuerySqlField] public string PartyIDSource { get; set; }
        [QuerySqlField] public string PartyRole { get; set; }
        [QuerySqlField] public string SecurityID { get; set; }
        [QuerySqlField] public string TradeDate { get; set; }
        [QuerySqlField] public string SettlType { get; set; }
        [QuerySqlField] public string SettlDate { get; set; }
        [QuerySqlField] public string GrossTradeAmt { get; set; }
        [QuerySqlField] public string Commision { get; set; }
        [QuerySqlField] public string CommType { get; set; }
        [QuerySqlField] public string BlotExchCd { get; set; }
        [QuerySqlField] public string BlotterId { get; set; }
        [QuerySqlField] public string BlotterName { get; set; }
        [QuerySqlField] public string Text { get; set; }
        [QuerySqlField] public string AccruedInterestAmt { get; set; }
        [QuerySqlField] public string LastParPx { get; set; }
        [QuerySqlField] public string ReportedPx { get; set; }
        [QuerySqlField] public string SaleCrdt { get; set; }
        [QuerySqlField] public string DiscretionUsedSw { get; set; }
        [QuerySqlField] public string SolicitedFlag { get; set; }
        [QuerySqlField] public string CustOrderCapacity { get; set; }
        [QuerySqlField] public string YieldType { get; set; }
        [QuerySqlField] public string Yield { get; set; }
        [QuerySqlField] public string Currency { get; set; }
        [QuerySqlField] public string SettlCurrency { get; set; }
        [QuerySqlField] public string SettlLocation { get; set; }
        [QuerySqlField] public string Memo1 { get; set; }
        [QuerySqlField] public string Memo2 { get; set; }
        [QuerySqlField] public string TaxStrategy { get; set; }
        [QuerySqlField] public string TaxPrice { get; set; }
        [QuerySqlField] public string GroupAccount { get; set; }
        [QuerySqlField] public string TrailerCd { get; set; }
        [QuerySqlField] public string RrCd { get; set; }
        [QuerySqlField] public string AdCd { get; set; }
        [QuerySqlField] public string AnCd { get; set; }
        [QuerySqlField] public string TrCd { get; set; }
        [QuerySqlField] public string SettlCountry { get; set; }
        [QuerySqlField] public string TaxlotTrNo { get; set; }
        [QuerySqlField] public string NoMiscFees { get; set; }
        [QuerySqlField] public string MiscFeeType { get; set; }
        [QuerySqlField] public string MiscFeeAmt { get; set; }
    }
}
