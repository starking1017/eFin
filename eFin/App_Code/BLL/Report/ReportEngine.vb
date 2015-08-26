Imports System.IO
Imports System.Threading


Namespace BLL.Report

    Public Class ReportEngine
        Implements BLL.Report.Interface.IReportGenerator


        Private Function IsHtmlToFo(ByRef inputFileType As Integer, ByRef outputFileType As Integer) As Integer

            Return (inputFileType = BLL.Report.Base.eFileType.HTML AndAlso outputFileType = BLL.Report.Base.eFileType.FO)


        End Function



        Private Function IsFoToPDF(ByRef inputFileType As Integer, ByRef outputFileType As Integer) As Integer

            Return (inputFileType = BLL.Report.Base.eFileType.FO AndAlso outputFileType = BLL.Report.Base.eFileType.PDF)


        End Function



        Private Function CheckIfFileExist(ByRef strFullFilePath As String) As Boolean

            'Dim sRoot As String = Server.MapPath("/")
            'Dim sDrive As String = "" + sRoot(0)
            'If File.Exists(MapPath("" + Request.Params("File").ToString)) = False Then
            '    Return (False)
            'End If
            'Return (True)

            If File.Exists(strFullFilePath) = False Then
                Return False
            End If

            Return True


        End Function


        Private Function GetFileStatus(ByRef strFullFilePath As String) As Boolean

            Dim bRet As Boolean = False
            Dim iTimeout As Integer = 0
            '            Dim strFullFilePath As String = outputFile.FilePath.Trim + "\" + outputFile.FileName.Trim

            While bRet = False
                bRet = CheckIfFileExist(strFullFilePath)

                Thread.Sleep(1000)
                System.Math.Min(System.Threading.Interlocked.Increment(iTimeout), iTimeout - 1)
                If iTimeout = 10 Then
                    bRet = False
                    Exit While
                End If

            End While

            Return bRet


        End Function



        Public Function RunHtmlToFo(ByRef inputFile As Derived.InputFile, ByRef outputFile As Derived.outputFile) As Boolean

            Try
                Dim pProcess As System.Diagnostics.Process = New System.Diagnostics.Process
                pProcess.StartInfo.FileName = "html2fo.exe"
                'pProcess.StartInfo.Arguments = "--webpage --quiet " + "" + "" + " --bodyfont Arial " + "--landscape" + " -t pdf14 -f " + strHtmlPath.Trim + strHtmlFileName.Trim + ".pdf " + strHtmlPath.Trim + strHtmlFileName.Trim + ".html"
                pProcess.StartInfo.Arguments = inputFile.FilePath.Trim + "\" + inputFile.FileName.Trim + "   " + outputFile.FilePath.Trim + "\" + outputFile.FileName.Trim
                pProcess.StartInfo.WorkingDirectory = outputFile.FilePath
                pProcess.Start()


                Dim strFullFilePath As String = outputFile.FilePath.Trim + "\" + outputFile.FileName.Trim
                Return GetFileStatus(strFullFilePath)

            Catch ex As Exception

                Return False

            End Try


        End Function


        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Note: 
        ''' To run this application, we need to install JDK 1.3 
        ''' and configure java jar file path 
        '''         
        ''' </summary>
        ''' <param name="inputFile"></param>
        ''' <param name="outputFile"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Bruce Yang]	6/23/2005	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function RunFoToPDF(ByRef inputFile As Derived.InputFile, ByRef outputFile As Derived.outputFile) As Boolean

            Try

                Dim pProcess As System.Diagnostics.Process = New System.Diagnostics.Process
                pProcess.StartInfo.FileName = "fop"
                'pProcess.StartInfo.Arguments = "--webpage --quiet " + "" + "" + " --bodyfont Arial " + "--landscape" + " -t pdf14 -f " + strHtmlPath.Trim + strHtmlFileName.Trim + ".pdf " + strHtmlPath.Trim + strHtmlFileName.Trim + ".html"
                pProcess.StartInfo.Arguments = inputFile.FilePath.Trim + "\" + inputFile.FileName.Trim + "   " + outputFile.FilePath.Trim + "\" + outputFile.FileName.Trim
                pProcess.StartInfo.WorkingDirectory = outputFile.FilePath
                pProcess.Start()

                Dim strFullFilePath As String = outputFile.FilePath.Trim + "\" + outputFile.FileName.Trim
                Return GetFileStatus(strFullFilePath)

            Catch ex As Exception

                Return False

            End Try


        End Function



        'Public Sub ReportGenerator(ByRef inputFile As Derived.InputFile, ByRef outputFile As Derived.outputFile) Implements [Interface].IReportGenerator.ReportGenerator

        '    If Me.IsHtmlToFo(inputFile.FileType, outputFile.FileType) Then

        '        RunHtmlToFo(inputFile, outputFile)

        '    End If

        '    If Me.IsFoToPDF(inputFile.FileType, outputFile.FileType) Then

        '        RunFoToPDF(inputFile, outputFile)

        '    End If

        'End Sub



        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="inputFile"></param>
        ''' <param name="outputFile"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Process Pathways]	6/23/2005	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function ReportGenerator(ByRef inputFile As Derived.InputFile, ByRef outputFile As Derived.outputFile) As Boolean Implements [Interface].IReportGenerator.ReportGenerator


            If Me.IsHtmlToFo(inputFile.FileType, outputFile.FileType) Then

                Return RunHtmlToFo(inputFile, outputFile)

            End If


            If Me.IsFoToPDF(inputFile.FileType, outputFile.FileType) Then

                Return RunFoToPDF(inputFile, outputFile)

            End If





        End Function


    End Class

End Namespace
