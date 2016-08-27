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

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=8) AND ((Prod.Shift)=1))"

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

        'Need to add as400 import sql
        Dim sqlStrImport As String
        sqlStrImport = ""


        Dim sqlStrDeleteTmpImpSCP As String = "DELETE SCPImportTempTable.* FROM SCPImportTempTable"
        Dim dbConnDeleteTmpImpSCP As New OleDbCommand(sqlStrDeleteTmpImpSCP, dbConnAccess)

        Dim sqlStrSelectNewTable As String = "SELECT * FROM SCPImportTempTable"

        'Tailor this to field names in table and command
        Dim sqlStrInsertImp As String = "INSERT INTO SCPImportTempTable VALUES (@DTE, @EmpID, @TagNum, @Type)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable

        If DateText = Date.Today Then
            SuccessLabel.Text = "You must wait until at least tomorrow to import today's productivity."
            Exit Sub

        End If

        Dim sqlStrFunction As String
        sqlStrFunction = "SELECT Count(PROD.DTE) AS CountOfDTE FROM PROD WHERE (PROD.DTE) = @Date AND (PROD.Department)=8 AND (PROD.Shift)=1"

        Dim RecordCount As Int32 = 0

        Dim dbCommCheckDate As New OleDbCommand(sqlStrFunction, dbConnAccess)
        dbCommCheckDate.Parameters.Add("@Date", OleDbType.VarWChar)
        dbCommCheckDate.Parameters("@Date").Value = DateText

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
            dbConnDeleteTmpImpSCP.ExecuteNonQuery()
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


            'need to tailor insert command parameters for actual column names in sqlStrImport
            Dim insCmd = New OleDbCommand(sqlStrInsertImp, dbConnAccess)

            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "ACTUAL_DATE_TAG_PRINTED"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "RECEIVER_EMPLOYEE_ID"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@TagNum", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfPALLET_TAG_NBR"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@Type", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "SHIPMENT_CHARACTERISTICS"})


            AccessDB.InsertCommand = insCmd

            Dim Builder As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDB)

            Builder.GetInsertCommand()
            AccessDB.Update(newDataSet, "ProdTable")


        Finally

        End Try

        'need to edit field names to match fields in SCP table 
        Dim sqlStrCountImport As String
        sqlStrCountImport = "SELECT Count(SCPImportTempTable.RECEIVER_EMPLOYEE_ID) AS CountOfRECEIVER_EMPLOYEE_ID, SCPImportTempTable.CountOfPALLET_TAG_NBR, SCPImportTempTable.SHIPMENT_CHARACTERISTICS FROM TMs INNER JOIN SCPImportTempTable ON TMs.EmployeeID = SCPImportTempTable.RECEIVER_EMPLOYEE_ID GROUP BY SCPImportTempTable.CountOfPALLET_TAG_NBR, SCPImportTempTable.SHIPMENT_CHARACTERISTICS, TMs.DepartmentID, TMs.ShiftID HAVING(TMs.DepartmentID) = 8 And (TMs.ShiftID) = 1"

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

        'need to edit field names to match field names in import table
        Dim sqlStr As String
        sqlStr = "INSERT INTO PROD ( DTE, M, D, Y, EmployeeID, Amount, Department, Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([SCPImportTempTable].[ACTUAL_DATE_TAG_PRINTED],4,2) AS M, RIGHT([SCPImportTempTable].[ACTUAL_DATE_TAG_PRINTED],2) AS D, Mid([SCPImportTempTable].[ACTUAL_DATE_TAG_PRINTED],2,2) AS Y, SCPImportTempTable.RECEIVER_EMPLOYEE_ID, SCPImportTempTable.CountOfPALLET_TAG_NBR, 8 AS Department, 1 AS Shift, SCPImportTempTable.SHIPMENT_CHARACTERISTICS FROM TMs INNER JOIN SCPImportTempTable ON TMs.EmployeeID = SCPImportTempTable.RECEIVER_EMPLOYEE_ID WHERE (((TMs.DepartmentID)=8) AND ((TMs.ShiftID)=1)) ORDER BY SCPImportTempTable.RECEIVER_EMPLOYEE_ID;"

        Dim dbCommInsertProd As New OleDbCommand(sqlStr, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProd.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try

        SuccessLabel.Text = "Successful Import!"

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=8) AND ((Prod.Shift)=1))"

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
