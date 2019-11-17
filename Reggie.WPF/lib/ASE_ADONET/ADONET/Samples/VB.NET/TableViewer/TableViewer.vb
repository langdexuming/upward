Imports Sybase.Data.AseClient

Public Class TableViewer
    Inherits System.Windows.Forms.Form
    Private _conn As AseConnection

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        'populate the default hostname
        Try
            txtConnectString.Text = "Data Source='" + System.Net.Dns.GetHostName() + "';Port='5000';UID='sa';PWD='';Database='pubs2';"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        ' close any open connections to the database
        If (Not _conn Is Nothing AndAlso _conn.State <> ConnectionState.Closed) Then
            _conn.Close()
        End If

        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents comboBoxTables As System.Windows.Forms.ComboBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents btlClose As System.Windows.Forms.Button
    Friend WithEvents dgResults As System.Windows.Forms.DataGrid
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents txtSQLStatement As System.Windows.Forms.TextBox
    Friend WithEvents txtConnectString As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.comboBoxTables = New System.Windows.Forms.ComboBox
        Me.label3 = New System.Windows.Forms.Label
        Me.btlClose = New System.Windows.Forms.Button
        Me.dgResults = New System.Windows.Forms.DataGrid
        Me.btnExecute = New System.Windows.Forms.Button
        Me.btnConnect = New System.Windows.Forms.Button
        Me.txtSQLStatement = New System.Windows.Forms.TextBox
        Me.txtConnectString = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        CType(Me.dgResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'comboBoxTables
        '
        Me.comboBoxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxTables.ItemHeight = 13
        Me.comboBoxTables.Location = New System.Drawing.Point(336, 32)
        Me.comboBoxTables.Name = "comboBoxTables"
        Me.comboBoxTables.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxTables.Sorted = True
        Me.comboBoxTables.TabIndex = 16
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(336, 8)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(40, 16)
        Me.label3.TabIndex = 11
        Me.label3.Text = "Tables:"
        '
        'btlClose
        '
        Me.btlClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btlClose.Location = New System.Drawing.Point(464, 64)
        Me.btlClose.Name = "btlClose"
        Me.btlClose.TabIndex = 19
        Me.btlClose.Text = "C&lose"
        '
        'dgResults
        '
        Me.dgResults.CaptionText = "Results"
        Me.dgResults.DataMember = ""
        Me.dgResults.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgResults.Location = New System.Drawing.Point(8, 184)
        Me.dgResults.Name = "dgResults"
        Me.dgResults.ReadOnly = True
        Me.dgResults.Size = New System.Drawing.Size(528, 320)
        Me.dgResults.TabIndex = 18
        '
        'btnExecute
        '
        Me.btnExecute.Enabled = False
        Me.btnExecute.Location = New System.Drawing.Point(464, 32)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.TabIndex = 14
        Me.btnExecute.Text = "&Execute"
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(464, 8)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.TabIndex = 13
        Me.btnConnect.Text = "&Connect"
        '
        'txtSQLStatement
        '
        Me.txtSQLStatement.Location = New System.Drawing.Point(8, 96)
        Me.txtSQLStatement.Multiline = True
        Me.txtSQLStatement.Name = "txtSQLStatement"
        Me.txtSQLStatement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSQLStatement.Size = New System.Drawing.Size(528, 80)
        Me.txtSQLStatement.TabIndex = 17
        Me.txtSQLStatement.Text = "SELECT * FROM authors"
        '
        'txtConnectString
        '
        Me.txtConnectString.Location = New System.Drawing.Point(8, 32)
        Me.txtConnectString.Name = "txtConnectString"
        Me.txtConnectString.Size = New System.Drawing.Size(312, 20)
        Me.txtConnectString.TabIndex = 15
        Me.txtConnectString.Text = "Data Source='';Port='5000';UID='sa';PWD='';Database='pubs2';"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(8, 72)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(88, 16)
        Me.label2.TabIndex = 10
        Me.label2.Text = "SQL Statement:"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 8)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(168, 16)
        Me.label1.TabIndex = 12
        Me.label1.Text = "Connection String:"
        '
        'TableViewer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(544, 517)
        Me.Controls.Add(Me.comboBoxTables)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.btlClose)
        Me.Controls.Add(Me.dgResults)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.txtSQLStatement)
        Me.Controls.Add(Me.txtConnectString)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Name = "TableViewer"
        Me.Text = "TableViewer"
        CType(Me.dgResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        Dim cmd As AseCommand = Nothing
        Dim dr As AseDataReader = Nothing

        Try
            'connect to the database
            _conn = New AseConnection(txtConnectString.Text)
            _conn.Open()
            btnExecute.Enabled = True

            'get the list of user tables
            cmd = New AseCommand("SELECT name FROM sysobjects where type = 'U'", _conn)
            dr = cmd.ExecuteReader()

            ' populate drop down liat
            comboBoxTables.Items.Clear()
            While dr.Read()
                comboBoxTables.Items.Add(dr.GetString(0))
            End While
            comboBoxTables.SelectedIndex = 0

        Catch ex As AseException
            MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Failed to connect")
        Finally
            'close opened objects
            If Not (dr Is Nothing) AndAlso Not (dr.IsClosed) Then
                dr.Close()
            End If
            If Not (cmd Is Nothing) Then
                cmd.Dispose()
            End If
        End Try

    End Sub

    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click

        ' check for valid connection
        If (_conn Is Nothing) OrElse (_conn.State <> ConnectionState.Open) Then
            MessageBox.Show("Connect to a database first.", "Not connected")
            Return
        End If

        ' has a command been entered?
        If (txtSQLStatement.Text.Trim().Length < 1) Then
            MessageBox.Show("Please enter the command text.", "Empty command text")
            txtSQLStatement.SelectAll()
            txtSQLStatement.Focus()
            Return
        End If

        'populate the DataGrid using DataSet filled from a DataAdapter object
        Dim cmd As AseCommand = Nothing
        Dim da As AseDataAdapter = Nothing
        Dim ds As DataSet = Nothing

        Try
            dgResults.DataSource = Nothing

            cmd = New AseCommand(txtSQLStatement.Text.Trim(), _conn)
            da = New AseDataAdapter(cmd)
            ds = New DataSet

            da.Fill(ds, "Table")
            dgResults.DataSource = ds.Tables("Table")

            da.Dispose()
            cmd.Dispose()
        Catch ex As AseException
            MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Failed to execute SQL statement")
        Finally
            ' close the various objects opened...
            If Not (ds Is Nothing) Then
                ds.Dispose()
            End If
            If Not (da Is Nothing) Then
                da.Dispose()
            End If
            If Not (cmd Is Nothing) Then
                cmd.Dispose()
            End If
        End Try

        ' set the focus back on the command 
        txtSQLStatement.SelectAll()
        txtSQLStatement.Focus()

    End Sub

    Private Sub btlClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btlClose.Click
        ' close the app
        Close()
    End Sub
End Class
