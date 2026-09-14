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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarsier.Extensions;

namespace Mega_World_Mall_Linking.Views
{
    public partial class frmMain : Form
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
        public HourlyDataDetailsStorage hourlyDataDetailsStorage = null;
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
        private bool AUTO_EOD_ENABLED = true;
        private string AUTO_EOD_TIME = "23:30";
        private System.Windows.Forms.Timer autoEodTimer;
        private DateTime lastAutoEodDate = DateTime.MinValue;
        //public csvhelper herlper = null;
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
            tranCnt = 0;
            CusCnt = 0;
            DLY_TNTCODE = TENT_CODE;
            DLY_TERNO = TER_NO;
            DLY_DATE = DateTime.Now.ToString("yyyy-MM-dd");
            DLY_STR_TRAN = string.Empty;
            DLY_END_TRAN = string.Empty;
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
            HR_TOT_NETSLS = 0.00M;
            HR_TOT_SLSCNT = 0;
            HR_TOT_CUSCNT = 0;
        }

        private void InitializedHourlyDetailData()
        {
            HR_CODE = string.Empty;
            HR_NETSLS = 0.00M;
            HR_SLSCNT = 0;
            HR_CUSCNT = 0;
        }

        private void InitializeDiscountData()
        {
            DS_TRN_ID = string.Empty;
            DS_DISCCODE = "";
            DS_DISCRIPT = "";
            DS_DISCAMT = 0.00M;
            DS_BATCHNO = 0;
        }
        public frmMain()
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
                    try
                    {
                        _dbParadox = new DbParadox(PRDX_DBLOC, PRDX_DBPASS);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Failed to initialize Paradox database at '{PRDX_DBLOC}'", ex);
                        MessageBox.Show($"Unable to connect to Paradox database: {ex.Message}\n\nPlease check database configuration in Settings.", "Database Connection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    try
                    {
                        _dbsqlite = new DbSQLite(TEMP_DBLOC, TEMP_DBNAME);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Failed to initialize SQLite database at '{TEMP_DBLOC}' ({TEMP_DBNAME})", ex);
                        MessageBox.Show($"Unable to connect to SQLite database: {ex.Message}\n\nPlease verify Temp DB path in Settings.", "Database Connection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    _discountConfig = _settings.Read<DiscountModelConfig>("DiscountConfig");
                    _paymentConfig = _settings.Read<PaymentModelConfig>("PaymentConfig");
                    _disc = _discountConfig?.discountModels ?? new List<DiscountModel>();
                    _discount = _disc;
                    _payment = _paymentConfig?.paymentModels ?? new List<PaymentModel>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("System Initialization encountered an error", ex);
                MessageBox.Show(String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace), "System Initialization", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                        AUTO_EOD_ENABLED = configuration.AutoEodEnabled;
                        AUTO_EOD_TIME = string.IsNullOrWhiteSpace(configuration.AutoEodTime) ? "23:30" : configuration.AutoEodTime;
                    }
                }
            }

            return validConfig;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            Program.DatabaseFile = _databaseFileName;
            notifyIcon1.BalloonTipText = "Running...";
            notifyIcon1.ShowBalloonTip(2000);
            dailyDataDetailsStorage = new DailyDataDetailsStorage();
            dailyDataStorage = new DailyDataStorage();
            discountDataStorage = new DiscountDataStorage();
            hourlyDataStorage = new HourlyDataStorage();
            hourlyDataDetailsStorage = new HourlyDataDetailsStorage();
            SqlLiteTable.ORDERDATA = "orderdata";
            SqlLiteTable.DISCDATA = "discountdata";
            SqlLiteTable.ITEMS = "itemdata";
            SqlLiteTable.VOIDDATA = "voiddata";
            SqlLiteTable.PAYMENTDATA = "paymentdata";

            _isBackup = true;
            ParadoxTable.ORDERS = (_isBackup ? "ordbkup" : "orders");
            ParadoxTable.PAYMENTS = (_isBackup ? "paybkup" : "payment");
            ParadoxTable.ITEMS = (_isBackup ? "itemBkup" : "orditem");

            Logger.LogInfo($"Megaworld Mall Linking service initialized. Terminal: {TER_NO}, Tenant: {TENT_CODE}, Output Directory: {SLS_LOC}");
            InitAutoEodTimer();
        }

        private void InitAutoEodTimer()
        {
            try
            {
                if (autoEodTimer != null)
                {
                    autoEodTimer.Stop();
                    autoEodTimer.Dispose();
                }

                autoEodTimer = new System.Windows.Forms.Timer();
                autoEodTimer.Interval = 30000; // Check every 30 seconds
                autoEodTimer.Tick += AutoEodTimer_Tick;
                autoEodTimer.Start();
                Logger.LogInfo($"Auto EOD timer started. Enabled: {AUTO_EOD_ENABLED}, Scheduled Time: {AUTO_EOD_TIME}");
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to initialize Auto EOD timer", ex);
            }
        }

        private void AutoEodTimer_Tick(object sender, EventArgs e)
        {
            if (!AUTO_EOD_ENABLED) return;
            if (lastAutoEodDate.Date == DateTime.Today) return;

            DateTime now = DateTime.Now;
            if (TimeSpan.TryParse(AUTO_EOD_TIME, out TimeSpan scheduledTime))
            {
                if (now.TimeOfDay >= scheduledTime)
                {
                    RunAutoEod();
                }
            }
        }

        private void RunAutoEod()
        {
            lastAutoEodDate = DateTime.Today;
            Logger.LogInfo($"[Auto EOD] Checking scheduled End of Day for {DateTime.Today:yyyy-MM-dd} (Scheduled: {AUTO_EOD_TIME}, Current: {DateTime.Now:HH:mm:ss})");

            if (_dbsqlite == null)
            {
                Logger.LogWarning("[Auto EOD] SQLite database connection is not ready. Auto EOD skipped.");
                return;
            }

            string countQuery = $"SELECT COUNT(*) FROM {SqlLiteTable.ORDERDATA} WHERE AccDate = '{DateTime.Today:yyyyMMdd}'";
            int count = 0;
            try
            {
                string res = _dbsqlite.ExecuteScalar(countQuery);
                int.TryParse(res, out count);
            }
            catch (Exception ex)
            {
                Logger.LogError("[Auto EOD] Error querying order count", ex);
            }

            if (count == 0)
            {
                Logger.LogInfo($"[Auto EOD] No sales transactions found for today ({DateTime.Today:yyyy-MM-dd}). Auto EOD skipped for today.");
                return;
            }

            try
            {
                dateNow = DateTime.Today;
                HourlySales();
                DiscountSales();
                DailySales();

                Logger.LogInfo($"[Auto EOD] End of Day reports successfully generated for {DateTime.Today:yyyy-MM-dd}. Transactions: {count}");

                notifyIcon1.BalloonTipTitle = "Megaworld Mall Linking - Auto EOD";
                notifyIcon1.BalloonTipText = $"Automatic End of Day completed for {DateTime.Today:yyyy-MM-dd}.\nTotal Transactions: {count}\nFiles saved in: {SLS_LOC}";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(4000);
            }
            catch (Exception ex)
            {
                Logger.LogError("[Auto EOD] Exception occurred during automated EOD generation", ex);
            }
        }

        private void generateEODToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DailySales();
            HourlySales();
            DiscountSales();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings config = new Settings();
            config.Show();

        }

        private void endOfDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.LogInfo($"[Manual EOD] End of Day triggered via system tray for {DateTime.Today:yyyy-MM-dd}");

            if (_dbsqlite == null)
            {
                Logger.LogError("[Manual EOD] Database is not initialized.");
                notifyIcon1.BalloonTipTitle = "Megaworld Mall Linking - End of Day";
                notifyIcon1.BalloonTipText = "Database is not initialized. Please verify configuration in Settings.";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                notifyIcon1.ShowBalloonTip(4000);
                return;
            }

            DateTime today = DateTime.Today;
            string countQuery = $"SELECT COUNT(*) FROM {SqlLiteTable.ORDERDATA} WHERE AccDate = '{today:yyyyMMdd}'";
            int count = 0;
            try
            {
                string res = _dbsqlite.ExecuteScalar(countQuery);
                int.TryParse(res, out count);
            }
            catch (Exception ex)
            {
                Logger.LogError("[Manual EOD] Error querying order count", ex);
            }

            if (count == 0)
            {
                Logger.LogWarning($"[Manual EOD] No sales transactions found for today ({today:yyyy-MM-dd}).");
                notifyIcon1.BalloonTipTitle = "Megaworld Mall Linking - End of Day";
                notifyIcon1.BalloonTipText = $"No sales transactions found for today ({today:yyyy-MM-dd}).\nIf you need to process past dates, please use 'Date Range'.";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                notifyIcon1.ShowBalloonTip(4000);
                return;
            }

            try
            {
                dateNow = today;
                HourlySales();
                DiscountSales();
                DailySales();

                Logger.LogInfo($"[Manual EOD] End of Day reports successfully generated for {today:yyyy-MM-dd}. Transactions: {count}, Output: {SLS_LOC}");

                MessageBox.Show(
                    $"End of Day reports have been successfully generated for {today:yyyy-MM-dd}.\n\nTotal Transactions: {count}\nFiles saved in: {SLS_LOC}",
                    "End of Day Completed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.LogError("[Manual EOD] Error during manual EOD generation", ex);
                MessageBox.Show($"Error generating End of Day reports: {ex.Message}", "End of Day Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HourlySales()
        {
            tranCnt = 0;
            InitializeHourlyData();
            InitializedHourlyDetailData();
            hourlySalesEntries.Clear();
            if (dateNow == DateTime.MinValue) dateNow = DateTime.Today;
            dateNow = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);

            int s = 0;
            int e = 0;
            string startQuery = $"SELECT CASE WHEN Time IS NOT NULL AND Time != '' THEN Time ELSE Time_Now END AS EffectiveTime FROM {SqlLiteTable.ORDERDATA} WHERE AccDate = '{dateNow:yyyyMMdd}' AND ((Time IS NOT NULL AND Time != '') OR (Time_Now IS NOT NULL AND Time_Now != '')) ORDER BY EffectiveTime ASC LIMIT 1";
            DataTable sHR = _dbsqlite.GetDataTable(startQuery);
            if (sHR != null && sHR.Rows.Count > 0)
            {
                if (DateTime.TryParse(sHR.Rows[0]["EffectiveTime"]?.ToString(), out DateTime dtStart))
                {
                    s = dtStart.Hour;
                }
            }

            string endQuery = $"SELECT CASE WHEN Time IS NOT NULL AND Time != '' THEN Time ELSE Time_Now END AS EffectiveTime FROM {SqlLiteTable.ORDERDATA} WHERE AccDate = '{dateNow:yyyyMMdd}' AND ((Time IS NOT NULL AND Time != '') OR (Time_Now IS NOT NULL AND Time_Now != '')) ORDER BY EffectiveTime DESC LIMIT 1";
            DataTable eHR = _dbsqlite.GetDataTable(endQuery);
            if (eHR != null && eHR.Rows.Count > 0)
            {
                if (DateTime.TryParse(eHR.Rows[0]["EffectiveTime"]?.ToString(), out DateTime dtEnd))
                {
                    e = dtEnd.Hour;
                }
            }

            HR_TNTCODE = TENT_CODE.ToString();
            HR_TERNO = TER_NO.ToString();
            HR_DATE = dateNow.ToString("MMddyyyy");

            if (sHR != null && sHR.Rows.Count > 0)
            {
                #region hourly Details
                for (int hr = s; hr <= e; hr++)
                {
                    int hrTrnCnt = 0;
                    int hrCusCnt = 0;
                    decimal srvcCharge = 0.00M;
                    decimal hrNetSales = 0.00M;
                    string str_HR = string.Format("{0:D2}:00:00", hr);
                    string end_HR = string.Format("{0:D2}:59:59", hr);

                    string gethourlydetail = $"SELECT * FROM {SqlLiteTable.ORDERDATA} WHERE AccDate = '{dateNow:yyyyMMdd}' AND (CASE WHEN Time IS NOT NULL AND Time != '' THEN Time ELSE Time_Now END) BETWEEN '{str_HR}' AND '{end_HR}'";
                    DataTable dtHourlyDetails = _dbsqlite.GetDataTable(gethourlydetail);

                    if (dtHourlyDetails != null && dtHourlyDetails.Rows.Count > 0)
                    {
                        foreach (DataRow Hdr in dtHourlyDetails.Rows)
                        {
                            bool isVoided = (Hdr.Table.Columns.Contains("Void") && Hdr["Void"] != DBNull.Value &&
                                            (Hdr["Void"].ToString() == "1" || Hdr["Void"].ToString().Equals("true", StringComparison.OrdinalIgnoreCase)));
                            string getVoided = $"SELECT * FROM {SqlLiteTable.VOIDDATA} WHERE OrderID = '{Hdr["OrderNo"]}'";
                            DataTable dtVoid = _dbsqlite.GetDataTable(getVoided);
                            if (dtVoid != null && dtVoid.Rows.Count > 0) isVoided = true;
                            if (isVoided) continue;

                            hrTrnCnt++;
                            hrCusCnt++;
                            hrNetSales += Hdr["Total"].ToSafeDecimal();
                            srvcCharge += Hdr["ServiceCharge"].ToSafeDecimal();
                        }

                        if (hrTrnCnt > 0)
                        {
                            DateTime hour = dateNow.Date.AddHours(hr);
                            HR_TOT_SLSCNT += hrTrnCnt;
                            HR_TOT_CUSCNT += hrCusCnt;
                            HR_TOT_NETSLS += hrNetSales;

                            hourlySalesEntries.Add(new HourlySalesEntry
                            {
                                Timestamp = hour,
                                NetSalesAmount = hrNetSales,
                                TransactionCount = hrTrnCnt,
                                CustomerCount = hrCusCnt
                            });

                            var HourlySalesDetails = new HourlyDataDetails
                            {
                                TRAN_ID = dtHourlyDetails.Rows[0]["OrderNo"].ToString(),
                                HOUR_CODE = hour.ToString(),
                                HNET_SLS = hrNetSales,
                                TRN_NO = hrTrnCnt,
                                CUS_NO = hrCusCnt
                            };
                            hourlyDataDetailsStorage.Add(HourlySalesDetails, false);
                        }
                    }
                }
                #endregion

                int batch = dailyDataStorage.GetDailyBatchNumber(TER_NO, dateNow);
                var salesFile = new HourlySalesFile
                {
                    TenantCode = HR_TNTCODE,
                    POSTerminalNumber = HR_TERNO.ToSafeInteger(),
                    BusinessDate = HR_DATE,
                    HourlyEntries = hourlySalesEntries,
                    TotalNetSalesAmount = HR_TOT_NETSLS,
                    TotalTransactionCount = HR_TOT_SLSCNT,
                    TotalCustomerCount = HR_TOT_CUSCNT,
                    DateNoFormat = dateNow,
                    BatchNumber = batch
                };

                var hourSales = new HourlyData
                {
                    TENANTCODE = HR_TNTCODE,
                    TER_NO = HR_TERNO,
                    TRAN_ID = sHR.Rows[0]["EffectiveTime"].ToString(),
                    BUS_DATE = HR_DATE,
                    TOT_NET = HR_TOT_NETSLS,
                    TOT_CUS = HR_TOT_CUSCNT,
                    TOT_TRN = HR_TOT_SLSCNT
                };
                hourlyDataStorage.Add(hourSales, false);

                HourlySalesFileGenerator.GenerateFile(salesFile, SLS_LOC);
            }
        }
        private void DiscountSales()
        {
            InitializeDiscountData();
            discountEntries.Clear();
            if (dateNow == DateTime.MinValue) dateNow = DateTime.Today;
            dateNow = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);

            DS_BATCHNO = dailyDataStorage.GetDailyBatchNumber(TER_NO, dateNow);

            string transaction = string.Format(Queries.SELECT_TABLE_WHERE, SqlLiteTable.ORDERDATA, string.Format("AccDate = '{0}'", dateNow.ToString("yyyyMMdd")));
            DataTable dtTransaction = _dbsqlite.GetDataTable(transaction);

            var groupedDiscounts = new Dictionary<string, DiscountDetailEntry>(StringComparer.OrdinalIgnoreCase);

            if (dtTransaction != null && dtTransaction.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTransaction.Rows)
                {
                    string orderNo = dr["OrderNo"]?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(orderNo)) continue;

                    bool isVoided = (dr.Table.Columns.Contains("Void") && dr["Void"] != DBNull.Value &&
                                    (dr["Void"].ToString() == "1" || dr["Void"].ToString().Equals("true", StringComparison.OrdinalIgnoreCase)));
                    string getVoided = string.Format("SELECT * FROM {0} WHERE OrderID = '{1}'", SqlLiteTable.VOIDDATA, orderNo);
                    DataTable dtVoid = _dbsqlite.GetDataTable(getVoided);
                    if (dtVoid != null && dtVoid.Rows.Count > 0) isVoided = true;
                    if (isVoided) continue;

                    string getDiscount = string.Format(Queries.SELECT_TABLE_WHERE, SqlLiteTable.DISCDATA, string.Format("OrderNo = '{0}'", orderNo));
                    DataTable discount = _dbsqlite.GetDataTable(getDiscount);

                    if (discount != null && discount.Rows.Count > 0)
                    {
                        foreach (DataRow dsr in discount.Rows)
                        {
                            decimal amount = dsr["Amount"].ToSafeDecimal();
                            if (amount <= 0) continue;

                            string rawType = dsr["Type"]?.ToString()?.Trim() ?? string.Empty;
                            if (string.IsNullOrEmpty(rawType)) continue;

                            DiscountModel disc = _disc?.Find(x =>
                                (!string.IsNullOrEmpty(x.WboxDiscount) && x.WboxDiscount.Trim().Equals(rawType, StringComparison.OrdinalIgnoreCase)) ||
                                (!string.IsNullOrEmpty(x.MallDiscount) && x.MallDiscount.Trim().Equals(rawType, StringComparison.OrdinalIgnoreCase)));

                            string discCode;
                            string discDesc;
                            if (disc != null && !string.IsNullOrWhiteSpace(disc.MallDiscount))
                            {
                                discCode = disc.MallDiscount.Trim();
                                discDesc = !string.IsNullOrWhiteSpace(disc.WboxDiscount) ? disc.WboxDiscount.Trim() : rawType;
                            }
                            else
                            {
                                string upper = rawType.ToUpper();
                                if (upper == "SENIOR" || upper == "SENIOR CITIZEN" || upper == "SC" || upper == "SCD")
                                {
                                    discCode = "SC";
                                    discDesc = "SENIOR CITIZEN";
                                }
                                else if (upper == "PWD" || upper.Contains("DISABILITY"))
                                {
                                    discCode = "PWD";
                                    discDesc = "PERSON WITH DISABILITY";
                                }
                                else if (upper == "NAAC" || upper.Contains("ATHLETE"))
                                {
                                    discCode = "NAAC";
                                    discDesc = "NATL ATHLETES & COACHES";
                                }
                                else if (upper == "SOLO PARENT" || upper == "SOLOPARENT" || upper == "SP")
                                {
                                    discCode = "SP";
                                    discDesc = "SOLO PARENT";
                                }
                                else if (upper == "MEDAL OF VALOR" || upper == "MOV")
                                {
                                    discCode = "MOV";
                                    discDesc = "MEDAL OF VALOR";
                                }
                                else
                                {
                                    discCode = rawType.Length > 6 ? rawType.Substring(0, 6) : rawType;
                                    discDesc = rawType;
                                }
                            }

                            if (!discountDataStorage.IsExistValue("ORDER_ID", orderNo))
                            {
                                var DiscData = new DiscountData
                                {
                                    ORDER_ID = orderNo,
                                    DISCOUNT_CODE = discCode,
                                    DISCOUNT_DESC = discDesc,
                                    DISCOUNT_AMT = amount
                                };
                                discountDataStorage.Add(DiscData, false);
                            }

                            string key = $"{discCode}|{discDesc}";
                            if (groupedDiscounts.ContainsKey(key))
                            {
                                groupedDiscounts[key].DiscountAmount += amount;
                            }
                            else
                            {
                                groupedDiscounts[key] = new DiscountDetailEntry
                                {
                                    DiscountCode = discCode,
                                    DiscountDescription = discDesc,
                                    DiscountAmount = amount
                                };
                            }
                        }
                    }
                }
            }

            discountEntries.AddRange(groupedDiscounts.Values);

            DiscountDetailFile data = new DiscountDetailFile
            {
                TenantCode = TENT_CODE,
                POSTerminalNumber = Convert.ToInt32(TER_NO),
                BatchNumber = DS_BATCHNO,
                BusinessDate = dateNow,
                Entries = discountEntries
            };
            DiscountDetailFileGenerator.GenerateFile(data, SLS_LOC);
        }
        public void DailySales()
        {
            InitializeDailyData();
            dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            string GetDailySales = $"SELECT * FROM orderdata WHERE AccDate = '{dateNow:yyyyMMdd}'";
            DataTable dtDaily = _dbsqlite.GetDataTable(GetDailySales);

            List<DailyDataDetails> salesTypeList = new List<DailyDataDetails>();
            var salesTypeTotals = new Dictionary<string, decimal>();

            //InitializeDailyData();
            if (dtDaily.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDaily.Rows)
                {
                    
                    bool isVoided = (dr.Table.Columns.Contains("Void") && dr["Void"] != DBNull.Value &&
                                    (dr["Void"].ToString() == "1" || dr["Void"].ToString().Equals("true", StringComparison.OrdinalIgnoreCase)));

                    string getVoided = $"SELECT * FROM voiddata WHERE OrderID = '{dr["OrderNo"]}'";
                    DataTable dtVoid = _dbsqlite.GetDataTable(getVoided);
                    if (dtVoid != null && dtVoid.Rows.Count > 0)
                    {
                        isVoided = true;
                    }

                    if (isVoided)
                    {
                        DLY_TOT_VOIDAMT += dr["Total"].ToSafeDecimal();
                        continue;
                    }

                    tranCnt++;
                    CusCnt++;

                    DLY_TNTCODE = TENT_CODE;
                    DLY_TERNO = TER_NO;
                    DLY_DATE = dateNow.ToString("MMddyyyy");

                    bool orderIsVatExempt = false;
                    decimal orderVatExemptDiscounts = 0.00M;

                    string getDisc = $"SELECT * FROM discountdata WHERE OrderNo = '{dr["OrderNo"]}'";
                    DataTable dtDisc = _dbsqlite.GetDataTable(getDisc);
                    foreach (DataRow drDisc in dtDisc.Rows)
                    {
                        string rawType = drDisc["Type"]?.ToString()?.Trim() ?? "";
                        string discType = rawType.ToUpper();
                        decimal discAmt = drDisc["Amount"].ToSafeDecimal();

                        DiscountModel disc = _disc?.Find(x =>
                            (!string.IsNullOrEmpty(x.WboxDiscount) && x.WboxDiscount.Trim().Equals(rawType, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrEmpty(x.MallDiscount) && x.MallDiscount.Trim().Equals(rawType, StringComparison.OrdinalIgnoreCase)));
                        string mappedCode = disc != null && !string.IsNullOrWhiteSpace(disc.MallDiscount) ? disc.MallDiscount.Trim().ToUpper() : discType;

                        // Government Mandated Discounts (#08): Senior Citizen, PWD, NAAC, Solo Parent, Medal of Valor
                        bool isGovMandated = discType == "SC" || discType == "SCD" || discType == "SENIOR CITIZEN" || discType == "SENIOR" ||
                                             discType == "PWD" || discType.Contains("DISABILITY") ||
                                             discType == "NAAC" || discType.Contains("ATHLETE") ||
                                             discType == "SOLO PARENT" || discType == "SOLOPARENT" || discType == "SP" ||
                                             discType == "MEDAL OF VALOR" || discType == "MOV" ||
                                             mappedCode == "SC" || mappedCode == "PWD" || mappedCode == "NAAC" || mappedCode == "SP" || mappedCode == "MOV";

                        if (isGovMandated)
                        {
                            DLY_TOT_SCDISC = DLY_TOT_SCDISC + discAmt;
                        }
                        else
                        {
                            DLY_TOT_OTHDISC = DLY_TOT_OTHDISC + discAmt;
                        }

                        // Non-taxable (VAT Exempt) check for #07:
                        // Senior and PWD are VAT-exempt. Diplomat and Zero Rated are also VAT-exempt (No VAT).
                        // NAAC, Solo Parent, and Medal of Valor are WITH VAT, so they do NOT add to Non-Taxable Sales.
                        bool isVatExempt = discType == "SC" || discType == "SCD" || discType == "SENIOR CITIZEN" || discType == "SENIOR" ||
                                           discType == "PWD" || discType.Contains("DISABILITY") ||
                                           discType == "DIPLOMAT" || discType == "ZERO RATED" || discType == "ZERORATED" ||
                                           mappedCode == "SC" || mappedCode == "PWD" || mappedCode == "DIPLOMAT" || mappedCode == "ZERORATED";

                        if (isVatExempt)
                        {
                            orderIsVatExempt = true;
                            orderVatExemptDiscounts += discAmt;
                        }
                    }

                    if (orderIsVatExempt)
                    {
                        // VAT-exempt sales base for this order
                        DLY_NON_TAXSLS += (dr["Total"].ToSafeDecimal() + orderVatExemptDiscounts);
                    }

                        DLY_TOT_TAXAMT = DLY_TOT_TAXAMT + dr["TaxTotal"].ToSafeDecimal();
                        DLY_TOT_SRVC_CHRGE = DLY_TOT_SRVC_CHRGE + dr["ServiceCharge"].ToSafeDecimal();

                        string getPayment1 = $"SELECT * FROM paymentdata WHERE OrderNo = '{dr["OrderNo"]}'";
                        DataTable dtpay = _dbsqlite.GetDataTable(getPayment1);
                        foreach (DataRow dvp in dtpay.Rows)
                        {
                            decimal payVal = dvp["Amount"].ToSafeDecimal();
                            string payName = dvp["Name"]?.ToString()?.Trim() ?? "";
                            string payUpper = payName.ToUpper();

                            var payModel = _payment?.Find(x => x.WboxPayment != null && x.WboxPayment.Trim().Equals(payName, StringComparison.OrdinalIgnoreCase));
                            string mappedPay = payModel != null && !string.IsNullOrWhiteSpace(payModel.MallPayment) ? payModel.MallPayment.Trim().ToUpper() : "";

                            if (mappedPay == "CASH" || payUpper == "CASH")
                            {
                                DLY_TOT_CASHSLS = DLY_TOT_CASHSLS + payVal;
                            }
                            else if (mappedPay == "CHARGE" || mappedPay == "CREDIT CARD" || mappedPay == "DEBIT CARD" || mappedPay == "CARD" ||
                                     payUpper == "CREDIT CARD" || payUpper == "DEBIT CARD" || payUpper == "CARD" ||
                                     payUpper.Contains("CREDIT") || payUpper.Contains("DEBIT") || payUpper == "CHARGE")
                            {
                                DLY_TOT_CHRGESLS = DLY_TOT_CHRGESLS + payVal;
                            }
                            else
                            {
                                DLY_TOT_OTHSLS = DLY_TOT_OTHSLS + payVal;
                            }
                        }

                        DLY_TOT_CUSCNT = CusCnt;
                        DLY_CTRLNO = 1;
                        DLY_TOT_SLSTRAN = tranCnt;

                        decimal orderTotal = dr["Total"].ToSafeDecimal();
                        string sType = string.IsNullOrWhiteSpace(SLS_TYPE) ? "01" : SLS_TYPE;
                        if (salesTypeTotals.ContainsKey(sType))
                        {
                            salesTypeTotals[sType] += orderTotal;
                        }
                        else
                        {
                            salesTypeTotals[sType] = orderTotal;
                        }

                        var details = new DailyDataDetails
                        {
                            TRAN_ID = dr["OrderNo"].ToString(),
                            SLS_TYPE = sType,
                            NET_SLS = orderTotal
                        };
                        dailyDataDetailsStorage.Add(details, false);
                    
                }

                // Aggregate Fields 21 & 22: One entry per unique Sales Type with daily total net sales
                foreach (var kvp in salesTypeTotals)
                {
                    salesTypeList.Add(new DailyDataDetails
                    {
                        SLS_TYPE = kvp.Key,
                        NET_SLS = kvp.Value
                    });
                }

                if (salesTypeList.Count == 0)
                {
                    salesTypeList.Add(new DailyDataDetails
                    {
                        SLS_TYPE = string.IsNullOrWhiteSpace(SLS_TYPE) ? "01" : SLS_TYPE,
                        NET_SLS = 0.00M
                    });
                }
                string GetStrTran = string.Format(Queries.SELECT_TABLE_WHERE_LIMIT_ASC, SqlLiteTable.ORDERDATA, (string.Format("AccDAte = '{0}'", dateNow.ToString("yyyyMMdd"))));
                DataTable dtStart = _dbsqlite.GetDataTable(GetStrTran);
                if (dtStart.Rows.Count > 0)
                {
                    foreach (DataRow st in dtStart.Rows)
                    {
                        DLY_STR_TRAN = st["OrderNo"].ToString();
                    }
                }
                string GetEndTran = string.Format(Queries.SELECT_TABLE_WHERE_LIMIT_DESC, SqlLiteTable.ORDERDATA, (string.Format("AccDAte = '{0}'", dateNow.ToString("yyyyMMdd"))));
                DataTable dtEnd = _dbsqlite.GetDataTable(GetEndTran);
                if (dtEnd.Rows.Count > 0)
                {
                    foreach (DataRow et in dtEnd.Rows)
                    {
                        DLY_END_TRAN = et["OrderNo"].ToString();
                    }
                }
                DLY_TOT_NETSLS = DLY_TOT_CASHSLS + DLY_TOT_CHRGESLS + DLY_TOT_OTHSLS;
                DLY_TOT_GROSS = DLY_TOT_NETSLS + DLY_TOT_SCDISC + DLY_TOT_OTHDISC;
                DLY_OLD_GRANTOT = dailyDataStorage.GetPreviousGrandTotal(TER_NO, dateNow);
                DLY_NEW_GRANTOT = DLY_TOT_NETSLS + DLY_OLD_GRANTOT;

                int batchNumber = dailyDataStorage.GetDailyBatchNumber(TER_NO, dateNow);
                DLY_EODCNT = batchNumber;

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
                    EOD_CNT = batchNumber
                };
                dailyDataStorage.Add(dly_sls, false);
                DailySalesGenerator.Generate(header, salesTypeList, SLS_LOC, batchNumber);
            }
        }
        private string GetSalesDiscount(string OrderNumber, DataTable dtDiscount)
        {
            if (dtDiscount == null || dtDiscount.Rows.Count == 0)
                return string.Empty;

            var discountNames = new List<string>();

            foreach (DataRow dr in dtDiscount.Rows)
            {
                if (dr["OrderNo"]?.ToString() != OrderNumber)
                    continue;

                string discountName = dr["Type"]?.ToString() ?? string.Empty;
                decimal discountAmount = dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0.00M;

                var disc = _discountConfig.discountModels?.Find(x => x.WboxDiscount == discountName);
                if (disc != null)
                {
                    discountName = disc.MallDiscount;
                }

                if (!string.IsNullOrEmpty(discountName))
                    discountNames.Add(discountName);
            }

            // Join discounts with comma if multiple
            return string.Join(", ", discountNames);
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
            _payment = _paymentConfig.paymentModels;
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
                //taena bahala na spaghetti code nalang muna to
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
                                otherpaystring1 = dt.Rows[0]["String1"].ToString();
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

        private void dateRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateRange dt_frm = new DateRange();
            dt_frm.Show();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

