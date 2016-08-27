Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration

Partial Class SCP
    Inherits System.Web.UI.Page

    Protected WithEvents TMProd As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents DistFunct As Global.System.Web.UI.WebControls.Repeater

    Dim FunctHoursTotal As Decimal = 0
    Dim GoalHoursTotal As Decimal = 0
    Dim FinalPercentofGoal As Decimal = 0
    Dim AmtTotal As Decimal = 0
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
            StDateCalendarExtender.EndDate = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            EndDateCalendarExtender.EndDate = FormatDateTime(SugImpDate, DateFormat.ShortDate)

            DistTM.Visible = "True"
        End If

    End Sub

    Protected Sub SearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click

        FunctHoursTotal = 0
        GoalHoursTotal = 0
        FinalPercentofGoal = 0
        AmtTotal = 0

        'If they searched by TM Last Name
        If TMListbyName.SelectedIndex = 0 Then

            DistTM.Visible = "True"
            DistTM.DataSourceID = "TMListByLastName"
            DistTM.DataBind()

        Else

            If TMList.SelectedValue = "All" Then
                If FunctList.SelectedValue = "All" Then

                    'All TMs and All Functions
                    DistTM.Visible = "True"
                    DistTM.DataSourceID = "TMProductofDate"
                    DistTM.DataBind()

                Else
                    'All TMs and One Function
                    DistTM.Visible = "True"
                    DistTM.DataSourceID = "TMProductofDateOneFunct"
                    DistTM.DataBind()

                End If

            Else 'One TM was selected

                DistTM.Visible = "True"
                DistTM.DataSourceID = "TMListName"
                DistTM.DataBind()

            End If
        End If

        If DistTM.Items.Count < 1 Then

            NoData.Visible = "True"
            DistTM.Visible = "False"
        Else
            NoData.Visible = "False"
        End If


    End Sub
    Protected Sub ClearSearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearSearchButton.Click

        TMListbyName.Items.Clear()
        TMList.Visible = "true"
        TMListbyName.Visible = "false"

        FunctList.SelectedValue = "All"
        TMList.SelectedValue = "All"
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        DistTM.Visible = "False"
        TMNotFound.Visible = "False"
        NoData.Visible = "False"

    End Sub
    Protected Sub DistTM_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DistTM.ItemDataBound

        FunctHoursTotal = 0
        GoalHoursTotal = 0
        FinalPercentofGoal = 0
        AmtTotal = 0

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection String

        Dim TM As HiddenField = e.Item.FindControl("HiddenTMField")
        OuterTMField.Value = TM.Value

        Dim Date1 As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
        Dim Date2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")
        Dim FunctList As DropDownList = Page.FindControl("ctl00$MainContent$FunctList")

        Dim sqlStrDistFunct As String = "SELECT DISTINCT Functions.Description, Functions.Rate, PROD.Function FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE (((Prod.DTE) Between [@Date1] And [@Date2]) AND ((Prod.EmployeeID)=[@TM])) AND (PROD.Department=8) ORDER BY Functions.Description"

        Dim dbCommDistFunct As New OleDbCommand(sqlStrDistFunct, dbConn)
        dbCommDistFunct.Parameters.Add("@Date1", OleDbType.Date)
        dbCommDistFunct.Parameters("@Date1").Value = Date1.Text
        dbCommDistFunct.Parameters.Add("@Date2", OleDbType.Date)
        dbCommDistFunct.Parameters("@Date2").Value = Date2.Text
        dbCommDistFunct.Parameters.Add("@TM", OleDbType.VarWChar)
        dbCommDistFunct.Parameters("@TM").Value = TM.Value

        Dim sqlStrDistFunctOne As String = "SELECT DISTINCT Functions.Description, Functions.Rate, PROD.Function FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE (((Prod.DTE) Between [@Date1] And [@Date2]) AND ((Prod.EmployeeID)=[@TM]) AND ((Prod.Function)=[@Funct])) AND (PROD.Department=8) ORDER BY Functions.Description"

        Dim dbCommDistFunctOne As New OleDbCommand(sqlStrDistFunctOne, dbConn)
        dbCommDistFunctOne.Parameters.Add("@Date1", OleDbType.Date)
        dbCommDistFunctOne.Parameters("@Date1").Value = Date1.Text
        dbCommDistFunctOne.Parameters.Add("@Date2", OleDbType.Date)
        dbCommDistFunctOne.Parameters("@Date2").Value = Date2.Text
        dbCommDistFunctOne.Parameters.Add("@TM", OleDbType.VarWChar)
        dbCommDistFunctOne.Parameters("@TM").Value = TM.Value
        dbCommDistFunctOne.Parameters.Add("@Funct", OleDbType.VarWChar)
        dbCommDistFunctOne.Parameters("@Funct").Value = FunctList.SelectedValue

        Dim DistFunct As Repeater
        DistFunct = e.Item.FindControl("DistFunct")

        Try
            dbConn.Open()

            If FunctList.SelectedValue = "All" Then

                Dim ReaderAll As OleDbDataReader
                ReaderAll = dbCommDistFunct.ExecuteReader()
                DistFunct.DataSource = ReaderAll
                DistFunct.DataBind()
                ReaderAll.Close()
                DistFunct.Visible = "True"

            Else
                Dim ReaderAllOne As OleDbDataReader
                ReaderAllOne = dbCommDistFunctOne.ExecuteReader()
                DistFunct.DataSource = ReaderAllOne
                DistFunct.DataBind()
                ReaderAllOne.Close()
                DistFunct.Visible = "True"
            End If

        Finally
            dbConn.Close()

        End Try

        Dim NoDataDistTM As Label = e.Item.FindControl("NoDataDistTM")
        Dim EmployeeIDLabel As Label = e.Item.FindControl("EmployeeIDLabel")

        If DistFunct.Items.Count < 1 Then
            NoDataDistTM.Visible = "True"
            EmployeeIDLabel.Visible = "False"
        Else
            NoDataDistTM.Visible = "False"
        End If

    End Sub
    Public Sub DistFunct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DistFunct.ItemDataBound


        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            FunctHoursTotal = 0
            GoalHoursTotal = 0
            FinalPercentofGoal = 0
            AmtTotal = 0
            NullCount = 0

            Dim Date1 As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
            Dim Date2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")

            Dim TM As HiddenField = e.Item.FindControl("HiddenTMField")
            Dim Funct As HiddenField = e.Item.FindControl("HiddenFunctionField")

            Dim FunctLabel As Label = e.Item.FindControl("FunctLabel")
            Dim RateLabel As Label = e.Item.FindControl("RateLabel")

            Dim TMLabel As Label = e.Item.NamingContainer.NamingContainer.FindControl("EmployeeIDLabel")

            Dim GoalRateLabel As Label = e.Item.FindControl("GoalRateLabel")

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection String

            Dim TMSelected As TextBox = Page.FindControl("ctl00$MainContent$TMSelected")
            TMSelected.Text = OuterTMField.Value

            Dim TMProd As GridView
            TMProd = e.Item.FindControl("TMProd")

            'Now it's where the TM is the TM on the hidden field of the outer repeater

            Dim sqlStringProd As String = "SELECT Prod.DTE, Prod.EmployeeID, Prod.Function, Prod.Amount, Prod.FunctHours, Functions.Rate, PROD.Amount/PROD.FunctHours AS ActualRate, PROD.Amount/Functions.Rate AS GoalHours,  PROD.Amount/Functions.Rate/PROD.FunctHours AS PercentAvg, Prod.Shift FROM TMs INNER JOIN (Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function)) ON TMs.EmployeeID = Prod.EmployeeID  WHERE ((Prod.DTE) Between [@Date1] And [@Date2]) AND (Prod.EmployeeID)=[@TM] AND ((Prod.Function)=[@Funct]) AND (PROD.Department=8) AND (PROD.Shift=3)  ORDER BY Prod.DTE, Functions.Description, Prod.EmployeeID"

            Try
                dbConn.Open()

                Dim dbCommProd As New OleDbCommand(sqlStringProd, dbConn)
                dbCommProd.Parameters.Add("@Date1", OleDbType.Date)
                dbCommProd.Parameters("@Date1").Value = Date1.Text
                dbCommProd.Parameters.Add("@Date2", OleDbType.Date)
                dbCommProd.Parameters("@Date2").Value = Date2.Text
                dbCommProd.Parameters.Add("@TM", OleDbType.VarWChar)
                dbCommProd.Parameters("@TM").Value = TMSelected.Text
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
            GoalHoursTotal += DataBinder.Eval(e.Row.DataItem, "GoalHours")

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(0).Text = "Totals"
            e.Row.Cells(2).Text = AmtTotal.ToString("f0")
            e.Row.Cells(5).Text = GoalHoursTotal.ToString("f2")

            If FunctHoursTotal > 0 Then

                Dim FinalPercentofGoal As Decimal = GoalHoursTotal / FunctHoursTotal
                Dim ActRate As Decimal = AmtTotal / FunctHoursTotal

                

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
        NoData.Visible = "False"
        DistTM.Visible = "False"
    End Sub
End Class
