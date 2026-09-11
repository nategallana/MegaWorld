using Mega_World_Mall_Linking.Constant;
using Mega_World_Mall_Linking.Helpers;
using Mega_World_Mall_Linking.Models;
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

namespace Mega_World_Mall_Linking.Views
{
    public partial class Settings : Form
    {
        public SettingWriter _settings = null;
        public SettingWriter _settingsEOD = null;
        public DBSql _dbSql = null;
        public List<ConfigurationModel> _system = new List<ConfigurationModel>();
        public ConfigurationModelConfig _systemConfig = null;
        public List<DiscountModel> _discount = new List<DiscountModel>();
        public DiscountModelConfig _discountConfig = null;
        public DbParadox _dbParadox = null;
        public DbSQLite _dbSQLite = null;
        public List<PaymentModel> _payment = new List<PaymentModel>();
        public PaymentModelConfig _paymentConfig = null;
        private string sls_Type;
        bool isLoading = true;
        public Settings()
        {
            InitializeComponent();
            _settings = new SettingWriter(Path.Combine(Application.StartupPath, "Settings"), false);
            _settingsEOD = new SettingWriter(Path.Combine(Application.StartupPath, "Settings"), true);
            InitializeSystem();
            InitializePayment();
            InitializeDiscount();
        }

        private void InitializeDiscount()
        {
            _discountConfig = _settings.Read<DiscountModelConfig>("DiscountConfig");
            if (_discountConfig == null)
            {
                _discountConfig = new DiscountModelConfig();
            }
            else
            {
                _discount = _discountConfig.discountModels;

                if (_discount == null)
                {
                    _discount = new List<DiscountModel>();
                }
                else
                {
                    foreach (DiscountModel discount in _discount)
                    {
                        dgv_Discount.Rows.Add(discount.WboxDiscount, discount.MallDiscount);
                    }
                }
            }
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("tabPage2");
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("tabPage3");
        }

        private void btn_txtFP_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Browse destination folder for CSV sales files";
                if (fbd.ShowDialog().Equals(DialogResult.OK))
                {
                    txt_txtFilePath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btn_DBPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Browse destination folder for CSV sales files";
                if (fbd.ShowDialog().Equals(DialogResult.OK))
                {
                    txt_DBPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void bunifuLabel10_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ///sls_Type = dpd_SlsType.SelectedValue.ToString();
            if (dpd_SlsType.Text.ToString() == "FOOD")
            {
                sls_Type = "01";
            }
            else if (dpd_SlsType.Text.ToString() == "NON-FOOD")
            {
                sls_Type = "02";
            }
            else if (dpd_SlsType.Text.ToString() == "GROCERIES")
            {
                sls_Type = "03";
            }
            else if (dpd_SlsType.Text.ToString() == "MEDICINES")
            {
                sls_Type = "04";
            }
            else
            {
                sls_Type = "05";
            }

            var model = new ConfigurationModel();
            model.TenanCode = txt_TenantCode.Text.Trim();
            model.TerminalNumber = txt_TerminalNo.Text.Trim();
            model.SalesType = sls_Type;
            model.SalesLocation = txt_txtFilePath.Text.Trim();
            model.db_Location = txt_DBPath.Text.Trim();
            model.DatabasePassword = txt_dbPassword.Text.Trim();
            model.TempDB = txt_tempDB.Text.Trim();
            model.TempLocation = txt_TempLoc.Text.Trim();
            //model.SalesType = sls_Type;
            new ModelDataValidation().Validate(model);

            #region Basic config
            _system = new List<ConfigurationModel>();
            _system.Add(new ConfigurationModel()
            {
                TenanCode = txt_TenantCode.Text.Trim(),
                TerminalNumber = txt_TerminalNo.Text.Trim(),
                SalesLocation = txt_txtFilePath.Text.Trim(),
                db_Location = txt_DBPath.Text.Trim(),
                SalesType =sls_Type,
                DatabasePassword = txt_dbPassword.Text.Trim(),
                TempDB = txt_tempDB.Text.Trim(),
                TempLocation = txt_TempLoc.Text.Trim()
            });
            _systemConfig = new ConfigurationModelConfig();
            _systemConfig.configurationModels = _system;
            _settings.Save<ConfigurationModelConfig>(_systemConfig, "SystemConfig");
            #endregion

            #region Payment
            var paymodel = new PaymentModel();
            _payment = new List<PaymentModel>();
            foreach (DataGridViewRow row in dgv_Payment.Rows)
            {
                if (row.Cells["WboxPayment"].Value == null || row.Cells["MallPayment"].Value == null)
                {
                    continue;
                }
                _payment.Add(new PaymentModel()
                {
                    WboxPayment = row.Cells["WboxPayment"].Value.ToString(),
                    MallPayment = row.Cells["MallPayment"].Value.ToString()
                });
            }
            _paymentConfig = new PaymentModelConfig();
            _paymentConfig.paymentModels = _payment;
            _settings.Save<PaymentModelConfig>(_paymentConfig, "PaymentConfig");
            #endregion

            #region Discount
            var discmodel = new DiscountModel();
            _discount = new List<DiscountModel>();
            foreach (DataGridViewRow row in dgv_Discount.Rows)
            {
                if (row.Cells["WboxDiscount"].Value == null || row.Cells["MallDiscount"].Value == null)
                {
                    continue;
                }
                _discount.Add(new DiscountModel()
                {
                    WboxDiscount = row.Cells["WboxDiscount"].Value.ToString(),
                    MallDiscount = row.Cells["MallDiscount"].Value.ToString()
                });
            }
            _discountConfig = new DiscountModelConfig();
            _discountConfig.discountModels = _discount;
            _settings.Save<DiscountModelConfig>(_discountConfig, "DiscountConfig");
            #endregion
            MessageBox.Show("System Setting Saved.", "System Configured", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void dgv_Payment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("tabPage4");
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to continue reloading of payment types?", "Reload Payment Types", MessageBoxButtons.YesNo, MessageBoxIcon.Question, true ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                dgv_Payment.Rows.Clear();
                LoadPaymentsFromWbox(_dbParadox);
            }
        }
        private void LoadPaymentsFromWbox(DbParadox db)
        {
            DataTable dtPayment = db.GetDataTable(string.Format(Queries.GET_PAYMENT));
            if (dtPayment != null)
            {
                foreach (DataRow dr in dtPayment.Rows)
                {
                    dgv_Payment.Rows.Add(dr["Name2"]);
                }
            }
            //otherPayment
            DataTable dtOtherPayment = db.GetDataTable(string.Format(Queries.GET_OTHERPAYMENT));
            if (dtOtherPayment != null)
            {
                foreach (DataRow dr in dtOtherPayment.Rows)
                {
                    dgv_Payment.Rows.Add(dr["Name2"]);
                }
            }

        }
        public bool InitializeConfiguration()
        {
            _settings = new SettingWriter(Path.Combine(Application.StartupPath, "Settings"), false);
            _settingsEOD = new SettingWriter(Path.Combine(Application.StartupPath, "Settings"), true);
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
                        txt_TenantCode.Text = configuration.TenanCode;
                        txt_DBPath.Text = configuration.db_Location;
                        txt_txtFilePath.Text = configuration.SalesLocation;
                        txt_TerminalNo.Text = configuration.TerminalNumber;
                        txt_dbPassword.Text = configuration.DatabasePassword;
                        txt_TempLoc.Text = configuration.TempLocation;
                        txt_tempDB.Text = configuration.TempDB;
                        dpd_SlsType.Text = configuration.SalesType;
                    }
                }
            }
            return validConfig;
        }
        private void InitializePayment()
        {
            _paymentConfig = _settings.Read<PaymentModelConfig>("PaymentConfig");
            if(_paymentConfig == null)
            {
                _paymentConfig = new PaymentModelConfig();

            }
            else
            {
                _payment = _paymentConfig.paymentModels;
                if(_payment == null)
                {
                    _payment = new List<PaymentModel>();

                }
                else
                {
                    foreach (PaymentModel payment in _payment)
                    {
                        dgv_Payment.Rows.Add(payment.WboxPayment, payment.MallPayment);
                    }
                }
            }
        }
        private void InitializeSystem()
        {

            if (!InitializeConfiguration())
            {
                //MessageBox.Show("System setting is not yet configured.", "Invalid Configuration", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
            if (!string.IsNullOrEmpty(txt_DBPath.Text))
            {
                try
                {
                    _dbParadox = new DbParadox(txt_DBPath.Text.Trim(), txt_dbPassword.Text.Trim());
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(txt_TempLoc.Text))
            {
                try
                {
                    _dbSQLite = new DbSQLite(txt_TempLoc.Text.Trim(), txt_tempDB.Text.Trim());
                }
                catch { }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Browse destination folder for Temp DB location";
                if (fbd.ShowDialog().Equals(DialogResult.OK))
                {
                    txt_TempLoc.Text = fbd.SelectedPath;
                }
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to continue reloading of discount types?", "Reload Discount Types", MessageBoxButtons.YesNo, MessageBoxIcon.Question, true ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                dgv_Discount.Rows.Clear();
                LoadDiscountsFromWbox(_dbParadox);
            }
            else
            {
                return;
            }
        }
        private void LoadDiscountsFromWbox(DbParadox db)
        {
            DataTable dtDiscount = db.GetDataTable(string.Format(Queries.GET_DISCOUNT));
            if (dtDiscount != null)
            {
                foreach (DataRow dr in dtDiscount.Rows)
                {
                    dgv_Discount.Rows.Add(dr["Name1"]);
                }
            }
        }
    }
}
