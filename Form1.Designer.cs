namespace HorseRacingAutoPurchaser
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_AutoPurchase = new System.Windows.Forms.Button();
            this.button_LoginConfig = new System.Windows.Forms.Button();
            this.button_AutoPurchase_Cancel = new System.Windows.Forms.Button();
            this.label_Running = new System.Windows.Forms.Label();
            this.button_SavePurchaseSetting = new System.Windows.Forms.Button();
            this.button_ResetPurchaseSetting = new System.Windows.Forms.Button();
            this.button_ExecSimulation = new System.Windows.Forms.Button();
            this.button_StopSimulation = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker_SimulateFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_SimulateTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_SimulateBySavedData = new System.Windows.Forms.CheckBox();
            this.label_RunningSimulation = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.oddsConfigForTicketTypeUserControl_Quinella = new HorseRacingAutoPurchaser.OddsConfigForTicketTypeUserControl();
            this.oddsConfigForTicketTypeUserControl_Wide = new HorseRacingAutoPurchaser.OddsConfigForTicketTypeUserControl();
            this.userControl_Wide = new HorseRacingAutoPurchaser.OddsConfigForTicketTypeUserControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_AutoPurchase
            // 
            this.button_AutoPurchase.Location = new System.Drawing.Point(547, 583);
            this.button_AutoPurchase.Name = "button_AutoPurchase";
            this.button_AutoPurchase.Size = new System.Drawing.Size(96, 23);
            this.button_AutoPurchase.TabIndex = 0;
            this.button_AutoPurchase.Text = "自動購入開始";
            this.button_AutoPurchase.UseVisualStyleBackColor = true;
            this.button_AutoPurchase.Click += new System.EventHandler(this.Button_AutoPurchase_Click);
            // 
            // button_LoginConfig
            // 
            this.button_LoginConfig.Location = new System.Drawing.Point(683, 8);
            this.button_LoginConfig.Name = "button_LoginConfig";
            this.button_LoginConfig.Size = new System.Drawing.Size(85, 23);
            this.button_LoginConfig.TabIndex = 1;
            this.button_LoginConfig.Text = "ログイン設定";
            this.button_LoginConfig.UseVisualStyleBackColor = true;
            this.button_LoginConfig.Click += new System.EventHandler(this.Button_LoginConfig_Click);
            // 
            // button_AutoPurchase_Cancel
            // 
            this.button_AutoPurchase_Cancel.Location = new System.Drawing.Point(649, 583);
            this.button_AutoPurchase_Cancel.Name = "button_AutoPurchase_Cancel";
            this.button_AutoPurchase_Cancel.Size = new System.Drawing.Size(119, 23);
            this.button_AutoPurchase_Cancel.TabIndex = 2;
            this.button_AutoPurchase_Cancel.Text = "自動購入停止";
            this.button_AutoPurchase_Cancel.UseVisualStyleBackColor = true;
            this.button_AutoPurchase_Cancel.Click += new System.EventHandler(this.Button_AutoPurchase_Cancel_Click);
            // 
            // label_Running
            // 
            this.label_Running.AutoSize = true;
            this.label_Running.ForeColor = System.Drawing.Color.Red;
            this.label_Running.Location = new System.Drawing.Point(12, 13);
            this.label_Running.Name = "label_Running";
            this.label_Running.Size = new System.Drawing.Size(65, 12);
            this.label_Running.TabIndex = 5;
            this.label_Running.Text = "自動購入中";
            this.label_Running.Visible = false;
            // 
            // button_SavePurchaseSetting
            // 
            this.button_SavePurchaseSetting.Location = new System.Drawing.Point(14, 554);
            this.button_SavePurchaseSetting.Name = "button_SavePurchaseSetting";
            this.button_SavePurchaseSetting.Size = new System.Drawing.Size(102, 23);
            this.button_SavePurchaseSetting.TabIndex = 7;
            this.button_SavePurchaseSetting.Text = "購入設定の保存";
            this.button_SavePurchaseSetting.UseVisualStyleBackColor = true;
            this.button_SavePurchaseSetting.Click += new System.EventHandler(this.Button_SavePurchaseSetting_Click);
            // 
            // button_ResetPurchaseSetting
            // 
            this.button_ResetPurchaseSetting.Location = new System.Drawing.Point(122, 554);
            this.button_ResetPurchaseSetting.Name = "button_ResetPurchaseSetting";
            this.button_ResetPurchaseSetting.Size = new System.Drawing.Size(114, 23);
            this.button_ResetPurchaseSetting.TabIndex = 8;
            this.button_ResetPurchaseSetting.Text = "購入設定のリセット";
            this.button_ResetPurchaseSetting.UseVisualStyleBackColor = true;
            this.button_ResetPurchaseSetting.Click += new System.EventHandler(this.Button_ResetPurchaseSetting_Click);
            // 
            // button_ExecSimulation
            // 
            this.button_ExecSimulation.Location = new System.Drawing.Point(547, 554);
            this.button_ExecSimulation.Name = "button_ExecSimulation";
            this.button_ExecSimulation.Size = new System.Drawing.Size(96, 23);
            this.button_ExecSimulation.TabIndex = 11;
            this.button_ExecSimulation.Text = "シミュレーション";
            this.button_ExecSimulation.UseVisualStyleBackColor = true;
            this.button_ExecSimulation.Click += new System.EventHandler(this.Button_ExecSimulation_Click);
            // 
            // button_StopSimulation
            // 
            this.button_StopSimulation.Location = new System.Drawing.Point(649, 554);
            this.button_StopSimulation.Name = "button_StopSimulation";
            this.button_StopSimulation.Size = new System.Drawing.Size(119, 23);
            this.button_StopSimulation.TabIndex = 12;
            this.button_StopSimulation.Text = "シミュレーション中止";
            this.button_StopSimulation.UseVisualStyleBackColor = true;
            this.button_StopSimulation.Click += new System.EventHandler(this.Button_StopSimulation_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.checkBox_SimulateBySavedData);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker_SimulateTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker_SimulateFrom);
            this.groupBox1.Location = new System.Drawing.Point(8, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 89);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "シミュレーション設定";
            // 
            // dateTimePicker_SimulateFrom
            // 
            this.dateTimePicker_SimulateFrom.Location = new System.Drawing.Point(6, 18);
            this.dateTimePicker_SimulateFrom.Name = "dateTimePicker_SimulateFrom";
            this.dateTimePicker_SimulateFrom.Size = new System.Drawing.Size(200, 19);
            this.dateTimePicker_SimulateFrom.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "から";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // dateTimePicker_SimulateTo
            // 
            this.dateTimePicker_SimulateTo.Location = new System.Drawing.Point(241, 18);
            this.dateTimePicker_SimulateTo.Name = "dateTimePicker_SimulateTo";
            this.dateTimePicker_SimulateTo.Size = new System.Drawing.Size(200, 19);
            this.dateTimePicker_SimulateTo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(447, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "まで";
            // 
            // checkBox_SimulateBySavedData
            // 
            this.checkBox_SimulateBySavedData.AutoSize = true;
            this.checkBox_SimulateBySavedData.Checked = true;
            this.checkBox_SimulateBySavedData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SimulateBySavedData.Location = new System.Drawing.Point(6, 43);
            this.checkBox_SimulateBySavedData.Name = "checkBox_SimulateBySavedData";
            this.checkBox_SimulateBySavedData.Size = new System.Drawing.Size(158, 16);
            this.checkBox_SimulateBySavedData.TabIndex = 4;
            this.checkBox_SimulateBySavedData.Text = "保存済みのデータのみを使う";
            this.checkBox_SimulateBySavedData.UseVisualStyleBackColor = true;
            // 
            // label_RunningSimulation
            // 
            this.label_RunningSimulation.AutoSize = true;
            this.label_RunningSimulation.ForeColor = System.Drawing.Color.Red;
            this.label_RunningSimulation.Location = new System.Drawing.Point(95, 13);
            this.label_RunningSimulation.Name = "label_RunningSimulation";
            this.label_RunningSimulation.Size = new System.Drawing.Size(86, 12);
            this.label_RunningSimulation.TabIndex = 14;
            this.label_RunningSimulation.Text = "シミュレーション中";
            this.label_RunningSimulation.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // oddsConfigForTicketTypeUserControl_Quinella
            // 
            this.oddsConfigForTicketTypeUserControl_Quinella.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.oddsConfigForTicketTypeUserControl_Quinella.DisplayName = "馬連設定";
            this.oddsConfigForTicketTypeUserControl_Quinella.Location = new System.Drawing.Point(8, 37);
            this.oddsConfigForTicketTypeUserControl_Quinella.Name = "oddsConfigForTicketTypeUserControl_Quinella";
            this.oddsConfigForTicketTypeUserControl_Quinella.Size = new System.Drawing.Size(760, 205);
            this.oddsConfigForTicketTypeUserControl_Quinella.TabIndex = 10;
            // 
            // oddsConfigForTicketTypeUserControl_Wide
            // 
            this.oddsConfigForTicketTypeUserControl_Wide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.oddsConfigForTicketTypeUserControl_Wide.DisplayName = "ワイド設定";
            this.oddsConfigForTicketTypeUserControl_Wide.Location = new System.Drawing.Point(8, 248);
            this.oddsConfigForTicketTypeUserControl_Wide.Name = "oddsConfigForTicketTypeUserControl_Wide";
            this.oddsConfigForTicketTypeUserControl_Wide.Size = new System.Drawing.Size(760, 205);
            this.oddsConfigForTicketTypeUserControl_Wide.TabIndex = 9;
            // 
            // userControl_Wide
            // 
            this.userControl_Wide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userControl_Wide.DisplayName = "ワイド設定";
            this.userControl_Wide.Location = new System.Drawing.Point(8, 248);
            this.userControl_Wide.Name = "userControl_Wide";
            this.userControl_Wide.Size = new System.Drawing.Size(780, 191);
            this.userControl_Wide.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(170, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(533, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "保存済みのデータを使わない場合、必要なデータをnetkeiba.comからスクレイピングで取得します。\r\nこの取得処理が重いため無効化は非推奨。必要なデータは別途" +
    "専用データとして配布するのでその利用を推奨。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(513, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "※シミュレーション結果は、このプログラムが存在するフォルダのSimulationResultフォルダ配下に保存されます。";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(795, 618);
            this.Controls.Add(this.label_RunningSimulation);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_StopSimulation);
            this.Controls.Add(this.button_ExecSimulation);
            this.Controls.Add(this.oddsConfigForTicketTypeUserControl_Quinella);
            this.Controls.Add(this.oddsConfigForTicketTypeUserControl_Wide);
            this.Controls.Add(this.button_ResetPurchaseSetting);
            this.Controls.Add(this.button_SavePurchaseSetting);
            this.Controls.Add(this.label_Running);
            this.Controls.Add(this.button_AutoPurchase_Cancel);
            this.Controls.Add(this.button_LoginConfig);
            this.Controls.Add(this.button_AutoPurchase);
            this.Name = "Form1";
            this.Text = "馬券自動購入（α）";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_AutoPurchase;
        private System.Windows.Forms.Button button_LoginConfig;
        private System.Windows.Forms.Button button_AutoPurchase_Cancel;
        private System.Windows.Forms.Label label_Running;
        private System.Windows.Forms.Button button_SavePurchaseSetting;
        private System.Windows.Forms.Button button_ResetPurchaseSetting;
        private OddsConfigForTicketTypeUserControl userControl_Wide;
        private OddsConfigForTicketTypeUserControl oddsConfigForTicketTypeUserControl_Wide;
        private OddsConfigForTicketTypeUserControl oddsConfigForTicketTypeUserControl_Quinella;
        private System.Windows.Forms.Button button_ExecSimulation;
        private System.Windows.Forms.Button button_StopSimulation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_SimulateFrom;
        private System.Windows.Forms.CheckBox checkBox_SimulateBySavedData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_SimulateTo;
        private System.Windows.Forms.Label label_RunningSimulation;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

