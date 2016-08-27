Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration

Partial Class UTILITY
    Inherits System.Web.UI.Page

    Protected WithEvents TMProd As Global.System.Web.UI.WebControls.GridView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            DistTM.Visible = "False"


        End If

    End Sub

    Protected Sub SearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click

        'If all Tms selected and all Functions
        If TMList.SelectedValue = "All" Then
            If FunctList.SelectedValue = "All" Then

                DistTM.Visible = "True"
                DistTM.DataSourceID = "TMProductofDate"
                DistTM.DataBind()

                If DistTM.Items.Count < 1 Then

                    NoData.Visible = "True"
                Else
                    NoData.Visible = "False"
                End If

                'If all Tms selected and One Functions
            Else
                DistTM.Visible = "True"
                DistTM.DataSourceID = "TMProductofDateOneFunct"
                DistTM.DataBind()
                If DistTM.Items.Count < 1 Then

                    NoData.Visible = "True"
                Else
                    NoData.Visible = "False"
                End If

            End If

        Else 'If one Tm selected and all Functions

            If FunctList.SelectedValue = "All" Then
                DistTM.Visible = "True"
                DistTM.DataSourceID = "OneTMProductofDateAllFunct"
                DistTM.DataBind()
                If DistTM.Items.Count < 1 Then

                    NoData.Visible = "True"
                Else
                    NoData.Visible = "False"
                End If

            Else 'If One Tms selected and one Functions

                DistTM.Visible = "True"
                DistTM.DataSourceID = "OneTMProductofDateOneFunct"
                DistTM.DataBind()
                If DistTM.Items.Count < 1 Then

                    NoData.Visible = "True"
                Else
                    NoData.Visible = "False"
                End If

            End If


            End If


    End Sub
    Protected Sub ClearSearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearSearchButton.Click

        TMList.Visible = "true"


        FunctList.SelectedValue = "All"
        TMList.SelectedValue = "All"
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        DistTM.Visible = "False"
        NoData.Visible = "False"

    End Sub
    Dim FunctHoursTotal As Decimal = 0
    Dim GoalHoursTotal As Decimal = 0
    Dim FinalPercentofGoal As Decimal = 0
    Dim MiscHoursTotal As Decimal = 0
    Dim TotalHoursTotal As Decimal = 0
    Dim AmtTotal As Decimal = 0
    Dim RecordCount As Int32 = 0

    Protected Sub DistTM_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DistTM.ItemDataBound

        FunctHoursTotal = 0
        GoalHoursTotal = 0
        FinalPercentofGoal = 0
        MiscHoursTotal = 0
        TotalHoursTotal = 0
        AmtTotal = 0
        NullCount = 0
        RecordCount = 0

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection String

        Dim TMProd As New GridView
        TMProd = e.Item.FindControl("TMProd")

        Dim TM As HiddenField = e.Item.FindControl("HiddenTMField")
        Dim Date1 As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
        Dim Date2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")
        Dim FunctList As DropDownList = Page.FindControl("ctl00$MainContent$FunctList")


        Dim sqlStringAllFunct As String = "SELECT PROD.DTE, Functions.Description, PROD.EmployeeID, PROD.Function, PROD.Amount, PROD.FunctHours,  Functions.Rate, PROD.Amount/PROD.FunctHours AS ActualRate, PROD.Amount/Functions.Rate AS GoalHours,  (PROD.Amount/Functions.Rate)/PROD.FunctHours AS PercentAvg FROM Functions INNER JOIN PROD ON (Functions.Department = PROD.Department) AND (Functions.Function = PROD.Function) WHERE ((PROD.DTE) Between [@Date1] And [@Date2]) AND ((PROD.EmployeeID)=[@TM]) ORDER BY PROD.DTE, PROD.EmployeeID, PROD.Function"

        Dim sqlStrOneFunct As String = "SELECT PROD.DTE, Functions.Description, PROD.EmployeeID, PROD.Function, PROD.Amount, PROD.FunctHours,  Functions.Rate, PROD.Amount/PROD.FunctHours AS ActualRate, PROD.Amount/Functions.Rate AS GoalHours,  (PROD.Amount/Functions.Rate)/PROD.FunctHours AS PercentAvg FROM Functions INNER JOIN PROD ON (Functions.Department = PROD.Department) AND (Functions.Function = PROD.Function) WHERE ((PROD.DTE) Between [@Date1] And [@Date2]) AND ((PROD.EmployeeID)=[@TM]) AND (PROD.Function =@Funct ) ORDER BY PROD.DTE, PROD.EmployeeID, PROD.Function"

        Dim sqlStrEmpCount As String = "SELECT Count(PROD.EmployeeID) AS CountOfEmployeeID FROM PROD WHERE (PROD.DTE) Between @Date1 And @Date2 AND (PROD.EmployeeID =@TM ) AND (PROD.Shift = 3)"

        Dim dbCommCountEmp As New OleDbCommand(sqlStrEmpCount, dbConn)
        dbCommCountEmp.Parameters.Add("@Date1", OleDbType.Date)
        dbCommCountEmp.Parameters("@Date1").Value = Date1.Text
        dbCommCountEmp.Parameters.Add("@Date2", OleDbType.Date)
        dbCommCountEmp.Parameters("@Date2").Value = Date2.Text
        dbCommCountEmp.Parameters.Add("@TM", OleDbType.VarWChar)
        dbCommCountEmp.Parameters("@TM").Value = TM.Value

       
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then


            Try
                dbConn.Open()
                'Is this TM in the productivity table for this date

                RecordCount = Convert.ToInt32(dbCommCountEmp.ExecuteScalar())

                If RecordCount <> 0 Then
                    If FunctList.SelectedValue <> "All" Then

                        Dim dbCommProdOne As New OleDbCommand(sqlStrOneFunct, dbConn)
                        dbCommProdOne.Parameters.Add("@Date1", OleDbType.Date)
                        dbCommProdOne.Parameters("@Date1").Value = Date1.Text
                        dbCommProdOne.Parameters.Add("@Date2", OleDbType.Date)
                        dbCommProdOne.Parameters("@Date2").Value = Date2.Text
                        dbCommProdOne.Parameters.Add("@TM", OleDbType.VarWChar)
                        dbCommProdOne.Parameters("@TM").Value = TM.Value
                        dbCommProdOne.Parameters.Add("@Funct", OleDbType.VarWChar)
                        dbCommProdOne.Parameters("@Funct").Value = FunctList.SelectedValue

                        Dim ReaderAll As OleDbDataReader = dbCommProdOne.ExecuteReader()

                        TMProd.DataSource = ReaderAll
                        TMProd.DataBind()
                        ReaderAll.Close()

                    Else

                        Dim dbCommProdAll As New OleDbCommand(sqlStringAllFunct, dbConn)
                        dbCommProdAll.Parameters.Add("@Date1", OleDbType.Date)
                        dbCommProdAll.Parameters("@Date1").Value = Date1.Text
                        dbCommProdAll.Parameters.Add("@Date2", OleDbType.Date)
                        dbCommProdAll.Parameters("@Date2").Value = Date2.Text
                        dbCommProdAll.Parameters.Add("@TM", OleDbType.VarWChar)
                        dbCommProdAll.Parameters("@TM").Value = TM.Value

                        Dim ReaderAll As OleDbDataReader = dbCommProdAll.ExecuteReader()

                        TMProd.DataSource = ReaderAll
                        TMProd.DataBind()
                        ReaderAll.Close()

                    End If
                End If


            Finally
                dbConn.Close()

            End Try
        End If


    End Sub

    Dim NullCount As Integer = 0

    Protected Sub TMProd_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles TMProd.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'add the FunctHours and GoalHours to the running total values


            If Not IsDBNull((DataBinder.Eval(e.Row.DataItem, "FunctHours"))) Then
                FunctHoursTotal += DataBinder.Eval(e.Row.DataItem, "FunctHours")
                TotalHoursTotal += DataBinder.Eval(e.Row.DataItem, "TotalHours")

            Else
                NullCount = NullCount + 1
            End If

            AmtTotal += DataBinder.Eval(e.Row.DataItem, "Amount")
            GoalHoursTotal += DataBinder.Eval(e.Row.DataItem, "GoalHours")
            MiscHoursTotal += DataBinder.Eval(e.Row.DataItem, "MiscHours")

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            If FunctHoursTotal > 0 Then

                Dim FinalPercentofGoal As Decimal = GoalHoursTotal / FunctHoursTotal
                Dim ActRate As Decimal = AmtTotal / FunctHoursTotal

                e.Row.Cells(0).Text = "Totals"
                e.Row.Cells(3).Text = AmtTotal.ToString("f0")
                e.Row.Cells(6).Text = GoalHoursTotal.ToString("f1")

                If NullCount = 0 Then

                    e.Row.Cells(4).Text = FunctHoursTotal.ToString("f1")
                    e.Row.Cells(5).Text = ActRate.ToString("f1")
                    e.Row.Cells(7).Text = FinalPercentofGoal.ToString("p1")
                    e.Row.Cells(8).Text = MiscHoursTotal.ToString("f1")
                    e.Row.Cells(9).Text = TotalHoursTotal.ToString("f1")
                End If
            End If



        End If
    End Sub

    Protected Sub txtStartDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged
        txtEndDate.Text = txtStartDate.Text

    End Sub


End Class
