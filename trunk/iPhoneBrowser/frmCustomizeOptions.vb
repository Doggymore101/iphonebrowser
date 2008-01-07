Public Class frmCustomizeOptions
    Private Sub chkCarrier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCarrier.CheckedChanged
        cbCarriers.Enabled = chkCarrier.Checked
    End Sub

    Private Sub frmCustomizeOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sndArray() As String = {"Unlock", "Lock", "Received", "Sent", "Voicemail", "Alarm", _
            "BeepBeep", "LowPower", "Mail", "NewMail", "Photo", "SMSReceived"}
        cbSounds.Items.Clear()
        For Each s As String In sndArray
            cbSounds.Items.Add(s & " Sound")
        Next
        txtCategory.Select()
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        For Each c As Control In grpImages.Controls
            If TypeOf c Is CheckBox Then
                DirectCast(c, CheckBox).Checked = True
            End If
        Next
        For j As Integer = 0 To cbSounds.Items.Count - 1
            cbSounds.SetItemChecked(j, True)
        Next
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        For Each c As Control In grpImages.Controls
            If TypeOf c Is CheckBox Then
                DirectCast(c, CheckBox).Checked = False
            End If
        Next
        For j As Integer = 0 To cbSounds.Items.Count - 1
            cbSounds.SetItemChecked(j, False)
        Next
    End Sub

    Private Sub btnSliders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSliders.Click
        Dim sliderArray() As CheckBox = {chkMainSlider, chkPowerSlider, chkCallSlider, chkHiMask}
        Dim newVal As Boolean = Not chkMainSlider.Checked
        For Each c As CheckBox In sliderArray
            c.Checked = newVal
        Next
    End Sub

    Private Sub btnSounds_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSounds.Click
        Dim newVal As Boolean = Not cbSounds.GetItemChecked(0)
        For j As Integer = 0 To cbSounds.Items.Count - 1
            cbSounds.SetItemChecked(j, newVal)
        Next
    End Sub
    Private Shared Function IsChecked(ByVal aControl As Control) As Boolean
        If TypeOf aControl Is CheckBox Then
            Return DirectCast(aControl, CheckBox).Checked
        Else
            Return False
        End If
    End Function

    Public Function HasChecked() As Boolean
        Dim aryImages(grpImages.Controls.Count) As Control
        grpImages.Controls.CopyTo(aryImages, 0)

        Dim ans As Boolean = Array.Exists(aryImages, AddressOf IsChecked)
        If Not ans Then
            ans = (cbSounds.CheckedItems.Count > 0)
        End If
        Return ans
    End Function

    Private Sub txtThemeName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtThemeName.TextChanged
        btnOK.Enabled = (txtThemeName.Text <> "")
    End Sub
End Class