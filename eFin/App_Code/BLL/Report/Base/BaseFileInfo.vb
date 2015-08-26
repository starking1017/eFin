Namespace BLL.Report.Base

    Public Enum eFileType As Integer

        HTML = 1
        FO = 2
        PDF = 3


    End Enum

    Public Class BaseFileInfo



        Private _filepath As String
        Private _filename As String

        Private _filetype As Integer

        Public Sub New(ByRef filepath As String, ByRef filename As String, ByRef filetype As Integer)

            Me._filepath = filepath
            Me._filename = filename
            Me._filetype = filetype


        End Sub

        Public ReadOnly Property FilePath() As String
            Get
                Return Me._filepath
            End Get
        End Property

        Public ReadOnly Property FileType() As Integer
            Get

                Return Me._filetype

            End Get

        End Property


        Public ReadOnly Property FileName() As String
            Get
                Return Me._filename
            End Get
        End Property




    End Class

End Namespace
