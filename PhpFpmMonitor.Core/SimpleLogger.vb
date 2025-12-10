' PhpFpmMonitor.Core/SimpleLogger.vb
Imports System.IO

Public Class SimpleLogger
    Private logFilePath As String
    Private lockObj As New Object()
    Private enableLogging As Boolean = True

    Public Sub New(logPath As String, Optional enabled As Boolean = True)
        logFilePath = logPath
        enableLogging = enabled

        ' Create log directory if not exists
        Dim directory As String = Path.GetDirectoryName(logFilePath)
        If Not String.IsNullOrEmpty(directory) AndAlso Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If
    End Sub

    Public Sub Log(message As String, Optional logType As String = "INFO")
        If Not enableLogging Then Return

        Try
            SyncLock lockObj
                Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim logEntry As String = String.Format("[{0}] [{1}] {2}", timestamp, logType, message)

                File.AppendAllText(logFilePath, logEntry & Environment.NewLine)
            End SyncLock
        Catch ex As Exception
            ' Fail silently - logging should not crash the app
            Console.WriteLine(String.Format("Logging error: {0}", ex.Message))
        End Try
    End Sub

    Public Sub LogError(message As String)
        Log(message, "ERROR")
    End Sub

    Public Sub LogWarning(message As String)
        Log(message, "WARN")
    End Sub

    Public Sub ClearOldLogs(daysToKeep As Integer)
        Try
            Dim directory As String = Path.GetDirectoryName(logFilePath)
            If String.IsNullOrEmpty(directory) Then Return

            Dim files As String() = System.IO.Directory.GetFiles(directory, "*.log")
            Dim cutoffDate As DateTime = DateTime.Now.AddDays(-daysToKeep)

            For Each filePath As String In files
                Dim fileInfo As New FileInfo(filePath)
                If fileInfo.LastWriteTime < cutoffDate Then
                    fileInfo.Delete()
                End If
            Next
        Catch ex As Exception
            ' Fail silently
        End Try
    End Sub
End Class