Imports System.Data.OleDb
Imports System.Web
Imports System.Data
Imports System.Configuration

Partial Class ImportTM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=4) AND ((Prod.Shift)=3))"

        Dim dbCommLastDate As New OleDbCommand(sqlStrLastDate, dbConn)

        Dim LastDate As Date
        Dim testVar As Object

        Try
            dbConn.Open()
            testVar = dbCommLastDate.ExecuteScalar()

            If Not IsDBNull(testVar) Then
                LastDate = dbCommLastDate.ExecuteScalar()
                LastDateImport.Text = "Last Date Imported: " & LastDate

            End If

        Finally
            dbConn.Close()
        End Try


    End Sub

    Protected Sub ImportButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportButton.Click

        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text
        Dim FormattedDate As String = Format(DateText, "MM/dd/yy")

        Dim DateString As String = CStr("1" & Right$((FormattedDate), 2) & Left$((FormattedDate), 2) & Mid$((FormattedDate), 4, 2))

        Dim connectionStringAS400 As String = _
        ConfigurationManager.ConnectionStrings("ConnectionStringAS400").ConnectionString

        Dim connectionStringAccess As String = _
        ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString

        Dim dbConnAS400 As New OleDbConnection(connectionStringAS400)
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)

        'need to insert as400 import query
        Dim sqlStrImport As String
        sqlStrImport = "SELECT PROCESS_CYCLE_DATE, USER_STAMP, COUNT(OUTBOUND_CASE_SCAN_ID) AS CountOfOUTBOUND_CASE_SCAN_ID FROM DIPDLIBB.SL135P GROUP BY PROCESS_CYCLE_DATE, USER_STAMP HAVING (PROCESS_CYCLE_DATE = ?)"

        Dim sqlStrDeleteTmpImpSHIP As String = "DELETE SHIPImportTempTable.* FROM SHIPImportTempTable"
        Dim dbConnDeleteTmpImpSHIP As New OleDbCommand(sqlStrDeleteTmpImpSHIP, dbConnAccess)

        Dim sqlStrSelectNewTable As String = "SELECT * FROM SHIPImportTempTable"

        Dim sqlStrInsertImp As String = "INSERT INTO SHIPImportTempTable VALUES (@DTE, @EmpID, @TagNum,@Funct)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable

        Dim sqlStrFunction As String
        sqlStrFunction = "SELECT Count(Prod.DTE) AS CountOfDTE FROM Prod WHERE (Prod.DTE) = @Date AND (Prod.Shift)=3 AND (Prod.Department)=4"

        Dim RecordCount As Int32 = 0

        Dim dbCommCheckDate As New OleDbCommand(sqlStrFunction, dbConnAccess)
        dbCommCheckDate.Parameters.Add("@Date", OleDbType.VarWChar)
        dbCommCheckDate.Parameters("@Date").Value = DateText

        If DateText = Date.Today Then
            SuccessLabel.Text = "You must wait until at least tomorrow to import today's productivity."
            Exit Sub

        End If


        Try
            dbConnAccess.Open()
            RecordCount = Convert.ToInt32(dbCommCheckDate.ExecuteScalar())

            If RecordCount <> 0 Then

                SuccessLabel.Text = "Productivity Data has already been imported for that date."


                Exit Sub
            End If

        Finally
            dbConnAccess.Close()
        End Try

        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpSHIP.ExecuteNonQuery()
        Finally
            dbConnAccess.Close()
        End Try

        Try

            Dim Prod = New OleDbDataAdapter
            Prod.AcceptChangesDuringFill = "False"

            Prod.SelectCommand = New OleDbCommand(sqlStrImport, dbConnAS400)
            Prod.SelectCommand.Parameters.Add("@Date", OleDbType.VarWChar)
            Prod.SelectCommand.Parameters("@Date").Value = DateString
            Prod.Fill(dataSet, "ProdTable")

            newDataSet = dataSet.Copy()

            Dim SelectCmd = New OleDbCommand(sqlStrSelectNewTable, dbConnAccess)

            Dim AccessDB As New OleDbDataAdapter
            AccessDB.SelectCommand = SelectCmd
            AccessDB.Fill(newDataSet, "ProdTable")


            'need to change field names to match import field names
            Dim insCmd = New OleDbCommand(sqlStrInsertImp, dbConnAccess)

            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "PROCESS_CYCLE_DATE"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "USER_STAMP"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@TagNum", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfOUTBOUND_CASE_SCAN_ID"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@Funct", .OleDbType = OleDbType.VarWChar, .Size = 5, .Value = "S"})

            AccessDB.InsertCommand = insCmd

            Dim Builder As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDB)

            Builder.GetInsertCommand()
            AccessDB.Update(newDataSet, "ProdTable")


        Finally

        End Try


        Dim sqlStrCountImport As String
        sqlStrCountImport = "SELECT Count(SHIPImportTempTable.USER_STAMP) AS CountOfUSER_STAMP FROM TMs INNER JOIN SHIPImportTempTable ON TMs.EmployeeID = SHIPImportTempTable.USER_STAMP GROUP BY TMs.DepartmentID, TMs.ShiftID HAVING(TMs.DepartmentID) = 5 And (TMs.ShiftID) = 3"

        Dim dbCommCheckImportCount As New OleDbCommand(sqlStrCountImport, dbConnAccess)

        Try

            dbConnAccess.Open()
            RecordCount = Convert.ToInt32(dbCommCheckImportCount.ExecuteScalar())

            If RecordCount = 0 Then

                SuccessLabel.Text = "There is no data to import for that date."

                Exit Sub
            End If

        Finally
            dbConnAccess.Close()
        End Try

        'need to change field names to match import table fields
        Dim sqlStr As String
        sqlStr = "INSERT INTO PROD (DTE, M, D, Y, EmployeeID, Amount, Department, Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS DTE, Mid([SHIPImportTempTable].[PROCESS_CYCLE_DATE],4,2) AS M, RIGHT([SHIPImportTempTable].[PROCESS_CYCLE_DATE],2) AS D, Mid([SHIPImportTempTable].[PROCESS_CYCLE_DATE],2,2) AS Y, SHIPImportTempTable.USER_STAMP, SHIPImportTempTable.CountOfOUTBOUND_CASE_SCAN_ID, 4 AS Department, 3 AS Shift, SHIPImportTempTable.FUNCTION FROM TMs INNER JOIN SHIPImportTempTable ON TMs.EmployeeID = SHIPImportTempTable.USER_STAMP WHERE (((TMs.DepartmentID)=5) AND ((TMs.ShiftID)=3)) ORDER BY SHIPImportTempTable.USER_STAMP;"

        Dim dbCommInsertProd As New OleDbCommand(sqlStr, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProd.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try

        SuccessLabel.Text = "Successful Import!"

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=4) AND ((Prod.Shift)=3))"

        Dim dbCommLastDate As New OleDbCommand(sqlStrLastDate, dbConnAccess)

        Dim LastDate As Date

        Try
            dbConnAccess.Open()
            LastDate = dbCommLastDate.ExecuteScalar()
            LastDateImport.Text = "Last Date Imported: " & LastDate

        Finally
            dbConnAccess.Close()
        End Try

    End Sub
    Protected Sub ClearSearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearSearchButton.Click


        txtDate.Text = ""
        SuccessLabel.Text = ""


    End Sub

End Class
