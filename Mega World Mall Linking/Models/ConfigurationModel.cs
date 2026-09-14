using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Models
{
    public class ConfigurationModel
    {
        //Fields Store Config Info
        private string TenantCode;
        private string merchantName;
        private string terminalNo;
        private string terminalNo2;
        private string terminalNo3;
        private string temploc;
        private string tempdatabaseName;
        private string databaseUser;
        private string databasePassword;
#pragma warning disable CS0169 // The field 'ConfigurationModel.discountType' is never used
        private string discountType;
#pragma warning restore CS0169 // The field 'ConfigurationModel.discountType' is never used
        //Fields Sale Config Info
        private string salesLocation;
        private string DatabaseLocation;
        private string sls_Type;
        private string salesBIR;
        private string salesPOSSerial;
        //POS Field Config
        private string storekey;
        private string poskey1;
        private string poskey2;
        private string poskey3;

        //Properties -Validations for Store Info
        [DisplayName("Tenant Code")]
        [Required(ErrorMessage = "Tenant Code is required.")]
        public string TenanCode
        {
            get { return TenantCode; }
            set { TenantCode = value; }
        }

        [DisplayName("Database password")]
        public string DatabasePassword
        {
            get { return databasePassword; }
            set { databasePassword = value; }
        }
        [DisplayName("Terminal Number")]
        [Required(ErrorMessage = "POS Terminal number is required.")]
        public string TerminalNumber
        {
            get { return terminalNo; }
            set { terminalNo = value; }
        }

        [DisplayName("Sales Location")]
        [Required(ErrorMessage = "Sales Location is required.")]
        public string SalesLocation
        {
            get { return salesLocation; }
            set { salesLocation = value; }
        }
        [DisplayName("Database Location")]
        [Required(ErrorMessage = "Database Location is required.")]
        public string db_Location
        {
            get { return DatabaseLocation; }
            set { DatabaseLocation = value; }
        }
        [DisplayName("Sales Type")]
        [Required(ErrorMessage = "Sale Type is required.")]
        public string SalesType
        {
            get { return sls_Type; }
            set { sls_Type = value; }
        }
        [DisplayName("Temp_db location")]
        public string TempLocation
        {
            get { return temploc; }
            set { temploc = value; }
        }

        [DisplayName("Temp_db Name")]
        public string TempDB
        {
            get { return tempdatabaseName; }
            set { tempdatabaseName = value; }
        }

        private bool autoEodEnabled = true;
        private string autoEodTime = "23:30";

        [DisplayName("Auto EOD Enabled")]
        public bool AutoEodEnabled
        {
            get { return autoEodEnabled; }
            set { autoEodEnabled = value; }
        }

        [DisplayName("Auto EOD Time")]
        public string AutoEodTime
        {
            get { return string.IsNullOrWhiteSpace(autoEodTime) ? "23:30" : autoEodTime; }
            set { autoEodTime = value; }
        }
    }
    public class ConfigurationModelConfig
    {
        public List<ConfigurationModel> configurationModels { get; set; }
    }
    public class PaymentModel
    {
        private string wboxPayment { get; set; }
        private string mallPayment { get; set; }

        [DisplayName("Wbox Payment")]
        [Required(ErrorMessage = "Wbox payment is required")]
        public string WboxPayment
        {
            get { return wboxPayment; }
            set { wboxPayment = value; }
        }
        [DisplayName("Mall Payment")]
        [Required(ErrorMessage = "MallPayment payment is required")]
        public string MallPayment
        {
            get { return mallPayment; }
            set { mallPayment = value; }
        }
    }
    public class PaymentModelConfig
    {
        public List<PaymentModel> paymentModels { get; set; }
    }
    public static class GlobalVar
    {
        public static string inputPassword { get; set; }
    }

    public class DiscountModel
    {
        private string wboxDiscount;
        private string mallDiscount;

        [DisplayName("Wbox Discount")]
        [Required(ErrorMessage = "Wbox discount is required")]
        public string WboxDiscount
        {
            get { return wboxDiscount; }
            set { wboxDiscount = value; }
        }
        [DisplayName("Mall Discount")]
        [Required(ErrorMessage = "MallPayment discount is required")]
        public string MallDiscount
        {
            get { return mallDiscount; }
            set { mallDiscount = value; }
        }

    }
    public class DiscountModelConfig
    {
        public List<DiscountModel> discountModels { get; set; }
    }
}
