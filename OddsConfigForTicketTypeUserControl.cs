using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HorseRacingAutoPurchaser.Utils;
using HorseRacingAutoPurchaser.Infrastructures;
using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser
{
    public partial class OddsConfigForTicketTypeUserControl : UserControl
    {

        [Browsable(true)]
        [Description("設定名の表示名")]
        public string DisplayName
        {
            get { return groupBox1.Text; }
            set { groupBox1.Text = value; }
        }

        //[Browsable(false)]
        //private BetConfigForTicketType BetConfigForTicketType { get; set; }



        public OddsConfigForTicketTypeUserControl()
        {
            InitializeComponent();
        }

        private void RadioButton_UseCocomo_CheckedChanged(object sender, EventArgs e)
        {
            ChangeBetTacticsStatus();
        }


        public bool NeedAlert()
        {

            if(!checkBox_PurchaseCentral.Checked && !checkBox_PurchaseRegional.Checked)
            {
                return false;
            }
            return numericUpDown_MinimumPayBack.Value >= 10000 ||
                    (radioButton_UseCocomo.Checked &&
                    (numericUpDown_CocomoThreshold.Value < 50 || numericUpDown_CocomoMaxMagnification.Value > 10)) ||
                    (numericUpDown_MaxPurchaseCountOrderByRatio.Value + numericUpDown_MaxPurchaseCountOrderByRatio.Value) * numericUpDown_MinimumPayBack.Value >= 30000 ||
                    numericUpDown_MinimumPayBack.Value >= 30000 ||
                    numericUpDown_MinimumPayBack.Value >= 10000 * numericUpDown_MinimumOdds.Value;
        }

        public void SetBetConfigForTicketType(BetConfigForTicketType betConfigForTicketType)
        {
            numericUpDown_OddsRatio.Value = (decimal)betConfigForTicketType.OddsRatio;
            numericUpDown_MinimumOdds.Value = (decimal)betConfigForTicketType.MinimumOdds;
            numericUpDown_MaximumOdds.Value = (decimal)betConfigForTicketType.MaximumOdds;
            numericUpDown_MinimumPayBack.Value = (decimal)betConfigForTicketType.MinimumPayBack;
            numericUpDown_MaxPurchaseCountOrderByRatio.Value = betConfigForTicketType.MaxPurchaseCountOrderByRatio;
            numericUpDown_MaxPurchaseCountOrderByProbability.Value = betConfigForTicketType.MaxPurchaseCountOrderByProbability;
            radioButton_UseCocomo.Checked = betConfigForTicketType.UseCocomo;
            radioButton_NoBetTactics.Checked = !radioButton_UseCocomo.Checked;
            numericUpDown_CocomoThreshold.Value = betConfigForTicketType.CocomoThreshold;
            numericUpDown_CocomoMaxMagnification.Value = betConfigForTicketType.CocomoMaxMagnification;
            checkBox_PurchaseCentral.Checked = betConfigForTicketType.PurchaseCentral;
            checkBox_PurchaseRegional.Checked = betConfigForTicketType.PurchaseRegional;
            ChangeBetTacticsStatus();
        }

        public BetConfigForTicketType GetCurrentConfig()
        {
            var betConfig = new BetConfigForTicketType
            {
                OddsRatio = (double)numericUpDown_OddsRatio.Value,
                MinimumOdds = (double)numericUpDown_MinimumOdds.Value,
                MaximumOdds = (double)numericUpDown_MaximumOdds.Value,
                MinimumPayBack = (double)numericUpDown_MinimumPayBack.Value,
                MaxPurchaseCountOrderByRatio = (int)numericUpDown_MaxPurchaseCountOrderByRatio.Value,
                MaxPurchaseCountOrderByProbability = (int)numericUpDown_MaxPurchaseCountOrderByProbability.Value,
                UseCocomo = radioButton_UseCocomo.Checked,
                CocomoThreshold = (int)numericUpDown_CocomoThreshold.Value,
                CocomoMaxMagnification = (int)numericUpDown_CocomoMaxMagnification.Value,
                PurchaseCentral = checkBox_PurchaseCentral.Checked,
                PurchaseRegional = checkBox_PurchaseRegional.Checked
            };

            return betConfig;
        }

        private void ChangeBetTacticsStatus()
        {
            if (radioButton_UseCocomo.Checked)
            {
                numericUpDown_CocomoThreshold.Enabled = true;
                numericUpDown_CocomoMaxMagnification.Enabled = true;
            }
            else
            {
                numericUpDown_CocomoThreshold.Enabled = false;
                numericUpDown_CocomoMaxMagnification.Enabled = false;
            }
        }
    }
}
