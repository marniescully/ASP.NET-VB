
Imports System.Data.OleDb
Imports System.Web
Imports System.Data
Imports System.Configuration

Partial Class REC
    Inherits System.Web.UI.Page

    Protected WithEvents TMProd As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents DistFunct As Global.System.Web.UI.WebControls.Repeater

    Dim FunctHoursTotal As Decimal = 0
    Dim GoalHoursTotal As Decimal = 0
    Dim FinalPercentofGoal As Decimal = 0
    Dim AmtTotal As Decimal = 0
    Dim TagTotal As Decimal = 0
    Dim NullCount As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim TodaysDate As Date = DateTime.Today()
            Dim SugImpDate As Date
            Dim txtStartDate As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")

            If TodaysDate.DayOfWeek = DayOfWeek.Monday Then
                SugImpDate = DateTime.Today.AddDays(-3)
            Else
                SugImpDate = DateTime.Today.AddDays(-1)
            End If

            txtStartDate.Text = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            txtEndDate.Text = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            CalendarExtender1.EndDate = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            CalendarExtender2.EndDate = FormatDateTime(SugImpDate, DateFormat.ShortDate)

            DateRepeat.DataSourceID = "DatesDataSource"
            DateRepeat.DataBind()
            DateRepeat.Visible = "True"
        End If

    End Sub

    Protected Sub ClearSearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearSearchButton.Click

        NoData.Visible = "False"
        FunctList.SelectedValue = "All"
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        DateRepeat.Visible = "False"


    End Sub
    Public Sub DateRepeat_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DateRepeat.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim sqlStrEmpCount As String = "SELECT Count(PROD.EmployeeID) AS CountOfEmployeeID FROM PROD WHERE (PROD.DTE=@Date1) AND (PROD.Function = @Funct)  AND (PROD.Department = 1)  AND (PROD.Shift = 2)"

            Dim sqlStrProductFunctofDate As String = "SELECT DISTINCT Functions.Description, Functions.Function, Functions.Rate FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE (PROD.DTE=@Date1) AND (PROD.Department = 1) AND (PROD.Shift = 2)"

            Dim hfDate As HiddenField
            hfDate = e.Item.FindControl("HiddenDateField")
            OuterDateField.Value = hfDate.Value

            Dim FunctList As DropDownList = Page.FindControl("ctl00$MainContent$FunctList")

            Dim dbCommCountEmp As New OleDbCommand(sqlStrEmpCount, dbConn)
            dbCommCountEmp.Parameters.Add("@Date1", OleDbType.Date)
            dbCommCountEmp.Parameters("@Date1").Value = hfDate.Value
            dbCommCountEmp.Parameters.Add("@Funct", OleDbType.VarWChar)
            dbCommCountEmp.Parameters("@Funct").Value = FunctList.SelectedValue

            Dim dbCommProductFunctofDate As New OleDbCommand(sqlStrProductFunctofDate, dbConn)
            dbCommProductFunctofDate.Parameters.Add("@Date1", OleDbType.Date)
            dbCommProductFunctofDate.Parameters("@Date1").Value = hfDate.Value


            Dim RecordCount As Int32 = 0
            Dim DteLabel As Label = e.Item.FindControl("DteLabel")

            If FunctList.SelectedValue <> "All" Then

                Try
                    dbConn.Open()
                    RecordCount = Convert.ToInt32(dbCommCountEmp.ExecuteScalar())

                    If RecordCount = 0 Then
                        DteLabel.Text = ""
                        Exit Sub
                    End If


                Finally
                    dbConn.Close()
                End Try

            End If


            Dim DistFunct As Repeater
            DistFunct = e.Item.FindControl("DistFunct")

            If FunctList.SelectedValue = "All" Then

                Try
                    dbConn.Open()
                    Dim ReaderAll As OleDbDataReader = dbCommProductFunctofDate.ExecuteReader()
                    DistFunct.DataSource = ReaderAll
                    DistFunct.DataBind()
                    DistFunct.Visible = "True"
                Finally
                    dbConn.Close()
                End Try


                If DistFunct.Items.Count < 1 Then
                    NoData.Visible = "True"
                    DteLabel.Visible = "False"
                Else
                    NoData.Visible = "False"
                    DteLabel.Visible = "True"
                End If

            Else

                DistFunct.DataSourceID = "ProductFunctofDateOne"
                DistFunct.DataBind()
                DistFunct.Visible = "True"

                If DistFunct.Items.Count < 1 Then
                    NoData.Visible = "True"
                    DteLabel.Visible = "False"
                Else
                    NoData.Visible = "False"
                    DteLabel.Visible = "True"
                End If

            End If

        End If

    End Sub
    Public Sub SearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click

        DateRepeat.DataSourceID = "DatesDataSource"
        DateRepeat.DataBind()
        DateRepeat.Visible = "True"

        If DateRepeat.Items.Count < 1 Then
            NoData.Visible = "True"
        Else
            NoData.Visible = "False"
        End If


        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection Strings

        Dim sqlStrFunctCount As String = "SELECT Count(Functions.Function) AS CountOfFunction FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) GROUP BY PROD.DTE, PROD.Function,PROD.Department, PROD.Shift HAVING (PROD.DTE) Between @Date1 And @Date2 AND (PROD.Function)=@Funct AND (PROD.Department)=1 AND (PROD.Shift)=2"

        Dim Date1 As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
        Dim Date2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")
        Dim FunctList As DropDownList = Page.FindControl("ctl00$MainContent$FunctList")
        Dim RecordCount As Int32 = 0

        If FunctList.SelectedValue = "All" Then

            If DateRepeat.Items.Count < 1 Then
                NoData.Visible = "True"
            Else
                NoData.Visible = "False"
            End If

        Else
            Try
                dbConn.Open()

                Dim dbCommCountFunct As New OleDbCommand(sqlStrFunctCount, dbConn)
                dbCommCountFunct.Parameters.Add("@Date1", OleDbType.Date)
                dbCommCountFunct.Parameters("@Date1").Value = Date1.Text
                dbCommCountFunct.Parameters.Add("@Date2", OleDbType.Date)
                dbCommCountFunct.Parameters("@Date2").Value = Date2.Text
                dbCommCountFunct.Parameters.Add("@Funct", OleDbType.VarWChar)
                dbCommCountFunct.Parameters("@Funct").Value = FunctList.SelectedValue

                RecordCount = Convert.ToInt32(dbCommCountFunct.ExecuteScalar())

                If RecordCount < 1 Then

                    NoData.Visible = "True"

                Else
                    NoData.Visible = "False"


                End If

            Finally
                dbConn.Close()
            End Try

        End If


    End Sub

    

    Public Sub DistFunct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DistFunct.ItemDataBound


        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            FunctHoursTotal = 0
            GoalHoursTotal = 0
            FinalPercentofGoal = 0
            AmtTotal = 0
            TagTotal = 0
            NullCount = 0

            Dim FunctLabel As Label = e.Item.FindControl("FunctLabel")
            Dim RateLabel As Label = e.Item.FindControl("RateLabel")

            Dim DteLabel As Label = e.Item.NamingContainer.NamingContainer.FindControl("DteLabel")

            Dim GoalRateLabel As Label = e.Item.FindControl("GoalRateLabel")

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim DateSelected As TextBox = Page.FindControl("ctl00$MainContent$DateSelected")
            DateSelected.Text = OuterDateField.Value

            Dim TMProd As GridView
            TMProd = e.Item.FindControl("TMProd")

            Dim Funct As HiddenField = e.Item.FindControl("HiddenFunctionField")

            'Now it's where the date is the date on the hidden field of the outer repeater

            Dim sqlStringProd As String = "SELECT  (TMS.FirstName + ' ' + TMS.LastName) AS FullName, Prod.DTE, Functions.Description, Prod.EmployeeID, Prod.Function, Prod.Amount, Prod.Tags, Prod.FunctHours,  Functions.Rate, PROD.Amount/PROD.FunctHours AS ActualRate, PROD.Amount/Functions.Rate AS GoalHours, PROD.Amount/Functions.Rate/PROD.FunctHours AS PercentAvg, Prod.Shift FROM TMs INNER JOIN (Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function)) ON TMs.EmployeeID = Prod.EmployeeID WHERE (((Prod.DTE)=[@Date1]) AND ((Prod.Function)=[@Funct]) AND ((Prod.Shift)=2) AND ((Prod.Department)=1))ORDER BY Prod.DTE, Functions.Description, Prod.EmployeeID"


            Try
                dbConn.Open()

                Dim dbCommProd As New OleDbCommand(sqlStringProd, dbConn)
                dbCommProd.Parameters.Add("@Date1", OleDbType.Date)
                dbCommProd.Parameters("@Date1").Value = DateSelected.Text
                dbCommProd.Parameters.Add("@Funct", OleDbType.VarWChar)
                dbCommProd.Parameters("@Funct").Value = Funct.Value

                Dim ReaderAll As OleDbDataReader = dbCommProd.ExecuteReader()

                TMProd.DataSource = ReaderAll
                TMProd.DataBind()

                ReaderAll.Close()


            Finally
                dbConn.Close()
            End Try


        End If
    End Sub


    Protected Sub TMProd_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles TMProd.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'add the FunctHours and GoalHours to the running total values


            If Not IsDBNull((DataBinder.Eval(e.Row.DataItem, "FunctHours"))) Then
                FunctHoursTotal += DataBinder.Eval(e.Row.DataItem, "FunctHours")


            Else
                NullCount = NullCount + 1
            End If

            AmtTotal += DataBinder.Eval(e.Row.DataItem, "Amount")
            If Not IsDBNull((DataBinder.Eval(e.Row.DataItem, "Tags"))) Then
                TagTotal += DataBinder.Eval(e.Row.DataItem, "Tags")
            End If
            GoalHoursTotal += DataBinder.Eval(e.Row.DataItem, "GoalHours")

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            If FunctHoursTotal > 0 Then

                Dim FinalPercentofGoal As Decimal = GoalHoursTotal / FunctHoursTotal
                Dim ActRate As Decimal = AmtTotal / FunctHoursTotal

                e.Row.Cells(0).Text = "Totals"
                e.Row.Cells(1).Text = AmtTotal.ToString("f0")
                e.Row.Cells(2).Text = TagTotal.ToString("f0")
                e.Row.Cells(5).Text = GoalHoursTotal.ToString("f2")

                If NullCount = 0 Then

                    e.Row.Cells(3).Text = FunctHoursTotal.ToString("f2")
                    e.Row.Cells(4).Text = ActRate.ToString("f2")
                    e.Row.Cells(6).Text = FinalPercentofGoal.ToString("p1")
                End If
            End If



        End If
    End Sub

    Protected Sub txtStartDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged
        txtEndDate.Text = txtStartDate.Text
        DateRepeat.Visible = "False"
        NoData.Visible = "False"
    End Sub
End Class

