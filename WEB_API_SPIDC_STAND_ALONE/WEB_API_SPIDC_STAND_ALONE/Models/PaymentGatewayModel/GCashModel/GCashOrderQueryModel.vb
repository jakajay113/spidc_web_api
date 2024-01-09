Imports System.Data.SqlClient

Public Class GCashOrderQueryModel

#Region "Variables Data"
    Private Shared _mSqlCon As SqlConnection
    Private Shared _mSqlCmd As SqlCommand
    Private Shared _mQuery As String = Nothing
    Private Shared _mSqlDataReader As SqlDataReader
    Private Shared _mDataTable As DataTable
#End Region

    Public Class Head
        Public Property reqTime As String
        Public Property reqMsgId As String
        Public Property clientSecret As String
        Public Property clientId As String
        Public Property _function As String
        Public Property version As String
    End Class

    Public Class Buyer
        Public Property externalUserId As String
        Public Property externalUserType As String
        Public Property userId As String
    End Class

    Public Class Order
        Public Property buyer As Buyer
    End Class


    Public Class Body

        Public Property acquirementId As String
        Public Property shortTransId As String
        Public Property merchantTransId As String

    End Class

    Public Class Request
        Public Property head As Head
        Public Property body As Body
    End Class


    Public Class GCashOrderQuery

        Public Property request As Request
        Public Property signature As String

    End Class


    Public Shared Function Gen_MD5() As String
        Dim result As String = Nothing
        Try
            _mSqlCmd = New SqlCommand("SELECT REPLACE(STR((select count(*) from OnlinePaymentRefno where via='GCASH'),5),' ','0')", Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS)
            _mSqlDataReader = _mSqlCmd.ExecuteReader()

            Do Until _mSqlDataReader.Read = False
                result = _mSqlDataReader.Item(0).ToString()
            Loop
        Catch ex As Exception

        End Try
        _mSqlCmd.Dispose()
        _mSqlDataReader.Close()
        Return result
    End Function


End Class
