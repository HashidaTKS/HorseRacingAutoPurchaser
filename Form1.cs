using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseRacingAutoPurchaser
{
    public partial class Form1 : Form
    {

        private AutoPurchaserMainTask AutoPurchaserMainTask;

        public Form1()
        {
            InitializeComponent();
            AutoPurchaserMainTask = new AutoPurchaserMainTask();

            var betConfigRepository = new BetConfigRepository();
            var betConfig = betConfigRepository.ReadAll();
            if (betConfig == null)
            {
                betConfig = new BetConfig();
                betConfigRepository.Store(betConfig);
            }
            SetPurchaseSetting(betConfig);
        }

        private void Button_LoginConfig_Click(object sender, EventArgs e)
        {
            var loginConfigForm = new LoginConfigForm();
            loginConfigForm.Show();
        }

        private void Button_AutoPurchase_Click(object sender, EventArgs e)
        {
            if (!AutoPurchaserMainTask.Running)
            {
                StoreCurrentConfig();
                AutoPurchaserMainTask.Run();
                label_Running.Visible = true;
            }
        }

        private void Button_AutoPurchase_Cancel_Click(object sender, EventArgs e)
        {
            if (AutoPurchaserMainTask.Running)
            {
                AutoPurchaserMainTask.Stop();
                label_Running.Visible = false;
            }
        }

        private void Button_SavePurchaseSetting_Click(object sender, EventArgs e)
        {
            StoreCurrentConfig();
        }

        private void StoreCurrentConfig()
        {
            var betConfig = new BetConfig();
            betConfig.QuinellaBetConfig.OddsRatio = (double)numericUpDown_QuinellaOddsRatio.Value;
            betConfig.QuinellaBetConfig.MinimumOdds = (double)numericUpDown_QuinellaMinimumOdds.Value;
            betConfig.QuinellaBetConfig.MaximumOdds = (double)numericUpDown_QuinellaMaximumOdds.Value;
            betConfig.QuinellaBetConfig.MinimumPayBack = (double)numericUpDown_QuinnelaMinimumPayBack.Value;
            betConfig.QuinellaBetConfig.UseCocomo = checkBox_QuinellaUseCocomo.Checked;
            betConfig.QuinellaBetConfig.CocomoThreshold = (int)numericUpDown_QuinellaCocomoThreshold.Value;
            betConfig.QuinellaBetConfig.CocomoMaxMagnification = (int)numericUpDown_QuinellaCocomoMaxMagnification.Value;
            betConfig.QuinellaBetConfig.PurchaseCentral = checkBox_PurchaseCentral.Checked;
            betConfig.QuinellaBetConfig.PurchaseRegional = checkBox_PurchaseRegional.Checked;

            new BetConfigRepository().Store(betConfig);
        }

        private void Button_ResetPurchaseSetting_Click(object sender, EventArgs e)
        {
            SetPurchaseSetting(new BetConfig());
        }

        private void SetPurchaseSetting(BetConfig betConfig)
        {
            numericUpDown_QuinellaOddsRatio.Value = (decimal)betConfig.QuinellaBetConfig.OddsRatio;
            numericUpDown_QuinellaMinimumOdds.Value = (decimal)betConfig.QuinellaBetConfig.MinimumOdds;
            numericUpDown_QuinellaMaximumOdds.Value = (decimal)betConfig.QuinellaBetConfig.MaximumOdds;
            numericUpDown_QuinnelaMinimumPayBack.Value = (decimal)betConfig.QuinellaBetConfig.MinimumPayBack;
            checkBox_QuinellaUseCocomo.Checked = betConfig.QuinellaBetConfig.UseCocomo;
            numericUpDown_QuinellaCocomoThreshold.Value = betConfig.QuinellaBetConfig.CocomoThreshold;
            numericUpDown_QuinellaCocomoMaxMagnification.Value = betConfig.QuinellaBetConfig.CocomoMaxMagnification;
            checkBox_PurchaseCentral.Checked = betConfig.QuinellaBetConfig.PurchaseCentral;
            checkBox_PurchaseRegional.Checked = betConfig.QuinellaBetConfig.PurchaseRegional;
        }
    }
}
