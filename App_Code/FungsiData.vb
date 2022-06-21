Imports System.Data.SqlClient

Public Class FungsiData
    Dim xConn As New SqlConnection(ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString)
    Dim xReader As System.Data.IDataReader

    Public Function MyReader(ByVal xSql As String) As System.Data.IDataReader
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand(xSql, xConn)
        xReader = xCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
        Return xReader
    End Function

    Public Function countData(ByVal xTable As String, ByVal xField As String, ByVal xCondition As String) As Integer
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand("select ISNULL(count(" & xField & "),0) " & " From " & xTable & " " & xCondition, xConn)
        Dim xCount As Integer = 0
        xCount = xCommand.ExecuteScalar
        xConn.Close()
        Return xCount
    End Function

    Public Function sumData(ByVal xTable As String, ByVal xField As String, ByVal xCondition As String) As Integer
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand("select ISNULL(sum(" & xField & "),0) " & " From " & xTable & " " & xCondition, xConn)
        Dim xSum As Integer = 0
        xSum = xCommand.ExecuteScalar
        xConn.Close()
        Return xSum
    End Function

    Public Function avgData(ByVal xTable As String, ByVal xField As String, ByVal xCondition As String) As Integer
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand("select ISNULL(avg(" & xField & "),0) " & " From " & xTable & " " & xCondition, xConn)
        Dim xAvg As Integer = 0
        xAvg = xCommand.ExecuteScalar
        xConn.Close()
        Return xAvg
    End Function

    Public Function minData(ByVal xTable As String, ByVal xField As String, ByVal xCondition As String) As Integer
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand("select min(" & xField & ") " & " From " & xTable & " " & xCondition, xConn)
        Dim xMin As Integer = 0
        xMin = xCommand.ExecuteScalar
        xConn.Close()
        Return xMin
    End Function

    Public Function maxData(ByVal xTable As String, ByVal xField As String, ByVal xCondition As String) As Integer
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand("select max(" & xField & ") " & " From " & xTable & " " & xCondition, xConn)
        Dim xMax As Integer = 0
        xMax = xCommand.ExecuteScalar
        xConn.Close()
        Return xMax
    End Function

    Public Function delData(ByVal xTable As String, ByVal xCondition As String) As Integer
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand("delete " & " From " & xTable & " " & xCondition, xConn)
        Dim xDel As Integer = 0
        xDel = xCommand.ExecuteScalar
        xConn.Close()
        Return xDel
    End Function

    Public Function insertData(ByVal xCondition As String) As Integer
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand As New SqlCommand(xCondition, xConn)
        Dim xNilai As Integer = 0
        xNilai = xCommand.ExecuteScalar
        xConn.Close()
        Return xNilai
    End Function

    Public Function checkData(ByVal xSql As String) As Boolean
        Dim Reader As System.Data.IDataReader = MyReader(xSql)
        Return Reader.Read
    End Function

    Public Sub UpdateData(ByVal xTabel As String, ByVal xField As String, ByVal xCondition As String)
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand1 As New SqlCommand("update " & xTabel & " set " & xField & " where " & xCondition, xConn)
        xCommand1.ExecuteNonQuery()
    End Sub

    Public Function GetData(ByVal xField As String, ByVal xTabel As String, ByVal xCondition As String) As String
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xData As String = ""
        Dim xCommand1 As New SqlCommand("Select " & xField & " From " & xTabel & " where " & xCondition, xConn)
        Dim xReader As SqlDataReader = xCommand1.ExecuteReader()
        If xReader.HasRows Then
            While xReader.Read()
                xData = xReader(xField).ToString
                Exit While
            End While
        Else
            xData = "-"
        End If
        xConn.Close()
        Return xData
    End Function

    Public Function GetDataSet(ByVal xSql As String) As System.Data.DataSet
        If xConn.State = System.Data.ConnectionState.Closed Then xConn.Open()
        Dim xCommand1 As New SqlCommand(xSql, xConn)
        Dim xDa As New SqlDataAdapter
        Dim xDs As New System.Data.DataSet
        xDa.SelectCommand = xCommand1
        xDa.Fill(xDs)
        Dim xDataset As System.Data.DataSet
        xDataset = xDs.Copy
        Return xDataset
    End Function


End Class
