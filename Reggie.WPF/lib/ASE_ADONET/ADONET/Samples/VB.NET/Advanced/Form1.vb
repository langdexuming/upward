Imports Sybase.Data.AseClient

Public Class Form1
    Inherits System.Windows.Forms.Form
    Private _conn As AseConnection = Nothing
    Private _strmWriter As StreamWriter = Nothing
    Private indentLevel As Integer = 0
    ' SQL Text to drop Stored Procedure
    Private Const strDropProc As String = "drop procedure sp_hello"
    ' SQL Text to create the test Stored Procedure
    Private Const strCreateProc As String = _
        "create procedure " + _
        "sp_hello(@inParam varchar(32), @inoutParam varchar(64) output, @outParam varchar(64) output) as " + _
        " begin " + _
        " declare @v_go varchar(64) " + _
        " select @v_go = 'Go ' + @inoutParam " + _
        " select @inoutParam = @v_go " + _
        " select @outParam = 'Hello ' + @inParam " + _
        " return 101 " + _
        " end"


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        ' set default hostname property
        Try
            textBoxHost.Text = System.Net.Dns.GetHostName()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents textBoxPass As System.Windows.Forms.TextBox
    Friend WithEvents label_pass As System.Windows.Forms.Label
    Friend WithEvents textBoxUser As System.Windows.Forms.TextBox
    Friend WithEvents label_user As System.Windows.Forms.Label
    Friend WithEvents textBoxPort As System.Windows.Forms.TextBox
    Friend WithEvents label_port As System.Windows.Forms.Label
    Friend WithEvents textBoxHost As System.Windows.Forms.TextBox
    Friend WithEvents label_host As System.Windows.Forms.Label
    Friend WithEvents textBoxInput As System.Windows.Forms.TextBox
    Friend WithEvents textBoxReturn As System.Windows.Forms.TextBox
    Friend WithEvents textBoxOutput As System.Windows.Forms.TextBox
    Friend WithEvents textBoxInOut As System.Windows.Forms.TextBox
    Friend WithEvents textBoxSP As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents buttonConnect As System.Windows.Forms.Button
    Friend WithEvents checkTrace As System.Windows.Forms.CheckBox
    Friend WithEvents checkNamedParams As System.Windows.Forms.CheckBox
    Friend WithEvents buttonExecute As System.Windows.Forms.Button
    Friend WithEvents buttonClose As System.Windows.Forms.Button
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents buttonReset As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.label6 = New System.Windows.Forms.Label
        Me.textBoxPass = New System.Windows.Forms.TextBox
        Me.label_pass = New System.Windows.Forms.Label
        Me.textBoxUser = New System.Windows.Forms.TextBox
        Me.label_user = New System.Windows.Forms.Label
        Me.textBoxPort = New System.Windows.Forms.TextBox
        Me.label_port = New System.Windows.Forms.Label
        Me.textBoxHost = New System.Windows.Forms.TextBox
        Me.label_host = New System.Windows.Forms.Label
        Me.textBoxInput = New System.Windows.Forms.TextBox
        Me.textBoxReturn = New System.Windows.Forms.TextBox
        Me.textBoxOutput = New System.Windows.Forms.TextBox
        Me.textBoxInOut = New System.Windows.Forms.TextBox
        Me.textBoxSP = New System.Windows.Forms.TextBox
        Me.label1 = New System.Windows.Forms.Label
        Me.buttonConnect = New System.Windows.Forms.Button
        Me.checkTrace = New System.Windows.Forms.CheckBox
        Me.checkNamedParams = New System.Windows.Forms.CheckBox
        Me.buttonExecute = New System.Windows.Forms.Button
        Me.buttonClose = New System.Windows.Forms.Button
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.buttonReset = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'label6
        '
        Me.label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(16, 16)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(360, 32)
        Me.label6.TabIndex = 44
        Me.label6.Text = "Please specify connection properties for an Adaptive Server with pubs2 database"
        '
        'textBoxPass
        '
        Me.textBoxPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxPass.Location = New System.Drawing.Point(208, 88)
        Me.textBoxPass.Name = "textBoxPass"
        Me.textBoxPass.Size = New System.Drawing.Size(88, 20)
        Me.textBoxPass.TabIndex = 43
        Me.textBoxPass.Text = ""
        '
        'label_pass
        '
        Me.label_pass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_pass.Location = New System.Drawing.Point(160, 88)
        Me.label_pass.Name = "label_pass"
        Me.label_pass.Size = New System.Drawing.Size(40, 23)
        Me.label_pass.TabIndex = 42
        Me.label_pass.Text = "Pass:"
        '
        'textBoxUser
        '
        Me.textBoxUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxUser.Location = New System.Drawing.Point(64, 88)
        Me.textBoxUser.Name = "textBoxUser"
        Me.textBoxUser.Size = New System.Drawing.Size(88, 20)
        Me.textBoxUser.TabIndex = 41
        Me.textBoxUser.Text = "sa"
        '
        'label_user
        '
        Me.label_user.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_user.Location = New System.Drawing.Point(16, 88)
        Me.label_user.Name = "label_user"
        Me.label_user.Size = New System.Drawing.Size(40, 23)
        Me.label_user.TabIndex = 40
        Me.label_user.Text = "User:"
        '
        'textBoxPort
        '
        Me.textBoxPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxPort.Location = New System.Drawing.Point(208, 56)
        Me.textBoxPort.Name = "textBoxPort"
        Me.textBoxPort.Size = New System.Drawing.Size(88, 20)
        Me.textBoxPort.TabIndex = 39
        Me.textBoxPort.Text = "5000"
        '
        'label_port
        '
        Me.label_port.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_port.Location = New System.Drawing.Point(160, 56)
        Me.label_port.Name = "label_port"
        Me.label_port.Size = New System.Drawing.Size(40, 23)
        Me.label_port.TabIndex = 38
        Me.label_port.Text = "Port:"
        '
        'textBoxHost
        '
        Me.textBoxHost.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxHost.Location = New System.Drawing.Point(64, 56)
        Me.textBoxHost.Name = "textBoxHost"
        Me.textBoxHost.Size = New System.Drawing.Size(88, 20)
        Me.textBoxHost.TabIndex = 37
        Me.textBoxHost.Text = ""
        '
        'label_host
        '
        Me.label_host.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_host.Location = New System.Drawing.Point(16, 56)
        Me.label_host.Name = "label_host"
        Me.label_host.Size = New System.Drawing.Size(40, 23)
        Me.label_host.TabIndex = 36
        Me.label_host.Text = "Host:"
        '
        'textBoxInput
        '
        Me.textBoxInput.Location = New System.Drawing.Point(120, 224)
        Me.textBoxInput.Name = "textBoxInput"
        Me.textBoxInput.Size = New System.Drawing.Size(200, 20)
        Me.textBoxInput.TabIndex = 33
        Me.textBoxInput.Text = "mango"
        '
        'textBoxReturn
        '
        Me.textBoxReturn.Location = New System.Drawing.Point(120, 296)
        Me.textBoxReturn.Name = "textBoxReturn"
        Me.textBoxReturn.Size = New System.Drawing.Size(200, 20)
        Me.textBoxReturn.TabIndex = 34
        Me.textBoxReturn.Text = ""
        '
        'textBoxOutput
        '
        Me.textBoxOutput.Location = New System.Drawing.Point(120, 272)
        Me.textBoxOutput.Name = "textBoxOutput"
        Me.textBoxOutput.Size = New System.Drawing.Size(200, 20)
        Me.textBoxOutput.TabIndex = 35
        Me.textBoxOutput.Text = ""
        '
        'textBoxInOut
        '
        Me.textBoxInOut.Location = New System.Drawing.Point(144, 248)
        Me.textBoxInOut.Name = "textBoxInOut"
        Me.textBoxInOut.Size = New System.Drawing.Size(176, 20)
        Me.textBoxInOut.TabIndex = 32
        Me.textBoxInOut.Text = "Sybase"
        '
        'textBoxSP
        '
        Me.textBoxSP.Enabled = False
        Me.textBoxSP.Location = New System.Drawing.Point(120, 200)
        Me.textBoxSP.Name = "textBoxSP"
        Me.textBoxSP.Size = New System.Drawing.Size(200, 20)
        Me.textBoxSP.TabIndex = 31
        Me.textBoxSP.Text = "sp_hello"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(24, 224)
        Me.label1.Name = "label1"
        Me.label1.TabIndex = 27
        Me.label1.Text = "Input Parameter:"
        '
        'buttonConnect
        '
        Me.buttonConnect.Location = New System.Drawing.Point(312, 48)
        Me.buttonConnect.Name = "buttonConnect"
        Me.buttonConnect.TabIndex = 24
        Me.buttonConnect.Text = "Connect"
        '
        'checkTrace
        '
        Me.checkTrace.Checked = True
        Me.checkTrace.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkTrace.Location = New System.Drawing.Point(16, 128)
        Me.checkTrace.Name = "checkTrace"
        Me.checkTrace.TabIndex = 21
        Me.checkTrace.Text = "Trace"
        '
        'checkNamedParams
        '
        Me.checkNamedParams.Checked = True
        Me.checkNamedParams.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkNamedParams.Location = New System.Drawing.Point(16, 152)
        Me.checkNamedParams.Name = "checkNamedParams"
        Me.checkNamedParams.Size = New System.Drawing.Size(144, 24)
        Me.checkNamedParams.TabIndex = 20
        Me.checkNamedParams.Text = "Use Named Parameters"
        '
        'buttonExecute
        '
        Me.buttonExecute.Enabled = False
        Me.buttonExecute.Location = New System.Drawing.Point(312, 80)
        Me.buttonExecute.Name = "buttonExecute"
        Me.buttonExecute.TabIndex = 23
        Me.buttonExecute.Text = "Execute"
        '
        'buttonClose
        '
        Me.buttonClose.Location = New System.Drawing.Point(312, 144)
        Me.buttonClose.Name = "buttonClose"
        Me.buttonClose.TabIndex = 22
        Me.buttonClose.Text = "Close"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(24, 200)
        Me.label2.Name = "label2"
        Me.label2.TabIndex = 28
        Me.label2.Text = "Stored Proc Name:"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(24, 248)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(128, 23)
        Me.label3.TabIndex = 29
        Me.label3.Text = "InputOutPut Parameter:"
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(24, 272)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(136, 23)
        Me.label4.TabIndex = 30
        Me.label4.Text = "OutPut Parameter:"
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(24, 296)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(136, 23)
        Me.label5.TabIndex = 26
        Me.label5.Text = "Return Value:"
        '
        'buttonReset
        '
        Me.buttonReset.Location = New System.Drawing.Point(312, 112)
        Me.buttonReset.Name = "buttonReset"
        Me.buttonReset.TabIndex = 25
        Me.buttonReset.Text = "Reset"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(408, 333)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.textBoxPass)
        Me.Controls.Add(Me.label_pass)
        Me.Controls.Add(Me.textBoxUser)
        Me.Controls.Add(Me.label_user)
        Me.Controls.Add(Me.textBoxPort)
        Me.Controls.Add(Me.label_port)
        Me.Controls.Add(Me.textBoxHost)
        Me.Controls.Add(Me.label_host)
        Me.Controls.Add(Me.textBoxInput)
        Me.Controls.Add(Me.textBoxReturn)
        Me.Controls.Add(Me.textBoxOutput)
        Me.Controls.Add(Me.textBoxInOut)
        Me.Controls.Add(Me.textBoxSP)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.buttonConnect)
        Me.Controls.Add(Me.checkTrace)
        Me.Controls.Add(Me.checkNamedParams)
        Me.Controls.Add(Me.buttonExecute)
        Me.Controls.Add(Me.buttonClose)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.buttonReset)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub buttonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonConnect.Click
        ' obtain the connection information specified
        Dim host, port, user, pass As String
        host = textBoxHost.Text
        port = textBoxPort.Text
        user = textBoxUser.Text
        pass = textBoxPass.Text

        ' build a connection string and get a connection object
        Dim connStr As String = "Data Source='" + host + "';Port='" + port + "';UID='" + user + "';PWD='" + pass + "';Database='pubs2';"
        _conn = New AseConnection(connStr)

        ' if tracing has been enabled, setup tracing
        If (checkTrace.Checked) Then
            Try
                ' Open Trace file for write
                _strmWriter = File.AppendText("trace.log")
                _strmWriter.WriteLine("************ Sybase ASE ADO.NET Data Provider Trace start ****************")
                indentLevel = 0
            Catch exc As Exception
                MsgBox("File could not be opened or written to." + vbCrLf + _
                    "Please verify that the filename is correct, " + _
                    "and that you have read permissions for the desired " + _
                    "directory." + vbCrLf + vbCrLf + "Exception: " + exc.Message)
            End Try

            ' register the trace event handlers
            AddHandler _conn.TraceEnter, AddressOf TraceEnter
            AddHandler _conn.TraceExit, AddressOf TraceExit

        End If

        ' open the connection and setup stored procs
        Try
            _conn.Open()
            setup()

            ' enable/disable command buttons
            buttonExecute.Enabled = True
            buttonConnect.Enabled = False
        Catch ex As AseException
            MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Failed to connect")
        End Try

    End Sub
    Private Sub setup()
        ' create the stored procedure that will be used for this sample
        Try
            Dim dropCmd As New AseCommand(strDropProc, _conn)
            dropCmd.ExecuteNonQuery()
        Catch ex As AseException
            ' If the proc does not exist we expect this exception, so ignore it..
        End Try
        Try
            Dim createCmd As New AseCommand(strCreateProc, _conn)
            createCmd.ExecuteNonQuery()
        Catch ex As AseException
            MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Failed to create Stored Procedure")
        End Try

    End Sub

    Private Sub buttonExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonExecute.Click
        ' check for valid connection before proceeding....
        If (_conn Is Nothing) OrElse (_conn.State <> ConnectionState.Open) Then
            MessageBox.Show("Not Connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Used Named parameters or Parameter markers based on user's choice
        If (checkNamedParams.Checked) Then
            ExecuteCommandUsingNamedParams()
        Else
            ExecuteCommandUsingParameterMarkers()
        End If
        ' disable Execute button
        buttonExecute.Enabled = False
    End Sub

    Private Sub ExecuteCommandUsingNamedParams()
        ' In this case you can use ADO.NET specific syntax to execute
        ' a stored procedure. All you need to do is set the name of
        ' the stored procedure as the CommandText and set CommandType to
        ' Stored Procedure
        Dim cmd As New AseCommand("sp_hello", _conn)
        ' set command type to stored procedure
        cmd.CommandType = CommandType.StoredProcedure

        ' create the input parameter object and bind it to the command
        Dim inParam As New AseParameter("@inParam", AseDbType.VarChar, 32)
        inParam.Direction = ParameterDirection.Input
        inParam.Value = textBoxInput.Text
        cmd.Parameters.Add(inParam)

        ' create the inout parameter object and bind it to the command
        Dim inoutParam As New AseParameter("@inoutParam", AseDbType.VarChar, 64)
        inoutParam.Direction = ParameterDirection.InputOutput
        inoutParam.Value = textBoxInOut.Text
        cmd.Parameters.Add(inoutParam)

        ' create the output parameter object and bind it to the command
        Dim outParam As New AseParameter("@outParam", AseDbType.VarChar, 64)
        outParam.Direction = ParameterDirection.Output
        cmd.Parameters.Add(outParam)

        ' create the return value object and bind it to the command
        Dim retValue As New AseParameter("@retValue", AseDbType.Integer)
        retValue.Direction = ParameterDirection.ReturnValue
        cmd.Parameters.Add(retValue)

        ' execute the stored procedure
        Try
            cmd.ExecuteNonQuery()

            ' get the output, inout and return values and display them
            textBoxInOut.Text = cmd.Parameters("@inoutParam").Value
            textBoxInOut.ForeColor = Color.Blue

            textBoxOutput.Text = cmd.Parameters("@outParam").Value
            textBoxOutput.ForeColor = Color.Blue

            textBoxReturn.Text = cmd.Parameters("@retValue").Value
            textBoxReturn.ForeColor = Color.Blue
        Catch ex As AseException
            MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Execute Query failed.")
        Finally
            ' dispose the command object
            cmd.Dispose()
        End Try



    End Sub

    Private Sub ExecuteCommandUsingParameterMarkers()
        ' In this case you need to use the SQL 92 syntax to execute
        ' a stored procedure. You need to set NamedParameter to false
        ' and use '?' as parameter markers.
        ' This syntax is similar to ODBC and JDBC
        Dim cmd As New AseCommand("{ ? = call sp_hello(?, ?, ?)}", _conn)
        ' need to notify Named Parameters are not being used (which is the default)
        cmd.NamedParameters = False

        ' create the return value object and bind it to the command
        Dim retValue As New AseParameter(0, AseDbType.Integer)
        retValue.Direction = ParameterDirection.ReturnValue
        cmd.Parameters.Add(retValue)

        ' create the input parameter object and bind it to the command
        Dim inParam As New AseParameter(1, AseDbType.VarChar, 32)
        inParam.Direction = ParameterDirection.Input
        inParam.Value = textBoxInput.Text
        cmd.Parameters.Add(inParam)

        ' create the inout parameter object and bind it to the command
        Dim inoutParam As New AseParameter(2, AseDbType.VarChar, 64)
        inoutParam.Direction = ParameterDirection.InputOutput
        inoutParam.Value = textBoxInOut.Text
        cmd.Parameters.Add(inoutParam)

        ' create the output parameter object and bind it to the command
        Dim outParam As New AseParameter(3, AseDbType.VarChar, 64)
        outParam.Direction = ParameterDirection.Output
        cmd.Parameters.Add(outParam)

        ' execute the stored procedure
        Try
            cmd.ExecuteNonQuery()

            ' get the output, inout and return values and display them
            textBoxReturn.Text = cmd.Parameters(0).Value
            textBoxReturn.ForeColor = Color.Blue

            textBoxInOut.Text = cmd.Parameters(2).Value
            textBoxInOut.ForeColor = Color.Blue

            textBoxOutput.Text = cmd.Parameters(3).Value
            textBoxOutput.ForeColor = Color.Blue
        Catch ex As AseException
            MessageBox.Show(ex.Source + " : " + ex.Message + " (" + ex.ToString() + ")", "Execute Query Failed")
        Finally
            ' dispose the command object
            cmd.Dispose()
        End Try



    End Sub

    Private Sub buttonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonClose.Click
        ' close all connections/files and return
        CloseAll()
        Close()
    End Sub

    Private Sub CloseAll()
        ' close the database connection and optional trace file
        If Not (_conn Is Nothing) AndAlso (_conn.State <> ConnectionState.Closed) Then
            _conn.Close()
            _conn = Nothing
        End If

        If Not (_strmWriter Is Nothing) Then
            _strmWriter.WriteLine("************ Sybase ASE ADO.NET Data Provider Trace end ****************")
            _strmWriter.Flush()
            _strmWriter.Close()
            _strmWriter = Nothing
        End If

    End Sub

    Private Sub buttonReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonReset.Click
        ' close opended connections/files and reset dialog
        CloseAll()

        buttonConnect.Enabled = True
        buttonExecute.Enabled = False

        textBoxInOut.Text = "Sybase"
        textBoxInOut.ForeColor = Color.Black

        textBoxOutput.Text = ""
        textBoxOutput.ForeColor = Color.Black

        textBoxReturn.Text = ""
        textBoxReturn.ForeColor = Color.Black

    End Sub

    Private Sub TraceEnter(ByVal connection As AseConnection, ByVal source As System.Object, ByVal method As String, ByVal parameters As System.Object())
        ' increment the indent in the output to show call stack
        indentLevel = indentLevel + 1
        Dim str As New StringBuilder(255)

        ' insert blanks to indent output
        Dim i As Integer
        For i = 1 To indentLevel
            str.Append("  ")
        Next

        ' write trace output
        str.Append("TraceEnter---")
        str.Append(source.GetType().Name + "(" + source.GetHashCode().ToString() + ")." + method + "(")
        If Not (parameters Is Nothing) Then
            If (parameters.Length > 0) Then
                ' print all but the last parameter - as we don't want a comma at the end of last one
                For i = 0 To (parameters.Length - 2)
                    str.Append(parameters(i).ToString() + ",")
                Next
                ' print the last param
                str.Append(parameters(parameters.Length - 1).ToString())
            End If
        End If
        str.Append(")")

        ' special case for Connection open event - write the ConnectString
        If (source.GetType().Name = "AseConnection" And method = "Open") Then
            str.Append(" DriverVersion = '" + AseConnection.DriverVersion + "'")
            str.Append(" ConnectionString = '" + connection.ConnectionString + "'")
        End If

        ' finally write the string built to the trace file
        _strmWriter.WriteLine(str.ToString())

    End Sub
    Private Sub TraceExit(ByVal connection As AseConnection, ByVal source As System.Object, ByVal method As String, ByVal returnValue As System.Object)

        Dim str As New StringBuilder(255)
        Dim i As Integer

        ' Insert spaces to indent output
        For i = 1 To indentLevel
            str.Append("  ")
        Next

        ' Buld string to be printed
        str.Append("TraceExit---")
        str.Append(source.GetType().Name + "(" + source.GetHashCode().ToString() + ")." + method + "() returned - ")
        If Not (returnValue Is Nothing) Then
            str.Append(returnValue.ToString())
        Else
            str.Append("null / void")
        End If

        ' Write string to trace file
        _strmWriter.WriteLine(str.ToString())

        ' reduce indent level
        indentLevel = indentLevel - 1

    End Sub
End Class
