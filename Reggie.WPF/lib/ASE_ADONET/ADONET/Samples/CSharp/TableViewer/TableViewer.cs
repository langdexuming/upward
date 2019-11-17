using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Sybase.Data.AseClient;

namespace TableViewer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtConnectString;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.TextBox txtSQLStatement;
		private System.Windows.Forms.Button btnExecute;
		private System.Windows.Forms.DataGrid dgResults;

		private AseConnection		_conn;
        private System.Windows.Forms.Button btlClose;
		private System.Windows.Forms.ComboBox comboBoxTables;
		private System.Windows.Forms.Label label3;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			// populate the default hostname
			try 
			{
			txtConnectString.Text = "Data Source='" + System.Net.Dns.GetHostName() + "';Port='5000';UID='sa';PWD='';Database='pubs2';";
			}
			catch (Exception ex ) 
			{
				MessageBox.Show( ex.Message );
			}

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			// close any open connections to the database
			if( _conn != null && _conn.State != ConnectionState.Closed ) {
				_conn.Close();
			}
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtConnectString = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSQLStatement = new System.Windows.Forms.TextBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnExecute = new System.Windows.Forms.Button();
			this.dgResults = new System.Windows.Forms.DataGrid();
			this.btlClose = new System.Windows.Forms.Button();
			this.comboBoxTables = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgResults)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Connection String:";
			// 
			// txtConnectString
			// 
			this.txtConnectString.Location = new System.Drawing.Point(8, 32);
			this.txtConnectString.Name = "txtConnectString";
			this.txtConnectString.Size = new System.Drawing.Size(312, 20);
			this.txtConnectString.TabIndex = 3;
			this.txtConnectString.Text = "Data Source=\'\';Port=\'5000\';UID=\'sa\';PWD=\'\';Database=\'pubs2\';";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "SQL Statement:";
			// 
			// txtSQLStatement
			// 
			this.txtSQLStatement.Location = new System.Drawing.Point(8, 96);
			this.txtSQLStatement.Multiline = true;
			this.txtSQLStatement.Name = "txtSQLStatement";
			this.txtSQLStatement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtSQLStatement.Size = new System.Drawing.Size(528, 80);
			this.txtSQLStatement.TabIndex = 5;
			this.txtSQLStatement.Text = "SELECT * FROM authors";
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(461, 8);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.TabIndex = 1;
			this.btnConnect.Text = "&Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnExecute
			// 
			this.btnExecute.Enabled = false;
			this.btnExecute.Location = new System.Drawing.Point(461, 36);
			this.btnExecute.Name = "btnExecute";
			this.btnExecute.TabIndex = 2;
			this.btnExecute.Text = "&Execute";
			this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
			// 
			// dgResults
			// 
			this.dgResults.CaptionText = "Results";
			this.dgResults.DataMember = "";
			this.dgResults.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgResults.Location = new System.Drawing.Point(8, 184);
			this.dgResults.Name = "dgResults";
			this.dgResults.ReadOnly = true;
			this.dgResults.Size = new System.Drawing.Size(528, 320);
			this.dgResults.TabIndex = 6;
			// 
			// btlClose
			// 
			this.btlClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btlClose.Location = new System.Drawing.Point(461, 64);
			this.btlClose.Name = "btlClose";
			this.btlClose.TabIndex = 9;
			this.btlClose.Text = "C&lose";
			this.btlClose.Click += new System.EventHandler(this.btlClose_Click);
			// 
			// comboBoxTables
			// 
			this.comboBoxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTables.ItemHeight = 13;
			this.comboBoxTables.Location = new System.Drawing.Point(336, 32);
			this.comboBoxTables.Name = "comboBoxTables";
			this.comboBoxTables.Size = new System.Drawing.Size(112, 21);
			this.comboBoxTables.Sorted = true;
			this.comboBoxTables.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(336, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Tables:";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(544, 510);
			this.Controls.Add(this.comboBoxTables);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btlClose);
			this.Controls.Add(this.dgResults);
			this.Controls.Add(this.btnExecute);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.txtSQLStatement);
			this.Controls.Add(this.txtConnectString);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Table Viewer";
			((System.ComponentModel.ISupportInitialize)(this.dgResults)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnConnect_Click(object sender, System.EventArgs e) 
		{
			try 
			{
				_conn = new AseConnection( txtConnectString.Text );
				_conn.Open();
				btnExecute.Enabled = true;
				using (AseCommand cmd = new AseCommand( "SELECT name FROM sysobjects where type = 'U'", _conn ))
				{
					using (AseDataReader dr = cmd.ExecuteReader())
					{
						comboBoxTables.Items.Clear();  
						while ( dr.Read() ) 
						{
							comboBoxTables.Items.Add( dr.GetString( 0 ) );
						}
						comboBoxTables.SelectedIndex = 0;
					}
				}
			} 
			catch( AseException ex ) 
			{
				MessageBox.Show( ex.Source + " : " + ex.Message + " (" + 
					ex.ToString() + ")",
					"Failed to connect" );
			}
		}

		private void btnExecute_Click(object sender, System.EventArgs e) 
		{
			if( _conn == null || _conn.State != ConnectionState.Open ) {
				MessageBox.Show( "Connect to a database first.", "Not connected" );
				return;
			}
            if( txtSQLStatement.Text.Trim().Length < 1 ) {  
                MessageBox.Show( "Please enter the command text.", "Empty command text" );
                txtSQLStatement.SelectAll();
                txtSQLStatement.Focus(); 
                return;
            }
            
            try {
                dgResults.DataSource = null;
                
				using(AseCommand cmd = new AseCommand( txtSQLStatement.Text.Trim(), _conn ))
				{
					using(AseDataAdapter da = new AseDataAdapter(cmd))
					{
						DataSet ds = new DataSet();
						da.Fill(ds, "Table");
						dgResults.DataSource = ds.Tables["Table"];
						ds.Dispose();
					}
				}
			} catch( AseException ex ) {
				MessageBox.Show( ex.Source + " : " + ex.Message + " (" + 
					ex.ToString() + ")",
					"Failed to execute SQL statement" );
			}

			txtSQLStatement.SelectAll();
			txtSQLStatement.Focus(); 
		}

        private void btlClose_Click(object sender, System.EventArgs e)
        {
            this.Close(); 
        }

	}
}
