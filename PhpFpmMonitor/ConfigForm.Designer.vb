<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ConfigForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblHost = New System.Windows.Forms.Label()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.lblInterval = New System.Windows.Forms.Label()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.lblScript = New System.Windows.Forms.Label()
        Me.txtScript = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lblMaxFail = New System.Windows.Forms.Label()
        Me.txtMaxFail = New System.Windows.Forms.TextBox()
        Me.lblTimeout = New System.Windows.Forms.Label()
        Me.txtTimeout = New System.Windows.Forms.TextBox()
        Me.chkLogging = New System.Windows.Forms.CheckBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblHost
        '
        Me.lblHost.AutoSize = True
        Me.lblHost.Location = New System.Drawing.Point(20, 20)
        Me.lblHost.Name = "lblHost"
        Me.lblHost.Size = New System.Drawing.Size(32, 13)
        Me.lblHost.TabIndex = 0
        Me.lblHost.Text = "Host:"
        '
        'txtHost
        '
        Me.txtHost.Location = New System.Drawing.Point(150, 17)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.Size = New System.Drawing.Size(350, 20)
        Me.txtHost.TabIndex = 1
        '
        'lblPort
        '
        Me.lblPort.AutoSize = True
        Me.lblPort.Location = New System.Drawing.Point(20, 60)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(29, 13)
        Me.lblPort.TabIndex = 2
        Me.lblPort.Text = "Port:"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(150, 57)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(100, 20)
        Me.txtPort.TabIndex = 3
        '
        'lblInterval
        '
        Me.lblInterval.AutoSize = True
        Me.lblInterval.Location = New System.Drawing.Point(20, 100)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(102, 13)
        Me.lblInterval.TabIndex = 4
        Me.lblInterval.Text = "Check Interval (ms):"
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(150, 97)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(100, 20)
        Me.txtInterval.TabIndex = 5
        '
        'lblScript
        '
        Me.lblScript.AutoSize = True
        Me.lblScript.Location = New System.Drawing.Point(20, 140)
        Me.lblScript.Name = "lblScript"
        Me.lblScript.Size = New System.Drawing.Size(85, 13)
        Me.lblScript.TabIndex = 6
        Me.lblScript.Text = "BAT Script Path:"
        '
        'txtScript
        '
        Me.txtScript.Location = New System.Drawing.Point(150, 137)
        Me.txtScript.Name = "txtScript"
        Me.txtScript.Size = New System.Drawing.Size(270, 20)
        Me.txtScript.TabIndex = 7
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(425, 136)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 8
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblMaxFail
        '
        Me.lblMaxFail.AutoSize = True
        Me.lblMaxFail.Location = New System.Drawing.Point(20, 180)
        Me.lblMaxFail.Name = "lblMaxFail"
        Me.lblMaxFail.Size = New System.Drawing.Size(128, 13)
        Me.lblMaxFail.TabIndex = 9
        Me.lblMaxFail.Text = "Max Fails Before Execute:"
        '
        'txtMaxFail
        '
        Me.txtMaxFail.Location = New System.Drawing.Point(150, 177)
        Me.txtMaxFail.Name = "txtMaxFail"
        Me.txtMaxFail.Size = New System.Drawing.Size(100, 20)
        Me.txtMaxFail.TabIndex = 10
        '
        'lblTimeout
        '
        Me.lblTimeout.AutoSize = True
        Me.lblTimeout.Location = New System.Drawing.Point(20, 220)
        Me.lblTimeout.Name = "lblTimeout"
        Me.lblTimeout.Size = New System.Drawing.Size(126, 13)
        Me.lblTimeout.TabIndex = 11
        Me.lblTimeout.Text = "Connection Timeout (ms):"
        '
        'txtTimeout
        '
        Me.txtTimeout.Location = New System.Drawing.Point(150, 217)
        Me.txtTimeout.Name = "txtTimeout"
        Me.txtTimeout.Size = New System.Drawing.Size(100, 20)
        Me.txtTimeout.TabIndex = 12
        '
        'chkLogging
        '
        Me.chkLogging.AutoSize = True
        Me.chkLogging.Location = New System.Drawing.Point(150, 260)
        Me.chkLogging.Name = "chkLogging"
        Me.chkLogging.Size = New System.Drawing.Size(102, 17)
        Me.chkLogging.TabIndex = 13
        Me.chkLogging.Text = "Enable Logging"
        Me.chkLogging.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(20, 310)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(120, 30)
        Me.btnTest.TabIndex = 14
        Me.btnTest.Text = "Test Connection"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(330, 310)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 30)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(420, 310)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 30)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'ConfigForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 371)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.chkLogging)
        Me.Controls.Add(Me.txtTimeout)
        Me.Controls.Add(Me.lblTimeout)
        Me.Controls.Add(Me.txtMaxFail)
        Me.Controls.Add(Me.lblMaxFail)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtScript)
        Me.Controls.Add(Me.lblScript)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.lblInterval)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.lblPort)
        Me.Controls.Add(Me.txtHost)
        Me.Controls.Add(Me.lblHost)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConfigForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Monitor Configuration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblHost As Label
    Friend WithEvents txtHost As TextBox
    Friend WithEvents lblPort As Label
    Friend WithEvents txtPort As TextBox
    Friend WithEvents lblInterval As Label
    Friend WithEvents txtInterval As TextBox
    Friend WithEvents lblScript As Label
    Friend WithEvents txtScript As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents lblMaxFail As Label
    Friend WithEvents txtMaxFail As TextBox
    Friend WithEvents lblTimeout As Label
    Friend WithEvents txtTimeout As TextBox
    Friend WithEvents chkLogging As CheckBox
    Friend WithEvents btnTest As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
End Class