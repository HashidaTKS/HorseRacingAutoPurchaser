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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_QuinellaOddsRatio = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_QuinellaMinimumOdds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_QuinellaMaximumOdds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_QuinnelaMinimumPayBack = new System.Windows.Forms.NumericUpDown();
            this.checkBox_QuinellaUseCocomo = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_QuinellaCocomoThreshold = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_QuinellaCocomoMaxMagnification = new System.Windows.Forms.NumericUpDown();
            this.button_SavePurchaseSetting = new System.Windows.Forms.Button();
            this.button_ResetPurchaseSetting = new System.Windows.Forms.Button();
            this.checkBox_PurchaseCentral = new System.Windows.Forms.CheckBox();
            this.checkBox_PurchaseRegional = new System.Windows.Forms.CheckBox();
            this.groupBox_PurchaseTarget = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaOddsRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaMinimumOdds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaMaximumOdds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinnelaMinimumPayBack)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaCocomoThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaCocomoMaxMagnification)).BeginInit();
            this.groupBox_PurchaseTarget.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_AutoPurchase
            // 
            this.button_AutoPurchase.Location = new System.Drawing.Point(580, 219);
            this.button_AutoPurchase.Name = "button_AutoPurchase";
            this.button_AutoPurchase.Size = new System.Drawing.Size(107, 23);
            this.button_AutoPurchase.TabIndex = 0;
            this.button_AutoPurchase.Text = "自動購入開始";
            this.button_AutoPurchase.UseVisualStyleBackColor = true;
            this.button_AutoPurchase.Click += new System.EventHandler(this.Button_AutoPurchase_Click);
            // 
            // button_LoginConfig
            // 
            this.button_LoginConfig.Location = new System.Drawing.Point(703, 12);
            this.button_LoginConfig.Name = "button_LoginConfig";
            this.button_LoginConfig.Size = new System.Drawing.Size(85, 23);
            this.button_LoginConfig.TabIndex = 1;
            this.button_LoginConfig.Text = "ログイン設定";
            this.button_LoginConfig.UseVisualStyleBackColor = true;
            this.button_LoginConfig.Click += new System.EventHandler(this.Button_LoginConfig_Click);
            // 
            // button_AutoPurchase_Cancel
            // 
            this.button_AutoPurchase_Cancel.Location = new System.Drawing.Point(702, 219);
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
            this.label_Running.Location = new System.Drawing.Point(18, 12);
            this.label_Running.Name = "label_Running";
            this.label_Running.Size = new System.Drawing.Size(65, 12);
            this.label_Running.TabIndex = 5;
            this.label_Running.Text = "自動購入中";
            this.label_Running.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox_PurchaseTarget);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.numericUpDown_QuinnelaMinimumPayBack);
            this.groupBox1.Controls.Add(this.numericUpDown_QuinellaMaximumOdds);
            this.groupBox1.Controls.Add(this.numericUpDown_QuinellaMinimumOdds);
            this.groupBox1.Controls.Add(this.numericUpDown_QuinellaOddsRatio);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 161);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "馬連設定";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "(この金額以上になるようにベット額調整)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "最低払い戻し金額";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "購入最高理論オッズ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "購入最低理論オッズ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "最低オッズ比";
            // 
            // numericUpDown_QuinellaOddsRatio
            // 
            this.numericUpDown_QuinellaOddsRatio.DecimalPlaces = 1;
            this.numericUpDown_QuinellaOddsRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_QuinellaOddsRatio.Location = new System.Drawing.Point(138, 23);
            this.numericUpDown_QuinellaOddsRatio.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_QuinellaOddsRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_QuinellaOddsRatio.Name = "numericUpDown_QuinellaOddsRatio";
            this.numericUpDown_QuinellaOddsRatio.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_QuinellaOddsRatio.TabIndex = 5;
            this.numericUpDown_QuinellaOddsRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_QuinellaMinimumOdds
            // 
            this.numericUpDown_QuinellaMinimumOdds.DecimalPlaces = 1;
            this.numericUpDown_QuinellaMinimumOdds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_QuinellaMinimumOdds.Location = new System.Drawing.Point(138, 50);
            this.numericUpDown_QuinellaMinimumOdds.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_QuinellaMinimumOdds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_QuinellaMinimumOdds.Name = "numericUpDown_QuinellaMinimumOdds";
            this.numericUpDown_QuinellaMinimumOdds.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_QuinellaMinimumOdds.TabIndex = 6;
            this.numericUpDown_QuinellaMinimumOdds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_QuinellaMaximumOdds
            // 
            this.numericUpDown_QuinellaMaximumOdds.DecimalPlaces = 1;
            this.numericUpDown_QuinellaMaximumOdds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_QuinellaMaximumOdds.Location = new System.Drawing.Point(138, 81);
            this.numericUpDown_QuinellaMaximumOdds.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_QuinellaMaximumOdds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_QuinellaMaximumOdds.Name = "numericUpDown_QuinellaMaximumOdds";
            this.numericUpDown_QuinellaMaximumOdds.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_QuinellaMaximumOdds.TabIndex = 7;
            this.numericUpDown_QuinellaMaximumOdds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_QuinnelaMinimumPayBack
            // 
            this.numericUpDown_QuinnelaMinimumPayBack.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_QuinnelaMinimumPayBack.Location = new System.Drawing.Point(138, 111);
            this.numericUpDown_QuinnelaMinimumPayBack.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDown_QuinnelaMinimumPayBack.Name = "numericUpDown_QuinnelaMinimumPayBack";
            this.numericUpDown_QuinnelaMinimumPayBack.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_QuinnelaMinimumPayBack.TabIndex = 8;
            // 
            // checkBox_QuinellaUseCocomo
            // 
            this.checkBox_QuinellaUseCocomo.AutoSize = true;
            this.checkBox_QuinellaUseCocomo.Location = new System.Drawing.Point(23, 18);
            this.checkBox_QuinellaUseCocomo.Name = "checkBox_QuinellaUseCocomo";
            this.checkBox_QuinellaUseCocomo.Size = new System.Drawing.Size(61, 16);
            this.checkBox_QuinellaUseCocomo.TabIndex = 9;
            this.checkBox_QuinellaUseCocomo.Text = "ココモ法";
            this.checkBox_QuinellaUseCocomo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown_QuinellaCocomoMaxMagnification);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericUpDown_QuinellaCocomoThreshold);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.checkBox_QuinellaUseCocomo);
            this.groupBox2.Location = new System.Drawing.Point(278, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 128);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ベット戦略";
            // 
            // numericUpDown_QuinellaCocomoThreshold
            // 
            this.numericUpDown_QuinellaCocomoThreshold.Location = new System.Drawing.Point(23, 47);
            this.numericUpDown_QuinellaCocomoThreshold.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_QuinellaCocomoThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_QuinellaCocomoThreshold.Name = "numericUpDown_QuinellaCocomoThreshold";
            this.numericUpDown_QuinellaCocomoThreshold.Size = new System.Drawing.Size(65, 19);
            this.numericUpDown_QuinellaCocomoThreshold.TabIndex = 12;
            this.numericUpDown_QuinellaCocomoThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(101, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "連敗するごとに";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(101, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "倍のベット額になるまで";
            // 
            // numericUpDown_QuinellaCocomoMaxMagnification
            // 
            this.numericUpDown_QuinellaCocomoMaxMagnification.Location = new System.Drawing.Point(23, 91);
            this.numericUpDown_QuinellaCocomoMaxMagnification.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_QuinellaCocomoMaxMagnification.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_QuinellaCocomoMaxMagnification.Name = "numericUpDown_QuinellaCocomoMaxMagnification";
            this.numericUpDown_QuinellaCocomoMaxMagnification.Size = new System.Drawing.Size(65, 19);
            this.numericUpDown_QuinellaCocomoMaxMagnification.TabIndex = 14;
            this.numericUpDown_QuinellaCocomoMaxMagnification.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_SavePurchaseSetting
            // 
            this.button_SavePurchaseSetting.Location = new System.Drawing.Point(19, 219);
            this.button_SavePurchaseSetting.Name = "button_SavePurchaseSetting";
            this.button_SavePurchaseSetting.Size = new System.Drawing.Size(102, 23);
            this.button_SavePurchaseSetting.TabIndex = 7;
            this.button_SavePurchaseSetting.Text = "購入設定の保存";
            this.button_SavePurchaseSetting.UseVisualStyleBackColor = true;
            this.button_SavePurchaseSetting.Click += new System.EventHandler(this.Button_SavePurchaseSetting_Click);
            // 
            // button_ResetPurchaseSetting
            // 
            this.button_ResetPurchaseSetting.Location = new System.Drawing.Point(137, 219);
            this.button_ResetPurchaseSetting.Name = "button_ResetPurchaseSetting";
            this.button_ResetPurchaseSetting.Size = new System.Drawing.Size(114, 23);
            this.button_ResetPurchaseSetting.TabIndex = 8;
            this.button_ResetPurchaseSetting.Text = "購入設定のリセット";
            this.button_ResetPurchaseSetting.UseVisualStyleBackColor = true;
            this.button_ResetPurchaseSetting.Click += new System.EventHandler(this.Button_ResetPurchaseSetting_Click);
            // 
            // checkBox_PurchaseCentral
            // 
            this.checkBox_PurchaseCentral.AutoSize = true;
            this.checkBox_PurchaseCentral.Checked = true;
            this.checkBox_PurchaseCentral.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_PurchaseCentral.Location = new System.Drawing.Point(16, 18);
            this.checkBox_PurchaseCentral.Name = "checkBox_PurchaseCentral";
            this.checkBox_PurchaseCentral.Size = new System.Drawing.Size(72, 16);
            this.checkBox_PurchaseCentral.TabIndex = 11;
            this.checkBox_PurchaseCentral.Text = "中央競馬";
            this.checkBox_PurchaseCentral.UseVisualStyleBackColor = true;
            // 
            // checkBox_PurchaseRegional
            // 
            this.checkBox_PurchaseRegional.AutoSize = true;
            this.checkBox_PurchaseRegional.Location = new System.Drawing.Point(105, 18);
            this.checkBox_PurchaseRegional.Name = "checkBox_PurchaseRegional";
            this.checkBox_PurchaseRegional.Size = new System.Drawing.Size(72, 16);
            this.checkBox_PurchaseRegional.TabIndex = 12;
            this.checkBox_PurchaseRegional.Text = "地方競馬";
            this.checkBox_PurchaseRegional.UseVisualStyleBackColor = true;
            // 
            // groupBox_PurchaseTarget
            // 
            this.groupBox_PurchaseTarget.Controls.Add(this.checkBox_PurchaseCentral);
            this.groupBox_PurchaseTarget.Controls.Add(this.checkBox_PurchaseRegional);
            this.groupBox_PurchaseTarget.Location = new System.Drawing.Point(522, 21);
            this.groupBox_PurchaseTarget.Name = "groupBox_PurchaseTarget";
            this.groupBox_PurchaseTarget.Size = new System.Drawing.Size(200, 48);
            this.groupBox_PurchaseTarget.TabIndex = 13;
            this.groupBox_PurchaseTarget.TabStop = false;
            this.groupBox_PurchaseTarget.Text = "購入対象";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 250);
            this.Controls.Add(this.button_ResetPurchaseSetting);
            this.Controls.Add(this.button_SavePurchaseSetting);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_Running);
            this.Controls.Add(this.button_AutoPurchase_Cancel);
            this.Controls.Add(this.button_LoginConfig);
            this.Controls.Add(this.button_AutoPurchase);
            this.Name = "Form1";
            this.Text = "競馬自動購入（α）";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaOddsRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaMinimumOdds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaMaximumOdds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinnelaMinimumPayBack)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaCocomoThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_QuinellaCocomoMaxMagnification)).EndInit();
            this.groupBox_PurchaseTarget.ResumeLayout(false);
            this.groupBox_PurchaseTarget.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_AutoPurchase;
        private System.Windows.Forms.Button button_LoginConfig;
        private System.Windows.Forms.Button button_AutoPurchase_Cancel;
        private System.Windows.Forms.Label label_Running;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_QuinnelaMinimumPayBack;
        private System.Windows.Forms.NumericUpDown numericUpDown_QuinellaMaximumOdds;
        private System.Windows.Forms.NumericUpDown numericUpDown_QuinellaMinimumOdds;
        private System.Windows.Forms.NumericUpDown numericUpDown_QuinellaOddsRatio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown_QuinellaCocomoMaxMagnification;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_QuinellaCocomoThreshold;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox_QuinellaUseCocomo;
        private System.Windows.Forms.Button button_SavePurchaseSetting;
        private System.Windows.Forms.Button button_ResetPurchaseSetting;
        private System.Windows.Forms.GroupBox groupBox_PurchaseTarget;
        private System.Windows.Forms.CheckBox checkBox_PurchaseCentral;
        private System.Windows.Forms.CheckBox checkBox_PurchaseRegional;
    }
}

