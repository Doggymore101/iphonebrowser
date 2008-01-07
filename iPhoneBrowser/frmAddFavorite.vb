Imports System.IO

Public Class frmAddFavorite
    Private myPath As String = "", myTitle As String
    Public favName As String = ""
    Public favPath As String = ""

    Public Sub New(ByVal aPath As String, Optional ByVal aTitle As String = "Add Favorite")
        myPath = aPath
        myTitle = aTitle
        InitializeComponent()
    End Sub

    Private Sub frmAddFavorite_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtName.Text = Path.GetFileName(myPath)
        txtPath.Text = myPath
        Me.Text = myTitle
    End Sub

    Private Sub frmAddFavorite_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        favName = txtName.Text
        favPath = txtPath.Text
    End Sub
End Class