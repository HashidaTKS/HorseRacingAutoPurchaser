namespace HorseRacingAutoPurchaser
{
    partial class OddsConfigForTicketTypeUserControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_MaxPurchaseCountOrderByProbability = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown_MaxPurchaseCountOrderByRatio = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_PurchaseTarget = new System.Windows.Forms.GroupBox();
            this.checkBox_PurchaseCentral = new System.Windows.Forms.CheckBox();
            this.checkBox_PurchaseRegional = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_NoBetTactics = new System.Windows.Forms.RadioButton();
            this.radioButton_UseCocomo = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_CocomoMaxMagnification = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_CocomoThreshold = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_MinimumPayBack = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_MaximumOdds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_MinimumOdds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_OddsRatio = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxPurchaseCountOrderByProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxPurchaseCountOrderByRatio)).BeginInit();
            this.groupBox_PurchaseTarget.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CocomoMaxMagnification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CocomoThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinimumPayBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumOdds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinimumOdds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OddsRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.numericUpDown_MaxPurchaseCountOrderByProbability);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numericUpDown_MaxPurchaseCountOrderByRatio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox_PurchaseTarget);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.numericUpDown_MinimumPayBack);
            this.groupBox1.Controls.Add(this.numericUpDown_MaximumOdds);
            this.groupBox1.Controls.Add(this.numericUpDown_MinimumOdds);
            this.groupBox1.Controls.Add(this.numericUpDown_OddsRatio);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 205);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "設定名";
            // 
            // numericUpDown_MaxPurchaseCountOrderByProbability
            // 
            this.numericUpDown_MaxPurchaseCountOrderByProbability.Location = new System.Drawing.Point(161, 115);
            this.numericUpDown_MaxPurchaseCountOrderByProbability.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_MaxPurchaseCountOrderByProbability.Name = "numericUpDown_MaxPurchaseCountOrderByProbability";
            this.numericUpDown_MaxPurchaseCountOrderByProbability.Size = new System.Drawing.Size(78, 19);
            this.numericUpDown_MaxPurchaseCountOrderByProbability.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "購入件数(理論的中率降順)";
            // 
            // numericUpDown_MaxPurchaseCountOrderByRatio
            // 
            this.numericUpDown_MaxPurchaseCountOrderByRatio.Location = new System.Drawing.Point(161, 92);
            this.numericUpDown_MaxPurchaseCountOrderByRatio.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_MaxPurchaseCountOrderByRatio.Name = "numericUpDown_MaxPurchaseCountOrderByRatio";
            this.numericUpDown_MaxPurchaseCountOrderByRatio.Size = new System.Drawing.Size(78, 19);
            this.numericUpDown_MaxPurchaseCountOrderByRatio.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "購入件数(歪み降順)";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_NoBetTactics);
            this.groupBox2.Controls.Add(this.radioButton_UseCocomo);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(278, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 166);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ベット戦略";
            // 
            // radioButton_NoBetTactics
            // 
            this.radioButton_NoBetTactics.AutoSize = true;
            this.radioButton_NoBetTactics.Location = new System.Drawing.Point(6, 115);
            this.radioButton_NoBetTactics.Name = "radioButton_NoBetTactics";
            this.radioButton_NoBetTactics.Size = new System.Drawing.Size(73, 16);
            this.radioButton_NoBetTactics.TabIndex = 18;
            this.radioButton_NoBetTactics.TabStop = true;
            this.radioButton_NoBetTactics.Text = "何もしない";
            this.radioButton_NoBetTactics.UseVisualStyleBackColor = true;
            // 
            // radioButton_UseCocomo
            // 
            this.radioButton_UseCocomo.AutoSize = true;
            this.radioButton_UseCocomo.Location = new System.Drawing.Point(6, 21);
            this.radioButton_UseCocomo.Name = "radioButton_UseCocomo";
            this.radioButton_UseCocomo.Size = new System.Drawing.Size(60, 16);
            this.radioButton_UseCocomo.TabIndex = 16;
            this.radioButton_UseCocomo.TabStop = true;
            this.radioButton_UseCocomo.Text = "ココモ法";
            this.radioButton_UseCocomo.UseVisualStyleBackColor = true;
            this.radioButton_UseCocomo.CheckedChanged += new System.EventHandler(this.RadioButton_UseCocomo_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown_CocomoMaxMagnification);
            this.groupBox3.Controls.Add(this.numericUpDown_CocomoThreshold);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(32, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 68);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ココモ法詳細設定";
            // 
            // numericUpDown_CocomoMaxMagnification
            // 
            this.numericUpDown_CocomoMaxMagnification.Location = new System.Drawing.Point(6, 43);
            this.numericUpDown_CocomoMaxMagnification.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_CocomoMaxMagnification.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_CocomoMaxMagnification.Name = "numericUpDown_CocomoMaxMagnification";
            this.numericUpDown_CocomoMaxMagnification.Size = new System.Drawing.Size(65, 19);
            this.numericUpDown_CocomoMaxMagnification.TabIndex = 14;
            this.numericUpDown_CocomoMaxMagnification.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_CocomoThreshold
            // 
            this.numericUpDown_CocomoThreshold.Location = new System.Drawing.Point(6, 18);
            this.numericUpDown_CocomoThreshold.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_CocomoThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_CocomoThreshold.Name = "numericUpDown_CocomoThreshold";
            this.numericUpDown_CocomoThreshold.Size = new System.Drawing.Size(65, 19);
            this.numericUpDown_CocomoThreshold.TabIndex = 12;
            this.numericUpDown_CocomoThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "倍のベット額になるまで";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "連敗するごとに";
            // 
            // numericUpDown_MinimumPayBack
            // 
            this.numericUpDown_MinimumPayBack.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_MinimumPayBack.Location = new System.Drawing.Point(138, 140);
            this.numericUpDown_MinimumPayBack.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDown_MinimumPayBack.Name = "numericUpDown_MinimumPayBack";
            this.numericUpDown_MinimumPayBack.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_MinimumPayBack.TabIndex = 8;
            // 
            // numericUpDown_MaximumOdds
            // 
            this.numericUpDown_MaximumOdds.DecimalPlaces = 1;
            this.numericUpDown_MaximumOdds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_MaximumOdds.Location = new System.Drawing.Point(138, 67);
            this.numericUpDown_MaximumOdds.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_MaximumOdds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_MaximumOdds.Name = "numericUpDown_MaximumOdds";
            this.numericUpDown_MaximumOdds.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_MaximumOdds.TabIndex = 7;
            this.numericUpDown_MaximumOdds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_MinimumOdds
            // 
            this.numericUpDown_MinimumOdds.DecimalPlaces = 1;
            this.numericUpDown_MinimumOdds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_MinimumOdds.Location = new System.Drawing.Point(138, 45);
            this.numericUpDown_MinimumOdds.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_MinimumOdds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_MinimumOdds.Name = "numericUpDown_MinimumOdds";
            this.numericUpDown_MinimumOdds.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_MinimumOdds.TabIndex = 6;
            this.numericUpDown_MinimumOdds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_OddsRatio
            // 
            this.numericUpDown_OddsRatio.DecimalPlaces = 1;
            this.numericUpDown_OddsRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_OddsRatio.Location = new System.Drawing.Point(138, 23);
            this.numericUpDown_OddsRatio.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_OddsRatio.Name = "numericUpDown_OddsRatio";
            this.numericUpDown_OddsRatio.Size = new System.Drawing.Size(101, 19);
            this.numericUpDown_OddsRatio.TabIndex = 5;
            this.numericUpDown_OddsRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "（各々がこの金額以上になるようにベット額調整)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "最低払い戻し金額";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "購入最高理論オッズ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 47);
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
            this.label2.Size = new System.Drawing.Size(117, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "最低オッズ比(実/理論)";
            // 
            // OddsConfigForTicketTypeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox1);
            this.Name = "OddsConfigForTicketTypeUserControl";
            this.Size = new System.Drawing.Size(760, 205);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxPurchaseCountOrderByProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxPurchaseCountOrderByRatio)).EndInit();
            this.groupBox_PurchaseTarget.ResumeLayout(false);
            this.groupBox_PurchaseTarget.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CocomoMaxMagnification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CocomoThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinimumPayBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumOdds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinimumOdds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OddsRatio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaxPurchaseCountOrderByRatio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_PurchaseTarget;
        private System.Windows.Forms.CheckBox checkBox_PurchaseCentral;
        private System.Windows.Forms.CheckBox checkBox_PurchaseRegional;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_NoBetTactics;
        private System.Windows.Forms.RadioButton radioButton_UseCocomo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown_CocomoMaxMagnification;
        private System.Windows.Forms.NumericUpDown numericUpDown_CocomoThreshold;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_MinimumPayBack;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaximumOdds;
        private System.Windows.Forms.NumericUpDown numericUpDown_MinimumOdds;
        private System.Windows.Forms.NumericUpDown numericUpDown_OddsRatio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaxPurchaseCountOrderByProbability;
        private System.Windows.Forms.Label label9;
    }
}
