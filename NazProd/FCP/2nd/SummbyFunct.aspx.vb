
Imports System.Data.OleDb
Imports System.Web
Imports System.Data
Imports System.Configuration

Partial Class FCP
    Inherits System.Web.UI.Page

    Protected WithEvents TMProd As Global.System.Web.UI.WebControls.GridView

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Loads the previous picking date when page loads. 
        'If someone changes the date that date remains selected until it is changed again 

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

            DistFunct.DataSourceID = "ProductFunctofDate"
            DistFunct.DataBind()
            DistFunct.Visible = "True"

        End If



    End Sub
    Protected Sub ClearSearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearSearchButton.Click

        NoData.Visible = "False"
        FunctList.SelectedValue = "All"
        txtStartDate.Text = ""
        txtEndDate.Text = ""



    End Sub

    Public Sub SearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click

        Dim FunctList As DropDownList = Page.FindControl("ctl00$MainContent$FunctList")

        If FunctList.SelectedValue = "All" Then
            'Give the all data source to DistFunct whatever functions are within that date

            DistFunct.DataSourceID = "ProductFunctofDate"
            DistFunct.DataBind()
            DistFunct.Visible = "True"

            If DistFunct.Items.Count < 1 Then
                NoData.Visible = "True"

            Else
                NoData.Visible = "False"

            End If

        Else

            DistFunct.DataSourceID = "ProductFunctofDateOne"
            DistFunct.DataBind()
            DistFunct.Visible = "True"

            If DistFunct.Items.Count < 1 Then
                NoData.Visible = "True"
            Else
                NoData.Visible = "False"

            End If

        End If


    End Sub

    Dim FunctHoursTotal As Decimal = 0
    Dim GoalHoursTotal As Decimal = 0
    Dim FinalPercentofGoal As Decimal = 0
    Dim AmtTotal As Decimal = 0

    Public Sub DistFunct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DistFunct.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            FunctHoursTotal = 0
            GoalHoursTotal = 0
            FinalPercentofGoal = 0
            AmtTotal = 0
            NullCount = 0

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim TMProd As GridView
            TMProd = e.Item.FindControl("TMProd")

            Dim Date1 As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
            Dim Date2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")

            Dim Funct As HiddenField = e.Item.FindControl("HiddenFunctionField")

            'Now it's where the date is the date on the hidden field of the outer repeater

            Dim sqlStringProd As String = "SELECT Prod.DTE, Functions.Description, Prod.Function, Sum(Prod.Amount) AS SumOfAmount, Sum(Prod.FunctHours) AS SumOfFunctHours, Functions.Rate, [SumOfAmount]/[SumOfFunctHours] AS ActualRate, [SumOfAmount]/[Functions].[Rate] AS GoalHours, [SumOfAmount]/Functions.Rate/[SumOfFunctHours] AS PercentAvg, Prod.Shift FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) GROUP BY Prod.DTE, Functions.Description, Prod.Function, Functions.Rate, Prod.Shift,Prod.Department HAVING (((Prod.DTE) Between [@Date1] And [@Date2]) AND ((Prod.Function)=[@Funct]) AND ((Prod.Department)=2) AND ((Prod.Shift)=2)) ORDER BY Prod.DTE, Functions.Description"

            Dim RecordCount As Int32 = 0

            Try
                dbConn.Open()


                Dim dbCommProd As New OleDbCommand(sqlStringProd, dbConn)
                dbCommProd.Parameters.Add("@Date1", OleDbType.Date)
                dbCommProd.Parameters("@Date1").Value = Date1.Text
                dbCommProd.Parameters.Add("@Date2", OleDbType.Date)
                dbCommProd.Parameters("@Date2").Value = Date2.Text
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
    Dim NullCount As Integer = 0
    Protected Sub TMProd_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles TMProd.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'add the FunctHours and GoalHours to the running total values


            If Not IsDBNull((DataBinder.Eval(e.Row.DataItem, "SumOfFunctHours"))) Then
                FunctHoursTotal += DataBinder.Eval(e.Row.DataItem, "SumOfFunctHours")

            Else
                NullCount = NullCount + 1
            End If

            AmtTotal += DataBinder.Eval(e.Row.DataItem, "SumOfAmount")
            GoalHoursTotal += DataBinder.Eval(e.Row.DataItem, "GoalHours")


        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(0).Text = "Totals"
            e.Row.Cells(1).Text = AmtTotal.ToString("f0")
            e.Row.Cells(4).Text = GoalHoursTotal.ToString("f2")

            If FunctHoursTotal > 0 Then

                Dim FinalPercentofGoal As Decimal = GoalHoursTotal / FunctHoursTotal
                Dim ActRate As Decimal = AmtTotal / FunctHoursTotal

                If NullCount = 0 Then

                    e.Row.Cells(2).Text = FunctHoursTotal.ToString("f2")
                    e.Row.Cells(3).Text = ActRate.ToString("f2")
                    e.Row.Cells(5).Text = FinalPercentofGoal.ToString("p1")
                   
                End If
            End If



        End If
    End Sub

    Protected Sub txtStartDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged
        txtEndDate.Text = txtStartDate.Text
        NoData.Visible = "False"
        DistFunct.Visible = "False"

    End Sub
End Class

