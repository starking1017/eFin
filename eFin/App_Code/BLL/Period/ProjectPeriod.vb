Namespace BLL

    Public Class ProjectPeriod

        Private _startdate As String
        Private _enddate As String

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


        Public Sub New(ByRef strStartDate As String, ByRef strEndDate As String)

            Me._startdate = strStartDate
            Me._enddate = strEndDate

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

    End Class


End Namespace
