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

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=1) AND ((Prod.Shift)=3))"

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

        If Not IsPostBack Then

            Dim TodaysDate As Date = DateTime.Today()
            Dim SugImpDate As Date
            Dim txtDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")

            If TodaysDate.DayOfWeek = DayOfWeek.Monday Then
                SugImpDate = DateTime.Today.AddDays(-3)
            Else
                SugImpDate = DateTime.Today.AddDays(-1)
            End If

            txtDate.Text = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            CalendarExtender.EndDate = FormatDateTime(SugImpDate, DateFormat.ShortDate)

        End If

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

        Dim sqlStrImport As String
        sqlStrImport = "SELECT DIPDLIB.DI066P.ACTUAL_DATE_TAG_PRINTED, DIPDLIB.DI066P.RECEIVER_EMPLOYEE_ID, DIPDLIB.DI032P.SHIPMENT_CHARACTERISTICS, Count(DIPDLIB.DI066P.PALLET_TAG_NBR) AS CountOfPALLET_TAG_NBR, Sum(DIPDLIB.DI066P.RCVD_QTY_CASES) AS SumOfRCVD_QTY_CASES FROM (DIPDLIB.DI066P INNER JOIN DIPDLIB.DI028P ON DIPDLIB.DI066P.FREIGHT_BILL_NUMBER = DIPDLIB.DI028P.FREIGHT_BILL_NUMBER) INNER JOIN DIPDLIB.DI032P ON DIPDLIB.DI028P.INBOUND_SHIPMENT_NBR = DIPDLIB.DI032P.INBOUND_SHIPMENT_NBR GROUP BY DIPDLIB.DI066P.ACTUAL_DATE_TAG_PRINTED, DIPDLIB.DI066P.RECEIVER_EMPLOYEE_ID, DIPDLIB.DI032P.SHIPMENT_CHARACTERISTICS HAVING (((DIPDLIB.DI066P.ACTUAL_DATE_TAG_PRINTED)=?) AND ((DIPDLIB.DI032P.SHIPMENT_CHARACTERISTICS)<>' '))ORDER BY DIPDLIB.DI066P.RECEIVER_EMPLOYEE_ID"


        Dim sqlStrDeleteTmpImpREC As String = "DELETE RECImportTempTable.* FROM RECImportTempTable"
        Dim dbConnDeleteTmpImpREC As New OleDbCommand(sqlStrDeleteTmpImpREC, dbConnAccess)

        Dim sqlStrSelectNewTable As String = "SELECT * FROM RECImportTempTable"

        Dim sqlStrInsertImp As String = "INSERT INTO RECImportTempTable VALUES (@DTE, @EmpID, @TagNum, @Type, @SumCases)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable


        Dim sqlStrFunction As String
        sqlStrFunction = "SELECT Count(Prod.DTE) AS CountOfDTE FROM Prod WHERE (Prod.DTE) = @Date AND (Prod.Shift)=3 AND (Prod.Department)=1"

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

                SuccessLabel.Text = "Productivity has already been imported for " & DateText & "."


                Exit Sub
            End If

        Finally
            dbConnAccess.Close()
        End Try

        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpREC.ExecuteNonQuery()
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

            Dim insCmd = New OleDbCommand(sqlStrInsertImp, dbConnAccess)

            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "ACTUAL_DATE_TAG_PRINTED"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "RECEIVER_EMPLOYEE_ID"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@TagNum", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfPALLET_TAG_NBR"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@Type", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "SHIPMENT_CHARACTERISTICS"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@SumCases", .OleDbType = OleDbType.VarWChar, .Size = 10, .SourceColumn = "SumOfRCVD_QTY_CASES"})

            AccessDB.InsertCommand = insCmd

            Dim Builder As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDB)

            Builder.GetInsertCommand()
            AccessDB.Update(newDataSet, "ProdTable")


        Finally

        End Try

        Dim sqlStrCountImport As String
        sqlStrCountImport = "SELECT Count(RECImportTempTable.RECEIVER_EMPLOYEE_ID) AS CountOfRECEIVER_EMPLOYEE_ID FROM TMs INNER JOIN RECImportTempTable ON TMs.EmployeeID = RECImportTempTable.RECEIVER_EMPLOYEE_ID GROUP BY TMs.DepartmentID, TMs.ShiftID HAVING(TMs.DepartmentID) = 1 And (TMs.ShiftID) = 3"

        Dim dbCommCheckImportCount As New OleDbCommand(sqlStrCountImport, dbConnAccess)

        Try

            dbConnAccess.Open()
            RecordCount = Convert.ToInt32(dbCommCheckImportCount.ExecuteScalar())

            If RecordCount = 0 Then

                SuccessLabel.Text = "There is no data to import for " & DateText & "."

                Exit Sub
            End If

        Finally
            dbConnAccess.Close()
        End Try


        Dim sqlStrCheckFunctions As String
        sqlStrCheckFunctions = "SELECT Count(RECImportTempTable.SHIPMENT_CHARACTERISTICS) AS CountOfSHIPMENT_CHARACTERISTICS FROM TMs INNER JOIN RECImportTempTable ON TMs.EmployeeID = RECImportTempTable.RECEIVER_EMPLOYEE_ID WHERE (((Exists (SELECT * FROM Functions WHERE Functions.Department = 1 and Functions.Function = RecImportTempTable.SHIPMENT_CHARACTERISTICS))=False) AND ((TMs.DepartmentID)=1) AND ((TMs.ShiftID)=3))"


        Dim dbCommCheckFunctions As New OleDbCommand(sqlStrCheckFunctions, dbConnAccess)

        Try

            dbConnAccess.Open()

            RecordCount = dbCommCheckFunctions.ExecuteScalar


            If RecordCount <> 0 Then

                SuccessLabel.Text = "You have tried to import a function that is not on the function list for this department. Please check your function types on As/400 before importing again."

                Exit Sub
            End If

        Finally
            dbConnAccess.Close()
        End Try



        Dim sqlStr As String
        sqlStr = "INSERT INTO Prod ( DTE, M, D, Y, EmployeeID, Amount, Tags, Department, Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([RECImportTempTable].[ACTUAL_DATE_TAG_PRINTED],4,2) AS M, RIGHT([RECImportTempTable].[ACTUAL_DATE_TAG_PRINTED],2) AS D, Mid([RECImportTempTable].[ACTUAL_DATE_TAG_PRINTED],2,2) AS Y, RECImportTempTable.RECEIVER_EMPLOYEE_ID, RECImportTempTable.SumOfRCVD_QTY_CASES, RECImportTempTable.CountOfPALLET_TAG_NBR, 1 AS Department, 3 AS Shift, RECImportTempTable.SHIPMENT_CHARACTERISTICS FROM TMs INNER JOIN RECImportTempTable ON TMs.EmployeeID = RECImportTempTable.RECEIVER_EMPLOYEE_ID WHERE (((TMs.DepartmentID)=1) AND ((TMs.ShiftID)=3)) ORDER BY RECImportTempTable.RECEIVER_EMPLOYEE_ID"

        Dim dbCommInsertProd As New OleDbCommand(sqlStr, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProd.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try

        SuccessLabel.Text = "Successful Import!"
        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=1) AND ((Prod.Shift)=3))"

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
