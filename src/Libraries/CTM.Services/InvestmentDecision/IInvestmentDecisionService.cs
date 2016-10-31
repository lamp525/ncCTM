using System;
using System.Collections.Generic;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Services.InvestmentDecision
{
    public partial interface IInvestmentDecisionService : IBaseService
    {
        string GenerateIDFSerialNo(DateTime applyDate);

        void SubmitInvestmentDecisionApplication(InvestmentDecisionForm entity);

        void DeleteInvestmentDecisionForm(string serialNo);

        void DeleteInvestmentDecisionForm(IList<string> serialNos);

        void InvestmentDecisionVoteProcess(string investorCode, string formSerialNo, EnumLibrary.IDVoteFlag flag, string reason);

        IList<InvestmentDecisionVote> GetInvestmentDecisionVotes(string investorCode);

        MarketTrendForecastDetail GetMTFDetail(string investorCode, string serialNo);

        void UpdateMTFDetail(MarketTrendForecastDetail entity);

        void DeleteMTFInfo(string serialNo);

        void DeleteCSAInfo(string serialNo);

        CloseStockAnalysisDetail GetCSADetailById(int id);

        void UpdateCSADetail(CloseStockAnalysisDetail entity);

        IList<InvestmentDecisionStockPool> GetIDStockPool();

        void DeleteIDStockPool(IList<string> stockCodes);

        void AddIDStockPool(string stockCode, string stockName);

        IList<InvestmentDecisionCommittee> GetIDCommittees();

        void AddIDCommittee(string code, string name);

        void DeleteIDCommittee(IList<int> ids);
    }
}