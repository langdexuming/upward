using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Sybase.Data.AseClient;

namespace Simple
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.ListBox listAuthors;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label_host;
		private System.Windows.Forms.TextBox textBoxHost;
		private System.Windows.Forms.Label label_port;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Label label_user;
		private System.Windows.Forms.TextBox textBoxUser;
		private System.Windows.Forms.Label label_pass;
		private System.Windows.Forms.TextBox textBoxPass;
		private System.Windows.Forms.Label label2;
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

			// set default hostname property
			try 
			{
				textBoxHost.Text = System.Net.Dns.GetHostName();
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
			this.btnConnect = new System.Windows.Forms.Button();
			this.listAuthors = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label_host = new System.Windows.Forms.Label();
			this.textBoxHost = new System.Windows.Forms.TextBox();
			this.label_port = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.label_user = new System.Windows.Forms.Label();
			this.textBoxUser = new System.Windows.Forms.TextBox();
			this.label_pass = new System.Windows.Forms.Label();
			this.textBoxPass = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnConnect
			// 
			this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnConnect.Location = new System.Drawing.Point(96, 120);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(80, 23);
			this.btnConnect.TabIndex = 0;
			this.btnConnect.Text = "Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// listAuthors
			// 
			this.listAuthors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listAuthors.ItemHeight = 16;
			this.listAuthors.Location = new System.Drawing.Point(8, 192);
			this.listAuthors.Name = "listAuthors";
			this.listAuthors.Size = new System.Drawing.Size(272, 276);
			this.listAuthors.TabIndex = 1;
			this.listAuthors.SelectedIndexChanged += new System.EventHandler(this.listAuthors_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 160);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "List of Authors Last Names:";
			// 
			// label_host
			// 
			this.label_host.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_host.Location = new System.Drawing.Point(0, 56);
			this.label_host.Name = "label_host";
			this.label_host.Size = new System.Drawing.Size(40, 23);
			this.label_host.TabIndex = 3;
			this.label_host.Text = "Host:";
			// 
			// textBoxHost
			// 
			this.textBoxHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxHost.Location = new System.Drawing.Point(48, 56);
			this.textBoxHost.Name = "textBoxHost";
			this.textBoxHost.Size = new System.Drawing.Size(88, 22);
			this.textBoxHost.TabIndex = 4;
			this.textBoxHost.Text = "";
			// 
			// label_port
			// 
			this.label_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_port.Location = new System.Drawing.Point(144, 56);
			this.label_port.Name = "label_port";
			this.label_port.Size = new System.Drawing.Size(40, 23);
			this.label_port.TabIndex = 5;
			this.label_port.Text = "Port:";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxPort.Location = new System.Drawing.Point(192, 56);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(88, 22);
			this.textBoxPort.TabIndex = 6;
			this.textBoxPort.Text = "5000";
			// 
			// label_user
			// 
			this.label_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_user.Location = new System.Drawing.Point(0, 88);
			this.label_user.Name = "label_user";
			this.label_user.Size = new System.Drawing.Size(40, 23);
			this.label_user.TabIndex = 7;
			this.label_user.Text = "User:";
			// 
			// textBoxUser
			// 
			this.textBoxUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxUser.Location = new System.Drawing.Point(48, 88);
			this.textBoxUser.Name = "textBoxUser";
			this.textBoxUser.Size = new System.Drawing.Size(88, 22);
			this.textBoxUser.TabIndex = 8;
			this.textBoxUser.Text = "sa";
			// 
			// label_pass
			// 
			this.label_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_pass.Location = new System.Drawing.Point(144, 88);
			this.label_pass.Name = "label_pass";
			this.label_pass.Size = new System.Drawing.Size(40, 23);
			this.label_pass.TabIndex = 9;
			this.label_pass.Text = "Pass:";
			// 
			// textBoxPass
			// 
			this.textBoxPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxPass.Location = new System.Drawing.Point(192, 88);
			this.textBoxPass.Name = "textBoxPass";
			this.textBoxPass.Size = new System.Drawing.Size(88, 22);
			this.textBoxPass.TabIndex = 10;
			this.textBoxPass.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(288, 32);
			this.label2.TabIndex = 11;
			this.label2.Text = "Please specify connection properties for an Adaptive Server with pubs2 database";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(296, 485);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxPass);
			this.Controls.Add(this.label_pass);
			this.Controls.Add(this.textBoxUser);
			this.Controls.Add(this.label_user);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.label_port);
			this.Controls.Add(this.textBoxHost);
			this.Controls.Add(this.label_host);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listAuthors);
			this.Controls.Add(this.btnConnect);
			this.Name = "Form1";
			this.Text = "AseSample";
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
			// obtain the connection information specified
			String host = textBoxHost.Text;
			String port = textBoxPort.Text;
			String user = textBoxUser.Text;
			String pass = textBoxPass.Text;

			// build a connect string
			AseConnection conn = new AseConnection( "Data Source='" + host + "';Port='" + port + "';UID='" + user + "';PWD='" + pass + "';Database='pubs2';" );
			AseCommand cmd = null;
			AseDataReader reader = null;

			try 
			{
				conn.Open();

				cmd = new AseCommand( "select au_lname from authors", conn );
				reader = cmd.ExecuteReader();

				listAuthors.BeginUpdate();
				while( reader.Read() ) 
				{
					listAuthors.Items.Add( reader.GetString( 0 ) );
				}
				listAuthors.EndUpdate();
			} 
			catch( AseException ex ) 
			{
				MessageBox.Show( ex.Message );
			} 
			finally 
			{
				if (reader != null && !reader.IsClosed)
					reader.Close();
				if (cmd != null)
					cmd.Dispose();
				if (conn != null && conn.State != ConnectionState.Closed)
					conn.Close();
			}

		}

		private void listAuthors_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
