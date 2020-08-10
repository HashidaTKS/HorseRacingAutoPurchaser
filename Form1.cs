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
            SetBetConfig(betConfig);
        }

        /// <summary>
        /// 大金を賭ける可能性があれば警告を表示する。
        /// 警告を表示する必要がないか、ユーザーが承認した場合true。
        /// </summary>
        /// <returns></returns>
        private bool AlertIfNeed()
        {
            return oddsConfigForTicketTypeUserControl_Quinella.NeedAlert() ||
                oddsConfigForTicketTypeUserControl_Wide.NeedAlert();
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
            var betConfig = new BetConfig
            {
                QuinellaBetConfig = oddsConfigForTicketTypeUserControl_Quinella.GetCurrentConfig(),
                WideBetConfig = oddsConfigForTicketTypeUserControl_Wide.GetCurrentConfig()
            };
            new BetConfigRepository().Store(betConfig);
        }

        private void Button_ResetPurchaseSetting_Click(object sender, EventArgs e)
        {
            SetBetConfig(new BetConfig());
        }

        private void SetBetConfig(BetConfig betConfig)
        {
            oddsConfigForTicketTypeUserControl_Quinella.SetBetConfigForTicketType(betConfig.QuinellaBetConfig);
            oddsConfigForTicketTypeUserControl_Wide.SetBetConfigForTicketType(betConfig.WideBetConfig);
        }

        private void Button_ExecSimulation_Click(object sender, EventArgs e)
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
        }

        private void Button_StopSimulation_Click(object sender, EventArgs e)
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
        }
    }
}
