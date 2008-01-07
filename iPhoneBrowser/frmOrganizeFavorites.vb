Public Class frmOrganizeFavorites
    Public favNames As Specialized.StringCollection, favPaths As Specialized.StringCollection
    Public Sub New(ByVal someNames As Specialized.StringCollection, ByVal somePaths As Specialized.StringCollection)
        favNames = New Specialized.StringCollection
        favPaths = New Specialized.StringCollection
        For Each m As String In someNames
            favNames.Add(m)
        Next
        For Each m As String In somePaths
            favPaths.Add(m)
        Next
        InitializeComponent()
    End Sub
    Private Sub frmOrganizeFavorites_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lstFavs.Items.Clear()
        For j1 As Integer = 0 To favNames.Count - 1
            Dim anItem As New ListViewItem(favNames(j1))
            anItem.SubItems.Add(favPaths(j1))
            lstFavs.Items.Add(anItem)
        Next
        btnOK.Focus()
    End Sub

    Private Sub frmOrganizeFavorites_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        favNames.Clear()
        favPaths.Clear()
        For j1 As Integer = 0 To lstFavs.Items.Count - 1
            favNames.Add(lstFavs.Items(j1).Text)
            favPaths.Add(lstFavs.Items(j1).SubItems(1).Text)
        Next
    End Sub

    Private Sub lstFavs_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFavs.SizeChanged
        lstFavs.Columns(1).Width = lstFavs.Width - lstFavs.Columns(0).Width - 5
    End Sub

    Private Sub lstFavs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFavs.SelectedIndexChanged
        Dim itemSelected As Boolean = (lstFavs.SelectedItems.Count > 0)
        btnEdit.Enabled = itemSelected
        btnDelete.Enabled = itemSelected
        If lstFavs.Items.Count > 1 And itemSelected Then
            btnUp.Enabled = lstFavs.SelectedIndices(0) > 0
            btnDown.Enabled = lstFavs.SelectedIndices(0) < lstFavs.Items.Count - 1
        Else
            btnUp.Enabled = False
            btnDown.Enabled = False
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If lstFavs.SelectedIndices.Count > 0 Then
            lstFavs.Items.RemoveAt(lstFavs.SelectedIndices(0))
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If lstFavs.SelectedItems.Count > 0 Then
            Dim selItem As ListViewItem = lstFavs.SelectedItems(0)
            Dim frmFav As frmAddFavorite = New frmAddFavorite(selItem.SubItems(1).Text, "Edit Favorite")
            If frmFav.ShowDialog() = Windows.Forms.DialogResult.OK Then
                selItem.Text = frmFav.favName
                selItem.SubItems(1).Text = frmFav.favPath
            End If
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim selName As String, selPath As String, selPos As Integer
        If lstFavs.SelectedItems.Count > 0 Then
            selName = lstFavs.SelectedItems(0).Text
            selPath = lstFavs.SelectedItems(0).SubItems(1).Text
            selPos = lstFavs.SelectedIndices(0)
            lstFavs.Items.RemoveAt(selPos)
            Dim anItem As ListViewItem = New ListViewItem(selName)
            anItem.SubItems.Add(selPath)
            lstFavs.Items.Insert(selPos - 1, anItem)
            lstFavs.Items(selPos - 1).Selected = True
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim selName As String, selPath As String, selPos As Integer
        If lstFavs.SelectedItems.Count > 0 Then
            selName = lstFavs.SelectedItems(0).Text
            selPath = lstFavs.SelectedItems(0).SubItems(1).Text
            selPos = lstFavs.SelectedIndices(0)
            lstFavs.Items.RemoveAt(selPos)
            Dim anItem As ListViewItem = New ListViewItem(selName)
            anItem.SubItems.Add(selPath)
            lstFavs.Items.Insert(selPos + 1, anItem)
            lstFavs.Items(selPos + 1).Selected = True
        End If
    End Sub
End Class