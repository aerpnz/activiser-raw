Imports System.ComponentModel

Public Class SideBarButton

    Private _checked As Boolean = False

    <DefaultValue(False), Category("Behavior")> _
    Public Property Checked() As Boolean
        Get
            Return _checked
        End Get
        Set(ByVal value As Boolean)
            _checked = value
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return Me.Caption.Text
        End Get
        Set(ByVal value As String)
            Me.Caption.Text = value
        End Set
    End Property

    Public Property Image() As System.Drawing.Image
        Get
            Return Caption.Image
        End Get
        Set(ByVal value As System.Drawing.Image)
            Caption.Image = value
        End Set
    End Property

    Private Sub SideBarButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Me.BackgroundImage = My.Resources.ButtonMouseDown
    End Sub

    Private Sub SideBarButton_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter
        If Me.Checked Then
            Me.BackgroundImage = My.Resources.ButtonMouseDown
        Else
            Me.BackgroundImage = My.Resources.ButtonOver
        End If
    End Sub

    Private Sub SideBarButton_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave
        If Me.Checked Then
            Me.BackgroundImage = My.Resources.ButtonSelected
        Else
            Me.BackgroundImage = My.Resources.ButtonBG
        End If
    End Sub

    Private Sub SideBarButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        If Me.Checked Then
            Me.BackgroundImage = My.Resources.ButtonSelected
        Else
            Me.BackgroundImage = My.Resources.ButtonOver
        End If
    End Sub
End Class
