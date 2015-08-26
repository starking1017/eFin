Imports System.Data
Namespace BLL.Status

    Public Class ActivityHeader

#Region "Member variable "
        Private _actStatus As String
        Private _actDescription As String
        Private _actStartDate As String
        Private _actEndDate As String
        Private _actTitle As String

#End Region


#Region "Property Functions"
        Public ReadOnly Property ActivityStatus() As String
            Get
                Return Me._actStatus
            End Get
        End Property

        Public ReadOnly Property ActivityDescription() As String
            Get
                Return Me._actDescription
            End Get
        End Property

        Public ReadOnly Property ActivityStartDate() As String
            Get
                Return Me._actStartDate
            End Get
        End Property

        Public ReadOnly Property ActivityEndDate() As String
            Get
                Return Me._actEndDate
            End Get
        End Property

        Public ReadOnly Property ActivityTitle() As String
            Get
                Return Me._actTitle
            End Get
        End Property
#End Region


#Region "Constructor"
        Public Sub New(ByRef newActStatus As String, _
                       ByRef newActDescription As String, _
                       ByRef newActStartDate As String, _
                       ByRef newActEndDate As String, _
                       ByRef newActTitle As String)

            Me._actStatus = newActStatus
            Me._actDescription = newActDescription
            Me._actStartDate = newActStartDate
            Me._actEndDate = newActEndDate
            Me._actTitle = newActTitle
        End Sub

        Public Sub New()

        End Sub
#End Region

        Public Function LoadActivityHeader(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As Boolean
            Dim dt As DataTable = DAL.Status.ProjectHeaderDB.LoadProjectHeaderDB(DBOperation, SP_Name, arrParams)

            If dt.Rows.Count <> 0 Then
                Dim dr As DataRow = dt.Rows(0)

                Me._actStatus = dr("Activity_Status").ToString.Trim
                Me._actDescription = dr("Activity_Desc").ToString.Trim

                Dim actStartDate As Date = dr("Activity_Begin_Date")
                Dim actEndDate As Date = dr("Activity_End_Date")

                Me._actStartDate = actStartDate.ToString("MMM/dd/yyyy").Trim
                Me._actEndDate = actEndDate.ToString("MMM/dd/yyyy").Trim
                Me._actTitle = dr("Activity_Title").ToString.Trim

                Return True
            Else
                Return False
            End If

        End Function

    End Class

End Namespace
