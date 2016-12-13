using System.Collections.Generic;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Services.InvestmentDecision
{
    public partial interface IInvestmentDecisionService : IBaseService
    {
        void SubmitInvestmentDecisionApplication(InvestmentDecisionForm entity);

        void IDApplicationApplyProcess(InvestmentDecisionApplication applicationEntity, InvestmentDecisionOperation operationEntity);

        void DeleteInvestmentDecisionForm(string serialNo);

        void DeleteInvestmentDecisionForm(IList<string> serialNos);

        void InvestmentDecisionVoteProcess(string investorCode, string formSerialNo, EnumLibrary.IDVoteFlag flag, string reason);

        IList<InvestmentDecisionVote> GetInvestmentDecisionVotes(string investorCode);

        MarketTrendForecastDetail GetMTFDetail(string investorCode, string serialNo);

        void UpdateMTFDetail(MarketTrendForecastDetail entity);

        void DeleteMTFInfo(string serialNo);

        IList<InvestmentDecisionStockPool> GetIDStockPool();

        InvestmentDecisionStockPool GetIDStockPoolByCode(string stockCode);

        void DeleteIDStockPool(IList<string> stockCodes);

        void AddIDStockPool(InvestmentDecisionStockPool entity);

        void UpdateIDStockPool(InvestmentDecisionStockPool entity);

        IList<InvestmentDecisionCommittee> GetIDCommittees();

        void AddIDCommittee(string code, string name);

        void DeleteIDCommittee(IList<int> ids);

        PositionStockAnalysisDetail GetPSADetailById(int id);

        void UpdatePSADetail(PositionStockAnalysisDetail entity);

        void DeletePSAInfo(string serialNo);

        PositionStockAnalysisSummary GetPSASummaryById(int id);

        void UpdatePSASummary(PositionStockAnalysisSummary entity);

        IList<DecisionReasonCategoryEntity> GetIDReasonCategories(string jointMark = "->");

        int AddIDReasonCategory(DecisionReasonCategory entity);

        void UpdateIDReasonCategory(int id, string name);

        void DeleteIDReasonCategory(int id);

        void DeleteIDReasonContent(int[] contentIds);

        void AddIDReasonContent(DecisionReasonContent entity);

        void UpdateIDReasonContent(DecisionReasonContent entity);

        DecisionReasonContent GetIDReasonContent(int contentId);

        string GetIDReasonCategoryNameWithParent(int categoryId, string jointMark = "-");

        void IDOperationVoteProcess(string investorCode, string applyNo, string operateNo, EnumLibrary.IDVoteFlag flag, int reasonCategoryId, string reasonContent)
    }
}