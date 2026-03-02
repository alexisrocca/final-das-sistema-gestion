namespace FINAL_POO_Sistema_de_Gestion
{
    partial class FrmModificarProducto
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
            this.gbCategoria = new System.Windows.Forms.GroupBox();
            this.cbCategorias = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.gbSucursal = new System.Windows.Forms.GroupBox();
            this.cbSucursal = new System.Windows.Forms.ComboBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.btnGuardarProducto = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.gbDatosBasicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioUnitario)).BeginInit();
            this.gbCategoria.SuspendLayout();
            this.gbSucursal.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(24, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(215, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Modificar Producto";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(19, 60);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(397, 22);
            this.txtNombre.TabIndex = 2;
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
            this.chbPoseeVencimiento.TabIndex = 5;
            this.chbPoseeVencimiento.Text = "Posee Vencimiento";
            this.chbPoseeVencimiento.UseVisualStyleBackColor = true;
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
            this.numPrecioUnitario.TabIndex = 4;
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
            this.txtDescripcion.TabIndex = 3;
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
            // gbCategoria
            // 
            this.gbCategoria.Controls.Add(this.cbCategorias);
            this.gbCategoria.Controls.Add(this.lblCategoria);
            this.gbCategoria.Location = new System.Drawing.Point(531, 87);
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
            this.cbCategorias.Click += new System.EventHandler(this.cbCategorias_Click);
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
            // gbSucursal
            //
            this.gbSucursal.Controls.Add(this.cbSucursal);
            this.gbSucursal.Controls.Add(this.lblSucursal);
            this.gbSucursal.Location = new System.Drawing.Point(531, 220);
            this.gbSucursal.Name = "gbSucursal";
            this.gbSucursal.Size = new System.Drawing.Size(504, 124);
            this.gbSucursal.TabIndex = 11;
            this.gbSucursal.TabStop = false;
            this.gbSucursal.Text = "Sucursal";
            //
            // cbSucursal
            //
            this.cbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal.FormattingEnabled = true;
            this.cbSucursal.Location = new System.Drawing.Point(25, 60);
            this.cbSucursal.Name = "cbSucursal";
            this.cbSucursal.Size = new System.Drawing.Size(397, 24);
            this.cbSucursal.TabIndex = 9;
            //
            // lblSucursal
            //
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(22, 41);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(170, 16);
            this.lblSucursal.TabIndex = 10;
            this.lblSucursal.Text = "Sucursal a la que pertenece";
            //
            // btnGuardarProducto
            // 
            this.btnGuardarProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarProducto.AutoSize = true;
            this.btnGuardarProducto.BackColor = System.Drawing.Color.PaleGreen;
            this.btnGuardarProducto.Location = new System.Drawing.Point(806, 32);
            this.btnGuardarProducto.Name = "btnGuardarProducto";
            this.btnGuardarProducto.Size = new System.Drawing.Size(147, 28);
            this.btnGuardarProducto.TabIndex = 7;
            this.btnGuardarProducto.Text = "Guardar Cambios";
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
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cbProducto
            // 
            this.cbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(296, 32);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(397, 24);
            this.cbProducto.Sorted = true;
            this.cbProducto.TabIndex = 1;
            this.cbProducto.SelectedIndexChanged += new System.EventHandler(this.cbProducto_SelectedIndexChanged);
            this.cbProducto.Click += new System.EventHandler(this.cbProducto_Click);
            // 
            // FrmModificarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 655);
            this.Controls.Add(this.cbProducto);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardarProducto);
            this.Controls.Add(this.gbSucursal);
            this.Controls.Add(this.gbCategoria);
            this.Controls.Add(this.gbDatosBasicos);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmModificarProducto";
            this.Text = "Modificar Producto";
            this.VisibleChanged += new System.EventHandler(this.FrmModificarProducto_VisibleChanged);
            this.gbDatosBasicos.ResumeLayout(false);
            this.gbDatosBasicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioUnitario)).EndInit();
            this.gbCategoria.ResumeLayout(false);
            this.gbCategoria.PerformLayout();
            this.gbSucursal.ResumeLayout(false);
            this.gbSucursal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.GroupBox gbDatosBasicos;
        private System.Windows.Forms.Label lblPrecioUnitario;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblNumPrecioUnitario;
        private System.Windows.Forms.NumericUpDown numPrecioUnitario;
        private System.Windows.Forms.GroupBox gbCategoria;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Button btnGuardarProducto;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cbCategorias;
        private System.Windows.Forms.CheckBox chbPoseeVencimiento;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.GroupBox gbSucursal;
        private System.Windows.Forms.ComboBox cbSucursal;
        private System.Windows.Forms.Label lblSucursal;
    }
}