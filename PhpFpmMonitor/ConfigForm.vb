' PhpFpmMonitor.TrayApp/ConfigForm.vb
Imports System.IO
Imports System.Windows.Forms
Imports PhpFpmMonitor.Core

Public Class ConfigForm
    Private config As MonitorConfig

    Public Sub New(currentConfig As MonitorConfig)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        config = currentConfig
        LoadConfig()
    End Sub

    Private Sub LoadConfig()
        txtHost.Text = config.Host
        txtPort.Text = config.Port.ToString()
        txtInterval.Text = config.CheckInterval.ToString()
        txtScript.Text = config.BatScriptPath
        txtMaxFail.Text = config.MaxFailBeforeExecute.ToString()
        txtTimeout.Text = config.ConnectionTimeout.ToString()
        chkLogging.Checked = config.EnableLogging
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using dialog As New OpenFileDialog()
            dialog.Filter = "Batch Files (*.bat)|*.bat|All Files (*.*)|*.*"
            dialog.Title = "Select BAT Script"

            If dialog.ShowDialog() = DialogResult.OK Then
                txtScript.Text = dialog.FileName
            End If
        End Using
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Try
            Dim host As String = txtHost.Text
            Dim port As Integer = Integer.Parse(txtPort.Text)
            Dim timeout As Integer = Integer.Parse(txtTimeout.Text)

            ' Manual test
            Dim isOpen As Boolean = TestConnection(host, port, timeout)

            If isOpen Then
                MessageBox.Show(String.Format("Connection successful!{0}Port {1} on {2} is OPEN", vbCrLf, port, host),
                    "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(String.Format("Connection failed!{0}Port {1} on {2} is CLOSED", vbCrLf, port, host),
                    "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("Test error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function TestConnection(host As String, port As Integer, timeout As Integer) As Boolean
        Try
            Using client As New System.Net.Sockets.TcpClient()
                Dim result As IAsyncResult = client.BeginConnect(host, port, Nothing, Nothing)
                Dim success As Boolean = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout))

                If success Then
                    client.EndConnect(result)
                    Return True
                End If
            End Using
        Catch
        End Try
        Return False
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Validate inputs
            If String.IsNullOrWhiteSpace(txtHost.Text) Then
                MessageBox.Show("Host cannot be empty", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtHost.Focus()
                Return
            End If

            Dim port As Integer
            If Not Integer.TryParse(txtPort.Text, port) OrElse port <= 0 OrElse port > 65535 Then
                MessageBox.Show("Invalid port number (1-65535)", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPort.Focus()
                Return
            End If

            Dim interval As Integer
            If Not Integer.TryParse(txtInterval.Text, interval) OrElse interval < 1000 Then
                MessageBox.Show("Check interval must be at least 1000ms", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtInterval.Focus()
                Return
            End If

            Dim maxFail As Integer
            If Not Integer.TryParse(txtMaxFail.Text, maxFail) OrElse maxFail < 1 Then
                MessageBox.Show("Max fail count must be at least 1", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtMaxFail.Focus()
                Return
            End If

            Dim timeout As Integer
            If Not Integer.TryParse(txtTimeout.Text, timeout) OrElse timeout < 500 Then
                MessageBox.Show("Connection timeout must be at least 500ms", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtTimeout.Focus()
                Return
            End If

            ' Save config
            config.Host = txtHost.Text.Trim()
            config.Port = port
            config.CheckInterval = interval
            config.BatScriptPath = txtScript.Text.Trim()
            config.MaxFailBeforeExecute = maxFail
            config.ConnectionTimeout = timeout
            config.EnableLogging = chkLogging.Checked

            ConfigManager.SaveConfig(config)

            MessageBox.Show("Configuration saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(String.Format("Error saving configuration: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class