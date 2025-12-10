' PhpFpmMonitor.Core/PortMonitor.vb
Imports System.Net.Sockets
Imports System.Diagnostics
Imports System.IO

Public Class PortMonitor
    Private timer As System.Timers.Timer
    Private lastStatus As Boolean = True
    Private failCount As Integer = 0
    Private isExecuting As Boolean = False

    ' Configuration
    Public Property Host As String = "localhost"
    Public Property Port As Integer = 9123
    Public Property CheckInterval As Integer = 5000 ' milliseconds
    Public Property BatScriptPath As String = ""
    Public Property MaxFailBeforeExecute As Integer = 2
    Public Property ConnectionTimeout As Integer = 2000 ' milliseconds

    ' Events
    Public Event StatusChanged(isOnline As Boolean, failCount As Integer)
    Public Event ScriptExecuted(scriptPath As String, timestamp As DateTime)
    Public Event ErrorOccurred(errorMessage As String)
    Public Event LogMessage(message As String)

    Public Sub New()
    End Sub

    Public Sub StartMonitoring()
        If timer IsNot Nothing Then
            StopMonitoring()
        End If

        RaiseEvent LogMessage($"Starting monitoring on {Host}:{Port} every {CheckInterval}ms")

        timer = New System.Timers.Timer(CheckInterval)
        AddHandler timer.Elapsed, AddressOf CheckPort
        timer.AutoReset = True
        timer.Start()

        ' Initial check
        CheckPort(Nothing, Nothing)
    End Sub

    Public Sub StopMonitoring()
        If timer IsNot Nothing Then
            timer.Stop()
            RemoveHandler timer.Elapsed, AddressOf CheckPort
            timer.Dispose()
            timer = Nothing
            RaiseEvent LogMessage("Monitoring stopped")
        End If
    End Sub

    Private Sub CheckPort(sender As Object, e As System.Timers.ElapsedEventArgs)
        Try
            Dim portOpen As Boolean = IsPortOpen(Host, Port)

            If portOpen Then
                ' Port is online
                If Not lastStatus Then
                    RaiseEvent LogMessage($"Port {Port} is back ONLINE")
                End If
                failCount = 0
                lastStatus = True
            Else
                ' Port is offline
                failCount += 1
                RaiseEvent LogMessage($"Port {Port} is OFFLINE (fail count: {failCount})")

                If lastStatus Then
                    RaiseEvent LogMessage($"Port {Port} went DOWN!")
                End If

                lastStatus = False

                ' Execute script after threshold
                If failCount >= MaxFailBeforeExecute And Not isExecuting Then
                    ExecuteBatScript()
                End If
            End If

            RaiseEvent StatusChanged(portOpen, failCount)

        Catch ex As Exception
            RaiseEvent ErrorOccurred($"Check error: {ex.Message}")
        End Try
    End Sub

    Private Function IsPortOpen(host As String, port As Integer) As Boolean
        Try
            Using client As New TcpClient()
                Dim result As IAsyncResult = client.BeginConnect(host, port, Nothing, Nothing)
                Dim success As Boolean = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(ConnectionTimeout))

                If success Then
                    Try
                        client.EndConnect(result)
                        Return True
                    Catch
                        Return False
                    End Try
                Else
                    Return False
                End If
            End Using
        Catch ex As SocketException
            Return False
        Catch ex As Exception
            RaiseEvent ErrorOccurred($"Port check error: {ex.Message}")
            Return False
        End Try
    End Function

    Private Sub ExecuteBatScript()
        If isExecuting Then
            RaiseEvent LogMessage("Script already executing, skipping...")
            Return
        End If

        If String.IsNullOrWhiteSpace(BatScriptPath) Then
            RaiseEvent ErrorOccurred("Bat script path is not configured")
            Return
        End If

        If Not File.Exists(BatScriptPath) Then
            RaiseEvent ErrorOccurred($"Bat script not found: {BatScriptPath}")
            Return
        End If

        isExecuting = True

        Try
            RaiseEvent LogMessage($"Executing script: {BatScriptPath}")

            Dim psi As New ProcessStartInfo()
            psi.FileName = BatScriptPath
            psi.UseShellExecute = True
            psi.CreateNoWindow = True
            psi.WindowStyle = ProcessWindowStyle.Hidden

            ' Set working directory to script's folder
            Dim scriptDir As String = Path.GetDirectoryName(BatScriptPath)
            If Not String.IsNullOrEmpty(scriptDir) Then
                psi.WorkingDirectory = scriptDir
                RaiseEvent LogMessage($"Working directory set to: {scriptDir}")
            End If

            Dim process As Process = Process.Start(psi)
            RaiseEvent ScriptExecuted(BatScriptPath, DateTime.Now)

            ' Wait for script to complete (with timeout)
            If process IsNot Nothing Then
                If process.WaitForExit(30000) Then ' 30 second timeout
                    RaiseEvent LogMessage($"Script completed with exit code: {process.ExitCode}")
                Else
                    RaiseEvent LogMessage("Script timeout after 30 seconds")
                End If
            End If

        Catch ex As Exception
            RaiseEvent ErrorOccurred($"Script execution error: {ex.Message}")
        Finally
            isExecuting = False
        End Try
    End Sub

    Public Sub ManualExecuteScript()
        RaiseEvent LogMessage("Manual script execution triggered")
        ExecuteBatScript()
    End Sub

    Public Function GetCurrentStatus() As Boolean
        Return lastStatus
    End Function
End Class