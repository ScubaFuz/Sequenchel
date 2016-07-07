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
        TableAliasNew = CheckTableAlias(TableAliasNew)
        txtNewFieldAlias.Text = FieldAliasNew

        If TableAlias.Length > 0 And cbxNewTableAlias.Items.Contains(TableAlias) = False Then cbxNewTableAlias.Items.Add(TableAlias)
        If TableAliasNew.Length > 0 And cbxNewTableAlias.Items.Contains(TableAliasNew) = False Then cbxNewTableAlias.Items.Add(TableAliasNew)

        Select Case Mode
            Case 1
                Me.Text = "Duplicate Table Alias"
                grpFieldAlias.Enabled = False
                If TableAliasNew.Length > 0 And cbxNewTableAlias.Items.Contains(TableAliasNew) = True Then cbxNewTableAlias.SelectedItem = TableAliasNew
            Case 2
                Me.Text = "Rename Table Alias"
                grpFieldAlias.Enabled = False
                If TableAliasNew.Length > 0 And cbxNewTableAlias.Items.Contains(TableAliasNew) = True Then cbxNewTableAlias.SelectedItem = TableAliasNew
            Case 3
                Me.Text = "Duplicate Field Alias"
                cbxNewTableAlias.DropDownStyle = ComboBoxStyle.DropDownList
                If TableAlias.Length > 0 Then cbxNewTableAlias.SelectedItem = TableAlias
            Case 4
                Me.Text = "Reassign Field Alias"
                cbxNewTableAlias.DropDownStyle = ComboBoxStyle.DropDownList
                If TableAlias.Length > 0 Then cbxNewTableAlias.SelectedItem = TableAlias
            Case 5
                Me.Text = "Rename Field Alias"
                cbxNewTableAlias.DropDownStyle = ComboBoxStyle.DropDownList
                If TableAlias.Length > 0 Then cbxNewTableAlias.SelectedItem = TableAlias
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
                TableAliasNew = cbxNewTableAlias.SelectedItem
            Case 4
                FieldAliasNew = txtNewFieldAlias.Text
                TableAliasNew = cbxNewTableAlias.SelectedItem
            Case 5
                FieldAliasNew = txtNewFieldAlias.Text
                TableAliasNew = cbxNewTableAlias.SelectedItem
            Case Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Exit Sub
        End Select
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function CheckTableAlias(strTableAlias As String) As String
        Dim intNumber As Integer = 1
        Dim strBaseName As String = strTableAlias

        Try
            If cbxNewTableAlias.Items.Contains(strTableAlias) = True Then
                For intCount As Integer = 0 To cbxNewTableAlias.Items.Count - 1
                    Dim strAlias As String = cbxNewTableAlias.Items(intCount)
                    If strAlias.LastIndexOf("_") > 0 Then
                        Dim strEnd As String = strAlias.Substring(strAlias.LastIndexOf("_") + 1, strAlias.Length - (strAlias.LastIndexOf("_") + 1))
                        If IsNumeric(strEnd) = True Then
                            Dim intEnd As Integer = CInt(strEnd)
                            If intNumber <= intEnd + 1 Then
                                intNumber = intEnd + 1
                                strBaseName = strAlias.Substring(0, strAlias.LastIndexOf("_"))
                            End If
                        End If
                    End If
                Next
                strBaseName &= "_" & intNumber
            End If
            'strTableAlias = CheckTableAlias(strBaseName)
        Catch ex As Exception
            'ErrorLevel = -1
            'ErrorMessage = ex.Message
        End Try
        Return strBaseName
    End Function

End Class