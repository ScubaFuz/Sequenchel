Imports System.ComponentModel

Public Class usrDataGridView
    Inherits Windows.Forms.DataGridView

    Private m_Image As Drawing.Image

    Public Sub New()
        'Me.m_Image = Drawing.Image.FromFile("c:\TSfavicon.jpg")
        Me.m_Image = My.Resources.Background_Sequenchel_relieftransparant30down
    End Sub

    Protected Overrides Sub PaintBackground( _
        ByVal graphics As System.Drawing.Graphics, _
        ByVal clipBounds As System.Drawing.Rectangle, _
        ByVal gridBounds As System.Drawing.Rectangle)
        SetImageLayout()
        MyBase.PaintBackground(graphics, clipBounds, gridBounds)
        graphics.DrawImage(Me.m_Image, gridBounds)

    End Sub

    Public Overrides Property BackgroundImageLayout As ImageLayout
        Get
            Return MyBase.BackgroundImageLayout
        End Get
        Set(ByVal value As ImageLayout)
            MyBase.BackgroundImageLayout = value
            'MyBase.Parent.BackgroundImageLayout = value
        End Set
    End Property

    Private Sub SetImageLayout()
        Me.BackgroundImageLayout = ImageLayout.None
    End Sub

    '<description("set /> _
    Public Property BackImage() As Drawing.Image
        Get
            Return Me.m_Image
        End Get
        Set(ByVal value As Drawing.Image)
            Me.m_Image = value
        End Set
    End Property

End Class
