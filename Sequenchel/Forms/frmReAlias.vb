Public Class frmReAlias

    Private _TableName As String = ""
    Private _TableAlias As String = ""
    Private _TableAliasNew As String = ""
    Private _FieldName As String = ""
    Private _FieldAlias As String = ""
    Private _FieldAliasNew As String = ""
    Private _Mode As Integer = 0

    Public Property TableName() As String
        Get
            Return _TableName
        End Get
        Set(ByVal Value As String)
            _TableName = Value
        End Set
    End Property

    Public Property TableAlias() As String
        Get
            Return _TableAlias
        End Get
        Set(ByVal Value As String)
            _TableAlias = Value
        End Set
    End Property

    Public Property TableAliasNew() As String
        Get
            Return _TableAliasNew
        End Get
        Set(ByVal Value As String)
            _TableAliasNew = Value
        End Set
    End Property

    Public Property FieldName() As String
        Get
            Return _FieldName
        End Get
        Set(ByVal Value As String)
            _FieldName = Value
        End Set
    End Property

    Public Property FieldAlias() As String
        Get
            Return _FieldAlias
        End Get
        Set(ByVal Value As String)
            _FieldAlias = Value
        End Set
    End Property

    Public Property FieldAliasNew() As String
        Get
            Return _FieldAliasNew
        End Get
        Set(ByVal Value As String)
            _FieldAliasNew = Value
        End Set
    End Property

    Public Property Mode() As Integer
        Get
            Return _Mode
        End Get
        Set(ByVal Value As Integer)
            _Mode = Value
        End Set
    End Property

    Private Sub frmReAlias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowFields()
    End Sub

    Private Sub ShowFields()
        txtFieldName.Text = FieldName
        txtCurrentFieldAlias.Text = FieldAlias
        txtTableName.Text = TableName
        txtCurrentTableAlias.Text = TableAlias
        cbxNewTableAlias.Text = TableAliasNew
        txtNewFieldAlias.Text = FieldAliasNew

        Select Case Mode
            Case 1
                Me.Text = "Duplicate Table Alias"
                grpFieldAlias.Enabled = False
            Case 2
                Me.Text = "Rename Table Alias"
                grpFieldAlias.Enabled = False
            Case 3
                Me.Text = "Duplicate Field Alias"
                grpTableAlias.Enabled = False
            Case 4
                Me.Text = "Reassign Field Alias"
                grpTableAlias.Enabled = False
            Case 5
                Me.Text = "Rename Field Alias"
                grpTableAlias.Enabled = False
            Case Else
                Me.Text = "No Mode Selected"
                grpFieldAlias.Enabled = False
                grpTableAlias.Enabled = False
        End Select
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Select Case Mode
            Case 1
                TableAliasNew = cbxNewTableAlias.Text
            Case 2
                TableAliasNew = cbxNewTableAlias.Text
            Case 3
                FieldAliasNew = txtNewFieldAlias.Text
            Case 4
                FieldAliasNew = txtNewFieldAlias.Text
            Case 5
                FieldAliasNew = txtNewFieldAlias.Text
            Case Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Select
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class