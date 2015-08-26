#Region " Bruce Yang, Copyright "
'**********************************************************************
'* Generic Database Operation : SQL Command Line , Stored Procedures 
'*
'*	Copyright (c) 2003-2005 Bruce Yang.  All Rights Reserved
'*
'**********************************************************************
#End Region
Imports System.Data
Imports System.Data.SqlClient


Namespace DAL

    Public Class GenericDBOperation



        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="DbOperation"></param>
        ''' <param name="strRawSQL"></param>
        ''' <param name="strFilterCondition"></param>
        ''' <param name="arrParams"></param>
        ''' <param name="arrValues"></param>
        ''' <param name="strOrderBy"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Bruce Yang]	6/8/2005	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Shared Function GenericDataBaseSQLCommandOperation(ByRef DbOperation As String, ByRef strRawSQL As String, ByRef strFilterCondition As String, Optional ByRef arrParams As ArrayList = Nothing, Optional ByRef arrValues As ArrayList = Nothing, Optional ByRef strOrderBy As String = "") As Object

            '            Dim CONN_STRING As String = utils.getSqlConnectionString()
            'Dim cn As SqlConnection = New SqlConnection(Utils.getSqlConnectionString())

            Const CONN_STRING As String = "Server=(local);UID=bruce;PWD=bruce;Database=testdb"

            Dim strConn As String = CONN_STRING


            Dim cn As SqlConnection = New SqlConnection(CONN_STRING)

            Dim strSQL As String = String.Empty
            Dim ds As DataSet

            If Not arrParams Is Nothing AndAlso Not arrValues Is Nothing Then

                Dim paramsToStore(arrParams.Count - 1) As SqlParameter
                Dim storedParams(arrParams.Count - 1) As SqlParameter

                strFilterCondition = String.Format(strFilterCondition, arrParams(0), arrParams(1))

                If strOrderBy = "" Then

                    strSQL = strRawSQL + " where " + strFilterCondition

                Else

                    strSQL = strRawSQL + " where " + strFilterCondition + " " + strOrderBy

                End If

                storedParams = DAL.SqlHelperParameterCache.GetCachedParameterSet( _
                                                                 strConn, strSQL)

                If storedParams Is Nothing Then

                    For i As Integer = 0 To arrParams.Count - 1

                        Dim strSqlCmdParameter As String = CType(arrParams(i), String)
                        paramsToStore(i) = New SqlParameter(strSqlCmdParameter, arrValues(i).GetType)

                    Next

                    DAL.SqlHelperParameterCache.CacheParameterSet(strConn, _
                                                              strSQL, _
                                                              paramsToStore)

                    storedParams = DAL.SqlHelperParameterCache.GetCachedParameterSet( _
                                                     strConn, strSQL)

                    If Not storedParams Is Nothing Then

                        For i As Integer = 0 To arrParams.Count - 1

                            storedParams(i).Value = arrValues(i)

                        Next


                    End If


                Else ' if not nothing, it means in the database sql command cache, we can find these parameters

                    For i As Integer = 0 To arrParams.Count - 1

                        storedParams(i).Value = arrValues(i)

                    Next


                End If


                ''finally, we need to execute sql here based  on different parameter 
                Select Case DbOperation

                    Case "SELECT"

                        Try

                            ds = DAL.SqlHelper.ExecuteDataset(cn, _
                            CommandType.Text, _
                         strSQL, storedParams)

                            Return ds.Tables(0)

                        Catch ex As Exception

                            Return Nothing

                        End Try

                    Case "SCALAR"


                        Dim nCount As Integer
                        Try

                            nCount = Integer.Parse(DAL.SqlHelper.ExecuteScalar(cn, _
        System.Data.CommandType.Text, strSQL, storedParams))

                        Catch ex As Exception

                            Return -1

                        End Try


                    Case "AVERAGE"



                    Case "UPDATE", "INSERT", "DELETE"

                End Select



            Else ' if not set any parameters , only execute SQL 


                If strOrderBy = "" Then

                    strSQL = strRawSQL

                Else

                    strSQL = strRawSQL + " " + strOrderBy

                End If

                Select Case DbOperation

                    Case "SELECT"

                        Try

                            ds = DAL.SqlHelper.ExecuteDataset(strConn, _
                            CommandType.Text, _
                            strSQL)

                        Catch ex As Exception

                            Return Nothing

                        End Try

                    Case "SCALAR"


                        Dim nCount As Integer
                        Try

                            nCount = Integer.Parse(DAL.SqlHelper.ExecuteScalar(cn, _
        System.Data.CommandType.Text, strSQL))

                        Catch ex As Exception

                            Return -1

                        End Try


                    Case "AVERAGE"


                    Case "UPDATE", "INSERT", "DELETE"


                End Select




            End If








        End Function





        Public Shared Function GenericDataBaseOperationDB(ByRef DbOperation As String, ByRef sp_Name As String, Optional ByRef arrParams As ArrayList = Nothing) As Object

            'Const CONN_STRING As String = "Server=PPdev4;UID=law;PWD=law;Database=law"
            Dim CONN_STRING As String = Utils.getSqlConnectionString()
            'Dim cn As SqlConnection = New SqlConnection(Utils.getSqlConnectionString())

            Dim cn As SqlConnection = New SqlConnection(CONN_STRING)

            Dim ds As DataSet

            Dim spName As String = sp_Name

            Dim storedParams() As SqlParameter
            storedParams = SqlHelperParameterCache.GetSpParameterSet(CONN_STRING, spName)

            Try

                cn.Open()

                Select Case DbOperation

                    Case "SELECT"

                        If arrParams Is Nothing Then

                            ds = DAL.SqlHelper.ExecuteDataset( _
                                                        cn, _
                                                        System.Data.CommandType.StoredProcedure, _
                                                       spName)

                            Return ds.Tables(0)


                        Else

                            Dim obj As Object
                            Dim i As Integer = 0

                            For Each obj In arrParams

                                storedParams(i).Value = obj
                                i += 1

                            Next

                            ds = DAL.SqlHelper.ExecuteDataset( _
                                                        cn, _
                                                        System.Data.CommandType.StoredProcedure, _
                                                        spName, storedParams)
                            Return ds.Tables(0)
                        End If




                    Case "SCALAR"

                        Dim nCount As Integer
                        Try

                            If arrParams Is Nothing Then

                                nCount = Integer.Parse(DAL.SqlHelper.ExecuteScalar( _
                                                            cn, _
                                                            System.Data.CommandType.StoredProcedure, _
                                                           spName))

                            Else

                                Dim obj As Object
                                Dim i As Integer = 0

                                For Each obj In arrParams

                                    storedParams(i).Value = obj

                                    i += 1
                                Next

                                nCount = Integer.Parse(DAL.SqlHelper.ExecuteScalar( _
                                                            cn, _
                                                            System.Data.CommandType.StoredProcedure, _
                                                            spName, storedParams))


                            End If

                            Return nCount

                        Catch ex As Exception

                            Return -1

                        End Try



                        'Added by Bruce Yang, Feb 10, 2005 
                    Case "AVERAGE"

                        Dim dAvg As Double

                        Try

                            If arrParams Is Nothing Then

                                dAvg = Double.Parse(DAL.SqlHelper.ExecuteScalar( _
                                                            cn, _
                                                            System.Data.CommandType.StoredProcedure, _
                                                           spName))

                            Else

                                Dim obj As Object
                                Dim i As Integer = 0

                                For Each obj In arrParams

                                    storedParams(i).Value = obj

                                    i += 1
                                Next

                                dAvg = Double.Parse(DAL.SqlHelper.ExecuteScalar( _
                                                            cn, _
                                                            System.Data.CommandType.StoredProcedure, _
                                                            spName, storedParams))


                            End If

                            Return dAvg

                        Catch ex As Exception

                            Return -1

                        End Try





                    Case "UPDATE", "INSERT", "DELETE"

                        Dim obj As Object
                        Dim i As Integer = 0
                        For Each obj In arrParams
                            storedParams(i).Value = obj
                            i += 1
                        Next

                        Try
                            DAL.SqlHelper.ExecuteNonQuery( _
                                cn, _
                                System.Data.CommandType.StoredProcedure, _
                             spName, storedParams)
                            Return True
                        Catch ex As Exception
                            Return False
                        End Try


                        'There are some problems which we need to take care 

                    Case "UPDATE_TRANS", "INSERT_TRANS", "DELETE_TRANS"

                        'Modified by Bruce Yang, Feb 2, 2005 
                        'To set transaction against the opening connection 

                        Dim transaction As SqlTransaction

                        Dim obj As Object
                        Dim i As Integer = 0
                        For Each obj In arrParams
                            storedParams(i).Value = obj
                            i += 1
                        Next

                        Try

                            transaction = cn.BeginTransaction


                            DAL.SqlHelper.ExecuteNonQuery(transaction.Connection, _
                                                                System.Data.CommandType.StoredProcedure, _
                             spName, storedParams)

                            transaction.Commit()

                            Return True

                        Catch ex As Exception

                            transaction.Rollback()
                            Return False

                        Finally

                            If Not transaction Is Nothing Then
                                transaction.Dispose()

                            End If

                        End Try


                End Select

            Catch ex As Exception

                Dim strErr As String = ex.Message

                Return Nothing
            Finally
                If Not cn Is Nothing Then
                    cn.Close()
                    cn.Dispose()
                End If
            End Try


        End Function

    End Class


End Namespace
