Imports Sybase.Data.AseClient

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ' populate default hostname
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
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents textBoxPass As System.Windows.Forms.TextBox
    Friend WithEvents label_pass As System.Windows.Forms.Label
    Friend WithEvents textBoxUser As System.Windows.Forms.TextBox
    Friend WithEvents label_user As System.Windows.Forms.Label
    Friend WithEvents textBoxPort As System.Windows.Forms.TextBox
    Friend WithEvents label_port As System.Windows.Forms.Label
    Friend WithEvents textBoxHost As System.Windows.Forms.TextBox
    Friend WithEvents label_host As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents listAuthors As System.Windows.Forms.ListBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.label2 = New System.Windows.Forms.Label
        Me.textBoxPass = New System.Windows.Forms.TextBox
        Me.label_pass = New System.Windows.Forms.Label
        Me.textBoxUser = New System.Windows.Forms.TextBox
        Me.label_user = New System.Windows.Forms.Label
        Me.textBoxPort = New System.Windows.Forms.TextBox
        Me.label_port = New System.Windows.Forms.Label
        Me.textBoxHost = New System.Windows.Forms.TextBox
        Me.label_host = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.listAuthors = New System.Windows.Forms.ListBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(8, 8)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(288, 32)
        Me.label2.TabIndex = 35
        Me.label2.Text = "Please specify connection properties for an Adaptive Server with pubs2 database"
        '
        'textBoxPass
        '
        Me.textBoxPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxPass.Location = New System.Drawing.Point(200, 88)
        Me.textBoxPass.Name = "textBoxPass"
        Me.textBoxPass.Size = New System.Drawing.Size(88, 22)
        Me.textBoxPass.TabIndex = 34
        Me.textBoxPass.Text = ""
        '
        'label_pass
        '
        Me.label_pass.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_pass.Location = New System.Drawing.Point(152, 88)
        Me.label_pass.Name = "label_pass"
        Me.label_pass.Size = New System.Drawing.Size(40, 23)
        Me.label_pass.TabIndex = 33
        Me.label_pass.Text = "Pass:"
        '
        'textBoxUser
        '
        Me.textBoxUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxUser.Location = New System.Drawing.Point(56, 88)
        Me.textBoxUser.Name = "textBoxUser"
        Me.textBoxUser.Size = New System.Drawing.Size(88, 22)
        Me.textBoxUser.TabIndex = 32
        Me.textBoxUser.Text = "sa"
        '
        'label_user
        '
        Me.label_user.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_user.Location = New System.Drawing.Point(8, 88)
        Me.label_user.Name = "label_user"
        Me.label_user.Size = New System.Drawing.Size(40, 23)
        Me.label_user.TabIndex = 31
        Me.label_user.Text = "User:"
        '
        'textBoxPort
        '
        Me.textBoxPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxPort.Location = New System.Drawing.Point(200, 56)
        Me.textBoxPort.Name = "textBoxPort"
        Me.textBoxPort.Size = New System.Drawing.Size(88, 22)
        Me.textBoxPort.TabIndex = 30
        Me.textBoxPort.Text = "5000"
        '
        'label_port
        '
        Me.label_port.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_port.Location = New System.Drawing.Point(152, 56)
        Me.label_port.Name = "label_port"
        Me.label_port.Size = New System.Drawing.Size(40, 23)
        Me.label_port.TabIndex = 29
        Me.label_port.Text = "Port:"
        '
        'textBoxHost
        '
        Me.textBoxHost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxHost.Location = New System.Drawing.Point(56, 56)
        Me.textBoxHost.Name = "textBoxHost"
        Me.textBoxHost.Size = New System.Drawing.Size(88, 22)
        Me.textBoxHost.TabIndex = 28
        Me.textBoxHost.Text = ""
        '
        'label_host
        '
        Me.label_host.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_host.Location = New System.Drawing.Point(8, 56)
        Me.label_host.Name = "label_host"
        Me.label_host.Size = New System.Drawing.Size(40, 23)
        Me.label_host.TabIndex = 27
        Me.label_host.Text = "Host:"
        '
        'label1
        '
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(16, 160)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(176, 23)
        Me.label1.TabIndex = 26
        Me.label1.Text = "List of Authors Last Names:"
        '
        'listAuthors
        '
        Me.listAuthors.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listAuthors.ItemHeight = 16
        Me.listAuthors.Location = New System.Drawing.Point(16, 192)
        Me.listAuthors.Name = "listAuthors"
        Me.listAuthors.Size = New System.Drawing.Size(272, 276)
        Me.listAuthors.TabIndex = 25
        '
        'btnConnect
        '
        Me.btnConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConnect.Location = New System.Drawing.Point(104, 120)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(80, 23)
        Me.btnConnect.TabIndex = 24
        Me.btnConnect.Text = "Connect"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(304, 485)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.textBoxPass)
        Me.Controls.Add(Me.label_pass)
        Me.Controls.Add(Me.textBoxUser)
        Me.Controls.Add(Me.label_user)
        Me.Controls.Add(Me.textBoxPort)
        Me.Controls.Add(Me.label_port)
        Me.Controls.Add(Me.textBoxHost)
        Me.Controls.Add(Me.label_host)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.listAuthors)
        Me.Controls.Add(Me.btnConnect)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        ' obtain the connection information specified
        Dim host, port, user, pass As String
        host = textBoxHost.Text
        port = textBoxPort.Text
        user = textBoxUser.Text
        pass = textBoxPass.Text

        ' build a connection string and get a connection object
        Dim conn As New AseConnection("Data Source='" + host + "';Port='" + port + "';UID='" + user + "';PWD='" + pass + "';Database='pubs2';")
        Dim cmd As AseCommand
        Dim reader As AseDataReader

        ' open connection, execute query and populate listbox
        Try
            conn.Open()

            cmd = New AseCommand("select au_lname from authors", conn)
            reader = cmd.ExecuteReader()

            listAuthors.BeginUpdate()
            While reader.Read()
                listAuthors.Items.Add(reader.GetString(0))
            End While
            listAuthors.EndUpdate()

        Catch ex As AseException
            MessageBox.Show(ex.Message)
        Finally
            If Not (reader Is Nothing) AndAlso Not (reader.IsClosed) Then
                reader.Close()
            End If
            If Not (cmd Is Nothing) Then
                cmd.Dispose()
            End If
            If Not (conn Is Nothing) AndAlso (conn.State <> ConnectionState.Closed) Then
                conn.Close()
            End If
        End Try

    End Sub
End Class
