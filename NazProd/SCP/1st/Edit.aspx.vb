Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration

Partial Class SCP_1st_Add
    Inherits System.Web.UI.Page

    Protected WithEvents GVFunctProd As Global.System.Web.UI.WebControls.GridView

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Loads the previous picking date when page loads. 
        'If someone changes the date that date remains selected until it is changed again 

        If Not IsPostBack Then

            Dim TodaysDate As Date = DateTime.Today()
            Dim SugImpDate As Date
            Dim DTE As TextBox = Page.FindControl("ctl00$MainContent$DTE")

            If TodaysDate.DayOfWeek = DayOfWeek.Monday Then
                SugImpDate = DateTime.Today.AddDays(-3)
            Else
                SugImpDate = DateTime.Today.AddDays(-1)
            End If

            DTE.Text = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            DTE_CalendarExtender.EndDate = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            FVTM.DataBind()
        End If

        'Stop past sugimpdate from being selected 

    End Sub

    Protected Sub DTE_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTE.TextChanged
        FVTM.DataBind()
        'requeries the form for the new date

    End Sub

    Sub GVFunctProd_RowDatabound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(4).Visible = "False"

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim Dte As TextBox = Page.FindControl("ctl00$MainContent$DTE")

            Dim EmpID As HiddenField = Page.FindControl("ctl00$MainContent$FVTM$EmpID")
            Dim FunctHours As TextBox = e.Row.Cells(1).FindControl("Hours")
            Dim Amount As TextBox = e.Row.Cells(2).FindControl("Amount")
            Dim ErrorLabel As Label = e.Row.Cells(3).FindControl("ErrorLabel")
            Dim Funct As String = e.Row.Cells(4).Text

            'Check if function has been imported for this person, date and function
            Dim sqlStrTestImported As String
            sqlStrTestImported = "SELECT COUNT(PROD.Function) AS CountOfFunction FROM Prod WHERE (PROD.DTE) = @DTE AND (PROD.EmployeeID) = @EmployeeID AND (PROD.Function) = @Function AND (PROD.Department = 8) AND (PROD.Shift = 1)"

            Dim dbCommTestImported As New OleDbCommand(sqlStrTestImported, dbConn)
            dbCommTestImported.Parameters.Add("@DTE", OleDbType.VarWChar)
            dbCommTestImported.Parameters("@DTE").Value = Dte.Text
            dbCommTestImported.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
            dbCommTestImported.Parameters("@EmployeeID").Value = EmpID.Value
            dbCommTestImported.Parameters.Add("@Function", OleDbType.VarWChar)
            dbCommTestImported.Parameters("@Function").Value = Funct

            'Find the Amount of the function
            Dim sqlAmount As String
            sqlAmount = "SELECT Amount, FunctHours, M FROM Prod WHERE (DTE = @DTE) AND (EmployeeID = @EmployeeID) AND (Function = @Function) AND (Shift = 1) AND (Department = 8)"

            Dim dbCommAmount As New OleDbCommand(sqlAmount, dbConn)
            dbCommAmount.Parameters.Add("@DTE", OleDbType.VarWChar)
            dbCommAmount.Parameters("@DTE").Value = Dte.Text
            dbCommAmount.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
            dbCommAmount.Parameters("@EmployeeID").Value = EmpID.Value
            dbCommAmount.Parameters.Add("@Function", OleDbType.VarWChar)
            dbCommAmount.Parameters("@Function").Value = Funct

            Dim RecordCount As Int32 = 0

            Try
                dbConn.Open()


                RecordCount = Convert.ToInt32(dbCommTestImported.ExecuteScalar())

                If RecordCount <> 0 Then

                    Dim Reader As OleDbDataReader = dbCommAmount.ExecuteReader()

                    While (Reader.Read())
                        If IsDBNull(Reader("M")) Then

                            Amount.Text = Reader("Amount").ToString
                        Else
                            'This prevents editing AS400 data
                            Amount.Text = Reader("Amount").ToString
                            Amount.Enabled = "False"

                        End If

                        FunctHours.Text = Reader("FunctHours").ToString

                    End While

                    Reader.Close()


                End If

            Finally
                dbConn.Close()
            End Try

        End If

    End Sub



    Sub submitClick(sender As Object, e As CommandEventArgs)
        'On Submit 

        'loop through each row to test
        'if the fields have values if not move to next row in table
        'if it does then test if the data already exists in the database for the fields 
        'if it does then use update queries, one for as400 data, one for non-as400 data
        'if it doesn't then insert the data 

        'Take date from DTE field, employee id from hidden field, function from bound field, amount, hours
        'loop to next row
        'at end of grid move to next record in employee list formview


        Dim Dte As TextBox = Page.FindControl("ctl00$MainContent$DTE")

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim EmpID As HiddenField = Page.FindControl("ctl00$MainContent$FVTM$EmpID")

        Dim GVFunctProd As GridView = FindControl("ctl00$MainContent$FVTM$GVFunctProd")

        For Each row As GridViewRow In GVFunctProd.Rows

            Dim therowindex As Integer = row.RowIndex
            Dim FunctHours As TextBox = DirectCast(GVFunctProd.Rows(therowindex).Cells(1).FindControl("Hours"), TextBox)
            Dim Amount As TextBox = DirectCast(GVFunctProd.Rows(therowindex).Cells(2).FindControl("Amount"), TextBox)
            Dim ErrorLabel As Label = DirectCast(GVFunctProd.Rows(therowindex).Cells(3).FindControl("ErrorLabel"), Label)
            Dim Funct As String = GVFunctProd.Rows(therowindex).Cells(4).Text
            Dim RecordCount As Int32 = 0

            If String.IsNullOrEmpty(Amount.Text) Or String.IsNullOrEmpty(FunctHours.Text) Then

                If String.IsNullOrEmpty(Amount.Text) And String.IsNullOrEmpty(FunctHours.Text) Then

                    'to test if amount can be edited
                    Dim sqlStrTestDelete As String
                    sqlStrTestDelete = "SELECT COUNT(PROD.Function) AS CountOfFunction FROM Prod WHERE (PROD.DTE) = @DTE AND (PROD.EmployeeID) = @EmployeeID AND (PROD.Function) = @Function AND (PROD.Department = 8) AND (PROD.Shift = 1)"

                    Dim dbCommTestDelete As New OleDbCommand(sqlStrTestDelete, dbConn)
                    dbCommTestDelete.Parameters.Add("@DTE", OleDbType.VarWChar)
                    dbCommTestDelete.Parameters("@DTE").Value = Dte.Text
                    dbCommTestDelete.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                    dbCommTestDelete.Parameters("@EmployeeID").Value = EmpID.Value
                    dbCommTestDelete.Parameters.Add("@Function", OleDbType.VarWChar)
                    dbCommTestDelete.Parameters("@Function").Value = Funct

                    Dim sqlStrDelete As String
                    sqlStrDelete = "DELETE FROM Prod WHERE (PROD.DTE) = @DTE AND (PROD.EmployeeID) = @EmployeeID AND (PROD.Function) = @Function AND (PROD.Department = 8) AND (PROD.Shift = 1)"

                    Dim dbCommDelete As New OleDbCommand(sqlStrDelete, dbConn)
                    dbCommDelete.Parameters.Add("@DTE", OleDbType.VarWChar)
                    dbCommDelete.Parameters("@DTE").Value = Dte.Text
                    dbCommDelete.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                    dbCommDelete.Parameters("@EmployeeID").Value = EmpID.Value
                    dbCommDelete.Parameters.Add("@Function", OleDbType.VarWChar)
                    dbCommDelete.Parameters("@Function").Value = Funct


                    Try
                        dbConn.Open()
                        RecordCount = Convert.ToInt32(dbCommTestDelete.ExecuteScalar())

                        ' Test if record is in database
                        ' if it is delete record
                        'If not, Do nothing and move on to the next row

                        If RecordCount <> 0 Then
                            'delete record                        
                            dbCommDelete.ExecuteNonQuery()
                        End If


                    Finally

                        dbConn.Close()

                    End Try


                Else
                    If Amount.Visible = "true" And String.IsNullOrEmpty(Amount.Text) Then
                        ErrorLabel.Text = "Enter an amount."
                        Exit Sub
                    ElseIf String.IsNullOrEmpty(FunctHours.Text) Then
                        ErrorLabel.Text = "Enter hours."
                        Exit Sub
                    End If

                End If

            Else

                'Test if data is in database for this person, date, dept, shift, function
                Dim sqlStrTestInsert As String
                sqlStrTestInsert = "SELECT COUNT(PROD.Function) AS CountOfFunction FROM Prod WHERE (PROD.DTE) = @DTE AND (PROD.EmployeeID) = @EmployeeID AND (PROD.Function) = @Function AND (PROD.Department = 8) AND (PROD.Shift = 1) AND (PROD.M IS NOT NULL)"

                'To test if amount can't be edited or if insert needed
                Dim dbCommTestInsert As New OleDbCommand(sqlStrTestInsert, dbConn)
                dbCommTestInsert.Parameters.Add("@DTE", OleDbType.VarWChar)
                dbCommTestInsert.Parameters("@DTE").Value = Dte.Text
                dbCommTestInsert.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                dbCommTestInsert.Parameters("@EmployeeID").Value = EmpID.Value
                dbCommTestInsert.Parameters.Add("@Function", OleDbType.VarWChar)
                dbCommTestInsert.Parameters("@Function").Value = Funct

                'to test if amount can be edited
                Dim sqlStrTestInsertM As String
                sqlStrTestInsertM = "SELECT COUNT(PROD.Function) AS CountOfFunction FROM Prod WHERE (PROD.DTE) = @DTE AND (PROD.EmployeeID) = @EmployeeID AND (PROD.Function) = @Function AND (PROD.Department = 8) AND (PROD.Shift = 1) AND (PROD.M IS NULL)"

                Dim dbCommTestInsertM As New OleDbCommand(sqlStrTestInsertM, dbConn)
                dbCommTestInsertM.Parameters.Add("@DTE", OleDbType.VarWChar)
                dbCommTestInsertM.Parameters("@DTE").Value = Dte.Text
                dbCommTestInsertM.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                dbCommTestInsertM.Parameters("@EmployeeID").Value = EmpID.Value
                dbCommTestInsertM.Parameters.Add("@Function", OleDbType.VarWChar)
                dbCommTestInsertM.Parameters("@Function").Value = Funct

                Dim InsertSqlString As String
                InsertSqlString = "INSERT INTO Prod (DTE, EmployeeID, Function, Department, Shift, Amount, FunctHours) VALUES (@DTE,@EmployeeID,@Function,@Dept,@Shift, @Amount,@FunctHours)"

                Dim dbCommInsert As New OleDbCommand(InsertSqlString, dbConn)
                dbCommInsert.Parameters.Add("@DTE", OleDbType.Date)
                dbCommInsert.Parameters("@DTE").Value = Dte.Text
                dbCommInsert.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                dbCommInsert.Parameters("@EmployeeID").Value = EmpID.Value
                dbCommInsert.Parameters.Add("@Function", OleDbType.VarWChar)
                dbCommInsert.Parameters("@Function").Value = Funct
                dbCommInsert.Parameters.Add("@Dept", OleDbType.Integer)
                dbCommInsert.Parameters("@Dept").Value = Convert.ToInt32(8)
                dbCommInsert.Parameters.Add("@Shift", OleDbType.Integer)
                dbCommInsert.Parameters("@Shift").Value = Convert.ToInt32(1)
                dbCommInsert.Parameters.Add("@Amount", OleDbType.Integer)
                dbCommInsert.Parameters("@Amount").Value = Convert.ToInt32(Amount.Text)
                dbCommInsert.Parameters.Add("@FunctHours", OleDbType.Double)
                dbCommInsert.Parameters("@FunctHours").Value = Convert.ToDouble(FunctHours.Text)


                Dim UpdateSqlString As String
                UpdateSqlString = "UPDATE Prod SET Prod.FunctHours=@FunctHours WHERE ((Prod.DTE) = @DTE And (Prod.EmployeeID) = @EmployeeID And (Prod.Function) = @Function And (Prod.Department) = 8 And (Prod.Shift) = 1)"

                Dim dbCommUpdate As New OleDbCommand(UpdateSqlString, dbConn)
                dbCommUpdate.Parameters.Add("@FunctHours", OleDbType.Double)
                dbCommUpdate.Parameters("@FunctHours").Value = Convert.ToDouble(FunctHours.Text)
                dbCommUpdate.Parameters.Add("@DTE", OleDbType.Date)
                dbCommUpdate.Parameters("@DTE").Value = Dte.Text
                dbCommUpdate.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                dbCommUpdate.Parameters("@EmployeeID").Value = EmpID.Value
                dbCommUpdate.Parameters.Add("@Function", OleDbType.VarWChar)
                dbCommUpdate.Parameters("@Function").Value = Funct


                Dim UpdateAmountSqlString As String
                UpdateAmountSqlString = "UPDATE Prod SET Prod.FunctHours=@FunctHours, Prod.Amount = @Amount WHERE ((Prod.DTE) = @DTE And (Prod.EmployeeID) = @EmployeeID And (Prod.Function) = @Function And (Prod.Department) = 8 And (Prod.Shift) = 1)"

                Dim dbCommAmountUpdate As New OleDbCommand(UpdateAmountSqlString, dbConn)
                dbCommAmountUpdate.Parameters.Add("@FunctHours", OleDbType.Double)
                dbCommAmountUpdate.Parameters("@FunctHours").Value = Convert.ToDouble(FunctHours.Text)
                dbCommAmountUpdate.Parameters.Add("@Amount", OleDbType.Integer)
                dbCommAmountUpdate.Parameters("@Amount").Value = Convert.ToInt32(Amount.Text)
                dbCommAmountUpdate.Parameters.Add("@DTE", OleDbType.Date)
                dbCommAmountUpdate.Parameters("@DTE").Value = Dte.Text
                dbCommAmountUpdate.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
                dbCommAmountUpdate.Parameters("@EmployeeID").Value = EmpID.Value
                dbCommAmountUpdate.Parameters.Add("@Function", OleDbType.VarWChar)
                dbCommAmountUpdate.Parameters("@Function").Value = Funct




                Try
                    dbConn.Open()
                    RecordCount = Convert.ToInt32(dbCommTestInsertM.ExecuteScalar())

                    'if match then record exists but it wasn't imported data
                    If RecordCount <> 0 Then
                        dbCommAmountUpdate.ExecuteNonQuery()
                    Else
                        RecordCount = 0
                        RecordCount = Convert.ToInt32(dbCommTestInsert.ExecuteScalar())

                        'Does it exist and was it imported
                        If RecordCount <> 0 Then
                            dbCommUpdate.ExecuteNonQuery()
                        Else
                            dbCommInsert.ExecuteNonQuery()
                        End If

                    End If


                Finally

                    dbConn.Close()

                End Try

            End If

        Next


        Dim i As Int32 = Me.FVTM.PageIndex
        i = i + 1
        Me.FVTM.PageIndex = i



        'avoid entering zero but allow decimals


    End Sub

    Sub backClick(sender As Object, e As CommandEventArgs)

        Dim i As Int32 = Me.FVTM.PageIndex
        i = i - 1
        Me.FVTM.PageIndex = i

    End Sub

    Sub nextClick(sender As Object, e As CommandEventArgs)

        Dim i As Int32 = Me.FVTM.PageIndex
        i = i + 1
        Me.FVTM.PageIndex = i

    End Sub

    Sub resetClick(sender As Object, e As CommandEventArgs)

        Dim GVFunctProd As GridView = FindControl("ctl00$MainContent$FVTM$GVFunctProd")

        For Each row As GridViewRow In GVFunctProd.Rows

            Dim therowindex As Integer = row.RowIndex

            Dim ErrorLabel As Label = DirectCast(GVFunctProd.Rows(therowindex).Cells(3).FindControl("ErrorLabel"), Label)


            ErrorLabel.Text = ""

        Next
        FVTM.DataBind()

    End Sub
End Class
