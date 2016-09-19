Public Class CheckField
    Inherits System.Windows.Forms.CheckBox

    Private _Field As SeqCore.BaseField = Nothing

    Public Property Field() As SeqCore.BaseField
        Get
            Return _Field
        End Get
        Set(ByVal Value As SeqCore.BaseField)
            _Field = Value
        End Set
    End Property

End Class
