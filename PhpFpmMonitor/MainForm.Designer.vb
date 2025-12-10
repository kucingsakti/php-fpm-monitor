<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExecuteScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewLogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Text = "PHP-FPM Monitor"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusToolStripMenuItem, Me.ToolStripSeparator1, Me.ExecuteScriptToolStripMenuItem, Me.OpenConfigurationToolStripMenuItem, Me.ViewLogsToolStripMenuItem, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(181, 126)
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.Enabled = False
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.StatusToolStripMenuItem.Text = "Status: Checking..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(177, 6)
        '
        'ExecuteScriptToolStripMenuItem
        '
        Me.ExecuteScriptToolStripMenuItem.Name = "ExecuteScriptToolStripMenuItem"
        Me.ExecuteScriptToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExecuteScriptToolStripMenuItem.Text = "Execute Script Now"
        '
        'OpenConfigurationToolStripMenuItem
        '
        Me.OpenConfigurationToolStripMenuItem.Name = "OpenConfigurationToolStripMenuItem"
        Me.OpenConfigurationToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.OpenConfigurationToolStripMenuItem.Text = "Open Configuration"
        '
        'ViewLogsToolStripMenuItem
        '
        Me.ViewLogsToolStripMenuItem.Name = "ViewLogsToolStripMenuItem"
        Me.ViewLogsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ViewLogsToolStripMenuItem.Text = "View Logs"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(177, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(218, 270)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MainForm"
        Me.Opacity = 0R
        Me.ShowInTaskbar = False
        Me.Text = "PHP-FPM Monitor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents StatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ExecuteScriptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenConfigurationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewLogsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
End Class