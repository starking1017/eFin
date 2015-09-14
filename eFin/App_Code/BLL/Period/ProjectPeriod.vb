Namespace BLL

    Public Class ProjectPeriod

        Private _startdate As String
        Private _enddate As String
        Private _expirydate As String

        Public ReadOnly Property StartDate() As String
            Get
                Return Me._startdate
            End Get
        End Property

        Public ReadOnly Property EndDate() As String
            Get
                Return Me._enddate
            End Get
        End Property

        Public ReadOnly Property ExpiryDate() As String
            Get
                Return Me._expirydate
            End Get
        End Property


        Public Sub New(ByRef strStartDate As String, ByRef strEndDate As String, ByRef strExpiryDate As String)

            Me._startdate = strStartDate
            Me._enddate = strEndDate
            Me._expirydate = strExpiryDate

        End Sub



#Region "Add ProjectPeriod To Session"

        Public Shared Sub AddProjectPeriodToTheSession(ByRef projectPeriod As BLL.ProjectPeriod)

            If Not HttpContext.Current.Session Is Nothing Then
                HttpContext.Current.Session("ProjectPeriod") = projectPeriod


            End If

        End Sub


        Public Shared Function GetProjectPeriodFromTheSession() As BLL.ProjectPeriod

            Dim projectPeriod As BLL.ProjectPeriod = Nothing

            If Not HttpContext.Current.Session Is Nothing Then
                projectPeriod = HttpContext.Current.Session("ProjectPeriod")

            End If

            Return projectPeriod


        End Function


#End Region

        Public Shared Function GetBudgetLastDate(strAsAtDate As String, nMaxExtendYear As Integer) As String

            Dim iStartDate As Integer = CType(GetProjectPeriodFromTheSession.StartDate, Integer)
            Dim iEndDate As Integer = CType(GetProjectPeriodFromTheSession.EndDate, Integer)
            Dim iExpiryDate As Integer = CType(GetProjectPeriodFromTheSession.ExpiryDate, Integer)
            Dim iAsAtDate As Integer = CType(strAsAtDate, Integer)

            If (iExpiryDate >= iEndDate) Then
                If (iExpiryDate > strAsAtDate * 100 + nMaxExtendYear * 10000) Then
                    Return CType(strAsAtDate * 100 + nMaxExtendYear * 10000, String)
                Else
                    Return CType(iExpiryDate, String)
                End If
            Else
                If (iEndDate > strAsAtDate * 100 + nMaxExtendYear * 10000) Then
                    Return CType(strAsAtDate * 100 + nMaxExtendYear * 10000, String)
                Else
                    Return CType(iEndDate, String)
                End If
            End If
        End Function
    End Class


End Namespace
