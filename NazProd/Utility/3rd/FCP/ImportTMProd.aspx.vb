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

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=2) AND ((Prod.Shift)=3))"

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

    Private Sub ImportFCPMTV()

        'Code for formatting the date for DB and changing it AS400 format for queries
        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text
        Dim FormattedDate As String = Format(DateText, "MM/dd/yy")
        Dim DateString As String = CStr("1" & Right$((FormattedDate), 2) & Left$((FormattedDate), 2) & Mid$((FormattedDate), 4, 2))
        'end Date formatting code

        'Connection Strings to AS400
        Dim connectionStringAS400 As String = _
        ConfigurationManager.ConnectionStrings("ConnectionStringAS400").ConnectionString
        Dim dbConnAS400 As New OleDbConnection(connectionStringAS400)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        'Import Mount Vernon sql
        Dim sqlStrImportMTV As String
        sqlStrImportMTV = "SELECT PROCESS_CYCLE_DTE, USER_STAMP, COUNT(OUTBOUND_CASE_SCAN_ID) AS CountOfOUTBOUND_CASE_SCAN_ID, DISTRIBUTION_CENTER_NBR FROM DIPDLIBB.DI216P AND (DIPDLIBB.DI842P.WAVE_NUMBER <= 18)  GROUP BY DISTRIBUTION_CENTER_NBR, USER_STAMP, PROCESS_CYCLE_DTE HAVING (DISTRIBUTION_CENTER_NBR = '88008') AND (PROCESS_CYCLE_DTE = ?) ORDER BY PROCESS_CYCLE_DTE"


        'Update Temp Table to  Mount Vernon sql
        Dim sqlStrUpdateMTV As String
        sqlStrUpdateMTV = "UPDATE FCPMTVImportTempTable SET FCPMTVImportTempTable.DISTRIBUTION_CENTER_NBR = 'MTV'"

        'Delete MTVTemp Import table
        Dim sqlStrDeleteTmpImpMTV As String = "DELETE FCPMTVImportTempTable.* FROM FCPMTVImportTempTable"
        Dim dbConnDeleteTmpImpMTV As New OleDbCommand(sqlStrDeleteTmpImpMTV, dbConnAccess)


        'Open FCP-MTV Import table in Access
        Dim sqlStrSelectNewTableMTV As String = "SELECT FCPMTVImportTempTable.* FROM FCPMTVImportTempTable"

        'Add Data to MTV import table
        Dim sqlStrInsertImpMTV As String = "INSERT INTO FCPMTVImportTempTable VALUES (@DTE, @EmpID, @TagNum, @Type)"


        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable


        'Deleting MTV import table
        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpMTV.ExecuteNonQuery()
        Finally
            dbConnAccess.Close()
        End Try


        'Import MTV
        Try

            Dim ProdMTV = New OleDbDataAdapter
            ProdMTV.AcceptChangesDuringFill = "False"

            ProdMTV.SelectCommand = New OleDbCommand(sqlStrImportMTV, dbConnAS400)
            ProdMTV.SelectCommand.Parameters.Add("@Date", OleDbType.VarWChar)
            ProdMTV.SelectCommand.Parameters("@Date").Value = DateString
            ProdMTV.Fill(dataSet, "ProdTable")

            newDataSet = dataSet.Copy()

            Dim SelectCmdMTV = New OleDbCommand(sqlStrSelectNewTableMTV, dbConnAccess)

            Dim AccessDBMTV As New OleDbDataAdapter
            AccessDBMTV.SelectCommand = SelectCmdMTV
            AccessDBMTV.Fill(newDataSet, "ProdTable")

            Dim insCmdMTV = New OleDbCommand(sqlStrInsertImpMTV, dbConnAccess)

            insCmdMTV.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "PROCESS_CYCLE_DATE"})
            insCmdMTV.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "USER_STAMP"})
            insCmdMTV.Parameters.Add(New OleDbParameter With {.ParameterName = "@TagNum", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfOUTBOUND_CASE_SCAN_ID"})
            insCmdMTV.Parameters.Add(New OleDbParameter With {.ParameterName = "@Type", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "DISTRIBUTION_CENTER_NBR"})


            AccessDBMTV.InsertCommand = insCmdMTV

            Dim BuilderMTV As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDBMTV)

            BuilderMTV.GetInsertCommand()
            AccessDBMTV.Update(newDataSet, "ProdTable")


        Finally

        End Try

        ' Update Query to change Area Class ID to MTV

        Dim dbCommUpdateProdMTV As New OleDbCommand(sqlStrUpdateMTV, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommUpdateProdMTV.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try


        'Import updated Temp Table into Productivity table in Access
        Dim sqlStrMTV As String
        sqlStrMTV = "INSERT INTO PROD ( DTE, M, D, Y, EmployeeID, Amount, Department, Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([FCP-MTVImportTempTable].[PROCESS_CYCLE_DTE],4,2) AS M, RIGHT([FCP-MTVImportTempTable].[PROCESS_CYCLE_DTE],2) AS D, Mid([FCP-MTVImportTempTable].[PROCESS_CYCLE_DTE],2,2) AS Y, FCP-MTVImportTempTable.USER_STAMP, FCP-MTVImportTempTable.CountOfOUTBOUND_CASE_SCAN_ID_ATR, 2 AS Department, 3 AS Shift, FCP-MTVImportTempTable.DISTRIBUTION_CENTER_NBR FROM TMs INNER JOIN FCP-MTVImportTempTable ON TMs.EmployeeID = FCP-MTVImportTempTable.USER_STAMP WHERE (((TMs.DepartmentID)=5) AND ((TMs.ShiftID)=3)) ORDER BY FCP-MTVImportTempTable.USER_STAMP"

        Dim dbCommInsertProdMTV As New OleDbCommand(sqlStrMTV, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProdMTV.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try


        'End of Import Code Block



    End Sub

    Private Sub ImportFCP()

        'Code for formatting the date for DB and changing it AS400 format for queries
        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text
        Dim FormattedDate As String = Format(DateText, "MM/dd/yy")
        Dim DateString As String = CStr("1" & Right$((FormattedDate), 2) & Left$((FormattedDate), 2) & Mid$((FormattedDate), 4, 2))
        'end Date formatting code

        'Connection Strings to AS400
        Dim connectionStringAS400 As String = _
        ConfigurationManager.ConnectionStrings("ConnectionStringAS400").ConnectionString
        Dim dbConnAS400 As New OleDbConnection(connectionStringAS400)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        'Import FCP sql
        Dim sqlStrImportFCP As String
        sqlStrImportFCP = "SELECT DIPDLIBB.DI842P.PROCESS_CYCLE_DATE, DIPDLIBB.DI842P.ACTUAL_PICKER_ID, Count(DIPDLIBB.DI842P.CASE_SCAN_ID_ATR) AS CountOfCASE_SCAN_ID_ATR FROM DIPDLIBB.DI210P INNER JOIN DIPDLIBB.DI842P ON (DIPDLIBB.DI210P.TIER = DIPDLIBB.DI842P.TIER_ATR) AND (DIPDLIBB.DI210P.SECTION_CDE = DIPDLIBB.DI842P.SECTION_ATR) AND (DIPDLIBB.DI210P.AISLE = DIPDLIBB.DI842P.AISLE_ATR) WHERE (((DIPDLIBB.DI842P.ACTUAL_PICKER_ID)<>' ') AND ((DIPDLIBB.DI210P.AREA_CLASS_ID)<>'TR')) GROUP BY DIPDLIBB.DI842P.PROCESS_CYCLE_DATE, DIPDLIBB.DI842P.ACTUAL_PICKER_ID HAVING (((DIPDLIBB.DI842P.PROCESS_CYCLE_DATE)=?) AND ((DIPDLIBB.DI842P.ACTUAL_PICKER_ID) Like 'B%')) ORDER BY DIPDLIBB.DI842P.ACTUAL_PICKER_ID"



        'Delete FCP Temp Import table
        Dim sqlStrDeleteTmpImpFCP As String = "DELETE FCPImportTempTable.* FROM FCPImportTempTable"
        Dim dbConnDeleteTmpImpFCP As New OleDbCommand(sqlStrDeleteTmpImpFCP, dbConnAccess)

        'Open FCP Import table in Access
        Dim sqlStrSelectNewTableFCP As String = "SELECT FCPImportTempTable.* FROM FCPImportTempTable"

        'Add Data to FCP Import Table
        Dim sqlStrInsertImpFCP As String = "INSERT INTO FCPImportTempTable VALUES (@DTE, @EmpID, @TagNum)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable

        'Delete FCP Import Table
        'Need to repeat code for each time importing into FCP temp table
        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpFCP.ExecuteNonQuery()
        Finally
            dbConnAccess.Close()
        End Try

        'Import-FCP
        Try

            Dim ProdFCP = New OleDbDataAdapter
            ProdFCP.AcceptChangesDuringFill = "False"

            ProdFCP.SelectCommand = New OleDbCommand(sqlStrImportFCP, dbConnAS400)
            ProdFCP.SelectCommand.Parameters.Add("@Date", OleDbType.VarWChar)
            ProdFCP.SelectCommand.Parameters("@Date").Value = DateString
            ProdFCP.Fill(dataSet, "ProdTable")

            newDataSet = dataSet.Copy()

            Dim SelectCmdFCP = New OleDbCommand(sqlStrSelectNewTableFCP, dbConnAccess)

            Dim AccessDBFCP As New OleDbDataAdapter
            AccessDBFCP.SelectCommand = SelectCmdFCP
            AccessDBFCP.Fill(newDataSet, "ProdTable")


            'Insert values into Access temp table for FCP in sqlStrImport



            Dim insCmdFCP = New OleDbCommand(sqlStrInsertImpFCP, dbConnAccess)

            insCmdFCP.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "PROCESS_CYCLE_DATE"})
            insCmdFCP.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "ACTUAL_PICKER_ID"})
            insCmdFCP.Parameters.Add(New OleDbParameter With {.ParameterName = "@TagNum", .OleDbType = OleDbType.Integer, .Size = 5, .SourceColumn = "CountOfCASE_SCAN_ID_ATR"})

            AccessDBFCP.InsertCommand = insCmdFCP

            Dim BuilderFCP As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDBFCP)

            BuilderFCP.GetInsertCommand()
            AccessDBFCP.Update(newDataSet, "ProdTable")


        Finally

        End Try




        Dim sqlStrFCP As String
        sqlStrFCP = "INSERT INTO PROD ( DTE, M, D, Y, EmployeeID, Amount, Department,Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([FCPImportTempTable].[PROCESS_CYCLE_DTE],4,2) AS M, RIGHT([FCPImportTempTable].[PROCESS_CYCLE_DTE],2) AS D, Mid([FCPImportTempTable].[PROCESS_CYCLE_DTE],2,2) AS Y, FCPImportTempTable.ACTUAL_PICKER_ID, FCPImportTempTable.CountOfCASE_SCAN_ID_ATR, 2 AS Department, 3 AS Shift, 'P' AS Function FROM TMs INNER JOIN FCPImportTempTable ON TMs.EmployeeID = FCPImportTempTable.ACTUAL_PICKER_ID WHERE (((TMs.DepartmentID)=5) AND ((TMs.ShiftID)=3)) ORDER BY FCPImportTempTable.ACTUAL_PICKER_ID"

        Dim dbCommInsertProdFCP As New OleDbCommand(sqlStrFCP, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProdFCP.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try


        'End of Import Code Block


    End Sub

    Private Sub ImportEW()

        'Code for formatting the date for DB and changing it AS400 format for queries
        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text
        Dim FormattedDate As String = Format(DateText, "MM/dd/yy")
        Dim DateString As String = CStr("1" & Right$((FormattedDate), 2) & Left$((FormattedDate), 2) & Mid$((FormattedDate), 4, 2))
        'end Date formatting code

        'Connection Strings to AS400
        Dim connectionStringAS400 As String = _
        ConfigurationManager.ConnectionStrings("ConnectionStringAS400").ConnectionString
        Dim dbConnAS400 As New OleDbConnection(connectionStringAS400)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        'Import Extra Waves sql
        Dim sqlStrImportEW As String
        sqlStrImportEW = "SELECT DIPDLIBB.DI842P.PROCESS_CYCLE_DATE, DIPDLIBB.DI842P.ACTUAL_PICKER_ID, Count(DIPDLIBB.DI842P.CASE_SCAN_ID_ATR) AS CountOfCASE_SCAN_ID_ATR FROM DIPDLIBB.DI210P INNER JOIN DIPDLIBB.DI842P ON (DIPDLIBB.DI210P.TIER = DIPDLIBB.DI842P.TIER_ATR) AND (DIPDLIBB.DI210P.SECTION_CDE = DIPDLIBB.DI842P.SECTION_ATR) AND (DIPDLIBB.DI210P.AISLE = DIPDLIBB.DI842P.AISLE_ATR) WHERE (((DIPDLIBB.DI842P.WAVE_NUMBER)>=19) AND ((DIPDLIBB.DI842P.ACTUAL_PICKER_ID)<>' ') AND ((DIPDLIBB.DI210P.AREA_CLASS_ID)<>'TR')) GROUP BY DIPDLIBB.DI842P.PROCESS_CYCLE_DATE, DIPDLIBB.DI842P.ACTUAL_PICKER_ID HAVING (((DIPDLIBB.DI842P.PROCESS_CYCLE_DATE)=?) AND ((DIPDLIBB.DI842P.ACTUAL_PICKER_ID) Like 'B%')) ORDER BY DIPDLIBB.DI842P.ACTUAL_PICKER_ID"


        'Open FCP Import table in Access
        Dim sqlStrSelectNewTableFCP As String = "SELECT FCPImportTempTable.* FROM FCPImportTempTable"

        'Add Data to FCP Import Table
        Dim sqlStrInsertImpFCP As String = "INSERT INTO FCPImportTempTable VALUES (@DTE, @EmpID, @TagNum)"

        'Delete FCP Temp Import table
        Dim sqlStrDeleteTmpImpFCP As String = "DELETE FCPImportTempTable.* FROM FCPImportTempTable"
        Dim dbConnDeleteTmpImpFCP As New OleDbCommand(sqlStrDeleteTmpImpFCP, dbConnAccess)

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable

        'Delete FCP Import Table
        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpFCP.ExecuteNonQuery()
        Finally
            dbConnAccess.Close()
        End Try



        'Import - EW
        Try

            Dim ProdEW = New OleDbDataAdapter
            ProdEW.AcceptChangesDuringFill = "False"

            ProdEW.SelectCommand = New OleDbCommand(sqlStrImportEW, dbConnAS400)
            ProdEW.SelectCommand.Parameters.Add("@Date", OleDbType.VarWChar)
            ProdEW.SelectCommand.Parameters("@Date").Value = DateString
            ProdEW.Fill(dataSet, "ProdTable")

            newDataSet = dataSet.Copy()

            Dim SelectCmdEW = New OleDbCommand(sqlStrSelectNewTableFCP, dbConnAccess)

            Dim AccessDBEW As New OleDbDataAdapter
            AccessDBEW.SelectCommand = SelectCmdEW
            AccessDBEW.Fill(newDataSet, "ProdTable")


            'Insert values into Access temp table for EW in sqlStrImportEW

            Dim insCmdEW = New OleDbCommand(sqlStrInsertImpFCP, dbConnAccess)

            insCmdEW.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "PROCESS_CYCLE_DATE"})
            insCmdEW.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "ACTUAL_PICKER_ID"})
            insCmdEW.Parameters.Add(New OleDbParameter With {.ParameterName = "@TagNum", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfCASE_SCAN_ID_ATR"})



            AccessDBEW.InsertCommand = insCmdEW

            Dim BuilderEW As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDBEW)

            BuilderEW.GetInsertCommand()
            AccessDBEW.Update(newDataSet, "ProdTable")


        Finally

        End Try


        Dim sqlStrEW As String
        sqlStrEW = "INSERT INTO PROD ( DTE, M, D, Y, EmployeeID, Amount, Department,Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([FCPImportTempTable].[PROCESS_CYCLE_DTE],4,2) AS M, RIGHT([FCPImportTempTable].[PROCESS_CYCLE_DTE],2) AS D, Mid([FCPImportTempTable].[PROCESS_CYCLE_DTE],2,2) AS Y, FCPImportTempTable.ACTUAL_PICKER_ID, FCPImportTempTable.CountOfCASE_SCAN_ID_ATR, 2 AS Department, 3 AS Shift, 'EW' AS FUNCTION FROM TMs INNER JOIN FCPImportTempTable ON TMs.EmployeeID = FCPImportTempTable.ACTUAL_PICKER_ID WHERE (((TMs.DepartmentID)=5) AND ((TMs.ShiftID)=3)) ORDER BY FCPImportTempTable.ACTUAL_PICKER_ID"

        Dim dbCommInsertProdEW As New OleDbCommand(sqlStrEW, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProdEW.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try


        'End of Import Code Block


    End Sub

    Private Sub ImportTR()

        'Code for formatting the date for DB and changing it AS400 format for queries
        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text
        Dim FormattedDate As String = Format(DateText, "MM/dd/yy")
        Dim DateString As String = CStr("1" & Right$((FormattedDate), 2) & Left$((FormattedDate), 2) & Mid$((FormattedDate), 4, 2))
        'end Date formatting code

        'Connection Strings to AS400
        Dim connectionStringAS400 As String = _
        ConfigurationManager.ConnectionStrings("ConnectionStringAS400").ConnectionString
        Dim dbConnAS400 As New OleDbConnection(connectionStringAS400)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)
        'end Connection Strings


        'Import TR sql
        Dim sqlStrImportTR As String
        sqlStrImportTR = "SELECT DIPDLIBB.DI842P.PROCESS_CYCLE_DATE, DIPDLIBB.DI842P.ACTUAL_PICKER_ID, Count(DIPDLIBB.DI842P.CASE_SCAN_ID_ATR) AS CountOfCASE_SCAN_ID_ATR, DIPDLIBB.DI210P.AREA_CLASS_ID FROM DIPDLIBB.DI210P INNER JOIN DIPDLIBB.DI842P ON (DIPDLIBB.DI210P.TIER = DIPDLIBB.DI842P.TIER_ATR) AND (DIPDLIBB.DI210P.SECTION_CDE = DIPDLIBB.DI842P.SECTION_ATR) AND (DIPDLIBB.DI210P.AISLE = DIPDLIBB.DI842P.AISLE_ATR) WHERE (((DIPDLIBB.DI842P.ACTUAL_PICKER_ID)<>' ') AND ((DIPDLIBB.DI210P.AREA_CLASS_ID)='TR'))GROUP BY DIPDLIBB.DI842P.PROCESS_CYCLE_DATE, DIPDLIBB.DI842P.ACTUAL_PICKER_ID, DIPDLIBB.DI210P.AREA_CLASS_ID HAVING (((DIPDLIBB.DI842P.PROCESS_CYCLE_DATE)=?) AND ((DIPDLIBB.DI842P.ACTUAL_PICKER_ID) Like 'B%')) ORDER BY DIPDLIBB.DI842P.ACTUAL_PICKER_ID"


        'Delete FCP Temp Import table
        Dim sqlStrDeleteTmpImpFCP As String = "DELETE FCPImportTempTableTR.* FROM FCPImportTempTableTR"
        Dim dbConnDeleteTmpImpFCP As New OleDbCommand(sqlStrDeleteTmpImpFCP, dbConnAccess)

        'Open FCP Import table in Access
        Dim sqlStrSelectNewTableFCP As String = "SELECT FCPImportTempTableTR.* FROM FCPImportTempTableTR"

        'Add Data to FCP Import Table
        Dim sqlStrInsertImpTR As String = "INSERT INTO FCPImportTempTableTR VALUES (@DTE, @EmpID, @TagNum, @Type)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable


        'Delete FCP Import Table
        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpFCP.ExecuteNonQuery()
        Finally
            dbConnAccess.Close()
        End Try


        'Import- TR 
        Try

            Dim ProdTR = New OleDbDataAdapter
            ProdTR.AcceptChangesDuringFill = "False"

            ProdTR.SelectCommand = New OleDbCommand(sqlStrImportTR, dbConnAS400)
            ProdTR.SelectCommand.Parameters.Add("@Date", OleDbType.VarChar)
            ProdTR.SelectCommand.Parameters("@Date").Value = DateString
            ProdTR.Fill(dataSet, "ProdTable")

            newDataSet = dataSet.Copy()

            Dim SelectCmdTR = New OleDbCommand(sqlStrSelectNewTableFCP, dbConnAccess)

            Dim AccessDBTR As New OleDbDataAdapter
            AccessDBTR.SelectCommand = SelectCmdTR
            AccessDBTR.Fill(newDataSet, "ProdTable")


            'Insert values into Access temp table for TR in sqlStrImportTR

            Dim insCmdTR = New OleDbCommand(sqlStrInsertImpTR, dbConnAccess)

            insCmdTR.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "PROCESS_CYCLE_DATE"})
            insCmdTR.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "ACTUAL_PICKER_ID"})
            insCmdTR.Parameters.Add(New OleDbParameter With {.ParameterName = "@TagNum", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfCASE_SCAN_ID_ATR"})
            insCmdTR.Parameters.Add(New OleDbParameter With {.ParameterName = "@Type", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "AREA_CLASS_ID"})


            AccessDBTR.InsertCommand = insCmdTR

            Dim BuilderTR As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDBTR)

            BuilderTR.GetInsertCommand()
            AccessDBTR.Update(newDataSet, "ProdTable")


        Finally

        End Try

        Dim sqlStrTR As String
        sqlStrTR = "INSERT INTO PROD ( DTE, M, D, Y, EmployeeID, Amount, Department,Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([FCPImportTempTableTR].[PROCESS_CYCLE_DTE],4,2) AS M, RIGHT([FCPImportTempTableTR].[PROCESS_CYCLE_DTE],2) AS D, Mid([FCPImportTempTableTR].[PROCESS_CYCLE_DTE],2,2) AS Y, FCPImportTempTableTR.ACTUAL_PICKER_ID, FCPImportTempTableTR.CountOfCASE_SCAN_ID_ATR, 2 AS Department, 3 AS Shift, FCPImportTempTableTR.AREA_CLASS_ID FROM TMs INNER JOIN FCPImportTempTableTR ON TMs.EmployeeID = FCPImportTempTableTR.ACTUAL_PICKER_ID WHERE (((TMs.DepartmentID)=5) AND ((TMs.ShiftID)=3)) ORDER BY FCPImportTempTableTR.ACTUAL_PICKER_ID"

        Dim dbCommInsertProdTR As New OleDbCommand(sqlStrTR, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProdTR.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try


        'End of Import Code Block

    End Sub

    Private Sub ImportC()

        'Code for formatting the date for DB and changing it AS400 format for queries
        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text
        Dim FormattedDate As String = Format(DateText, "MM/dd/yy")
        Dim DateString As String = CStr("1" & Right$((FormattedDate), 2) & Left$((FormattedDate), 2) & Mid$((FormattedDate), 4, 2))
        'end Date formatting code

        'Connection Strings to AS400
        Dim connectionStringAS400 As String = _
        ConfigurationManager.ConnectionStrings("ConnectionStringAS400").ConnectionString
        Dim dbConnAS400 As New OleDbConnection(connectionStringAS400)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        'Import Clamp Replenishment sql
        Dim sqlStrImportC As String
        sqlStrImportC = "SELECT RF_TRANSACTION_DATE, USER_STAMP, COUNT(RF_TRANS_BAR_CODE) AS CountOfRF_TRANS_BAR_CODE FROM DIPDLIBB.DI281P WHERE (RF_TRANSACTION_DATE = ?) AND ((RF_TRANSACTION_TYPE LIKE 'F') OR (RF_TRANSACTION_TYPE LIKE 'P')) AND (RF_TRANS_TO_LOCATION LIKE 'D%') GROUP BY USER_STAMP, RF_TRANSACTION_DATE HAVING (USER_STAMP <> ' ') ORDER BY USER_STAMP"


        'Delete FCS Temp Import table
        Dim sqlStrDeleteTmpImpFCS As String = "DELETE FCPFCSImportTempTable.* FROM FCPFCSImportTempTable"

        Dim dbConnDeleteTmpImpFCS As New OleDbCommand(sqlStrDeleteTmpImpFCS, dbConnAccess)


        'Open FCS Import table in Access
        Dim sqlStrSelectNewTableFCS As String = "SELECT FCPFCSImportTempTable.* FROM FCPFCSImportTempTable"

        'Add Data to FCS Import table
        Dim sqlStrInsertImpFCS As String = "INSERT INTO FCPFCSImportTempTable VALUES (@DTE, @EmpID, @BarCode)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable


        'Deleting FCS import table
        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpFCS.ExecuteNonQuery()
        Finally
            dbConnAccess.Close()
        End Try

        'Import - Clamp Replenishment
        Try

            Dim ProdC = New OleDbDataAdapter
            ProdC.AcceptChangesDuringFill = "False"

            ProdC.SelectCommand = New OleDbCommand(sqlStrImportC, dbConnAS400)
            ProdC.SelectCommand.Parameters.Add("@Date", OleDbType.VarWChar)
            ProdC.SelectCommand.Parameters("@Date").Value = DateString
            ProdC.Fill(dataSet, "ProdTable")

            newDataSet = dataSet.Copy()

            Dim SelectCmd = New OleDbCommand(sqlStrSelectNewTableFCS, dbConnAccess)

            Dim AccessDBC As New OleDbDataAdapter
            AccessDBC.SelectCommand = SelectCmd
            AccessDBC.Fill(newDataSet, "ProdTable")


            'Insert values into Access temp table for FCS in sqlStrImportC

            Dim insCmdC = New OleDbCommand(sqlStrInsertImpFCS, dbConnAccess)

            insCmdC.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "RF_TRANSACTION_DATE"})
            insCmdC.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "USER_STAMP"})
            insCmdC.Parameters.Add(New OleDbParameter With {.ParameterName = "@BarCode", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfRF_TRANS_BAR_CODE"})


            AccessDBC.InsertCommand = insCmdC

            Dim BuilderC As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDBC)

            BuilderC.GetInsertCommand()
            AccessDBC.Update(newDataSet, "ProdTable")


        Finally

        End Try



        Dim sqlStrFCS As String
        sqlStrFCS = "INSERT INTO Prod (DTE, M, D, Y, EmployeeID, Amount, Department, Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([FCPFCSImportTempTable].[RF_TRANSACTION_DATE],4,2) AS M, RIGHT([FCPFCSImportTempTable].[RF_TRANSACTION_DATE],2) AS D, Mid([FCPFCSImportTempTable].[RF_TRANSACTION_DATE],2,2) AS Y, FCPFCSImportTempTable.USER_STAMP, FCPFCSImportTempTable.CountOfRF_TRANS_BAR_CODE, 2 AS Department, 3 AS Shift, 'C' AS Function FROM TMs INNER JOIN FCPFCSImportTempTable ON TMs.EmployeeID = FCPFCSImportTempTable.USER_STAMP WHERE (((TMs.DepartmentID)=5) AND ((TMs.ShiftID)=3)) ORDER BY FCPFCSImportTempTable.USER_STAMP"

        Dim dbCommInsertProdFCS As New OleDbCommand(sqlStrFCS, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProdFCS.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try


        'End of Import Code Block
    End Sub
    
    Private Sub ImportR()

        'Code for formatting the date for DB and changing it AS400 format for queries
        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text
        Dim FormattedDate As String = Format(DateText, "MM/dd/yy")
        Dim DateString As String = CStr("1" & Right$((FormattedDate), 2) & Left$((FormattedDate), 2) & Mid$((FormattedDate), 4, 2))
        'end Date formatting code

        'Connection Strings to AS400
        Dim connectionStringAS400 As String = _
        ConfigurationManager.ConnectionStrings("ConnectionStringAS400").ConnectionString
        Dim dbConnAS400 As New OleDbConnection(connectionStringAS400)

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        'Import Reach Replenishment sql
        Dim sqlStrImportR As String
        sqlStrImportR = "SELECT RF_TRANSACTION_DATE, USER_STAMP, COUNT(RF_TRANS_BAR_CODE) AS CountOfRF_TRANS_BAR_CODE FROM DIPDLIBB.DI281P WHERE (RF_TRANSACTION_DATE = ?) AND ((RF_TRANSACTION_TYPE LIKE 'F') OR (RF_TRANSACTION_TYPE LIKE 'P')) AND (RF_TRANS_TO_LOCATION NOT LIKE 'D%') GROUP BY USER_STAMP, RF_TRANSACTION_DATE HAVING (USER_STAMP <> ' ') ORDER BY USER_STAMP"


        'Delete FCS Temp Import table
        Dim sqlStrDeleteTmpImpFCS As String = "DELETE FCPFCSImportTempTable.* FROM FCPFCSImportTempTable"

        Dim dbConnDeleteTmpImpFCS As New OleDbCommand(sqlStrDeleteTmpImpFCS, dbConnAccess)


        'Open FCS Import table in Access
        Dim sqlStrSelectNewTableFCS As String = "SELECT FCPFCSImportTempTable.* FROM FCPFCSImportTempTable"


        'Add Data to FCS Import table
        Dim sqlStrInsertImpFCS As String = "INSERT INTO FCPFCSImportTempTable VALUES (@DTE, @EmpID, @BarCode)"

        Dim dataSet As New DataSet
        Dim newDataSet As New DataSet
        Dim ProdTable As New DataTable


        'Deleting FCS import table
        Try
            dbConnAccess.Open()
            dbConnDeleteTmpImpFCS.ExecuteNonQuery()
        Finally
            dbConnAccess.Close()
        End Try

        'Import -Reach Replenishment
        Try

            Dim ProdR = New OleDbDataAdapter
            ProdR.AcceptChangesDuringFill = "False"

            ProdR.SelectCommand = New OleDbCommand(sqlStrImportR, dbConnAS400)
            ProdR.SelectCommand.Parameters.Add("@Date", OleDbType.VarWChar)
            ProdR.SelectCommand.Parameters("@Date").Value = DateString
            ProdR.Fill(dataSet, "ProdTable")

            newDataSet = dataSet.Copy()

            Dim SelectCmd = New OleDbCommand(sqlStrSelectNewTableFCS, dbConnAccess)

            Dim AccessDBR As New OleDbDataAdapter
            AccessDBR.SelectCommand = SelectCmd
            AccessDBR.Fill(newDataSet, "ProdTable")


            'Insert values into Access temp table for FCS in sqlStrImportR

            Dim insCmdR = New OleDbCommand(sqlStrInsertImpFCS, dbConnAccess)

            insCmdR.Parameters.Add(New OleDbParameter With {.ParameterName = "@DTE", .OleDbType = OleDbType.VarWChar, .Size = 30, .SourceColumn = "RF_TRANSACTION_DATE"})
            insCmdR.Parameters.Add(New OleDbParameter With {.ParameterName = "@EmpID", .OleDbType = OleDbType.VarWChar, .Size = 50, .SourceColumn = "USER_STAMP"})
            insCmdR.Parameters.Add(New OleDbParameter With {.ParameterName = "@BarCode", .OleDbType = OleDbType.VarWChar, .Size = 5, .SourceColumn = "CountOfRF_TRANS_BAR_CODE"})


            AccessDBR.InsertCommand = insCmdR

            Dim BuilderR As OleDbCommandBuilder = New OleDbCommandBuilder(AccessDBR)

            BuilderR.GetInsertCommand()
            AccessDBR.Update(newDataSet, "ProdTable")


        Finally

        End Try




        Dim sqlStrFCS As String
        sqlStrFCS = "INSERT INTO Prod (DTE, M, D, Y, EmployeeID, Amount, Department, Shift, Function ) SELECT DateValue([M] & '/' & [D] & '/' & [Y]) AS [Date], Mid([FCPFCSImportTempTable].[RF_TRANSACTION_DATE],4,2) AS M, RIGHT([FCPFCSImportTempTable].[RF_TRANSACTION_DATE],2) AS D, Mid([FCPFCSImportTempTable].[RF_TRANSACTION_DATE],2,2) AS Y, FCPFCSImportTempTable.USER_STAMP, FCPFCSImportTempTable.CountOfRF_TRANS_BAR_CODE, 2 AS Department, 3 AS Shift, 'R' as Function FROM TMs INNER JOIN FCPFCSImportTempTable ON TMs.EmployeeID = FCPFCSImportTempTable.USER_STAMP WHERE (((TMs.DepartmentID)=5) AND ((TMs.ShiftID)=3)) ORDER BY FCPFCSImportTempTable.USER_STAMP"

        Dim dbCommInsertProdFCS As New OleDbCommand(sqlStrFCS, dbConnAccess)

        Try

            dbConnAccess.Open()
            dbCommInsertProdFCS.ExecuteNonQuery()

        Finally
            dbConnAccess.Close()
        End Try


        'End of Import Code Block
    End Sub

    

    Protected Sub ImportButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportButton.Click

        Dim ImportDate As TextBox = Page.FindControl("ctl00$MainContent$txtDate")
        Dim DateText As Date = ImportDate.Text

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConnAccess As New OleDbConnection(connectionStringAccess)
        'end Connection Strings


        Dim sqlStrFunction As String
        sqlStrFunction = "SELECT Count(Prod.DTE) AS CountOfDTE FROM Prod WHERE (Prod.DTE) = @Date AND (Prod.Shift)=3 AND (Prod.Department)=2"

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
        'ImportFCPMTV()

        ImportFCP()
        ImportEW()
        ImportTR()
        ImportC()
        ImportR()
        
        'This checks to see if anything was imported for this shift,department for this day.
        'Run this after all Importing is complete


        Dim sqlStrCountImport As String
        sqlStrCountImport = "SELECT Count(Prod.DTE) AS CountOfDTE FROM Prod WHERE (Prod.DTE) = @Date AND (Prod.Shift)=3 AND (Prod.Department)=2"

        Dim dbCommCheckImportCount As New OleDbCommand(sqlStrCountImport, dbConnAccess)
        dbCommCheckImportCount.Parameters.Add("@Date", OleDbType.VarWChar)
        dbCommCheckImportCount.Parameters("@Date").Value = DateText

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



        SuccessLabel.Text = "Successful Import!"

        Dim sqlStrLastDate = "SELECT Max(Prod.DTE) AS MaxOfDTE FROM PROD WHERE (((Prod.Department)=2) AND ((Prod.Shift)=3))"

        Dim dbCommLastDate As New OleDbCommand(sqlStrLastDate, dbConnAccess)

        Dim LastDate As Date3

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
