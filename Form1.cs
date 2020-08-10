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
        private SimulatorMainTask SimulatorMainTask;


        public Form1()
        {
            InitializeComponent();
            AutoPurchaserMainTask = new AutoPurchaserMainTask();
            SimulatorMainTask = new SimulatorMainTask();

            var betConfigRepository = new BetConfigRepository();
            var betConfig = betConfigRepository.ReadAll();
            if (betConfig == null)
            {
                betConfig = new BetConfig();
                betConfigRepository.Store(betConfig);
            }
            SetPurchaseSetting(betConfig);
        }

        /// <summary>
        /// 大金を賭ける可能性があれば警告を表示する。
        /// 警告を表示する必要がないか、ユーザーが承認した場合true。
        /// </summary>
        /// <returns></returns>
        private bool AlertIfNeed()
        {
            if (numericUpDown_QuinnelaMinimumPayBack.Value >= 10000 ||
                (radioButton_QuinellaUseCocomo.Checked &&
                  (numericUpDown_QuinellaCocomoThreshold.Value < 50 || numericUpDown_QuinellaCocomoMaxMagnification.Value > 10)) ||
                numericUpDown_QuinellaMaxPurchaseCount.Value * numericUpDown_QuinnelaMinimumPayBack.Value >= 30000 ||
                numericUpDown_QuinnelaMinimumPayBack.Value >= 10000 * numericUpDown_QuinellaMinimumOdds.Value)
            {
                var result = MessageBox.Show("ベット額が大きくなる可能性があります。構いませんか？",
                "警告",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }


        private void Button_LoginConfig_Click(object sender, EventArgs e)
        {
            var loginConfigForm = new LoginConfigForm();
            loginConfigForm.Show();
        }

        private void Button_AutoPurchase_Click(object sender, EventArgs e)
        {
            if (!SimulatorMainTask.Running)
            {
                var isIntendedSetting = AlertIfNeed();
                if (isIntendedSetting)
                {
                    StoreCurrentConfig();
                    SimulatorMainTask.Run();
                    label_Running.Visible = true;
                }
            }
            return;
            if (!AutoPurchaserMainTask.Running)
            {
                var isIntendedSetting = AlertIfNeed();
                if (isIntendedSetting)
                {
                    StoreCurrentConfig();
                    AutoPurchaserMainTask.Run();
                    label_Running.Visible = true;
                }
            }
        }

        private void Button_AutoPurchase_Cancel_Click(object sender, EventArgs e)
        {
            if (SimulatorMainTask.Running)
            {
                SimulatorMainTask.Stop();
                label_Running.Visible = false;
            }
            return;
            if (AutoPurchaserMainTask.Running)
            {
                AutoPurchaserMainTask.Stop();
                label_Running.Visible = false;
            }
        }

        private void Button_SavePurchaseSetting_Click(object sender, EventArgs e)
        {
            var isIntendedSetting = AlertIfNeed();
            if (isIntendedSetting)
            {
                StoreCurrentConfig();
            }
        }

        private void StoreCurrentConfig()
        {
            var betConfig = new BetConfig();
            betConfig.QuinellaBetConfig.OddsRatio = (double)numericUpDown_QuinellaOddsRatio.Value;
            betConfig.QuinellaBetConfig.MinimumOdds = (double)numericUpDown_QuinellaMinimumOdds.Value;
            betConfig.QuinellaBetConfig.MaximumOdds = (double)numericUpDown_QuinellaMaximumOdds.Value;
            betConfig.QuinellaBetConfig.MinimumPayBack = (double)numericUpDown_QuinnelaMinimumPayBack.Value;
            betConfig.QuinellaBetConfig.MaxPurchaseCount = (int)numericUpDown_QuinellaMaxPurchaseCount.Value;
            betConfig.QuinellaBetConfig.UseCocomo = radioButton_QuinellaUseCocomo.Checked;
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
            numericUpDown_QuinellaMaxPurchaseCount.Value = betConfig.QuinellaBetConfig.MaxPurchaseCount;
            radioButton_QuinellaUseCocomo.Checked = betConfig.QuinellaBetConfig.UseCocomo;
            radioButton_QuinellaNoBetTactics.Checked = !radioButton_QuinellaUseCocomo.Checked;
            numericUpDown_QuinellaCocomoThreshold.Value = betConfig.QuinellaBetConfig.CocomoThreshold;
            numericUpDown_QuinellaCocomoMaxMagnification.Value = betConfig.QuinellaBetConfig.CocomoMaxMagnification;
            checkBox_PurchaseCentral.Checked = betConfig.QuinellaBetConfig.PurchaseCentral;
            checkBox_PurchaseRegional.Checked = betConfig.QuinellaBetConfig.PurchaseRegional;
            ChangeBetTacticsStatus();
        }

        private void RadioButton_QuinellaUseCocomo_CheckedChanged(object sender, EventArgs e)
        {
            ChangeBetTacticsStatus();
        }

        private void ChangeBetTacticsStatus()
        {
            if (radioButton_QuinellaUseCocomo.Checked)
            {
                numericUpDown_QuinellaCocomoThreshold.Enabled = true;
                numericUpDown_QuinellaCocomoMaxMagnification.Enabled = true;
            }
            else
            {
                numericUpDown_QuinellaCocomoThreshold.Enabled = false;
                numericUpDown_QuinellaCocomoMaxMagnification.Enabled = false;
            }
        }
    }
}
