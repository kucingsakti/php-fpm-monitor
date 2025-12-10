' PhpFpmMonitor.TrayApp/MainForm.vb
Imports System.Diagnostics
Imports System.Windows.Forms
Imports PhpFpmMonitor.Core

Public Class MainForm
    Inherits Form

    Private WithEvents notifyIcon As NotifyIcon
    Private WithEvents monitor As PortMonitor
    Private logger As SimpleLogger
    Private config As MonitorConfig

    Private contextMenu As ContextMenuStrip
    Private statusMenuItem As ToolStripMenuItem
    Private executeMenuItem As ToolStripMenuItem
    Private configMenuItem As ToolStripMenuItem
    Private logsMenuItem As ToolStripMenuItem
    Private exitMenuItem As ToolStripMenuItem

    Public Sub New()
        InitializeComponent()
        LoadConfiguration()
        InitializeTrayIcon()
        InitializeMonitor()
    End Sub

    Private Sub LoadConfiguration()
        Try
            config = ConfigManager.LoadConfig()

            ' Initialize logger
            logger = New SimpleLogger(config.LogFilePath, config.EnableLogging)
            logger.Log("Application started")

            ' Clean old logs
            logger.ClearOldLogs(7) ' Keep 7 days

        Catch ex As Exception
            MessageBox.Show(String.Format("Error loading configuration: {0}", ex.Message), "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeTrayIcon()
        ' Create context menu
        contextMenu = New ContextMenuStrip()

        statusMenuItem = New ToolStripMenuItem("Status: Checking...")
        statusMenuItem.Enabled = False

        executeMenuItem = New ToolStripMenuItem("Execute Script Now")
        AddHandler executeMenuItem.Click, AddressOf ExecuteMenuItem_Click

        configMenuItem = New ToolStripMenuItem("Open Configuration")
        AddHandler configMenuItem.Click, AddressOf ConfigMenuItem_Click

        logsMenuItem = New ToolStripMenuItem("View Logs")
        AddHandler logsMenuItem.Click, AddressOf LogsMenuItem_Click

        exitMenuItem = New ToolStripMenuItem("Exit")
        AddHandler exitMenuItem.Click, AddressOf ExitMenuItem_Click

        contextMenu.Items.Add(statusMenuItem)
        contextMenu.Items.Add(New ToolStripSeparator())
        contextMenu.Items.Add(executeMenuItem)
        contextMenu.Items.Add(configMenuItem)
        contextMenu.Items.Add(logsMenuItem)
        contextMenu.Items.Add(New ToolStripSeparator())
        contextMenu.Items.Add(exitMenuItem)

        ' Create notify icon
        notifyIcon = New NotifyIcon()
        notifyIcon.Icon = SystemIcons.Application ' Replace with custom icon
        notifyIcon.Text = "PHP-FPM Monitor"
        notifyIcon.ContextMenuStrip = contextMenu
        notifyIcon.Visible = True

        AddHandler notifyIcon.DoubleClick, AddressOf NotifyIcon_DoubleClick
    End Sub

    Private Sub InitializeMonitor()
        Try
            monitor = New PortMonitor()
            monitor.Host = config.Host
            monitor.Port = config.Port
            monitor.CheckInterval = config.CheckInterval
            monitor.BatScriptPath = config.BatScriptPath
            monitor.MaxFailBeforeExecute = config.MaxFailBeforeExecute
            monitor.ConnectionTimeout = config.ConnectionTimeout

            monitor.StartMonitoring()

        Catch ex As Exception
            logger.LogError(String.Format("Monitor initialization error: {0}", ex.Message))
            MessageBox.Show(String.Format("Error starting monitor: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Monitor_StatusChanged(isOnline As Boolean, failCount As Integer) Handles monitor.StatusChanged
        If Me.InvokeRequired Then
            Me.Invoke(Sub() Monitor_StatusChanged(isOnline, failCount))
            Return
        End If

        Dim statusText As String = If(isOnline, "ONLINE", String.Format("OFFLINE (fails: {0})", failCount))
        statusMenuItem.Text = String.Format("Port {0}: {1}", config.Port, statusText)

        ' Change icon color based on status
        notifyIcon.Icon = If(isOnline, SystemIcons.Information, SystemIcons.Warning)
        notifyIcon.Text = String.Format("PHP-FPM Monitor - {0}", statusText)
    End Sub

    Private Sub Monitor_ScriptExecuted(scriptPath As String, timestamp As DateTime) Handles monitor.ScriptExecuted
        logger.Log(String.Format("Script executed: {0}", scriptPath))

        If Me.InvokeRequired Then
            Me.Invoke(Sub() Monitor_ScriptExecuted(scriptPath, timestamp))
            Return
        End If

        notifyIcon.ShowBalloonTip(3000, "Script Executed",
            String.Format("Restart script executed at {0:HH:mm:ss}", timestamp),
            ToolTipIcon.Info)
    End Sub

    Private Sub Monitor_ErrorOccurred(errorMessage As String) Handles monitor.ErrorOccurred
        logger.LogError(errorMessage)
    End Sub

    Private Sub Monitor_LogMessage(message As String) Handles monitor.LogMessage
        logger.Log(message)
    End Sub

    Private Sub ExecuteMenuItem_Click(sender As Object, e As EventArgs)
        Try
            monitor.ManualExecuteScript()
            MessageBox.Show("Script executed manually", "Execute", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(String.Format("Error executing script: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigMenuItem_Click(sender As Object, e As EventArgs)
        Dim configForm As New ConfigForm(config)
        If configForm.ShowDialog() = DialogResult.OK Then
            ' Restart monitor with new config
            monitor.StopMonitoring()
            config = ConfigManager.LoadConfig()
            InitializeMonitor()
            logger.Log("Configuration updated and monitor restarted")
        End If
    End Sub

    Private Sub LogsMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If System.IO.File.Exists(config.LogFilePath) Then
                Process.Start("notepad.exe", config.LogFilePath)
            Else
                MessageBox.Show("Log file not found", "Logs", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format("Error opening logs: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NotifyIcon_DoubleClick(sender As Object, e As EventArgs)
        ConfigMenuItem_Click(sender, e)
    End Sub

    Private Sub ExitMenuItem_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.Hide()
        Else
            If monitor IsNot Nothing Then
                monitor.StopMonitoring()
            End If
            If notifyIcon IsNot Nothing Then
                notifyIcon.Dispose()
            End If
            If logger IsNot Nothing Then
                logger.Log("Application closed")
            End If
        End If

        MyBase.OnFormClosing(e)
    End Sub
End Class