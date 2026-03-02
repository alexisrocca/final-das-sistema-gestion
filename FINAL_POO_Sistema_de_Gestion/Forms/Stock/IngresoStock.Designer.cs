namespace FINAL_POO_Sistema_de_Gestion
{
    partial class FrmIngresoStock
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
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.lblNumPrecioUnitario = new System.Windows.Forms.Label();
            this.numPrecioCompra = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioCompra = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.gbVencimiento = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.btnRegistrarIngreso = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.lblPrecioVentaCalculado = new System.Windows.Forms.Label();
            this.cbSucursal = new System.Windows.Forms.ComboBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.gbDatosBasicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioCompra)).BeginInit();
            this.gbVencimiento.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(24, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(298, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Registrar Ingreso de Stock";
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
            this.gbDatosBasicos.Controls.Add(this.cbProveedor);
            this.gbDatosBasicos.Controls.Add(this.lblCantidad);
            this.gbDatosBasicos.Controls.Add(this.cbProducto);
            this.gbDatosBasicos.Controls.Add(this.lblNumPrecioUnitario);
            this.gbDatosBasicos.Controls.Add(this.numPrecioCompra);
            this.gbDatosBasicos.Controls.Add(this.lblPrecioCompra);
            this.gbDatosBasicos.Controls.Add(this.lblProveedor);
            this.gbDatosBasicos.Controls.Add(this.lblProducto);
            this.gbDatosBasicos.Location = new System.Drawing.Point(29, 87);
            this.gbDatosBasicos.Name = "gbDatosBasicos";
            this.gbDatosBasicos.Size = new System.Drawing.Size(496, 380);
            this.gbDatosBasicos.TabIndex = 3;
            this.gbDatosBasicos.TabStop = false;
            this.gbDatosBasicos.Text = "Datos Basicos";
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(19, 189);
            this.numCantidad.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
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
            // cbProveedor
            // 
            this.cbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.Location = new System.Drawing.Point(19, 127);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(397, 24);
            this.cbProveedor.TabIndex = 2;
            this.cbProveedor.Click += new System.EventHandler(this.cbProveedor_Click);
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
            // lblNumPrecioUnitario
            // 
            this.lblNumPrecioUnitario.AutoSize = true;
            this.lblNumPrecioUnitario.Location = new System.Drawing.Point(16, 256);
            this.lblNumPrecioUnitario.Name = "lblNumPrecioUnitario";
            this.lblNumPrecioUnitario.Size = new System.Drawing.Size(14, 16);
            this.lblNumPrecioUnitario.TabIndex = 8;
            this.lblNumPrecioUnitario.Text = "$";
            // 
            // numPrecioCompra
            // 
            this.numPrecioCompra.DecimalPlaces = 2;
            this.numPrecioCompra.Location = new System.Drawing.Point(32, 254);
            this.numPrecioCompra.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numPrecioCompra.Name = "numPrecioCompra";
            this.numPrecioCompra.Size = new System.Drawing.Size(176, 22);
            this.numPrecioCompra.TabIndex = 4;
            this.numPrecioCompra.ThousandsSeparator = true;
            this.numPrecioCompra.ValueChanged += new System.EventHandler(this.numPrecioCompra_ValueChanged);
            // 
            // lblPrecioCompra
            // 
            this.lblPrecioCompra.AutoSize = true;
            this.lblPrecioCompra.Location = new System.Drawing.Point(16, 235);
            this.lblPrecioCompra.Name = "lblPrecioCompra";
            this.lblPrecioCompra.Size = new System.Drawing.Size(116, 16);
            this.lblPrecioCompra.TabIndex = 6;
            this.lblPrecioCompra.Text = "Precio de Compra";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(16, 108);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(71, 16);
            this.lblProveedor.TabIndex = 4;
            this.lblProveedor.Text = "Proveedor";
            // 
            // gbVencimiento
            // 
            this.gbVencimiento.Controls.Add(this.label1);
            this.gbVencimiento.Controls.Add(this.dtpFechaVencimiento);
            this.gbVencimiento.Location = new System.Drawing.Point(531, 87);
            this.gbVencimiento.Name = "gbVencimiento";
            this.gbVencimiento.Size = new System.Drawing.Size(504, 130);
            this.gbVencimiento.TabIndex = 4;
            this.gbVencimiento.TabStop = false;
            this.gbVencimiento.Text = "Datos de Vencimiento";
            this.gbVencimiento.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Vencimiento del Lote";
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(25, 59);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(397, 22);
            this.dtpFechaVencimiento.TabIndex = 0;
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
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(16, 302);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(62, 16);
            this.lblSucursal.TabIndex = 11;
            this.lblSucursal.Text = "Sucursal";
            // 
            // cbSucursal
            // 
            this.cbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal.FormattingEnabled = true;
            this.cbSucursal.Location = new System.Drawing.Point(19, 321);
            this.cbSucursal.Name = "cbSucursal";
            this.cbSucursal.Size = new System.Drawing.Size(397, 24);
            this.cbSucursal.TabIndex = 12;
            // 
            // lblStockActual
            // 
            this.lblStockActual.AutoSize = true;
            this.lblStockActual.Location = new System.Drawing.Point(29, 490);
            this.lblStockActual.Name = "lblStockActual";
            this.lblStockActual.Size = new System.Drawing.Size(87, 16);
            this.lblStockActual.TabIndex = 9;
            this.lblStockActual.Text = "Stock Actual: ";
            // 
            // lblPrecioVentaCalculado
            // 
            this.lblPrecioVentaCalculado.AutoSize = true;
            this.lblPrecioVentaCalculado.Location = new System.Drawing.Point(32, 520);
            this.lblPrecioVentaCalculado.Name = "lblPrecioVentaCalculado";
            this.lblPrecioVentaCalculado.Size = new System.Drawing.Size(0, 16);
            this.lblPrecioVentaCalculado.TabIndex = 10;
            // 
            // FrmIngresoStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 655);
            this.Controls.Add(this.lblPrecioVentaCalculado);
            this.Controls.Add(this.lblStockActual);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRegistrarIngreso);
            this.Controls.Add(this.gbVencimiento);
            this.Controls.Add(this.gbDatosBasicos);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmIngresoStock";
            this.Text = "Ingreso Stock";
            this.VisibleChanged += new System.EventHandler(this.FrmAltaProducto_VisibleChanged);
            this.gbDatosBasicos.ResumeLayout(false);
            this.gbDatosBasicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioCompra)).EndInit();
            this.gbVencimiento.ResumeLayout(false);
            this.gbVencimiento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.GroupBox gbDatosBasicos;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.GroupBox gbVencimiento;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnRegistrarIngreso;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblNumPrecioUnitario;
        private System.Windows.Forms.NumericUpDown numPrecioCompra;
        private System.Windows.Forms.Label lblPrecioCompra;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.ComboBox cbProveedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.Label lblStockActual;
        private System.Windows.Forms.Label lblPrecioVentaCalculado;
        private System.Windows.Forms.ComboBox cbSucursal;
        private System.Windows.Forms.Label lblSucursal;
    }
}