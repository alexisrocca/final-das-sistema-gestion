namespace FINAL_POO_Sistema_de_Gestion.Forms
{
    partial class Welcome
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
            this.components = new System.ComponentModel.Container();
            this.lblEstadoDatos = new System.Windows.Forms.Label();
            this.lblCargado = new System.Windows.Forms.Label();
            this.lblFechaActual = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbNotificaciones = new System.Windows.Forms.ListBox();
            this.lblNotificaciones = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEstadoDatos
            // 
            this.lblEstadoDatos.AutoSize = true;
            this.lblEstadoDatos.Location = new System.Drawing.Point(12, 145);
            this.lblEstadoDatos.Name = "lblEstadoDatos";
            this.lblEstadoDatos.Size = new System.Drawing.Size(148, 16);
            this.lblEstadoDatos.TabIndex = 0;
            this.lblEstadoDatos.Text = "Datos Leidos de JSON:";
            // 
            // lblCargado
            // 
            this.lblCargado.AutoSize = true;
            this.lblCargado.Location = new System.Drawing.Point(12, 175);
            this.lblCargado.Name = "lblCargado";
            this.lblCargado.Size = new System.Drawing.Size(73, 16);
            this.lblCargado.TabIndex = 2;
            this.lblCargado.Text = "Cargados: ";
            // 
            // lblFechaActual
            // 
            this.lblFechaActual.AutoSize = true;
            this.lblFechaActual.Location = new System.Drawing.Point(12, 38);
            this.lblFechaActual.Name = "lblFechaActual";
            this.lblFechaActual.Size = new System.Drawing.Size(88, 16);
            this.lblFechaActual.TabIndex = 3;
            this.lblFechaActual.Text = "Fecha Actual:";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // lbNotificaciones
            // 
            this.lbNotificaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNotificaciones.FormattingEnabled = true;
            this.lbNotificaciones.ItemHeight = 16;
            this.lbNotificaciones.Location = new System.Drawing.Point(12, 339);
            this.lbNotificaciones.Name = "lbNotificaciones";
            this.lbNotificaciones.Size = new System.Drawing.Size(1043, 308);
            this.lbNotificaciones.TabIndex = 4;
            // 
            // lblNotificaciones
            // 
            this.lblNotificaciones.AutoSize = true;
            this.lblNotificaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotificaciones.Location = new System.Drawing.Point(12, 307);
            this.lblNotificaciones.Name = "lblNotificaciones";
            this.lblNotificaciones.Size = new System.Drawing.Size(179, 29);
            this.lblNotificaciones.TabIndex = 5;
            this.lblNotificaciones.Text = "Notificaciones";
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 655);
            this.Controls.Add(this.lblNotificaciones);
            this.Controls.Add(this.lbNotificaciones);
            this.Controls.Add(this.lblFechaActual);
            this.Controls.Add(this.lblCargado);
            this.Controls.Add(this.lblEstadoDatos);
            this.Name = "Welcome";
            this.Text = "Welcome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEstadoDatos;
        private System.Windows.Forms.Label lblCargado;
        private System.Windows.Forms.Label lblFechaActual;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label lblNotificaciones;
        public System.Windows.Forms.ListBox lbNotificaciones;
    }
}