Namespace BLL

    Public Class Period

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



#Region "Add Period To Session"

        Public Shared Sub AddPeriodToTheSession(ByRef period As BLL.Period)

            If Not HttpContext.Current.Session Is Nothing Then
                HttpContext.Current.Session("Period") = period


            End If

        End Sub


        Public Shared Function GetPeriodFromTheSession() As BLL.Period

            Dim period As BLL.Period = Nothing

            If Not HttpContext.Current.Session Is Nothing Then
                period = HttpContext.Current.Session("Period")

            End If

            Return period


        End Function


#End Region

    End Class


End Namespace
