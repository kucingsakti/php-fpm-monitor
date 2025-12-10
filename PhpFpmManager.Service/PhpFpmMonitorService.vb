' PhpFpmMonitor.Service/PhpFpmMonitorService.vb
Imports System.ServiceProcess
Imports PhpFpmMonitor.Core

Public Class PhpFpmMonitorService
    Inherits ServiceBase

    Private WithEvents monitor As PortMonitor
    Private logger As SimpleLogger
    Private config As MonitorConfig

    Public Sub New()
        Me.ServiceName = "PhpFpmMonitorService"
        Me.CanStop = True
        Me.CanPauseAndContinue = False
        Me.AutoLog = True
    End Sub

    Protected Overrides Sub OnStart(args As String())
        Try
            ' Load configuration
            config = ConfigManager.LoadConfig()

            ' Initialize logger
            logger = New SimpleLogger(config.LogFilePath, config.EnableLogging)
            logger.Log("Service starting...")

            ' Initialize and start monitor
            monitor = New PortMonitor()
            monitor.Host = config.Host
            monitor.Port = config.Port
            monitor.CheckInterval = config.CheckInterval
            monitor.BatScriptPath = config.BatScriptPath
            monitor.MaxFailBeforeExecute = config.MaxFailBeforeExecute
            monitor.ConnectionTimeout = config.ConnectionTimeout

            monitor.StartMonitoring()

            logger.Log("Service started successfully")

        Catch ex As Exception
            If logger IsNot Nothing Then
                logger.LogError($"Service start error: {ex.Message}")
            End If

            EventLog.WriteEntry(Me.ServiceName, $"Service start error: {ex.Message}", EventLogEntryType.Error)
            Throw
        End Try
    End Sub

    Protected Overrides Sub OnStop()
        Try
            logger?.Log("Service stopping...")

            If monitor IsNot Nothing Then
                monitor.StopMonitoring()
                monitor = Nothing
            End If

            logger?.Log("Service stopped successfully")

        Catch ex As Exception
            logger?.LogError($"Service stop error: {ex.Message}")
            EventLog.WriteEntry(Me.ServiceName, $"Service stop error: {ex.Message}", EventLogEntryType.Error)
        End Try
    End Sub

    Private Sub Monitor_StatusChanged(isOnline As Boolean, failCount As Integer) Handles monitor.StatusChanged
        ' Log status changes
        If Not isOnline AndAlso failCount = 1 Then
            logger?.LogWarning($"Port {config.Port} went offline")
        ElseIf isOnline AndAlso failCount = 0 Then
            logger?.Log($"Port {config.Port} is back online")
        End If
    End Sub

    Private Sub Monitor_ScriptExecuted(scriptPath As String, timestamp As DateTime) Handles monitor.ScriptExecuted
        logger?.Log($"Script executed: {scriptPath} at {timestamp}")
        EventLog.WriteEntry(Me.ServiceName, $"Restart script executed: {scriptPath}", EventLogEntryType.Information)
    End Sub

    Private Sub Monitor_ErrorOccurred(errorMessage As String) Handles monitor.ErrorOccurred
        logger?.LogError(errorMessage)
        EventLog.WriteEntry(Me.ServiceName, errorMessage, EventLogEntryType.Error)
    End Sub

    Private Sub Monitor_LogMessage(message As String) Handles monitor.LogMessage
        logger?.Log(message)
    End Sub
End Class

' Program.vb - Service Entry Point
Module Program
    Sub Main()
        Dim servicesToRun() As ServiceBase = {New PhpFpmMonitorService()}
        ServiceBase.Run(servicesToRun)
    End Sub
End Module