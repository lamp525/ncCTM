using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;

namespace CTM.Services.InvestmentDecision
{
    public partial class InvestmentDecisionService : IInvestmentDecisionService
    {
        #region Fields

        private readonly IRepository<InvestmentDecisionCommittee> _IDCommitteeRepository;
        private readonly IRepository<InvestmentDecisionApplication> _IDApplicationRepository;
        private readonly IRepository<InvestmentDecisionOperation> _IDOperationRepository;
        private readonly IRepository<InvestmentDecisionOperationVote> _IDOperationVoteRepository;
        private readonly IRepository<InvestmentDecisionOperationAccuracy> _IDAccuracyRepository;
        private readonly IRepository<InvestmentDecisionTradeRecord> _IDTradeRecordRepository;

        private readonly IRepository<InvestmentDecisionForm> _IDFormRepository;
        private readonly IRepository<InvestmentDecisionVote> _IDVoteRepository;

        private readonly IRepository<InvestmentDecisionStockPool> _IDStockPoolRepository;
        private readonly IRepository<InvestmentDecisionStockPoolLog> _IDStockPoolLogRepository;

        private readonly IRepository<MarketTrendForecastInfo> _MTFInfoRepository;
        private readonly IRepository<MarketTrendForecastDetail> _MTFDetailRepository;

        private readonly IRepository<PositionStockAnalysisInfo> _PSAInfoRepository;
        private readonly IRepository<PositionStockAnalysisDetail> _PSADetailRepository;
        private readonly IRepository<PositionStockAnalysisSummary> _PSASummaryRepository;

        private readonly IRepository<DecisionReasonCategory> _DRCategoryRepository;
        private readonly IRepository<DecisionReasonContent> _DRContentRepository;

        private readonly IRepository<InvestmentPlanRecord> _IPRRepository;

        private readonly ICommonService _commonService;

        private readonly IDbContext _dbContext;

        #endregion Fields

        #region Constructors

        public InvestmentDecisionService(
            IRepository<InvestmentDecisionCommittee> IDCRepository,
            IRepository<InvestmentDecisionApplication> IDApplicationRepository,
            IRepository<InvestmentDecisionOperation> IDOperationRepository,
            IRepository<InvestmentDecisionOperationVote> IDOperationVoteRepository,
            IRepository<InvestmentDecisionOperationAccuracy> IDAccuracyRepository,
            IRepository<InvestmentDecisionTradeRecord> IDTradeRecordRepository,
            IRepository<InvestmentDecisionForm> IDFRepository,
            IRepository<InvestmentDecisionVote> IDVRepository,
            IRepository<InvestmentDecisionStockPool> IDStockPoolRepository,
            IRepository<InvestmentDecisionStockPoolLog> IDStockPoolLogRepository,
            IRepository<MarketTrendForecastInfo> MTFInfoRepository,
            IRepository<MarketTrendForecastDetail> MTFDetailRepository,
            IRepository<PositionStockAnalysisInfo> PSAInfoRepository,
            IRepository<PositionStockAnalysisDetail> PSADetailRepository,
            IRepository<PositionStockAnalysisSummary> PSASummaryRepository,
            IRepository<DecisionReasonCategory> DRCategoryRepository,
            IRepository<DecisionReasonContent> DRContentRepository,
            IRepository<InvestmentPlanRecord> IPRRepository,
            ICommonService commonService,
            IDbContext dbContext)
        {
            this._IDCommitteeRepository = IDCRepository;
            this._IDApplicationRepository = IDApplicationRepository;
            this._IDOperationRepository = IDOperationRepository;
            this._IDOperationVoteRepository = IDOperationVoteRepository;
            this._IDAccuracyRepository = IDAccuracyRepository;
            this._IDTradeRecordRepository = IDTradeRecordRepository;
            this._IDFormRepository = IDFRepository;
            this._IDVoteRepository = IDVRepository;
            this._IDStockPoolRepository = IDStockPoolRepository;
            this._IDStockPoolLogRepository = IDStockPoolLogRepository;
            this._MTFInfoRepository = MTFInfoRepository;
            this._MTFDetailRepository = MTFDetailRepository;
            this._PSADetailRepository = PSADetailRepository;
            this._PSAInfoRepository = PSAInfoRepository;
            this._PSASummaryRepository = PSASummaryRepository;
            this._DRCategoryRepository = DRCategoryRepository;
            this._DRContentRepository = DRContentRepository;
            this._IPRRepository = IPRRepository;

            this._commonService = commonService;
            this._dbContext = dbContext;
        }

        #endregion Constructors

        #region Utilities

        private void UpdateCommitteeWeight()
        {
            var committees = _IDCommitteeRepository.Table.ToList();

            if (committees.Any())
            {
                foreach (var item in committees)
                {
                    item.Weight = CommonHelper.SetDecimalDigits(1.000000M / committees.Count, 4);
                }
                _IDCommitteeRepository.Update(committees);
            }
        }

        private string GenerateIDFSerialNo(DateTime applyDate)
        {
            var serialNo = "SQ" + applyDate.ToString("yyMMdd");

            var info = _IDFormRepository.Table.Where(x => x.ApplyDate == applyDate).OrderBy(x => x.CreateTime).ToList().LastOrDefault();

            var suffix = string.Empty;
            if (info == null)
                suffix = "001";
            else
            {
                var lastSuffix = info.SerialNo.Substring(info.SerialNo.Length - 3, 3);
                suffix = (int.Parse(lastSuffix) + 1).ToString("00#");
            }

            serialNo = serialNo + suffix;
            return serialNo;
        }

        #endregion Utilities

        #region Methods

        public virtual string GenerateIDApplicationApplyNo()
        {
            var applyNo = "SQ";

            var info = _IDApplicationRepository.Table.OrderBy(x => x.CreateTime).ToList().LastOrDefault();

            var suffix = string.Empty;
            if (info == null)
                suffix = "000001";
            else
            {
                var lastSuffix = info.ApplyNo.Substring(info.ApplyNo.Length - 6, 6);
                suffix = (int.Parse(lastSuffix) + 1).ToString("00000#");
            }

            applyNo = applyNo + suffix;
            return applyNo;
        }

        public virtual string GenerateIDOperationNo()
        {
            var operateNo = "CZ";

            var info = _IDOperationRepository.Table.OrderBy(x => x.CreateTime).ToList().LastOrDefault();

            var suffix = string.Empty;
            if (info == null)
                suffix = "000001";
            else
            {
                var lastSuffix = info.OperateNo.Substring(info.ApplyNo.Length - 6, 6);
                suffix = (int.Parse(lastSuffix) + 1).ToString("00000#");
            }

            operateNo = operateNo + suffix;

            return operateNo;
        }

        public virtual void SubmitInvestmentDecisionApplication(InvestmentDecisionForm entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (_IDCommitteeRepository.Table.Count() == 0)
                throw new Exception("请设置投资决策委员会成员！");

            decimal totalWeight = 1;
            decimal applyUserWeight = 0;
            decimal otherWeight = 0;

            var committeeCodes = _IDCommitteeRepository.Table.Select(x => x.Code).ToList();

            if (committeeCodes.Contains(entity.ApplyUser))
            {
                applyUserWeight = 0.35M;
                otherWeight = CommonHelper.SetDecimalDigits((totalWeight - applyUserWeight), 4) / (committeeCodes.Count - 1);
            }
            else
            {
                applyUserWeight = 0;
                otherWeight = CommonHelper.SetDecimalDigits(totalWeight, 4) / committeeCodes.Count;

                committeeCodes.Add(entity.ApplyUser);
            }

            entity.Point = (int)(applyUserWeight * 100);
            entity.SerialNo = GenerateIDFSerialNo(entity.ApplyDate);
            _IDFormRepository.Insert(entity);

            var defaultVoteInfos = new List<InvestmentDecisionVote>();
            foreach (var code in committeeCodes)
            {
                var info = new InvestmentDecisionVote
                {
                    AuthorityLevel = 0,
                    Flag = code == entity.ApplyUser ? (int)EnumLibrary.IDVoteFlag.Approval : (int)EnumLibrary.IDVoteFlag.None,
                    FormSerialNo = entity.SerialNo,
                    Reason = code == entity.ApplyUser ? entity.Reason : string.Empty,
                    Type = code == entity.ApplyUser ? (int)EnumLibrary.IDVoteType.Applicant : (int)EnumLibrary.IDVoteType.Committee,
                    UserCode = code,
                    VoteTime = _commonService.GetCurrentServerTime(),
                    Weight = code == entity.ApplyUser ? applyUserWeight : otherWeight,
                };

                defaultVoteInfos.Add(info);
            }
            _IDVoteRepository.Insert(defaultVoteInfos);
        }

        public void IDApplicationApplyProcess(InvestmentDecisionApplication applicationEntity, InvestmentDecisionOperation operationEntity)
        {
            if (operationEntity == null)
                throw new ArgumentNullException(nameof(operationEntity));

            if (_IDCommitteeRepository.Table.Count() == 0)
                throw new Exception("请设置投资决策委员会成员！");

            if (applicationEntity != null && string.IsNullOrEmpty(applicationEntity.ApplyNo))
            {
                applicationEntity.ApplyNo = GenerateIDApplicationApplyNo();

                this._IDApplicationRepository.Insert(applicationEntity);

                operationEntity.ApplyNo = applicationEntity.ApplyNo;
            }

            decimal totalWeight = 1;
            decimal applyUserWeight = 0;
            decimal otherWeight = 0;

            var committeeCodes = _IDCommitteeRepository.Table.Select(x => x.Code).ToList();

            if (committeeCodes.Contains(operationEntity.OperateUser))
            {
                applyUserWeight = 0.35M;
                otherWeight = CommonHelper.SetDecimalDigits((totalWeight - applyUserWeight), 4) / (committeeCodes.Count - 1);
            }
            else
            {
                applyUserWeight = 0;
                otherWeight = CommonHelper.SetDecimalDigits(totalWeight, 4) / committeeCodes.Count;

                committeeCodes.Add(operationEntity.OperateUser);
            }

            operationEntity.VotePoint = (int)(applyUserWeight * 100);
            operationEntity.OperateNo = GenerateIDOperationNo();

            _IDOperationRepository.Insert(operationEntity);

            var defaultVoteInfos = new List<InvestmentDecisionOperationVote>();
            foreach (var code in committeeCodes)
            {
                var info = new InvestmentDecisionOperationVote
                {
                    ApplyNo = operationEntity.ApplyNo,
                    AuthorityLevel = 0,
                    Flag = operationEntity.OperateUser == code ? (int)EnumLibrary.IDVoteFlag.Approval : (int)EnumLibrary.IDVoteFlag.None,
                    OperateNo = operationEntity.OperateNo,
                    ReasonCategoryId = operationEntity.OperateUser == code ? operationEntity.ReasonCategoryId : 0,
                    ReasonContent = operationEntity.OperateUser == code ? operationEntity.ReasonContent : string.Empty,
                    Type = operationEntity.OperateUser == code ? (int)EnumLibrary.IDVoteType.Applicant : (int)EnumLibrary.IDVoteType.Committee,
                    UserCode = code,
                    VoteTime = _commonService.GetCurrentServerTime(),
                    Weight = operationEntity.OperateUser == code ? applyUserWeight : otherWeight,
                };

                defaultVoteInfos.Add(info);
            }
            _IDOperationVoteRepository.Insert(defaultVoteInfos);
        }

        public virtual void DeleteInvestmentDecisionForm(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
                throw new NotImplementedException();

            var info = _IDFormRepository.Table.SingleOrDefault(x => x.SerialNo == serialNo);

            _IDFormRepository.Delete(info);

            var votes = _IDVoteRepository.Table.Where(x => x.FormSerialNo == serialNo);

            _IDVoteRepository.Delete(votes.ToArray());
        }

        public virtual void DeleteInvestmentDecisionForm(IList<string> serialNos)
        {
            if (serialNos == null)
                throw new ArgumentNullException(nameof(serialNos));

            var forms = _IDFormRepository.Table.Where(x => serialNos.Contains(x.SerialNo));

            _IDFormRepository.Delete(forms.ToArray());

            var votes = _IDVoteRepository.Table.Where(x => serialNos.Contains(x.FormSerialNo));

            _IDVoteRepository.Delete(votes.ToArray());
        }

        public virtual InvestmentDecisionVote GetInvestmentDecisionVote(string investorCode, string formSerialNo)
        {
            if (investorCode == null)
                throw new ArgumentNullException(nameof(investorCode));

            if (formSerialNo == null)
                throw new ArgumentNullException(nameof(formSerialNo));

            var info = _IDVoteRepository.Table.SingleOrDefault(x => x.UserCode == investorCode && x.FormSerialNo == formSerialNo);

            return info;
        }

        public virtual void AddInvestmentDecisionVote(InvestmentDecisionVote entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDVoteRepository.Insert(entity);
        }

        public virtual void DeleteInvestmentDecisionVote(string investorCode, string formSerialNo)
        {
            if (investorCode == null)
                throw new ArgumentNullException(nameof(investorCode));

            if (formSerialNo == null)
                throw new ArgumentNullException(nameof(formSerialNo));

            var info = _IDVoteRepository.Table.SingleOrDefault(x => x.UserCode == investorCode && x.FormSerialNo == formSerialNo);

            _IDVoteRepository.Delete(info);
        }

        public virtual void UpdateInvestmentDecisionVote(InvestmentDecisionVote entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDVoteRepository.Update(entity);
        }

        public virtual IList<InvestmentDecisionVote> GetInvestmentDecisionVotes(string investorCode)
        {
            var query = _IDVoteRepository.TableNoTracking.Where(x => x.UserCode == investorCode && x.Flag != (int)EnumLibrary.IDVoteFlag.None);

            return query.ToList();
        }

        public virtual void InvestmentDecisionVoteProcess(string investorCode, string formSerialNo, EnumLibrary.IDVoteFlag flag, string reason)
        {
            if (string.IsNullOrEmpty(reason))
                reason = @"";

            reason = reason.Replace("'", "''");

            var commanText = $@"EXEC [dbo].[sp_InvestmentDecisionVoteProcess]
                                        @InvestorCode = '{investorCode}',
		                                @FormSerialNo = '{formSerialNo}',
		                                @VoteFlag = {(int)flag},
		                                @Reason =N'{@reason}'";

            _dbContext.ExecuteSqlCommand(commanText);
        }

        public virtual void IDOperationVoteProcess(string investorCode, string applyNo, string operateNo, EnumLibrary.IDVoteFlag flag, int reasonCategoryId, string reasonContent, bool isAdminVeto)
        {
            if (string.IsNullOrEmpty(reasonContent))
                reasonContent = @"";

            var commanText = $@"EXEC [dbo].[sp_IDOperationVoteProcess]
                                        @InvestorCode = '{investorCode}',
		                                @ApplyNo = '{applyNo}',
                                        @OperateNo='{operateNo}',
		                                @VoteFlag = {(int)flag},
                                        @ReasonCategoryId ={reasonCategoryId },
		                                @ReasonContent =N'{reasonContent}',
                                        @IsAdminVeto = {isAdminVeto}";

            _dbContext.ExecuteSqlCommand(commanText);
        }

        public virtual MarketTrendForecastDetail GetMTFDetail(string investorCode, string serialNo)
        {
            var detail = _MTFDetailRepository.Table.SingleOrDefault(x => x.InvestorCode == investorCode && x.SerialNo == serialNo);

            return detail;
        }

        public virtual void UpdateMTFDetail(MarketTrendForecastDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _MTFDetailRepository.Update(entity);
        }

        public void DeleteMTFInfo(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
                throw new NotImplementedException();

            var info = _MTFInfoRepository.Table.SingleOrDefault(x => x.SerialNo == serialNo);

            _MTFInfoRepository.Delete(info);

            var votes = _MTFDetailRepository.Table.Where(x => x.SerialNo == serialNo);

            _MTFDetailRepository.Delete(votes.ToArray());
        }

        public virtual IList<InvestmentDecisionStockPool> GetIDStockPool()
        {
            return _IDStockPoolRepository.Table.ToList();
        }

        public virtual InvestmentDecisionStockPool GetIDStockPoolByCode(string stockCode)
        {
            var info = _IDStockPoolRepository.Table.FirstOrDefault(x => x.StockCode == stockCode);

            return info;
        }

        public virtual void UpdateIDStockPool(InvestmentDecisionStockPool entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            _IDStockPoolRepository.Update(entity);
        }

        public virtual void DeleteIDStockPool(IList<string> stockCodes, string operateCode)
        {
            if (stockCodes == null)
                throw new NullReferenceException(nameof(stockCodes));

            foreach (var stockCode in stockCodes)
            {
                var info = _IDStockPoolRepository.Table.Where(x => x.StockCode == stockCode).SingleOrDefault();

                if (info == null) continue;

                _IDStockPoolRepository.Delete(info);

                var logModel = new InvestmentDecisionStockPoolLog
                {
                    StockCode = stockCode,
                    Type = (int)EnumLibrary.OperateType.Delete,
                    OperatorCode = operateCode,
                    OperateTime = _commonService.GetCurrentServerTime(),
                };

                AddIDStockPoolLog(logModel);
            }
        }

        public virtual void AddIDStockPool(InvestmentDecisionStockPool entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            _IDStockPoolRepository.Insert(entity);
        }

        public virtual void AddIDStockPoolLog(InvestmentDecisionStockPoolLog entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDStockPoolLogRepository.Insert(entity);
        }

        public virtual IList<InvestmentDecisionCommittee> GetIDCommittees()
        {
            var query = _IDCommitteeRepository.Table;

            return query.ToList();
        }

        public virtual void AddIDCommittee(string code, string name)
        {
            var entity = new InvestmentDecisionCommittee
            {
                Code = code,
                Name = name,
            };

            _IDCommitteeRepository.Insert(entity);

            UpdateCommitteeWeight();
        }

        public virtual void DeleteIDCommittee(IList<int> ids)
        {
            if (ids == null)
                throw new NullReferenceException(nameof(ids));

            var committees = _IDCommitteeRepository.Table.Where(x => ids.Contains(x.Id)).ToList();
            _IDCommitteeRepository.Delete(committees);

            UpdateCommitteeWeight();
        }

        public virtual PositionStockAnalysisDetail GetPSADetailById(int id)
        {
            var detail = _PSADetailRepository.GetById(id);

            return detail;
        }

        public virtual void DeletePSAInfo(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
                throw new NotImplementedException();

            var info = _PSAInfoRepository.Table.SingleOrDefault(x => x.SerialNo == serialNo);
            _PSAInfoRepository.Delete(info);

            var details = _PSADetailRepository.Table.Where(x => x.SerialNo == serialNo);
            _PSADetailRepository.Delete(details.ToArray());

            var summarys = _PSASummaryRepository.Table.Where(x => x.SerialNo == serialNo);
            _PSASummaryRepository.Delete(summarys);
        }

        public virtual void UpdatePSADetail(PositionStockAnalysisDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _PSADetailRepository.Update(entity);
        }

        public virtual PositionStockAnalysisSummary GetPSASummaryById(int id)
        {
            var info = _PSASummaryRepository.GetById(id);

            return info;
        }

        public virtual void UpdatePSASummary(PositionStockAnalysisSummary entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            _PSASummaryRepository.Update(entity);
        }

        public virtual void UpdateIDReasonCategory(int id, string name)
        {
            var category = _DRCategoryRepository.GetById(id);

            category.Name = name;

            _DRCategoryRepository.Update(category);
        }

        public virtual void DeleteIDReasonCategory(int id)
        {
            var childrenCount = _DRCategoryRepository.Table.Where(x => x.ParentId == id).Count();
            if (childrenCount > 0)
                throw new Exception("该分类下存在子分类，无法删除！");

            _DRCategoryRepository.Delete(_DRCategoryRepository.GetById(id));
        }

        public virtual IList<DecisionReasonCategoryEntity> GetIDReasonCategories(string jointMark = "->")
        {
            var commandText = $@"SELECT *,[dbo].[f_GetReasonCategoryNameWithParent](Id,'{jointMark}') FullName FROM DecisionReasonCategory ";

            var categories = _dbContext.SqlQuery<DecisionReasonCategoryEntity>(commandText);

            return categories.ToList();
        }

        public virtual int AddIDReasonCategory(DecisionReasonCategoryEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var category = new DecisionReasonCategory
            {
                Name = entity.Name,
                ParentId = entity.ParentId,
                Remarks = entity.Remarks
            };

            _DRCategoryRepository.Insert(category);

            return category.Id;
        }

        public virtual void DeleteIDReasonContent(int[] contentIds)
        {
            if (contentIds == null)
                throw new NullReferenceException(nameof(contentIds));

            var contents = _DRContentRepository.Table.Where(x => contentIds.Contains(x.Id));
            _DRContentRepository.Delete(contents);
        }

        public virtual void AddIDReasonContent(DecisionReasonContent entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            _DRContentRepository.Insert(entity);
        }

        public virtual void UpdateIDReasonContent(DecisionReasonContent entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            _DRContentRepository.Update(entity);
        }

        public virtual DecisionReasonContent GetIDReasonContent(int contentId)
        {
            var content = _DRContentRepository.GetById(contentId);

            return content;
        }

        public virtual string GetIDReasonCategoryNameWithParent(int categoryId, string jointMark = "->")
        {
            var commanText = $@"SELECT [dbo].[f_GetReasonCategoryNameWithParent]({categoryId},'{jointMark}')  NameWithParent";
            var query = _dbContext.SqlQuery<string>(commanText);

            return query.FirstOrDefault();
        }

        public virtual InvestmentDecisionOperationVote GetIDOperationVoteInfo(string userCode, string operateNo)
        {
            var query = _IDOperationVoteRepository.TableNoTracking.Where(x => x.UserCode == userCode && x.OperateNo == operateNo);

            return query.FirstOrDefault();
        }

        public virtual InvestmentDecisionOperationVote GetIDOperationVoteAdminVetoInfo(string operateNo)
        {
            var query = _IDOperationVoteRepository.TableNoTracking.Where(x => x.OperateNo == operateNo && x.Type == (int)EnumLibrary.IDVoteType.OneVoteVeto);

            return query.FirstOrDefault();
        }

        public virtual void IDOperationAccuracyProcess(string investorCode, string applyNo, string operateNo, EnumLibrary.IDVoteFlag voteFlag, string reasonContent, bool isAdminVeto)
        {
            if (string.IsNullOrEmpty(reasonContent))
                reasonContent = @"";

            var commanText = $@"EXEC [dbo].[sp_IDOperationAccuracyProcess]
                                        @InvestorCode = '{investorCode}',
		                                @ApplyNo = '{applyNo}',
                                        @OperateNo='{operateNo}',
		                                @VoteFlag = {(int)voteFlag},
		                                @ReasonContent =N'{reasonContent}',
                                        @IsAdminVeto = {isAdminVeto}";

            _dbContext.ExecuteSqlCommand(commanText);
        }

        public virtual InvestmentDecisionOperationAccuracy GetIDOperationAccuracyInfo(string userCode, string operateNo)
        {
            var query = _IDAccuracyRepository.TableNoTracking.Where(x => x.UserCode == userCode && x.OperateNo == operateNo && x.IsAdminVeto == false);

            return query.FirstOrDefault();
        }

        public virtual InvestmentDecisionOperationAccuracy GetIDOperationAccuracyAdminVetoInfo(string operateNo)
        {
            var query = _IDAccuracyRepository.TableNoTracking.Where(x => x.OperateNo == operateNo && x.IsAdminVeto == true);

            return query.FirstOrDefault();
        }

        public virtual IList<int> GetIDOperationRelatedRecordIds(string operateNo)
        {
            var query = _IDTradeRecordRepository.TableNoTracking.Where(x => x.OperateNo == operateNo).Select(x => x.DailyRecordId);

            return query.ToList();
        }

        public virtual void AddIDOperationRelatedRecords(string applyNo, string operateNo, IList<int> recordIds)
        {
            if (recordIds == null)
                throw new ArgumentNullException(nameof(recordIds));

            var previousRecords = _IDTradeRecordRepository.Table.Where(x => x.OperateNo == operateNo);

            if (previousRecords.Any())
                _IDTradeRecordRepository.Delete(previousRecords);

            if (recordIds.Any())
            {
                foreach (var recordId in recordIds)
                {
                    var item = new InvestmentDecisionTradeRecord
                    {
                        ApplyNo = applyNo,
                        OperateNo = operateNo,
                        DailyRecordId = recordId,
                    };
                    _IDTradeRecordRepository.Insert(item);
                }
            }

            var operationInfo = _IDOperationRepository.Table.Where(x => x.OperateNo == operateNo).FirstOrDefault();

            if (operationInfo != null)
            {
                operationInfo.TradeRecordRelateFlag = recordIds.Any();
            }

            _IDOperationRepository.Update(operationInfo);
        }

        public virtual void UpdateIDApplicationStatus(string applyNo, int status)
        {
            var application = _IDApplicationRepository.Table.Where(x => x.ApplyNo == applyNo).FirstOrDefault();

            if (application != null)
            {
                application.Status = status;
                application.UpdateTime = _commonService.GetCurrentServerTime();

                _IDApplicationRepository.Update(application);
            }
        }

        public virtual void DeleteInvestmentDecisionOperation(string applyNo, string operateNo)
        {
            var commanText = $@"EXEC [dbo].[sp_IDOperationDeleteProcess] @ApplyNo = '{applyNo}', @OperateNo='{operateNo}'";

            _dbContext.ExecuteSqlCommand(commanText);
        }

        public virtual void StopInvestmentDecisionOperation(string operateNo, string stopReasonContent)
        {
            var operation = _IDOperationRepository.Table.Where(x => x.OperateNo == operateNo).FirstOrDefault();

            if (operateNo != null)
            {
                operation.IsStopped = true;
                operation.ReasonContent += $@"{System.Environment.NewLine} {System.Environment.NewLine} 【强制中止】：{System.Environment.NewLine} {stopReasonContent}";
                operation.UpdateTime = _commonService.GetCurrentServerTime();

                _IDOperationRepository.Update(operation);
            }
        }

        public virtual void AddInvestmentPlanRecord(string serialNo, string stockCode, string stockName, string investorCode, DateTime analysisDate)
        {
            var now = _commonService.GetCurrentServerTime();

            var ipr = new InvestmentPlanRecord
            {
                AnalysisDate = analysisDate,
                CreateTime = now,
                DealDate = null,
                InvestorCode = investorCode,
                Probability =null,
                SerialNo = serialNo,
                StockCode = stockCode,
                StockName = stockName,
                UpdateTime = now,
            };

            _IPRRepository.Insert(ipr);
        }

        public virtual void DeleteInvestmentPlanRecord(IList<int> recordIds)
        {
            if (recordIds == null)
                throw new ArgumentNullException(nameof(recordIds));

            var query = _IPRRepository.Table;
            query = query.Where(x => recordIds.Contains(x.Id));

            _IPRRepository.Delete(query);
        }

        public virtual InvestmentPlanRecord GetIPRInfo(int id)
        {
            var info = _IPRRepository.GetById(id);

            return info;
        }

        public virtual  void UpdateIPRInfo(InvestmentPlanRecord entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IPRRepository.Update(entity);
        }

        #endregion Methods
    }
}