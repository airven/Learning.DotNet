﻿namespace spiritkitchen
{
    partial class SpiritKitchen
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Costlabel = new System.Windows.Forms.Label();
            this.dinnerWinerCheckBox = new System.Windows.Forms.CheckBox();
            this.dinnerMemberCheckBox = new System.Windows.Forms.CheckBox();
            this.dinnerCancelButton = new System.Windows.Forms.Button();
            this.dinnerOrderButton = new System.Windows.Forms.Button();
            this.dinnerDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dinnerNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dinnerCustomerPhone = new System.Windows.Forms.TextBox();
            this.dinnerCostomerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.partyCostlable = new System.Windows.Forms.Label();
            this.partyfinceCheckBox = new System.Windows.Forms.CheckBox();
            this.partyWineCheckBox = new System.Windows.Forms.CheckBox();
            this.partyMemberCheckBox = new System.Windows.Forms.CheckBox();
            this.PartyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.partyNuMericUpDown = new System.Windows.Forms.NumericUpDown();
            this.partyCustomerPhone = new System.Windows.Forms.TextBox();
            this.partyCustomerName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dinnerNumericUpDown)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.partyNuMericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(427, 435);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.PowderBlue;
            this.tabPage1.Controls.Add(this.Costlabel);
            this.tabPage1.Controls.Add(this.dinnerWinerCheckBox);
            this.tabPage1.Controls.Add(this.dinnerMemberCheckBox);
            this.tabPage1.Controls.Add(this.dinnerCancelButton);
            this.tabPage1.Controls.Add(this.dinnerOrderButton);
            this.tabPage1.Controls.Add(this.dinnerDateTimePicker);
            this.tabPage1.Controls.Add(this.dinnerNumericUpDown);
            this.tabPage1.Controls.Add(this.dinnerCustomerPhone);
            this.tabPage1.Controls.Add(this.dinnerCostomerName);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(419, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "自助用餐";
            // 
            // Costlabel
            // 
            this.Costlabel.BackColor = System.Drawing.Color.Silver;
            this.Costlabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Costlabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Costlabel.Location = new System.Drawing.Point(123, 244);
            this.Costlabel.Name = "Costlabel";
            this.Costlabel.Size = new System.Drawing.Size(63, 14);
            this.Costlabel.TabIndex = 14;
            // 
            // dinnerWinerCheckBox
            // 
            this.dinnerWinerCheckBox.AutoSize = true;
            this.dinnerWinerCheckBox.Location = new System.Drawing.Point(292, 292);
            this.dinnerWinerCheckBox.Name = "dinnerWinerCheckBox";
            this.dinnerWinerCheckBox.Size = new System.Drawing.Size(48, 16);
            this.dinnerWinerCheckBox.TabIndex = 13;
            this.dinnerWinerCheckBox.Text = "酒水";
            this.dinnerWinerCheckBox.UseVisualStyleBackColor = true;
            this.dinnerWinerCheckBox.CheckedChanged += new System.EventHandler(this.dinnerWinerCheckBox_CheckedChanged);
            // 
            // dinnerMemberCheckBox
            // 
            this.dinnerMemberCheckBox.AutoSize = true;
            this.dinnerMemberCheckBox.Location = new System.Drawing.Point(126, 292);
            this.dinnerMemberCheckBox.Name = "dinnerMemberCheckBox";
            this.dinnerMemberCheckBox.Size = new System.Drawing.Size(48, 16);
            this.dinnerMemberCheckBox.TabIndex = 12;
            this.dinnerMemberCheckBox.Text = "会员";
            this.dinnerMemberCheckBox.UseVisualStyleBackColor = true;
            this.dinnerMemberCheckBox.CheckedChanged += new System.EventHandler(this.dinnerMemberCheckBox_CheckedChanged);
            // 
            // dinnerCancelButton
            // 
            this.dinnerCancelButton.Location = new System.Drawing.Point(265, 340);
            this.dinnerCancelButton.Name = "dinnerCancelButton";
            this.dinnerCancelButton.Size = new System.Drawing.Size(75, 23);
            this.dinnerCancelButton.TabIndex = 11;
            this.dinnerCancelButton.Text = "取消";
            this.dinnerCancelButton.UseVisualStyleBackColor = true;
            // 
            // dinnerOrderButton
            // 
            this.dinnerOrderButton.Location = new System.Drawing.Point(36, 340);
            this.dinnerOrderButton.Name = "dinnerOrderButton";
            this.dinnerOrderButton.Size = new System.Drawing.Size(75, 23);
            this.dinnerOrderButton.TabIndex = 10;
            this.dinnerOrderButton.Text = "预订";
            this.dinnerOrderButton.UseVisualStyleBackColor = true;
            // 
            // dinnerDateTimePicker
            // 
            this.dinnerDateTimePicker.Location = new System.Drawing.Point(126, 181);
            this.dinnerDateTimePicker.Name = "dinnerDateTimePicker";
            this.dinnerDateTimePicker.Size = new System.Drawing.Size(214, 21);
            this.dinnerDateTimePicker.TabIndex = 8;
            // 
            // dinnerNumericUpDown
            // 
            this.dinnerNumericUpDown.Location = new System.Drawing.Point(126, 130);
            this.dinnerNumericUpDown.Name = "dinnerNumericUpDown";
            this.dinnerNumericUpDown.Size = new System.Drawing.Size(214, 21);
            this.dinnerNumericUpDown.TabIndex = 7;
            this.dinnerNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.dinnerNumericUpDown.ValueChanged += new System.EventHandler(this.dinnerNumericUpDown_ValueChanged);
            // 
            // dinnerCustomerPhone
            // 
            this.dinnerCustomerPhone.Location = new System.Drawing.Point(126, 77);
            this.dinnerCustomerPhone.Name = "dinnerCustomerPhone";
            this.dinnerCustomerPhone.Size = new System.Drawing.Size(214, 21);
            this.dinnerCustomerPhone.TabIndex = 6;
            // 
            // dinnerCostomerName
            // 
            this.dinnerCostomerName.Location = new System.Drawing.Point(126, 29);
            this.dinnerCostomerName.Name = "dinnerCostomerName";
            this.dinnerCostomerName.Size = new System.Drawing.Size(214, 21);
            this.dinnerCostomerName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(33, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "费用预算";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(33, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "用餐日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(33, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "用餐人数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "联系电话";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户姓名";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.PowderBlue;
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.partyCostlable);
            this.tabPage2.Controls.Add(this.partyfinceCheckBox);
            this.tabPage2.Controls.Add(this.partyWineCheckBox);
            this.tabPage2.Controls.Add(this.partyMemberCheckBox);
            this.tabPage2.Controls.Add(this.PartyDateTimePicker);
            this.tabPage2.Controls.Add(this.partyNuMericUpDown);
            this.tabPage2.Controls.Add(this.partyCustomerPhone);
            this.tabPage2.Controls.Add(this.partyCustomerName);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(419, 409);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "宴会用餐";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(249, 366);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "预定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // partyCostlable
            // 
            this.partyCostlable.AutoSize = true;
            this.partyCostlable.BackColor = System.Drawing.Color.Silver;
            this.partyCostlable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.partyCostlable.Location = new System.Drawing.Point(117, 312);
            this.partyCostlable.Name = "partyCostlable";
            this.partyCostlable.Size = new System.Drawing.Size(49, 14);
            this.partyCostlable.TabIndex = 12;
            this.partyCostlable.Text = "label11";
            // 
            // partyfinceCheckBox
            // 
            this.partyfinceCheckBox.AutoSize = true;
            this.partyfinceCheckBox.Location = new System.Drawing.Point(246, 266);
            this.partyfinceCheckBox.Name = "partyfinceCheckBox";
            this.partyfinceCheckBox.Size = new System.Drawing.Size(72, 16);
            this.partyfinceCheckBox.TabIndex = 11;
            this.partyfinceCheckBox.Text = "华丽装饰";
            this.partyfinceCheckBox.UseVisualStyleBackColor = true;
            this.partyfinceCheckBox.CheckedChanged += new System.EventHandler(this.partyfinceCheckBox_CheckedChanged);
            // 
            // partyWineCheckBox
            // 
            this.partyWineCheckBox.AutoSize = true;
            this.partyWineCheckBox.Location = new System.Drawing.Point(143, 266);
            this.partyWineCheckBox.Name = "partyWineCheckBox";
            this.partyWineCheckBox.Size = new System.Drawing.Size(48, 16);
            this.partyWineCheckBox.TabIndex = 10;
            this.partyWineCheckBox.Text = "酒水";
            this.partyWineCheckBox.UseVisualStyleBackColor = true;
            this.partyWineCheckBox.CheckedChanged += new System.EventHandler(this.partyWineCheckBox_CheckedChanged);
            // 
            // partyMemberCheckBox
            // 
            this.partyMemberCheckBox.AutoSize = true;
            this.partyMemberCheckBox.Location = new System.Drawing.Point(45, 266);
            this.partyMemberCheckBox.Name = "partyMemberCheckBox";
            this.partyMemberCheckBox.Size = new System.Drawing.Size(48, 16);
            this.partyMemberCheckBox.TabIndex = 9;
            this.partyMemberCheckBox.Text = "会员";
            this.partyMemberCheckBox.UseVisualStyleBackColor = true;
            this.partyMemberCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // PartyDateTimePicker
            // 
            this.PartyDateTimePicker.Location = new System.Drawing.Point(119, 204);
            this.PartyDateTimePicker.Name = "PartyDateTimePicker";
            this.PartyDateTimePicker.Size = new System.Drawing.Size(205, 21);
            this.PartyDateTimePicker.TabIndex = 8;
            // 
            // partyNuMericUpDown
            // 
            this.partyNuMericUpDown.Location = new System.Drawing.Point(119, 145);
            this.partyNuMericUpDown.Name = "partyNuMericUpDown";
            this.partyNuMericUpDown.Size = new System.Drawing.Size(205, 21);
            this.partyNuMericUpDown.TabIndex = 7;
            this.partyNuMericUpDown.ValueChanged += new System.EventHandler(this.partyNuMericUpDown_ValueChanged);
            // 
            // partyCustomerPhone
            // 
            this.partyCustomerPhone.Location = new System.Drawing.Point(119, 87);
            this.partyCustomerPhone.Name = "partyCustomerPhone";
            this.partyCustomerPhone.Size = new System.Drawing.Size(205, 21);
            this.partyCustomerPhone.TabIndex = 6;
            // 
            // partyCustomerName
            // 
            this.partyCustomerName.Location = new System.Drawing.Point(119, 25);
            this.partyCustomerName.Name = "partyCustomerName";
            this.partyCustomerName.Size = new System.Drawing.Size(205, 21);
            this.partyCustomerName.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 312);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "费用预算";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "用餐日期";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "用餐人数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "用餐电话";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "客户姓名";
            // 
            // SpiritKitchen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 468);
            this.Controls.Add(this.tabControl1);
            this.Name = "SpiritKitchen";
            this.Text = "订餐小精灵";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dinnerNumericUpDown)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.partyNuMericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button dinnerCancelButton;
        private System.Windows.Forms.Button dinnerOrderButton;
        private System.Windows.Forms.DateTimePicker dinnerDateTimePicker;
        private System.Windows.Forms.NumericUpDown dinnerNumericUpDown;
        private System.Windows.Forms.TextBox dinnerCustomerPhone;
        private System.Windows.Forms.TextBox dinnerCostomerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox dinnerWinerCheckBox;
        private System.Windows.Forms.CheckBox dinnerMemberCheckBox;
        private System.Windows.Forms.Label Costlabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label partyCostlable;
        private System.Windows.Forms.CheckBox partyfinceCheckBox;
        private System.Windows.Forms.CheckBox partyWineCheckBox;
        private System.Windows.Forms.CheckBox partyMemberCheckBox;
        private System.Windows.Forms.DateTimePicker PartyDateTimePicker;
        private System.Windows.Forms.NumericUpDown partyNuMericUpDown;
        private System.Windows.Forms.TextBox partyCustomerPhone;
        private System.Windows.Forms.TextBox partyCustomerName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}

