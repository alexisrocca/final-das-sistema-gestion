namespace FINAL_POO_Sistema_de_Gestion
{
    partial class FrmEgresoStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.gbDatosBasicos = new System.Windows.Forms.GroupBox();
            this.cbSucursal = new System.Windows.Forms.ComboBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.cbTipoEgreso = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.lblTipoEgreso = new System.Windows.Forms.Label();
            this.btnRegistrarIngreso = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.gbDatosBasicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(24, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(295, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Registrar Egreso de Stock";
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(16, 41);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(61, 16);
            this.lblProducto.TabIndex = 2;
            this.lblProducto.Text = "Producto";
            // 
            // gbDatosBasicos
            // 
            this.gbDatosBasicos.Controls.Add(this.cbSucursal);
            this.gbDatosBasicos.Controls.Add(this.lblSucursal);
            this.gbDatosBasicos.Controls.Add(this.numCantidad);
            this.gbDatosBasicos.Controls.Add(this.cbTipoEgreso);
            this.gbDatosBasicos.Controls.Add(this.lblCantidad);
            this.gbDatosBasicos.Controls.Add(this.cbProducto);
            this.gbDatosBasicos.Controls.Add(this.lblTipoEgreso);
            this.gbDatosBasicos.Controls.Add(this.lblProducto);
            this.gbDatosBasicos.Location = new System.Drawing.Point(29, 87);
            this.gbDatosBasicos.Name = "gbDatosBasicos";
            this.gbDatosBasicos.Size = new System.Drawing.Size(496, 310);
            this.gbDatosBasicos.TabIndex = 3;
            this.gbDatosBasicos.TabStop = false;
            this.gbDatosBasicos.Text = "Datos Basicos";
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(19, 189);
            this.numCantidad.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(176, 22);
            this.numCantidad.TabIndex = 3;
            this.numCantidad.ThousandsSeparator = true;
            this.numCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbTipoEgreso
            // 
            this.cbTipoEgreso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoEgreso.FormattingEnabled = true;
            this.cbTipoEgreso.Location = new System.Drawing.Point(19, 127);
            this.cbTipoEgreso.Name = "cbTipoEgreso";
            this.cbTipoEgreso.Size = new System.Drawing.Size(397, 24);
            this.cbTipoEgreso.TabIndex = 2;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(16, 168);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(61, 16);
            this.lblCantidad.TabIndex = 8;
            this.lblCantidad.Text = "Cantidad";
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(16, 230);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(62, 16);
            this.lblSucursal.TabIndex = 10;
            this.lblSucursal.Text = "Sucursal";
            // 
            // cbSucursal
            // 
            this.cbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal.FormattingEnabled = true;
            this.cbSucursal.Location = new System.Drawing.Point(19, 250);
            this.cbSucursal.Name = "cbSucursal";
            this.cbSucursal.Size = new System.Drawing.Size(397, 24);
            this.cbSucursal.TabIndex = 11;
            // 
            // cbProducto
            // 
            this.cbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(19, 61);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(397, 24);
            this.cbProducto.TabIndex = 1;
            this.cbProducto.SelectedIndexChanged += new System.EventHandler(this.cbProducto_SelectedIndexChanged);
            this.cbProducto.Click += new System.EventHandler(this.cbProducto_Click);
            // 
            // lblTipoEgreso
            // 
            this.lblTipoEgreso.AutoSize = true;
            this.lblTipoEgreso.Location = new System.Drawing.Point(16, 108);
            this.lblTipoEgreso.Name = "lblTipoEgreso";
            this.lblTipoEgreso.Size = new System.Drawing.Size(101, 16);
            this.lblTipoEgreso.TabIndex = 4;
            this.lblTipoEgreso.Text = "Tipo de Egreso";
            // 
            // btnRegistrarIngreso
            // 
            this.btnRegistrarIngreso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrarIngreso.AutoSize = true;
            this.btnRegistrarIngreso.BackColor = System.Drawing.Color.PaleGreen;
            this.btnRegistrarIngreso.Location = new System.Drawing.Point(846, 32);
            this.btnRegistrarIngreso.Name = "btnRegistrarIngreso";
            this.btnRegistrarIngreso.Size = new System.Drawing.Size(107, 28);
            this.btnRegistrarIngreso.TabIndex = 7;
            this.btnRegistrarIngreso.Text = "Registrar";
            this.btnRegistrarIngreso.UseVisualStyleBackColor = false;
            this.btnRegistrarIngreso.Click += new System.EventHandler(this.btnGuardarProducto_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancelar.Location = new System.Drawing.Point(960, 33);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 26);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblStockActual
            // 
            this.lblStockActual.AutoSize = true;
            this.lblStockActual.Location = new System.Drawing.Point(26, 415);
            this.lblStockActual.Name = "lblStockActual";
            this.lblStockActual.Size = new System.Drawing.Size(87, 16);
            this.lblStockActual.TabIndex = 9;
            this.lblStockActual.Text = "Stock Actual: ";
            // 
            // FrmEgresoStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 655);
            this.Controls.Add(this.lblStockActual);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRegistrarIngreso);
            this.Controls.Add(this.gbDatosBasicos);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmEgresoStock";
            this.Text = "Egreso Stock";
            this.VisibleChanged += new System.EventHandler(this.FrmEgresoStock_VisibleChanged);
            this.gbDatosBasicos.ResumeLayout(false);
            this.gbDatosBasicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.GroupBox gbDatosBasicos;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnRegistrarIngreso;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.Label lblStockActual;
        private System.Windows.Forms.ComboBox cbTipoEgreso;
        private System.Windows.Forms.Label lblTipoEgreso;
        private System.Windows.Forms.ComboBox cbSucursal;
        private System.Windows.Forms.Label lblSucursal;
    }
}