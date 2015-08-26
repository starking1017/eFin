Namespace BLL.Utils

    Public Class DataSetHelper
        Public ds As DataSet

        Public Sub New(ByVal DataSet As DataSet)
            ds = DataSet
        End Sub

        Public Sub New()
            ds = Nothing
        End Sub

        Private Function ColumnEqual(ByVal A As Object, ByVal B As Object) As Boolean
            '
            ' Compares two values to determine if they are equal. Also compares DBNULL.Value.
            '
            ' NOTE: If your DataTable contains object fields, you must extend this
            ' function to handle the fields in a meaningful way if you intend to group on them.
            '
            If A Is DBNull.Value And B Is DBNull.Value Then Return True ' Both are DBNull.Value.
            If A Is DBNull.Value Or B Is DBNull.Value Then Return False ' Only one is DBNull.Value.
            Return A = B                                                ' Value type standard comparison
        End Function


        Public Function SelectDistinct(ByVal TableName As String, _
                                       ByVal SourceTable As DataTable, _
                                       ByVal FieldName As String) As DataTable
            Dim dt As New DataTable(TableName)
            dt.Columns.Add(FieldName, SourceTable.Columns(FieldName).DataType)
            Dim dr As DataRow, LastValue As New Object
            For Each dr In SourceTable.Select("", FieldName)
                If LastValue Is Nothing OrElse Not ColumnEqual(LastValue, dr(FieldName)) Then
                    LastValue = dr(FieldName)
                    dt.Rows.Add(New Object() {LastValue})
                End If
            Next
            If Not ds Is Nothing Then ds.Tables.Add(dt)
            Return dt
        End Function

    End Class

End Namespace
