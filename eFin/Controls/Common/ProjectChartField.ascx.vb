
Partial Class Controls_Common_ProjectChartField
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        ' Load ChartFields as table header
        If Session("ProjectChartFields") IsNot Nothing Then
            Dim arrlistProjectChartFields As ArrayList = CType(Session("ProjectChartFields"), ArrayList)
            lblFund.Text = arrlistProjectChartFields(0).ToString()
            lbDeptCode.Text = arrlistProjectChartFields(1).ToString()
            lblBusinessUnit.Text = arrlistProjectChartFields(2).ToString()
        End If

    End Sub
End Class
