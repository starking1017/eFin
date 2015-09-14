Namespace BLL

    Public Class FinancialSummary

        Public Shared Function LoadFinancialSummary(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.FinancialSummaryDB.LoadFinancialSummaryDB(DBOperation, SP_Name, arrParams)

        End Function

        Public Shared Function convertDate(ByRef financialSummaryDate As String, ByRef type As String) As String
            Dim strDate As String()

            strDate = financialSummaryDate.Split("/")

            Select Case strDate(1)
                Case "01"
                    If type = "start" Then
                        Return "January 01, " + strDate(0)
                    Else
                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "January " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "January 31, " + strDate(0)
                        End If
                    End If

                Case "02"
                    If type = "start" Then
                        Return "February 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "February " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "February 28, " + strDate(0)
                        End If
                    End If
                Case "03"
                    If type = "start" Then
                        Return "March 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "March " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "March 31, " + strDate(0)
                        End If
                    End If
                Case "04"
                    If type = "start" Then
                        Return "April 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "April " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "April 30, " + strDate(0)
                        End If
                    End If
                Case "05"
                    If type = "start" Then
                        Return "May 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "May " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "May 31, " + strDate(0)
                        End If
                    End If
                Case "06"
                    If type = "start" Then
                        Return "June 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "June " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "June 30, " + strDate(0)
                        End If
                    End If
                Case "07"
                    If type = "start" Then
                        Return "July 01, " + strDate(0)
                    Else
                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "July " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "July 31, " + strDate(0)
                        End If
                    End If
                Case "08"
                    If type = "start" Then
                        Return "August 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "August " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "August 31, " + strDate(0)
                        End If
                    End If
                Case "09"
                    If type = "start" Then
                        Return "September 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "September " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "September 30, " + strDate(0)
                        End If
                    End If
                Case "10"
                    If type = "start" Then
                        Return "October 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "October " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "October 31, " + strDate(0)
                        End If
                    End If
                Case "11"
                    If type = "start" Then
                        Return "November 01, " + strDate(0)
                    Else
                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "November " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "November 30, " + strDate(0)
                        End If
                    End If
                Case "12"
                    If type = "start" Then
                        Return "December 01, " + strDate(0)
                    Else

                        If Integer.Parse(strDate(1)) = Now.Month And Integer.Parse(strDate(0)) = Now.Year Then
                            Return "December " + BLL.Header.GetAsAtDate().Day.ToString + ", " + strDate(0)
                        Else
                            Return "December 31, " + strDate(0)
                        End If
                    End If
                Case Else
                    Return ""
            End Select
        End Function


    End Class

End Namespace
