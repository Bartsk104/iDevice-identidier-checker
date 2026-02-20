Imports System.IO
Imports System.Diagnostics
Imports System.Text

Public Class Form1

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        TextBoxModel.Clear()

        Try

            Dim tempDir As String = Path.Combine(Application.StartupPath, "libimobiledevice")

            If Not Directory.Exists(tempDir) Then
                Directory.CreateDirectory(tempDir)
            End If


            WriteResource(My.Resources.ideviceinfo, Path.Combine(tempDir, "ideviceinfo.exe"))
            WriteResource(My.Resources.libimobiledevice, Path.Combine(tempDir, "libimobiledevice.dll"))
            WriteResource(My.Resources.libplist, Path.Combine(tempDir, "libplist.dll"))
            WriteResource(My.Resources.libusbmuxd, Path.Combine(tempDir, "libusbmuxd.dll"))
            WriteResource(My.Resources.libcrypto_1_1, Path.Combine(tempDir, "libcrypto-1_1.dll"))
            WriteResource(My.Resources.libssl_1_1, Path.Combine(tempDir, "libssl-1_1.dll"))
            WriteResource(My.Resources.zlib1, Path.Combine(tempDir, "zlib1.dll"))
            WriteResource(My.Resources.libiconv_2, Path.Combine(tempDir, "libiconv-2.dll"))
            WriteResource(My.Resources.libintl_8, Path.Combine(tempDir, "libintl-8.dll"))


            Dim psi As New ProcessStartInfo()
            psi.FileName = Path.Combine(tempDir, "ideviceinfo.exe")
            psi.Arguments = "-k ProductType"
            psi.UseShellExecute = False
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True
            psi.CreateNoWindow = True
            psi.WorkingDirectory = tempDir

            Dim p As Process = Process.Start(psi)
            Dim output As String = p.StandardOutput.ReadToEnd()
            Dim err As String = p.StandardError.ReadToEnd()
            p.WaitForExit()

            If output.Trim() <> "" Then
                TextBoxModel.Text = "iDevice detected:" & vbCrLf & output.Trim()
            Else
                TextBoxModel.Text = "Cannot connect to your iDevice." & vbCrLf & err
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub WriteResource(ByVal data As Byte(), ByVal path As String)
        If Not File.Exists(path) Then
            File.WriteAllBytes(path, data)
        End If
    End Sub

End Class
