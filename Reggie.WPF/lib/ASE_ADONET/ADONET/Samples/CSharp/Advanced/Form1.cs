using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Sybase.Data.AseClient;

namespace advanced
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox checkTrace;
		private System.Windows.Forms.CheckBox checkNamedParams;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonExecute;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxSP;
		private System.Windows.Forms.TextBox textBoxInput;
		private System.Windows.Forms.TextBox textBoxInOut;
		private System.Windows.Forms.TextBox textBoxOutput;
		private System.Windows.Forms.TextBox textBoxReturn;
		private AseConnection _conn = null;
		
		//For tracing
		FileStream _fs = null;
		StreamWriter _strmWriter = null;
		int indentLevel = 0;
		
		// SQL text to drop stored procedure
		private static string strDropProc = "drop procedure sp_hello";
		// SQL text to create the stored procedure used for this example
		private static string strCreateProc = 
			"create procedure sp_hello(@inParam varchar(32), @inoutParam varchar(64) output, @outParam varchar(64) output) as " +
			" begin " + 
			" declare @v_go varchar(64) " +
			" select @v_go = \"Go \" + @inoutParam " +
			" select @inoutParam = @v_go " +
			" select @outParam = \"Hello \" + @inParam " +
			" return 101 " +
			" end";
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.TextBox textBoxPass;
		private System.Windows.Forms.Label label_pass;
		private System.Windows.Forms.TextBox textBoxUser;
		private System.Windows.Forms.Label label_user;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Label label_port;
		private System.Windows.Forms.TextBox textBoxHost;
		private System.Windows.Forms.Label label_host;
		private System.Windows.Forms.Label label6;

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
			this.checkTrace = new System.Windows.Forms.CheckBox();
			this.checkNamedParams = new System.Windows.Forms.CheckBox();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonExecute = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxSP = new System.Windows.Forms.TextBox();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.textBoxInOut = new System.Windows.Forms.TextBox();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.textBoxReturn = new System.Windows.Forms.TextBox();
			this.buttonReset = new System.Windows.Forms.Button();
			this.textBoxPass = new System.Windows.Forms.TextBox();
			this.label_pass = new System.Windows.Forms.Label();
			this.textBoxUser = new System.Windows.Forms.TextBox();
			this.label_user = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.label_port = new System.Windows.Forms.Label();
			this.textBoxHost = new System.Windows.Forms.TextBox();
			this.label_host = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// checkTrace
			// 
			this.checkTrace.Checked = true;
			this.checkTrace.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkTrace.Location = new System.Drawing.Point(8, 120);
			this.checkTrace.Name = "checkTrace";
			this.checkTrace.TabIndex = 0;
			this.checkTrace.Text = "Trace";
			// 
			// checkNamedParams
			// 
			this.checkNamedParams.Checked = true;
			this.checkNamedParams.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNamedParams.Location = new System.Drawing.Point(8, 144);
			this.checkNamedParams.Name = "checkNamedParams";
			this.checkNamedParams.Size = new System.Drawing.Size(144, 24);
			this.checkNamedParams.TabIndex = 0;
			this.checkNamedParams.Text = "Use Named Parameters";
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(304, 40);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.TabIndex = 1;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// buttonExecute
			// 
			this.buttonExecute.Enabled = false;
			this.buttonExecute.Location = new System.Drawing.Point(304, 72);
			this.buttonExecute.Name = "buttonExecute";
			this.buttonExecute.TabIndex = 1;
			this.buttonExecute.Text = "Execute";
			this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(304, 136);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 216);
			this.label1.Name = "label1";
			this.label1.TabIndex = 2;
			this.label1.Text = "Input Parameter:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 192);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Stored Proc Name:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 240);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "InputOutPut Parameter:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 264);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(136, 23);
			this.label4.TabIndex = 2;
			this.label4.Text = "OutPut Parameter:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 288);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(136, 23);
			this.label5.TabIndex = 2;
			this.label5.Text = "Return Value:";
			// 
			// textBoxSP
			// 
			this.textBoxSP.Enabled = false;
			this.textBoxSP.Location = new System.Drawing.Point(112, 192);
			this.textBoxSP.Name = "textBoxSP";
			this.textBoxSP.Size = new System.Drawing.Size(200, 20);
			this.textBoxSP.TabIndex = 3;
			this.textBoxSP.Text = "sp_hello";
			// 
			// textBoxInput
			// 
			this.textBoxInput.Location = new System.Drawing.Point(112, 216);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.Size = new System.Drawing.Size(200, 20);
			this.textBoxInput.TabIndex = 3;
			this.textBoxInput.Text = "mango";
			// 
			// textBoxInOut
			// 
			this.textBoxInOut.Location = new System.Drawing.Point(136, 240);
			this.textBoxInOut.Name = "textBoxInOut";
			this.textBoxInOut.Size = new System.Drawing.Size(176, 20);
			this.textBoxInOut.TabIndex = 3;
			this.textBoxInOut.Text = "Sybase";
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.Location = new System.Drawing.Point(112, 264);
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.Size = new System.Drawing.Size(200, 20);
			this.textBoxOutput.TabIndex = 3;
			this.textBoxOutput.Text = "";
			// 
			// textBoxReturn
			// 
			this.textBoxReturn.Location = new System.Drawing.Point(112, 288);
			this.textBoxReturn.Name = "textBoxReturn";
			this.textBoxReturn.Size = new System.Drawing.Size(200, 20);
			this.textBoxReturn.TabIndex = 3;
			this.textBoxReturn.Text = "";
			// 
			// buttonReset
			// 
			this.buttonReset.Location = new System.Drawing.Point(304, 104);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.TabIndex = 1;
			this.buttonReset.Text = "Reset";
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// textBoxPass
			// 
			this.textBoxPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxPass.Location = new System.Drawing.Point(200, 80);
			this.textBoxPass.Name = "textBoxPass";
			this.textBoxPass.Size = new System.Drawing.Size(88, 20);
			this.textBoxPass.TabIndex = 18;
			this.textBoxPass.Text = "";
			// 
			// label_pass
			// 
			this.label_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_pass.Location = new System.Drawing.Point(152, 80);
			this.label_pass.Name = "label_pass";
			this.label_pass.Size = new System.Drawing.Size(40, 23);
			this.label_pass.TabIndex = 17;
			this.label_pass.Text = "Pass:";
			// 
			// textBoxUser
			// 
			this.textBoxUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxUser.Location = new System.Drawing.Point(56, 80);
			this.textBoxUser.Name = "textBoxUser";
			this.textBoxUser.Size = new System.Drawing.Size(88, 20);
			this.textBoxUser.TabIndex = 16;
			this.textBoxUser.Text = "sa";
			// 
			// label_user
			// 
			this.label_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_user.Location = new System.Drawing.Point(8, 80);
			this.label_user.Name = "label_user";
			this.label_user.Size = new System.Drawing.Size(40, 23);
			this.label_user.TabIndex = 15;
			this.label_user.Text = "User:";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxPort.Location = new System.Drawing.Point(200, 48);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(88, 20);
			this.textBoxPort.TabIndex = 14;
			this.textBoxPort.Text = "5000";
			// 
			// label_port
			// 
			this.label_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_port.Location = new System.Drawing.Point(152, 48);
			this.label_port.Name = "label_port";
			this.label_port.Size = new System.Drawing.Size(40, 23);
			this.label_port.TabIndex = 13;
			this.label_port.Text = "Port:";
			// 
			// textBoxHost
			// 
			this.textBoxHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBoxHost.Location = new System.Drawing.Point(56, 48);
			this.textBoxHost.Name = "textBoxHost";
			this.textBoxHost.Size = new System.Drawing.Size(88, 20);
			this.textBoxHost.TabIndex = 12;
			this.textBoxHost.Text = "";
			// 
			// label_host
			// 
			this.label_host.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_host.Location = new System.Drawing.Point(8, 48);
			this.label_host.Name = "label_host";
			this.label_host.Size = new System.Drawing.Size(40, 23);
			this.label_host.TabIndex = 11;
			this.label_host.Text = "Host:";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(360, 32);
			this.label6.TabIndex = 19;
			this.label6.Text = "Please specify connection properties for an Adaptive Server with pubs2 database";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 325);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxPass);
			this.Controls.Add(this.label_pass);
			this.Controls.Add(this.textBoxUser);
			this.Controls.Add(this.label_user);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.label_port);
			this.Controls.Add(this.textBoxHost);
			this.Controls.Add(this.label_host);
			this.Controls.Add(this.textBoxInput);
			this.Controls.Add(this.textBoxReturn);
			this.Controls.Add(this.textBoxOutput);
			this.Controls.Add(this.textBoxInOut);
			this.Controls.Add(this.textBoxSP);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.checkTrace);
			this.Controls.Add(this.checkNamedParams);
			this.Controls.Add(this.buttonExecute);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.buttonReset);
			this.Name = "Form1";
			this.Text = "Form1";
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

		private void buttonConnect_Click(object sender, System.EventArgs e)
		{
			// obtain the connection information specified
			String host = textBoxHost.Text;
			String port = textBoxPort.Text;
			String user = textBoxUser.Text;
			String pass = textBoxPass.Text;

			// build a connect string
			string connStr = "Data Source=\'" + host + "\';Port=\'" + port + "\';UID=" + user + ";PWD=\'" + pass + "\';Database=\'pubs2\';";

			_conn = new AseConnection( connStr );
			// if tracing has been enabled, setup tracing
			if(checkTrace.Checked)
			{
				// create the trace file
				_fs = new FileStream("trace.log", FileMode.Create, FileAccess.Write);
				_strmWriter = new StreamWriter(_fs);

				_strmWriter.WriteLine("************ Sybase ASE ADO.NET Data Provider Trace start ****************");
				indentLevel = 0;

				// register the trace event handlers
				_conn.TraceEnter += new TraceEnterEventHandler(TraceEnter);
				_conn.TraceExit += new TraceExitEventHandler(TraceExit);
			}

			try
			{
				_conn.Open();
				Setup();
				buttonExecute.Enabled = true;
				buttonConnect.Enabled = false;
			}
			catch (AseException ex)
			{
	            MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Failed to connect");
			}
		}

		private void Setup()
		{
			//setup
			try
			{
				AseCommand dropCmd = new AseCommand(strDropProc, _conn);
				dropCmd.ExecuteNonQuery();
			}
			catch(AseException)
			{
				//If the proc does not exist we expect this exception
			}
			
			AseCommand createCmd = new AseCommand(strCreateProc, _conn);
			createCmd.ExecuteNonQuery();
		}

		private void buttonExecute_Click(object sender, System.EventArgs e)
		{
			if(_conn == null || _conn.State != ConnectionState.Open)
			{
				MessageBox.Show ("Not Connected!", "Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if(checkNamedParams.Checked)
			{
				ExecuteCommandUsingNamedParams();
			}
			else
			{
				ExecuteCommandUsingParameterMarkers();
			}
			buttonExecute.Enabled = false;
		}

		private void ExecuteCommandUsingNamedParams()
		{
			//In this case you can use ADO.NET specific syntax to execute
			//a stored procedure. All you need to do is set the name of
			//the stored procedure as the CommandText and set CommandType to
			//Stored Procedure
			using(AseCommand cmd = new AseCommand("sp_hello", _conn))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				AseParameter inParam = new AseParameter("@inParam", AseDbType.VarChar, 32);
				inParam.Direction = ParameterDirection.Input;
				inParam.Value = textBoxInput.Text;
				cmd.Parameters.Add(inParam);

				AseParameter inoutParam = new AseParameter("@inoutParam", AseDbType.VarChar, 64);
				inoutParam.Direction = ParameterDirection.InputOutput;
				inoutParam.Value = textBoxInOut.Text;
				cmd.Parameters.Add(inoutParam);

				AseParameter outParam = new AseParameter("@outParam", AseDbType.VarChar, 64);
				outParam.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(outParam);

				AseParameter retValue = new AseParameter("@retValue", AseDbType.Integer);
				retValue.Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add(retValue);

				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (AseException ex)
				{
					MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Execute Stored Precedure failed.");
				}

				textBoxInOut.Text = (string) cmd.Parameters["@inoutParam"].Value;
				textBoxInOut.ForeColor = Color.Blue;

				textBoxOutput.Text= (string) cmd.Parameters["@outParam"].Value;
				textBoxOutput.ForeColor = Color.Blue;

				textBoxReturn.Text= ((int) cmd.Parameters["@retValue"].Value).ToString();
				textBoxReturn.ForeColor = Color.Blue;
			}
		}

		private void ExecuteCommandUsingParameterMarkers()
		{
			//In this case you need to use the SQL 92 syntax to execute
			//a stored procedure. You need to set NamedParameter to false
			//and use '?' as parameter markers.
			//This syntax is similar to ODBC and JDBC
			using(AseCommand cmd = new AseCommand("{ ? = call sp_hello(?, ?, ?)}", _conn))
			{
				cmd.NamedParameters = false;

				AseParameter retValue = new AseParameter(0, AseDbType.Integer);
				retValue.Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add(retValue);

				AseParameter inParam = new AseParameter(1, AseDbType.VarChar, 32);
				inParam.Direction = ParameterDirection.Input;
				inParam.Value = textBoxInput.Text;
				cmd.Parameters.Add(inParam);

				AseParameter inoutParam = new AseParameter(2, AseDbType.VarChar, 64);
				inoutParam.Direction = ParameterDirection.InputOutput;
				inoutParam.Value = textBoxInOut.Text;
				cmd.Parameters.Add(inoutParam);

				AseParameter outParam = new AseParameter(3, AseDbType.VarChar, 64);
				outParam.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(outParam);

				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (AseException ex)
				{
					MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Execute Stored Precedure failed.");
				}

				textBoxReturn.Text= ((int) cmd.Parameters[0].Value).ToString();
				textBoxReturn.ForeColor = Color.Blue;

				textBoxInOut.Text = (string) cmd.Parameters[2].Value;
				textBoxInOut.ForeColor = Color.Blue;

				textBoxOutput.Text= (string) cmd.Parameters[3].Value;
				textBoxOutput.ForeColor = Color.Blue;

			}
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			CloseAll();
			this.Close();
		}

		private void buttonReset_Click(object sender, System.EventArgs e)
		{
			CloseAll();

			buttonConnect.Enabled = true;
			buttonExecute.Enabled = false;

			textBoxInOut.Text = "Sybase";
			textBoxInOut.ForeColor = Color.Black;

			textBoxOutput.Text = "";
			textBoxOutput.ForeColor = Color.Black;

			textBoxReturn.Text= "";
			textBoxReturn.ForeColor = Color.Black;
		}

		private void CloseAll()
		{
			if(_conn != null && _conn.State != ConnectionState.Closed)
			{
				_conn.Close();
				_conn = null;
			}

			if(_strmWriter != null)
			{
				_strmWriter.WriteLine("************ Sybase ASE ADO.NET Data Provider Trace end ****************");
				_strmWriter.Flush();
				_strmWriter.Close();
				_strmWriter = null;
			}
			if(_fs != null)
			{
				_fs.Close();
				_fs = null;
			}
		}

		private void TraceEnter(AseConnection connection, object source, string method, object[] parameters)
		{
			indentLevel++;
			StringBuilder str = new StringBuilder(255);
			for(int i = 0; i < indentLevel; i++)
				str.Append("  ");
			str.Append("TraceEnter---");
			str.Append(source.GetType().Name + "(" + source.GetHashCode().ToString() + ")." + method + "(");
			if (parameters != null && parameters.Length > 0)
			{
				for (int i = 0; i < parameters.Length-1; i++)
				{
					str.Append(parameters[i].ToString() + ",");
				}
				str.Append(parameters[parameters.Length-1].ToString());
			}
			str.Append(")");

			if(source.GetType().Name == "AseConnection" && method == "Open")
			{
				str.Append(" DriverVersion = \"" + AseConnection.DriverVersion + "\"");
				str.Append(" ConnectionString = \"" + connection.ConnectionString + "\"");
			}

			_strmWriter.WriteLine(str.ToString());
		}
		private void TraceExit(AseConnection connection, object source, string method, object returnValue)
		{
			StringBuilder str = new StringBuilder(255);
			for(int i = 0; i < indentLevel; i++)
				str.Append("  ");
			str.Append("TraceExit---");
			str.Append(source.GetType().Name + "(" + source.GetHashCode().ToString() + ")." + method + "() returned - ");
			if (returnValue != null)
				str.Append(returnValue.ToString());
			else
				str.Append("null / void");

			_strmWriter.WriteLine(str.ToString());
			indentLevel--;
		}
	}
}
