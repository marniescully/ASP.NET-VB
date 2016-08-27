﻿Imports System.Data.OleDb
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
        'end Connection String

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=9) AND ((Prod.Shift)=1))"

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

        'Need AS400 import query for SCS
        Dim sqlStrImport As String
        sqlStrImport = ""

        Dim sqlStrDeleteTmpImpSCS As String = "DELETE SCSImportTempTable.* FROM SCSImportTempTable"
        Dim dbConnDeleteTmpImpSCS As New OleDbCommand(sqlStrDeleteTmpImpSCS, dbConnAccess)

        Dim sqlStrSelectNewTable As String = "SELECT * FROM SCSImportTempTable"

        'Tailor this to field names in table and command
        Dim sqlStrInsertImp As String = "INSERT INTO SCSImportTempTable VALUES (@DTE, @EmpID, @BarCode, @Type)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable

        Dim sqlStrFunction As String
        sqlStrFunction = "SELECT Count(Prod.DTE) AS CountOfDTE FROM Prod WHERE (Prod.DTE) = @Date AND (Prod.Shift)=1 and (Prod.Department) =9"

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
            dbConnDeleteTmpImpSCS.ExecuteNonQuery()
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

            'Tailor this to field names in table and command
            Dim insCmd = New OleDbCommand(sqlStrInsertImp, dbConnAccess)

            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "RF_TRANSACTION_DATE"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "USER_STAMP"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@BarCode", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfRF_TRANS_BAR_CODE"})
            insCmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@Type", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "RF_TRANSACTION_TYPE"})


            AccessDB.InsertCommand = insCmd

            Dim Builder As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDB)

            Builder.GetInsertCommand()
            AccessDB.Update(newDataSet, "ProdTable")


        Finally

        End Try

        Dim sqlStrCountImport As String

        'Tailor this to field names in table and command
        sqlStrCountImport = "SELECT Count(SCSImportTempTable.USER_STAMP) AS CountOfUSER_STAMP FROM TMs INNER JOIN SCSImportTempTable ON TMs.EmployeeID = SCSImportTempTable.USER_STAMP GROUP BY TMs.DepartmentID, TMs.ShiftID HAVING (TMs.DepartmentID)=9 AND (TMs.ShiftID)=1"


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




        Dim sqlStr As String
        sqlStr = "INSERT INTO Prod (DTE, M, D, Y, EmployeeID, Amount, Department, Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([SCSImportTempTable].[RF_TRANSACTION_DATE],4,2) AS M, RIGHT([SCSImportTempTable].[RF_TRANSACTION_DATE],2) AS D, Mid([SCSImportTempTable].[RF_TRANSACTION_DATE],2,2) AS Y, SCSImportTempTable.USER_STAMP, SCSImportTempTable.CountOfRF_TRANS_BAR_CODE, 9 AS Department, 1 AS Shift, SCSImportTempTable.RF_TRANSACTION_TYPE FROM TMs INNER JOIN SCSImportTempTable ON TMs.EmployeeID = SCSImportTempTable.USER_STAMP WHERE (((TMs.DepartmentID)=9) AND ((TMs.ShiftID)=1)) ORDER BY SCSImportTempTable.USER_STAMP;"

        Dim dbCommInsertProd As New OleDbCommand(sqlStr, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProd.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try

        SuccessLabel.Text = "Successful Import!"

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=9) AND ((Prod.Shift)=1))"

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
