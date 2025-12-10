' PhpFpmMonitor.Service/ProjectInstaller.vb
Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess

<RunInstaller(True)>
Public Class ProjectInstaller
    Inherits Installer

    Private serviceProcessInstaller As ServiceProcessInstaller
    Private serviceInstaller As ServiceInstaller

    Public Sub New()
        ' Service Process Installer
        serviceProcessInstaller = New ServiceProcessInstaller()
        serviceProcessInstaller.Account = ServiceAccount.LocalSystem
        serviceProcessInstaller.Username = Nothing
        serviceProcessInstaller.Password = Nothing

        ' Service Installer
        serviceInstaller = New ServiceInstaller()
        serviceInstaller.ServiceName = "PhpFpmMonitorService"
        serviceInstaller.DisplayName = "PHP-FPM Monitor Service"
        serviceInstaller.Description = "Monitors PHP-FPM port 9123 and executes restart script when port is down"
        serviceInstaller.StartType = ServiceStartMode.Automatic

        ' Add installers to collection
        Me.Installers.AddRange(New Installer() {serviceProcessInstaller, serviceInstaller})
    End Sub
End Class