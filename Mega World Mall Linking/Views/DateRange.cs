using Mega_World_Mall_Linking.Constant;
using Mega_World_Mall_Linking.Helpers;
using Mega_World_Mall_Linking.LocalStorage;
using Mega_World_Mall_Linking.Models;
using Mega_World_Mall_Linking.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarsier.Extensions;

namespace Mega_World_Mall_Linking.Views
{
    public partial class DateRange : Form
    {
        public SettingWriter _settings = null;
        public SettingWriter _settingEOD = null;
        public DbParadox _dbParadox = null;
        public DbSQLite _dbsqlite = null;
        public ConfigurationModelConfig _systemConfig = null;
        public List<ConfigurationModel> _system = new List<ConfigurationModel>();
        public List<PaymentModel> _payment = new List<PaymentModel>();
        public List<HourlySalesEntry> hourlySalesEntries = new List<HourlySalesEntry>();
        public List<DiscountModel> _disc = new List<DiscountModel>();
        public List<DiscountDetailEntry> discountEntries = new List<DiscountDetailEntry>();
        public List<DiscountModel> _discount = new List<DiscountModel>();
        public DailyDataStorage dailySales = null;
        public PaymentModelConfig _paymentConfig = null;
        public DailyDataStorage dailyDataStorage = null;
        public DailyDataDetailsStorage dailyDataDetailsStorage = null;
        public HourlyDataStorage hourlyDataStorage = null;
        public DiscountDataStorage discountDataStorage = null;
        public DiscountModelConfig _discountConfig = null;
        private string _databaseFileName = Path.Combine(Application.StartupPath, TempDBName.DATABASE);
        private string TENT_CODE;
        private string TER_NO;
        private string PRDX_DBLOC;
        private string PRDX_DBPASS;
        private string SLS_LOC;
        private string TEMP_DBLOC;
        private string TEMP_DBNAME;
        private string SLS_TYPE;
        private int tranCnt;
        private int CusCnt;
        private DateTime dateNow;
        private bool _isBackup;
        #region DAILYDATA
        public string DLY_TNTCODE;
        public string DLY_TERNO;
        public string DLY_DATE;
        public string DLY_STR_TRAN;
        public string DLY_END_TRAN;
        public decimal DLY_OLD_GRANTOT;
        public decimal DLY_NEW_GRANTOT;
        public decimal DLY_TOT_GROSS;
        public decimal DLY_NON_TAXSLS;
        public decimal DLY_TOT_SCDISC;
        public decimal DLY_TOT_OTHDISC;
        public decimal DLY_TOT_REF_AMT;
        public decimal DLY_TOT_TAXAMT;
        public decimal DLY_TOT_SRVC_CHRGE;
        public decimal DLY_TOT_NETSLS;
        public decimal DLY_TOT_CASHSLS;
        public decimal DLY_TOT_CHRGESLS;
        public decimal DLY_TOT_OTHSLS;
        public decimal DLY_TOT_VOIDAMT;
        public int DLY_TOT_CUSCNT;
        public int DLY_CTRLNO;
        public int DLY_TOT_SLSTRAN;
        public string DLY_SLSTYPE;
        public decimal DLY_PER_SLS;
        public int DLY_EODCNT;
        private string discountName;
        private decimal discountAmount;
        #endregion
        #region HOURLYDATA
        public string HR_TNTCODE;
        public string HR_TERNO;
        public string HR_TRN_ID;
        public string HR_DATE;
        public string HR_CODE;
        public decimal HR_NETSLS;
        public int HR_SLSCNT;
        public int HR_CUSCNT;
        //public decimal HR_NETSLS;
        public decimal HR_TOT_NETSLS;
        public int HR_TOT_SLSCNT;
        public int HR_TOT_CUSCNT;
        #endregion
        #region DISCDATA
        public string DS_TRN_ID;
        public string DS_DISCCODE;
        public string DS_DISCRIPT;
        public decimal DS_DISCAMT;
        public int DS_BATCHNO;
        #endregion
        private void InitializeDailyData()
        {
            DLY_TNTCODE = TENT_CODE;
            DLY_TERNO = TER_NO;
            DLY_DATE = DateTime.Now.ToString("yyyy-MM-dd");
            DLY_OLD_GRANTOT = 0.00M;
            DLY_NEW_GRANTOT = 0.00M;
            DLY_TOT_GROSS = 0.00M;
            DLY_NON_TAXSLS = 0.00M;
            DLY_TOT_SCDISC = 0.00M;
            DLY_TOT_OTHDISC = 0.00M;
            DLY_TOT_REF_AMT = 0.00M;
            DLY_TOT_TAXAMT = 0.00M;
            DLY_TOT_SRVC_CHRGE = 0.00M;
            DLY_TOT_NETSLS = 0.00M;
            DLY_TOT_CASHSLS = 0.00M;
            DLY_TOT_CHRGESLS = 0.00M;
            DLY_TOT_OTHSLS = 0.00M;
            DLY_TOT_VOIDAMT = 0.00M;
            DLY_TOT_CUSCNT = 0;
            DLY_CTRLNO = 1;
            DLY_TOT_SLSTRAN = 0;
            DLY_SLSTYPE = SLS_TYPE;
            DLY_PER_SLS = 0.00M;
            DLY_EODCNT = 0;
        }
        private void InitializeHourlyData()
        {
            HR_TNTCODE = TENT_CODE;
            HR_TERNO = TER_NO;
            HR_TRN_ID = string.Empty;
            HR_DATE = DateTime.Now.ToString("yyyy-MM-dd");
            HR_CODE = string.Empty;
            HR_NETSLS = 0.00M;
            HR_SLSCNT = 0;
            HR_CUSCNT = 0;
            HR_TOT_NETSLS = 0.00M;
            HR_TOT_SLSCNT = 0;
            HR_TOT_CUSCNT = 0;
        }

        private void InitializeDiscountData()
        {
            DS_TRN_ID = string.Empty;
            DS_DISCCODE = "";
            DS_DISCRIPT = "";
            DS_DISCAMT = 0.00M;
            DS_BATCHNO = 0;
        }
        public DateRange()
        {
            InitializeComponent();
            InitializeSystem();
           
        }
        private void InitializeSystem()
        {
            try
            {
                //lastHour = DateTime.Now.Hour;
                if (!InitializeConfiguration())
                {
                    MessageBox.Show("System setting is not yet configured.", "Invalid Configuration", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    Settings config = new Settings();
                    config.ShowDialog();
                    Environment.Exit(1);
                }
                if (!string.IsNullOrEmpty(PRDX_DBLOC))
                {
                    _dbParadox = new DbParadox(PRDX_DBLOC, PRDX_DBPASS);
                    _dbsqlite = new DbSQLite(TEMP_DBLOC, TEMP_DBNAME);
                    _discountConfig = _settings.Read<DiscountModelConfig>("DiscountConfig");
                    _paymentConfig = _settings.Read<PaymentModelConfig>("PaymentConfig");
                    _disc = _discountConfig?.discountModels ?? new List<DiscountModel>();
                    _discount = _disc;
                    _payment = _paymentConfig?.paymentModels ?? new List<PaymentModel>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0}/{1}", ex.Message, ex.StackTrace), "System Initialization", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private bool InitializeConfiguration()
        {
            _settings = new SettingWriter(Path.Combine(Application.StartupPath, "Settings"), false);
            //_settingsEOD = new SettingsWriter(Path.Combine(Application.StartupPath, "Settings"), true);
            bool validConfig = true;
            if (_settings == null)
            {
                _systemConfig = new ConfigurationModelConfig();
            }
            else
            {
                _systemConfig = _settings.Read<ConfigurationModelConfig>("SystemConfig");
                if (_systemConfig == null)
                {
                    validConfig = false;
                }
            }

            if (validConfig)
            {
                _system = _systemConfig.configurationModels;
                if (_system != null)
                {
                    foreach (ConfigurationModel configuration in _system)
                    {
                        TENT_CODE = configuration.TenanCode;
                        TER_NO = configuration.TerminalNumber;
                        PRDX_DBLOC = configuration.db_Location;
                        PRDX_DBPASS = configuration.DatabasePassword;
                        SLS_LOC = configuration.SalesLocation;
                        TEMP_DBLOC = configuration.TempLocation;
                        TEMP_DBNAME = configuration.TempDB;
                        SLS_TYPE = configuration.SalesType;
                    }
                }
            }

            return validConfig;
        }
        private void DateRange_Load(object sender, EventArgs e)
        {
            string systemDateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            //string date = DateTime.Now.ToString("MM/dd/yyyy");
            string date = DateTime.Now.ToString(systemDateFormat);

            dtp_StartDate.Value = DateTime.Parse(date);
            dtp_EndDate.Value = DateTime.Parse(date);
            dailyDataDetailsStorage = new DailyDataDetailsStorage();
            dailyDataStorage = new DailyDataStorage();
            dailySales = dailyDataStorage;
            hourlyDataStorage = new HourlyDataStorage();
            discountDataStorage = new DiscountDataStorage();
            SqlLiteTable.ORDERDATA = "orderdata";
            SqlLiteTable.DISCDATA = "discountdata";
            SqlLiteTable.ITEMS = "itemdata";
            SqlLiteTable.VOIDDATA = "voiddata";
            SqlLiteTable.PAYMENTDATA = "paymentdata";

            _isBackup = true;
            ParadoxTable.ORDERS = (_isBackup ? "ordbkup" : "orders");
            ParadoxTable.PAYMENTS = (_isBackup ? "paybkup" : "payment");
            ParadoxTable.ITEMS = (_isBackup ? "itemBkup" : "orditem");
        }
        private void getPayment(string paymenttype, string ornum)
        {
            _payment = _paymentConfig?.paymentModels ?? new List<PaymentModel>();

            string query = string.Format("Select * from {0} where OrderNo = '{1}'", SqlLiteTable.PAYMENTDATA, ornum);
            DataTable dtpayment = _dbsqlite.GetDataTable(query);

            if (dtpayment != null && dtpayment.Rows.Count > 0)
            {
                foreach (DataRow row in dtpayment.Rows)
                {
                    decimal payment1Value = row["Amount"].ToSafeDecimal();
                    string payName = row["Name"]?.ToString() ?? paymenttype;

                    if (payName.Trim().ToUpper() == "CASH")
                    {
                        DLY_TOT_CASHSLS += payment1Value;
                    }
                    else
                    {
                        DLY_TOT_OTHSLS += payment1Value;
                    }
                }
            }
        }
        private string GetPayment(string orNum, bool multiPayment, int count)
        {
            string paymenttype = "CASH";
            string otherpaystring1 = "";
            _payment = _paymentConfig?.paymentModels ?? new List<PaymentModel>();
            if (multiPayment)
            {
                string cmdtextMultiPay = string.Format(Queries.GET_PAYMENTNAME, count);
                DataTable dt = _dbParadox.GetDataTable(cmdtextMultiPay);
                if (dt != null && dt.Rows.Count > 0)
                {
                    paymenttype = dt.Rows[0]["Name2"].ToString();
                }
            }
            else
            {
                string cmdtext = String.Format("Select String1 From {0} Where OrderNo = '{1}'", ParadoxTable.ORDERS, orNum);
                DataTable dt = _dbParadox.GetDataTable(cmdtext);
                if (dt != null && dt.Rows.Count > 0)
                {
                    paymenttype = dt.Rows[0]["String1"].ToString();
                }

                string cmdpay = String.Format(String.Format("Select Remark From DefPay Where Name1 = '{0}'", paymenttype));
                DataTable dtpay = _dbParadox.GetDataTable(cmdpay);
                if (dtpay != null)
                {
                    foreach (DataRow row in dtpay.Rows)
                    {
                        if (row["Remark"].ToString() == "10")
                        {
                            string cmdotherpay = String.Format("Select * From {0} Where OrderNo = '{1}'", ParadoxTable.PAYMENTS, orNum);
                            DataTable dtotherpay = _dbParadox.GetDataTable(cmdotherpay);
                            if (dtotherpay != null && dtotherpay.Rows.Count > 0)
                            {
                                otherpaystring1 = dtotherpay.Rows[0]["String1"].ToString();
                            }

                            string cmddefpay2 = String.Format("Select * From DEFPAY2 Where code = '{0}'", otherpaystring1);
                            DataTable dtdefpay2 = _dbParadox.GetDataTable(cmddefpay2);
                            if (dtdefpay2 != null && dtdefpay2.Rows.Count > 0)
                            {
                                paymenttype = dtdefpay2.Rows[0]["Name1"] == DBNull.Value ? "CASH" : Convert.ToString(dtdefpay2.Rows[0]["Name1"]);
                            }
                        }
                    }
                }
            }
            return paymenttype;
        }

        private void HourlySales(DateTime dateNow)
        {
            tranCnt = 0;
            InitializeHourlyData();
            hourlySalesEntries.Clear();
            dateNow = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);

            string GetHourlyData = string.Format(Queries.SELECT_TABLE_WHERE, SqlLiteTable.ORDERDATA, (string.Format("AccDAte = '{0}'", dateNow.ToString("yyyyMMdd"))));
            DataTable dtHourly = _dbsqlite.GetDataTable(GetHourlyData);

            if (dtHourly.Rows.Count > 0)
            {
                int s = 0;
                int e = 0;
                string Start = string.Format(Queries.GET_STARTHOUR, SqlLiteTable.ORDERDATA, dateNow.ToString("yyyyMMdd"));
                DataTable sHR = _dbsqlite.GetDataTable(Start);
                string end = string.Format(Queries.GET_ENDHOUR, SqlLiteTable.ORDERDATA, dateNow.ToString("yyyyMMdd"));
                DataTable eHR = _dbsqlite.GetDataTable(end);
                if (sHR.Rows.Count > 0)
                {
                    foreach (DataRow drHR1 in sHR.Rows)
                    {
                        s = Convert.ToDateTime(drHR1["Time"]).Hour;
                    }
                }
                if (eHR.Rows.Count > 0)
                {
                    foreach (DataRow drHR2 in eHR.Rows)
                    {
                        e = Convert.ToDateTime(drHR2["Time"]).Hour;
                    }
                }

                HR_TNTCODE = TENT_CODE.ToString();
                HR_TERNO = TER_NO.ToString();
                HR_DATE = dateNow.ToString("MMddyyyy");

                #region hourly Details
                for (int hr = s; hr <= e; hr++)
                {
                    int hrTrnCnt = 0;
                    int hrCucCnt = 0;
                    decimal srvcCharge = 0;
                    decimal hrNetSales = 0.00M;
                    string str_HR = string.Format("{0:D2}:00:00", hr);
                    string end_HR = string.Format("{0:D2}:59:59", hr);
                    string gethourlydetail = string.Format(Queries.SELECT_TABLE_WHERE, SqlLiteTable.ORDERDATA, (string.Format("AccDAte = '{0}' and Time Between '{1}' and '{2}'", dateNow.ToString("yyyyMMdd"), str_HR, end_HR)));
                    DataTable dtHourlyDetails = _dbsqlite.GetDataTable(gethourlydetail);

                    if (dtHourlyDetails.Rows.Count > 0)
                    {
                        foreach (DataRow Hdr in dtHourlyDetails.Rows)
                        {
                            hrTrnCnt = hrTrnCnt + 1;
                            hrCucCnt = hrCucCnt + 1;
                            hrNetSales = hrNetSales + Hdr["Total"].ToSafeDecimal();
                            srvcCharge = srvcCharge + Hdr["ServiceCharge"].ToSafeDecimal();
                        }
                        DateTime hour = dateNow.Date.AddHours(hr);
                        HR_TOT_SLSCNT = HR_TOT_SLSCNT + hrTrnCnt;
                        HR_TOT_CUSCNT = HR_TOT_CUSCNT + hrCucCnt;
                        HR_TOT_NETSLS = HR_TOT_NETSLS + hrNetSales;
                        hourlySalesEntries.Add(new HourlySalesEntry
                        {
                            Timestamp = hour,
                            NetSalesAmount = hrNetSales,
                            TransactionCount = hrTrnCnt,
                            CustomerCount = hrCucCnt
                        });
                        #region saving to temp.db(HOURLY)
                        var HourlySalesDetails = new HourlyDataDetails
                        {
                            TRAN_ID = dtHourly.Rows[0]["OrderNo"].ToString(),
                            HOUR_CODE = hour.ToString(),
                            HNET_SLS = hrNetSales,
                            TRN_NO = hrTrnCnt,
                            CUS_NO = hrCucCnt
                        };
                        #endregion 
                    }
                }
                #endregion

                #region creating text file for Hourly
                var salesFile = new HourlySalesFile
                {
                    TenantCode = HR_TNTCODE,
                    POSTerminalNumber = HR_TERNO.ToSafeInteger(),
                    BusinessDate = HR_DATE,
                    HourlyEntries = hourlySalesEntries,
                    TotalNetSalesAmount = HR_TOT_NETSLS,
                    TotalTransactionCount = HR_TOT_SLSCNT,
                    TotalCustomerCount = HR_TOT_CUSCNT,
                    DateNoFormat = dateNow
                };

                var hourSales = new HourlyData
                {
                    TENANTCODE = HR_TNTCODE,
                    TER_NO = HR_TERNO,
                    TRAN_ID = dtHourly.Rows[0]["OrderNo"].ToString(),
                    BUS_DATE = HR_DATE,
                    TOT_NET = HR_TOT_NETSLS,
                    TOT_CUS = HR_TOT_CUSCNT,
                    TOT_TRN = HR_TOT_SLSCNT
                };
                hourlyDataStorage.Add(hourSales, false);
                #endregion

                HourlySalesFileGenerator.GenerateFile(salesFile, SLS_LOC);
            }
        }
        private void DiscountSales(DateTime dateNow)
        {
            InitializeDiscountData();
            discountEntries.Clear();
            dateNow = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);
            int Batch;
            string transcation = string.Format(Queries.SELECT_TABLE_WHERE, SqlLiteTable.ORDERDATA, (string.Format("AccDAte = '{0}'", dateNow.ToString("yyyyMMdd"))));
            DataTable dtTransaction = _dbsqlite.GetDataTable(transcation);

            if (dtTransaction.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTransaction.Rows)
                {
                    if (!discountDataStorage.IsExistValue("ORDER_ID", dr["OrderNo"].ToString()))
                    {
                        InitializeDiscountData();
                        string getDiscount = string.Format(Queries.SELECT_TABLE_WHERE, SqlLiteTable.DISCDATA, (string.Format("OrderNo = '{0}'", dr["OrderNo"].ToString())));
                        DataTable discount = _dbsqlite.GetDataTable(getDiscount);

                        if (discount.Rows.Count > 0)
                        {
                            foreach (DataRow dsr in discount.Rows)
                            {
                                int PREV_CNTR = 0;
                                string rawType = dsr["Type"]?.ToString() ?? string.Empty;
                                DiscountModel disc = _disc.Find(x => x.MallDiscount == rawType || x.WboxDiscount == rawType);
                                string d = disc != null ? disc.MallDiscount : rawType;
                                DiscountModel disDisc = _disc.Find(x => x.MallDiscount == d || x.WboxDiscount == d);
                                DS_TRN_ID = dsr["OrderNo"].ToString();
                                DS_DISCCODE = d;
                                DS_DISCRIPT = disDisc != null ? disDisc.WboxDiscount : d;
                                DS_DISCAMT = DS_DISCAMT + dsr["Amount"].ToSafeDecimal();

                                DataTable dtEOD = dailySales.GetLastEOD(TER_NO);
                                if (dtEOD.Rows.Count > 0)
                                {
                                    foreach (DataRow rowEOD in dtEOD.Rows)
                                    {
                                        PREV_CNTR = rowEOD["EOD_CNT"].ToSafeInteger();
                                    }
                                }
                                DS_BATCHNO = PREV_CNTR + 1;
                                discountEntries.Add(new DiscountDetailEntry
                                {
                                    DiscountCode = DS_DISCCODE,
                                    DiscountDescription = DS_DISCRIPT,
                                    DiscountAmount = DS_DISCAMT
                                });
                                var DiscData = new DiscountData
                                {
                                    ORDER_ID = DS_TRN_ID,
                                    DISCOUNT_CODE = DS_DISCCODE,
                                    DISCOUNT_DESC = DS_DISCRIPT,
                                    DISCOUNT_AMT = DS_DISCAMT
                                };
                                discountDataStorage.Add(DiscData, false);
                            }
                        }
                    }
                }
            }
            DiscountDetailFile data = new DiscountDetailFile
            {
                TenantCode = TENT_CODE,
                POSTerminalNumber = Convert.ToInt32(TER_NO),
                BatchNumber = DS_BATCHNO,
                BusinessDate = dateNow,
                Entries = discountEntries
            };
            //DiscountDetailFile.GenerateFile(data, outputDirectory);
            DiscountDetailFileGenerator.GenerateFile(data, SLS_LOC);
        }
        public void DailySales(DateTime dateNow)
        {
            InitializeDailyData();
            dateNow = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);
            DLY_TNTCODE = TENT_CODE;
            DLY_TERNO = TER_NO;
            DLY_DATE = dateNow.ToString("MMddyyyy");

            string GetHourlyData = $"SELECT * FROM {SqlLiteTable.ORDERDATA} WHERE AccDate = '{dateNow:yyyyMMdd}'";
            DataTable dtHourly = _dbsqlite.GetDataTable(GetHourlyData);

            List<DailyDataDetails> salesTypeList = new List<DailyDataDetails>();

            if (dtHourly.Rows.Count > 0)
            {
                foreach (DataRow dr in dtHourly.Rows)
                {
                    tranCnt++;
                    CusCnt++;

                    string getDisc = $"SELECT * FROM {SqlLiteTable.DISCDATA} WHERE OrderNo = '{dr["OrderNo"]}'";
                    DataTable dtDisc = _dbsqlite.GetDataTable(getDisc);
                    if (dtDisc != null)
                    {
                        foreach (DataRow drDisc in dtDisc.Rows)
                        {
                            string discType = drDisc["Type"]?.ToString().Trim().ToUpper() ?? "";
                            if (discType == "SC" || discType == "SCD" || discType == "SENIOR CITIZEN")
                            {
                                DLY_TOT_SCDISC += drDisc["Amount"].ToSafeDecimal();
                                DLY_NON_TAXSLS += drDisc["Amount"].ToSafeDecimal();
                            }
                            else
                            {
                                DLY_TOT_OTHDISC += drDisc["Amount"].ToSafeDecimal();
                            }
                        }
                    }

                    string getVoided = $"SELECT * FROM {SqlLiteTable.VOIDDATA} WHERE OrderID = '{dr["OrderNo"]}'";
                    DataTable dtVoid = _dbsqlite.GetDataTable(getVoided);
                    if (dtVoid != null)
                    {
                        foreach (DataRow dvr in dtVoid.Rows)
                        {
                            DLY_TOT_VOIDAMT += dr["Total"].ToSafeDecimal();
                        }
                    }

                    DLY_TOT_TAXAMT += dr["TaxTotal"].ToSafeDecimal();
                    DLY_TOT_SRVC_CHRGE += dr["ServiceCharge"].ToSafeDecimal();

                    string getPayment1 = $"SELECT * FROM {SqlLiteTable.PAYMENTDATA} WHERE OrderNo = '{dr["OrderNo"]}'";
                    DataTable dtpay = _dbsqlite.GetDataTable(getPayment1);
                    if (dtpay != null)
                    {
                        foreach (DataRow dvp in dtpay.Rows)
                        {
                            decimal payVal = dvp["Amount"].ToSafeDecimal();
                            string payName = dvp["Name"]?.ToString() ?? "";
                            if (payName.Trim().ToUpper() == "CASH")
                            {
                                DLY_TOT_CASHSLS += payVal;
                            }
                            else
                            {
                                DLY_TOT_OTHSLS += payVal;
                            }
                        }
                    }

                    DLY_TOT_CUSCNT = CusCnt;
                    DLY_CTRLNO = 1;
                    DLY_TOT_SLSTRAN = tranCnt;

                    salesTypeList.Add(new DailyDataDetails
                    {
                        SLS_TYPE = SLS_TYPE,
                        NET_SLS = dr["Total"].ToSafeDecimal()
                    });
                }
            }

                string GetStrTran = string.Format(Queries.SELECT_TABLE_WHERE_LIMIT_ASC, SqlLiteTable.ORDERDATA, (string.Format("AccDate = '{0}'", dateNow.ToString("yyyyMMdd"))));
                DataTable dtStart = _dbsqlite.GetDataTable(GetStrTran);
                if (dtStart != null && dtStart.Rows.Count > 0)
                {
                    DLY_STR_TRAN = dtStart.Rows[0]["OrderNo"].ToString();
                }
                string GetEndTran = string.Format(Queries.SELECT_TABLE_WHERE_LIMIT_DESC, SqlLiteTable.ORDERDATA, (string.Format("AccDate = '{0}'", dateNow.ToString("yyyyMMdd"))));
                DataTable dtEnd = _dbsqlite.GetDataTable(GetEndTran);
                if (dtEnd != null && dtEnd.Rows.Count > 0)
                {
                    DLY_END_TRAN = dtEnd.Rows[0]["OrderNo"].ToString();
                }

                DLY_TOT_NETSLS = DLY_TOT_CASHSLS + DLY_TOT_CHRGESLS + DLY_TOT_OTHSLS;
                DLY_TOT_GROSS = DLY_TOT_NETSLS + DLY_TOT_SCDISC + DLY_TOT_OTHDISC;

                string GetGT = $"SELECT * FROM dailysls WHERE BUS_DATE = '{dateNow.AddDays(-1):yyyy-MM-dd}'";
                DataTable dtGT = dailyDataStorage.GetDataTable(GetGT);
                if (dtGT != null && dtGT.Rows.Count > 0)
                {
                    DLY_OLD_GRANTOT = dtGT.Rows[0]["NEW_GRNTOT"].ToSafeDecimal();
                }
                else
                {
                    DLY_OLD_GRANTOT = 0.00M;
                }
                DLY_NEW_GRANTOT = DLY_TOT_NETSLS + DLY_OLD_GRANTOT;

                int currentEodCount = 0;
                DataTable dtPrevEOD = dailyDataStorage.GetLastEOD(TER_NO);
                if (dtPrevEOD != null && dtPrevEOD.Rows.Count > 0)
                {
                    currentEodCount = dtPrevEOD.Rows[0]["EOD_CNT"].ToSafeInteger();
                }
                int newEodCount = currentEodCount + 1;
                DLY_EODCNT = newEodCount;

                var header = new DailyDataHeader
                {
                    TenantCode = DLY_TNTCODE,
                    POSTerminalNumber = DLY_TERNO,
                    Date = dateNow,
                    OldAccumulatedTotal = DLY_OLD_GRANTOT,
                    NewAccumulatedTotal = DLY_NEW_GRANTOT,
                    TotalGrossSalesAmount = DLY_TOT_GROSS,
                    TotalNonTaxableSalesAmount = DLY_NON_TAXSLS,
                    TotalSeniorCitizenDiscount = DLY_TOT_SCDISC,
                    TotalOtherDiscount = DLY_TOT_OTHDISC,
                    TotalRefundAmount = 0,
                    TotalTaxAmount = DLY_TOT_TAXAMT,
                    TotalServiceCharge = DLY_TOT_SRVC_CHRGE,
                    TotalNetSalesAmount = DLY_TOT_NETSLS,
                    TotalCashSales = DLY_TOT_CASHSLS,
                    TotalChargeSales = DLY_TOT_CHRGESLS,
                    TotalGCOtherSales = DLY_TOT_OTHSLS,
                    TotalVoidAmount = DLY_TOT_VOIDAMT,
                    TotalCustomerCount = DLY_TOT_CUSCNT,
                    ControlNumber = DLY_CTRLNO,
                    TotalNumberOfTransactions = DLY_TOT_SLSTRAN
                };

                var dly_sls = new Daily_SLS
                {
                    TENANT_CODE = DLY_TNTCODE,
                    TER_NO = DLY_TERNO,
                    BUS_DATE = dateNow.ToString("yyyy-MM-dd"),
                    STR_TRN_ID = DLY_STR_TRAN,
                    END_TRN_ID = DLY_END_TRAN,
                    OLD_GRNTOT = DLY_OLD_GRANTOT,
                    NEW_GRNTOT = DLY_NEW_GRANTOT,
                    GROSS_SLS = DLY_TOT_GROSS,
                    NON_TAXSLS = DLY_NON_TAXSLS,
                    SC_DISC = DLY_TOT_SCDISC,
                    OTHR_DISC = DLY_TOT_OTHDISC,
                    REFUND = DLY_TOT_REF_AMT,
                    VAT_AMT = DLY_TOT_TAXAMT,
                    SRVC_CHRG = DLY_TOT_SRVC_CHRGE,
                    NET_SLS = DLY_TOT_NETSLS,
                    CASH_SLS = DLY_TOT_CASHSLS,
                    CHRG_SLS = DLY_TOT_CHRGESLS,
                    GC_OTHR_SLS = DLY_TOT_OTHSLS,
                    VOID_AMT = DLY_TOT_VOIDAMT,
                    CUS_CNT = DLY_TOT_CUSCNT,
                    CTL_NO = DLY_CTRLNO,
                    TRN_CNT = DLY_TOT_SLSTRAN,
                    EOD_CNT = newEodCount
                };
                dailyDataStorage.Add(dly_sls, false);

                DailySalesGenerator.Generate(header, salesTypeList, SLS_LOC, newEodCount);
            }

        private async Task btn_Send_ClickAsync(object sender, EventArgs e)
        {
            DateTime startDate = dtp_StartDate.Value.Date;
            DateTime endDate = dtp_EndDate.Value.Date;
            int totalDays = (endDate - startDate).Days + 1;

            Loading loadingForm = new Loading();
            loadingForm.Show();

            var progress = new Progress<(int current, string date)>(p =>
            {
                loadingForm.SetProgress(p.current, totalDays, $"Processing {p.date}...");
            });

            await Task.Run(() =>
            {
                int currentProgress = 0;

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    HourlySales(date);
                    DiscountSales(date);
                    DailySales(date);

                    currentProgress++;

                    ((IProgress<(int, string)>)progress).Report((currentProgress, date.ToString("yyyy-MM-dd")));
                }
            });

            loadingForm.Close();
            MessageBox.Show("All reports processed successfully.");
        }

        private async void btn_Send_Click(object sender, EventArgs e)
        {
            await btn_Send_ClickAsync(sender, e);
        }
    }
}



