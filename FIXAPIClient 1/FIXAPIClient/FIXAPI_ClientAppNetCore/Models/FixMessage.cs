using FIXAPINet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXAPI_ClientAppNetCore.Models
{
    public class FixMessage
    {
        public string OrderID { get; set; }
        public string ExecID { get; set; }
        public string ExecType { get; set; }
        public string OrdStatus { get; set; }
        public string Side { get; set; }
        public string LeavesQty { get; set; }
        public string CumQty { get; set; }
        public string AvgPx { get; set; }
        public string Symbol { get; set; }
        public string SecurityType { get; set; }
        public string LastQty { get; set; }
        public string LastPx { get; set; }
        public string TransactTime { get; set; }
        public string NoPartyIDs { get; set; }
        public string PartyID { get; set; }
        public string PartyIDSource { get; set; }
        public string PartyRole { get; set; }
        public string SecurityID { get; set; }
        public string TradeDate { get; set; }
        public string SettlType { get; set; }
        public string SettlDate { get; set; }
        public string GrossTradeAmt { get; set; }
        public string Commision { get; set; }
        public string CommType { get; set; }
        public string BlotExchCd { get; set; }
        public string BlotterId { get; set; }
        public string BlotterName { get; set; }
        public string Text { get; set; }
        public string AccruedInterestAmt { get; set; }
        public string LastParPx { get; set; }
        public string ReportedPx { get; set; }
        public string SaleCrdt { get; set; }
        public string DiscretionUsedSw { get; set; }
        public string SolicitedFlag { get; set; }
        public string CustOrderCapacity { get; set; }
        public string YieldType { get; set; }
        public string Yield { get; set; }
        public string Currency { get; set; }
        public string SettlCurrency { get; set; }
        public string SettlLocation { get; set; }
        public string Memo1 { get; set; }
        public string Memo2 { get; set; }
        public string TaxStrategy { get; set; }
        public string TaxPrice { get; set; }
        public string GroupAccount { get; set; }
        public string TrailerCd { get; set; }
        public string RrCd { get; set; }
        public string AdCd { get; set; }
        public string AnCd { get; set; }
        public string TrCd { get; set; }
        public string SettlCountry { get; set; }
        public string TaxlotTrNo { get; set; }
        public string NoMiscFees { get; set; }
        public string MiscFeeType { get; set; }
        public string MiscFeeAmt { get; set; }


        public FixMessageCache ToDto()
        {
            return new FixMessageCache()
            {
                ID = Guid.NewGuid().ToString(),
                OrderID = OrderID,
                ExecID = ExecID,
                ExecType = ExecType,
                OrdStatus =OrdStatus,
                Side = Side,
                LeavesQty = LeavesQty,
                CumQty = CumQty,
                AvgPx = AvgPx,
                Symbol =Symbol,
                SecurityType =SecurityType,
                LastQty = LastQty,
                LastPx = LastPx,
                TransactTime =TransactTime,
                NoPartyIDs = NoPartyIDs,
                PartyID = PartyID,
                PartyIDSource = PartyIDSource,
                PartyRole =PartyRole,
                SecurityID =SecurityID,
                TradeDate =TradeDate,
                SettlType = SettlType,
                GrossTradeAmt =GrossTradeAmt,
                Commision =Commision,
                CommType =CommType,
                BlotExchCd =BlotExchCd,
                BlotterId =BlotterId,
                BlotterName =BlotterName,
                Text =Text,
                AccruedInterestAmt =AccruedInterestAmt,
                LastParPx =LastParPx,
                ReportedPx=ReportedPx,
                SaleCrdt =SaleCrdt,
                DiscretionUsedSw =DiscretionUsedSw,
                SolicitedFlag =SolicitedFlag,
                CustOrderCapacity = CustOrderCapacity,
                YieldType =YieldType,
                Yield =Yield,
                Currency =Currency,
                SettlCurrency =SettlCurrency,
                SettlLocation =SettlLocation,
                Memo1 =Memo1,
                Memo2 =Memo2,
                TaxStrategy =TaxStrategy,
                TaxPrice =TaxPrice,
                GroupAccount =GroupAccount,
                TrailerCd =TrailerCd,
                RrCd =RrCd,
                AdCd=AdCd,
                AnCd =AnCd,
                TrCd =TrCd,
                SettlCountry =SettlCountry,
                TaxlotTrNo =TaxlotTrNo,
                NoMiscFees =NoMiscFees,
                MiscFeeType =MiscFeeType,
                MiscFeeAmt =MiscFeeAmt,
            };
        }

        public void Parse(Message message)
        {
            SortedDictionary<uint, STField> TagsAndValues = message.BodyFields;

            OrderID = TagsAndValues.Where(x => x.Key == 37).Select(x=> x.Value.Value).FirstOrDefault();
            ExecID = TagsAndValues.Where(x => x.Key == 17).Select(x => x.Value.Value).FirstOrDefault();
            ExecType = TagsAndValues.Where(x => x.Key == 150).Select(x => x.Value.Value).FirstOrDefault();
            OrdStatus = TagsAndValues.Where(x => x.Key == 39).Select(x => x.Value.Value).FirstOrDefault();
            Side = TagsAndValues.Where(x => x.Key == 54).Select(x => x.Value.Value).FirstOrDefault();
            LeavesQty = TagsAndValues.Where(x => x.Key == 151).Select(x => x.Value.Value).FirstOrDefault();
            CumQty = TagsAndValues.Where(x => x.Key == 14).Select(x => x.Value.Value).FirstOrDefault();
            AvgPx = TagsAndValues.Where(x => x.Key == 6).Select(x => x.Value.Value).FirstOrDefault();
            Symbol = TagsAndValues.Where(x => x.Key == 55).Select(x => x.Value.Value).FirstOrDefault();
            SecurityType = TagsAndValues.Where(x => x.Key == 167).Select(x => x.Value.Value).FirstOrDefault();
            LastQty = TagsAndValues.Where(x => x.Key == 32).Select(x => x.Value.Value).FirstOrDefault();
            LastPx = TagsAndValues.Where(x => x.Key == 31).Select(x => x.Value.Value).FirstOrDefault();
            TransactTime = TagsAndValues.Where(x => x.Key == 60).Select(x => x.Value.Value).FirstOrDefault();
            NoPartyIDs = TagsAndValues.Where(x => x.Key == 453).Select(x => x.Value.Value).FirstOrDefault();
            PartyID = TagsAndValues.Where(x => x.Key == 448).Select(x => x.Value.Value).FirstOrDefault();
            PartyIDSource = TagsAndValues.Where(x => x.Key == 447).Select(x => x.Value.Value).FirstOrDefault();
            PartyRole = TagsAndValues.Where(x => x.Key == 452).Select(x => x.Value.Value).FirstOrDefault();
            SecurityID = TagsAndValues.Where(x => x.Key == 48).Select(x => x.Value.Value).FirstOrDefault();
            TradeDate = TagsAndValues.Where(x => x.Key == 75).Select(x => x.Value.Value).FirstOrDefault();
            SettlType = TagsAndValues.Where(x => x.Key == 63).Select(x => x.Value.Value).FirstOrDefault();
            SettlDate = TagsAndValues.Where(x => x.Key == 64).Select(x => x.Value.Value).FirstOrDefault();
            GrossTradeAmt = TagsAndValues.Where(x => x.Key == 381).Select(x => x.Value.Value).FirstOrDefault();
            Commision = TagsAndValues.Where(x => x.Key == 12).Select(x => x.Value.Value).FirstOrDefault();
            CommType= TagsAndValues.Where(x => x.Key == 13).Select(x => x.Value.Value).FirstOrDefault();
            BlotExchCd = TagsAndValues.Where(x => x.Key == 7937).Select(x => x.Value.Value).FirstOrDefault();
            BlotterId = TagsAndValues.Where(x => x.Key == 7938).Select(x => x.Value.Value).FirstOrDefault();
            BlotterName = TagsAndValues.Where(x => x.Key == 7939).Select(x => x.Value.Value).FirstOrDefault();
            Text = TagsAndValues.Where(x => x.Key == 58).Select(x => x.Value.Value).FirstOrDefault();
            AccruedInterestAmt = TagsAndValues.Where(x => x.Key == 159).Select(x => x.Value.Value).FirstOrDefault();
            LastParPx = TagsAndValues.Where(x => x.Key == 669).Select(x => x.Value.Value).FirstOrDefault();
            ReportedPx = TagsAndValues.Where(x => x.Key == 861).Select(x => x.Value.Value).FirstOrDefault();
            SaleCrdt = TagsAndValues.Where(x => x.Key == 7503).Select(x => x.Value.Value).FirstOrDefault();
            DiscretionUsedSw = TagsAndValues.Where(x => x.Key == 6000).Select(x => x.Value.Value).FirstOrDefault();
            SolicitedFlag = TagsAndValues.Where(x => x.Key == 377).Select(x => x.Value.Value).FirstOrDefault();
            CustOrderCapacity = TagsAndValues.Where(x => x.Key == 582).Select(x => x.Value.Value).FirstOrDefault();
            YieldType = TagsAndValues.Where(x => x.Key == 253).Select(x => x.Value.Value).FirstOrDefault();
            Yield = TagsAndValues.Where(x => x.Key == 236).Select(x => x.Value.Value).FirstOrDefault();
            Currency = TagsAndValues.Where(x => x.Key == 15).Select(x => x.Value.Value).FirstOrDefault();
            SettlCurrency = TagsAndValues.Where(x => x.Key == 120).Select(x => x.Value.Value).FirstOrDefault();
            SettlLocation = TagsAndValues.Where(x => x.Key == 166).Select(x => x.Value.Value).FirstOrDefault();
            Memo1 = TagsAndValues.Where(x => x.Key == 20001).Select(x => x.Value.Value).FirstOrDefault();
            Memo2 = TagsAndValues.Where(x => x.Key == 20002).Select(x => x.Value.Value).FirstOrDefault();
            TaxStrategy = TagsAndValues.Where(x => x.Key == 20003).Select(x => x.Value.Value).FirstOrDefault();
            TaxPrice = TagsAndValues.Where(x => x.Key == 20004).Select(x => x.Value.Value).FirstOrDefault();
            GroupAccount = TagsAndValues.Where(x => x.Key == 20005).Select(x => x.Value.Value).FirstOrDefault();
            TrailerCd = TagsAndValues.Where(x => x.Key == 20006).Select(x => x.Value.Value).FirstOrDefault();
            RrCd = TagsAndValues.Where(x => x.Key == 20007).Select(x => x.Value.Value).FirstOrDefault();
            AdCd = TagsAndValues.Where(x => x.Key == 20008).Select(x => x.Value.Value).FirstOrDefault();
            AnCd = TagsAndValues.Where(x => x.Key == 20009).Select(x => x.Value.Value).FirstOrDefault();
            TrCd = TagsAndValues.Where(x => x.Key == 20010).Select(x => x.Value.Value).FirstOrDefault();
            SettlCountry = TagsAndValues.Where(x => x.Key == 20011).Select(x => x.Value.Value).FirstOrDefault();
            TaxlotTrNo = TagsAndValues.Where(x => x.Key == 20012).Select(x => x.Value.Value).FirstOrDefault();
            NoMiscFees = TagsAndValues.Where(x => x.Key == 136).Select(x => x.Value.Value).FirstOrDefault();
            MiscFeeType = TagsAndValues.Where(x => x.Key == 139).Select(x => x.Value.Value).FirstOrDefault();
            MiscFeeAmt = TagsAndValues.Where(x => x.Key == 137).Select(x => x.Value.Value).FirstOrDefault();

        }

       
    }
}
