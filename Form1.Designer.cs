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
            this.button_AutoPurchase = new System.Windows.Forms.Button();
            this.button_LoginConfig = new System.Windows.Forms.Button();
            this.button_AutoPurchase_Cancel = new System.Windows.Forms.Button();
            this.label_Running = new System.Windows.Forms.Label();
            this.button_SavePurchaseSetting = new System.Windows.Forms.Button();
            this.button_ResetPurchaseSetting = new System.Windows.Forms.Button();
            this.button_ExecSimulation = new System.Windows.Forms.Button();
            this.oddsConfigForTicketTypeUserControl_Quinella = new HorseRacingAutoPurchaser.OddsConfigForTicketTypeUserControl();
            this.oddsConfigForTicketTypeUserControl_Wide = new HorseRacingAutoPurchaser.OddsConfigForTicketTypeUserControl();
            this.userControl_Wide = new HorseRacingAutoPurchaser.OddsConfigForTicketTypeUserControl();
            this.button_StopSimulation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_AutoPurchase
            // 
            this.button_AutoPurchase.Location = new System.Drawing.Point(570, 459);
            this.button_AutoPurchase.Name = "button_AutoPurchase";
            this.button_AutoPurchase.Size = new System.Drawing.Size(107, 23);
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
            this.button_AutoPurchase_Cancel.Location = new System.Drawing.Point(683, 459);
            this.button_AutoPurchase_Cancel.Name = "button_AutoPurchase_Cancel";
            this.button_AutoPurchase_Cancel.Size = new System.Drawing.Size(85, 23);
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
            this.button_SavePurchaseSetting.Location = new System.Drawing.Point(8, 459);
            this.button_SavePurchaseSetting.Name = "button_SavePurchaseSetting";
            this.button_SavePurchaseSetting.Size = new System.Drawing.Size(102, 23);
            this.button_SavePurchaseSetting.TabIndex = 7;
            this.button_SavePurchaseSetting.Text = "購入設定の保存";
            this.button_SavePurchaseSetting.UseVisualStyleBackColor = true;
            this.button_SavePurchaseSetting.Click += new System.EventHandler(this.Button_SavePurchaseSetting_Click);
            // 
            // button_ResetPurchaseSetting
            // 
            this.button_ResetPurchaseSetting.Location = new System.Drawing.Point(120, 459);
            this.button_ResetPurchaseSetting.Name = "button_ResetPurchaseSetting";
            this.button_ResetPurchaseSetting.Size = new System.Drawing.Size(114, 23);
            this.button_ResetPurchaseSetting.TabIndex = 8;
            this.button_ResetPurchaseSetting.Text = "購入設定のリセット";
            this.button_ResetPurchaseSetting.UseVisualStyleBackColor = true;
            this.button_ResetPurchaseSetting.Click += new System.EventHandler(this.Button_ResetPurchaseSetting_Click);
            // 
            // button_ExecSimulation
            // 
            this.button_ExecSimulation.Location = new System.Drawing.Point(289, 459);
            this.button_ExecSimulation.Name = "button_ExecSimulation";
            this.button_ExecSimulation.Size = new System.Drawing.Size(96, 23);
            this.button_ExecSimulation.TabIndex = 11;
            this.button_ExecSimulation.Text = "シミュレーション";
            this.button_ExecSimulation.UseVisualStyleBackColor = true;
            this.button_ExecSimulation.Click += new System.EventHandler(this.Button_ExecSimulation_Click);
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
            // button_StopSimulation
            // 
            this.button_StopSimulation.Location = new System.Drawing.Point(391, 458);
            this.button_StopSimulation.Name = "button_StopSimulation";
            this.button_StopSimulation.Size = new System.Drawing.Size(119, 23);
            this.button_StopSimulation.TabIndex = 12;
            this.button_StopSimulation.Text = "シミュレーション中止";
            this.button_StopSimulation.UseVisualStyleBackColor = true;
            this.button_StopSimulation.Click += new System.EventHandler(this.Button_StopSimulation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(790, 493);
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
            this.Text = "競馬自動購入（α）";
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
    }
}

