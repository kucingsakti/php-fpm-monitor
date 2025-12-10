' PhpFpmMonitor.Core/ConfigManager.vb
Imports System.Configuration
Imports System.IO
Imports System.Xml.Serialization

<Serializable>
Public Class MonitorConfig
    Public Property Host As String = "localhost"
    Public Property Port As Integer = 9123
    Public Property CheckInterval As Integer = 5000
    Public Property BatScriptPath As String = ""
    Public Property MaxFailBeforeExecute As Integer = 2
    Public Property ConnectionTimeout As Integer = 2000
    Public Property EnableLogging As Boolean = True
    Public Property LogFilePath As String = "logs\monitor.log"
End Class

Public Class ConfigManager
    Private Shared ReadOnly ConfigFileName As String = "MonitorConfig.xml"
    Private Shared configPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName)

    Public Shared Function LoadConfig() As MonitorConfig
        Try
            If File.Exists(configPath) Then
                Dim serializer As New XmlSerializer(GetType(MonitorConfig))
                Using reader As New StreamReader(configPath)
                    Return CType(serializer.Deserialize(reader), MonitorConfig)
                End Using
            Else
                ' Return default config
                Dim defaultConfig As New MonitorConfig()
                SaveConfig(defaultConfig) ' Save default config
                Return defaultConfig
            End If
        Catch ex As Exception
            ' Return default on error
            Return New MonitorConfig()
        End Try
    End Function

    Public Shared Sub SaveConfig(config As MonitorConfig)
        Try
            Dim serializer As New XmlSerializer(GetType(MonitorConfig))

            ' Create directory if not exists
            Dim directory As String = Path.GetDirectoryName(configPath)
            If Not String.IsNullOrEmpty(directory) AndAlso Not System.IO.Directory.Exists(directory) Then
                System.IO.Directory.CreateDirectory(directory)
            End If

            Using writer As New StreamWriter(configPath)
                serializer.Serialize(writer, config)
            End Using
        Catch ex As Exception
            Throw New Exception($"Failed to save config: {ex.Message}")
        End Try
    End Sub

    Public Shared Function GetConfigPath() As String
        Return configPath
    End Function
End Class