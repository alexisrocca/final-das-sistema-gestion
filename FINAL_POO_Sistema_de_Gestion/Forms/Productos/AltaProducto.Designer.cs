namespace FINAL_POO_Sistema_de_Gestion
{
    partial class FrmAltaProducto
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.gbDatosBasicos = new System.Windows.Forms.GroupBox();
            this.chbPoseeVencimiento = new System.Windows.Forms.CheckBox();
            this.lblNumPrecioUnitario = new System.Windows.Forms.Label();
            this.numPrecioUnitario = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioUnitario = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.gbStock = new System.Windows.Forms.GroupBox();
            this.cbSucursal = new System.Windows.Forms.ComboBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.numStockInicial = new System.Windows.Forms.NumericUpDown();
            this.lblStockInicial = new System.Windows.Forms.Label();
            this.gbCategoria = new System.Windows.Forms.GroupBox();
            this.cbCategorias = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.btnGuardarProducto = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbVencimiento = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.gbDatosBasicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioUnitario)).BeginInit();
            this.gbStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStockInicial)).BeginInit();
            this.gbCategoria.SuspendLayout();
            this.gbVencimiento.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(24, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(266, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dar de Alta un Producto";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(19, 60);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(397, 22);
            this.txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(16, 41);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(56, 16);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre";
            // 
            // gbDatosBasicos
            // 
            this.gbDatosBasicos.Controls.Add(this.chbPoseeVencimiento);
            this.gbDatosBasicos.Controls.Add(this.lblNumPrecioUnitario);
            this.gbDatosBasicos.Controls.Add(this.numPrecioUnitario);
            this.gbDatosBasicos.Controls.Add(this.lblPrecioUnitario);
            this.gbDatosBasicos.Controls.Add(this.txtDescripcion);
            this.gbDatosBasicos.Controls.Add(this.lblDescripcion);
            this.gbDatosBasicos.Controls.Add(this.txtNombre);
            this.gbDatosBasicos.Controls.Add(this.lblNombre);
            this.gbDatosBasicos.Location = new System.Drawing.Point(29, 87);
            this.gbDatosBasicos.Name = "gbDatosBasicos";
            this.gbDatosBasicos.Size = new System.Drawing.Size(496, 504);
            this.gbDatosBasicos.TabIndex = 3;
            this.gbDatosBasicos.TabStop = false;
            this.gbDatosBasicos.Text = "Datos Basicos del Producto";
            // 
            // chbPoseeVencimiento
            // 
            this.chbPoseeVencimiento.AutoSize = true;
            this.chbPoseeVencimiento.Location = new System.Drawing.Point(19, 378);
            this.chbPoseeVencimiento.Name = "chbPoseeVencimiento";
            this.chbPoseeVencimiento.Size = new System.Drawing.Size(146, 20);
            this.chbPoseeVencimiento.TabIndex = 4;
            this.chbPoseeVencimiento.Text = "Posee Vencimiento";
            this.chbPoseeVencimiento.UseVisualStyleBackColor = true;
            this.chbPoseeVencimiento.CheckedChanged += new System.EventHandler(this.chbPoseeVencimiento_CheckedChanged);
            // 
            // lblNumPrecioUnitario
            // 
            this.lblNumPrecioUnitario.AutoSize = true;
            this.lblNumPrecioUnitario.Location = new System.Drawing.Point(16, 330);
            this.lblNumPrecioUnitario.Name = "lblNumPrecioUnitario";
            this.lblNumPrecioUnitario.Size = new System.Drawing.Size(14, 16);
            this.lblNumPrecioUnitario.TabIndex = 8;
            this.lblNumPrecioUnitario.Text = "$";
            // 
            // numPrecioUnitario
            // 
            this.numPrecioUnitario.DecimalPlaces = 2;
            this.numPrecioUnitario.Location = new System.Drawing.Point(32, 328);
            this.numPrecioUnitario.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numPrecioUnitario.Name = "numPrecioUnitario";
            this.numPrecioUnitario.Size = new System.Drawing.Size(176, 22);
            this.numPrecioUnitario.TabIndex = 3;
            this.numPrecioUnitario.ThousandsSeparator = true;
            // 
            // lblPrecioUnitario
            // 
            this.lblPrecioUnitario.AutoSize = true;
            this.lblPrecioUnitario.Location = new System.Drawing.Point(16, 309);
            this.lblPrecioUnitario.Name = "lblPrecioUnitario";
            this.lblPrecioUnitario.Size = new System.Drawing.Size(95, 16);
            this.lblPrecioUnitario.TabIndex = 6;
            this.lblPrecioUnitario.Text = "Precio Unitario";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(19, 127);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(397, 149);
            this.txtDescripcion.TabIndex = 2;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(16, 108);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(79, 16);
            this.lblDescripcion.TabIndex = 4;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // gbStock
            // 
            this.gbStock.Controls.Add(this.cbSucursal);
            this.gbStock.Controls.Add(this.lblSucursal);
            this.gbStock.Controls.Add(this.numStockInicial);
            this.gbStock.Controls.Add(this.lblStockInicial);
            this.gbStock.Location = new System.Drawing.Point(531, 87);
            this.gbStock.Name = "gbStock";
            this.gbStock.Size = new System.Drawing.Size(504, 124);
            this.gbStock.TabIndex = 4;
            this.gbStock.TabStop = false;
            this.gbStock.Text = "Stock";
            // 
            // numStockInicial
            // 
            this.numStockInicial.Location = new System.Drawing.Point(25, 61);
            this.numStockInicial.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numStockInicial.Name = "numStockInicial";
            this.numStockInicial.Size = new System.Drawing.Size(176, 22);
            this.numStockInicial.TabIndex = 5;
            this.numStockInicial.ThousandsSeparator = true;
            this.numStockInicial.ValueChanged += new System.EventHandler(this.numStockInicial_ValueChanged);
            // 
            // lblStockInicial
            // 
            this.lblStockInicial.AutoSize = true;
            this.lblStockInicial.Location = new System.Drawing.Point(22, 41);
            this.lblStockInicial.Name = "lblStockInicial";
            this.lblStockInicial.Size = new System.Drawing.Size(78, 16);
            this.lblStockInicial.TabIndex = 8;
            this.lblStockInicial.Text = "Stock Inicial";
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(220, 41);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(62, 16);
            this.lblSucursal.TabIndex = 10;
            this.lblSucursal.Text = "Sucursal";
            // 
            // cbSucursal
            // 
            this.cbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal.FormattingEnabled = true;
            this.cbSucursal.Location = new System.Drawing.Point(223, 61);
            this.cbSucursal.Name = "cbSucursal";
            this.cbSucursal.Size = new System.Drawing.Size(258, 24);
            this.cbSucursal.TabIndex = 11;
            // 
            // gbCategoria
            // 
            this.gbCategoria.Controls.Add(this.cbCategorias);
            this.gbCategoria.Controls.Add(this.lblCategoria);
            this.gbCategoria.Location = new System.Drawing.Point(531, 214);
            this.gbCategoria.Name = "gbCategoria";
            this.gbCategoria.Size = new System.Drawing.Size(504, 124);
            this.gbCategoria.TabIndex = 10;
            this.gbCategoria.TabStop = false;
            this.gbCategoria.Text = "Categoria";
            // 
            // cbCategorias
            // 
            this.cbCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategorias.FormattingEnabled = true;
            this.cbCategorias.Location = new System.Drawing.Point(25, 60);
            this.cbCategorias.Name = "cbCategorias";
            this.cbCategorias.Size = new System.Drawing.Size(397, 24);
            this.cbCategorias.TabIndex = 6;
            this.cbCategorias.MouseEnter += new System.EventHandler(this.cbCategorias_MouseEnter);
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(22, 41);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(148, 16);
            this.lblCategoria.TabIndex = 8;
            this.lblCategoria.Text = "Categoria al que pertenece";
            // 
            // btnGuardarProducto
            // 
            this.btnGuardarProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarProducto.AutoSize = true;
            this.btnGuardarProducto.BackColor = System.Drawing.Color.PaleGreen;
            this.btnGuardarProducto.Location = new System.Drawing.Point(806, 32);
            this.btnGuardarProducto.Name = "btnGuardarProducto";
            this.btnGuardarProducto.Size = new System.Drawing.Size(147, 28);
            this.btnGuardarProducto.TabIndex = 8;
            this.btnGuardarProducto.Text = "Guardar Producto";
            this.btnGuardarProducto.UseVisualStyleBackColor = false;
            this.btnGuardarProducto.Click += new System.EventHandler(this.btnGuardarProducto_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancelar.Location = new System.Drawing.Point(960, 33);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 26);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gbVencimiento
            // 
            this.gbVencimiento.Controls.Add(this.label1);
            this.gbVencimiento.Controls.Add(this.dtpFechaVencimiento);
            this.gbVencimiento.Location = new System.Drawing.Point(531, 344);
            this.gbVencimiento.Name = "gbVencimiento";
            this.gbVencimiento.Size = new System.Drawing.Size(504, 130);
            this.gbVencimiento.TabIndex = 11;
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
            this.dtpFechaVencimiento.TabIndex = 7;
            // 
            // FrmAltaProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 655);
            this.Controls.Add(this.gbVencimiento);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardarProducto);
            this.Controls.Add(this.gbCategoria);
            this.Controls.Add(this.gbStock);
            this.Controls.Add(this.gbDatosBasicos);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmAltaProducto";
            this.Text = "Alta de Producto";
            this.VisibleChanged += new System.EventHandler(this.FrmAltaProducto_VisibleChanged);
            this.gbDatosBasicos.ResumeLayout(false);
            this.gbDatosBasicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioUnitario)).EndInit();
            this.gbStock.ResumeLayout(false);
            this.gbStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStockInicial)).EndInit();
            this.gbCategoria.ResumeLayout(false);
            this.gbCategoria.PerformLayout();
            this.gbVencimiento.ResumeLayout(false);
            this.gbVencimiento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.GroupBox gbDatosBasicos;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.GroupBox gbStock;
        private System.Windows.Forms.NumericUpDown numStockInicial;
        private System.Windows.Forms.Label lblStockInicial;
        private System.Windows.Forms.GroupBox gbCategoria;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Button btnGuardarProducto;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cbCategorias;
        private System.Windows.Forms.CheckBox chbPoseeVencimiento;
        private System.Windows.Forms.Label lblNumPrecioUnitario;
        private System.Windows.Forms.NumericUpDown numPrecioUnitario;
        private System.Windows.Forms.Label lblPrecioUnitario;
        private System.Windows.Forms.GroupBox gbVencimiento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.ComboBox cbSucursal;
        private System.Windows.Forms.Label lblSucursal;
    }
}