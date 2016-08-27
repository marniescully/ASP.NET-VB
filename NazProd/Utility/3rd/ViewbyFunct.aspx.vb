
Imports System.Data.OleDb
Imports System.Web
Imports System.Data
Imports System.Configuration

Partial Class UTILITY
    Inherits System.Web.UI.Page

    Protected WithEvents TMProd As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents DistFunct As Global.System.Web.UI.WebControls.Repeater

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
            'end Connection String

            Dim sqlStrEmpCount As String = "SELECT Count(Shipping.EmployeeID) AS CountOfEmployeeID FROM Shipping WHERE (Shipping.DTE=@Date1) AND (Shipping.Function = @Funct)  AND (Shipping.Shift = 3)"
            Dim sqlStrProductFunctofDate As String = "SELECT DISTINCT SHIPFunctions.Description, SHIPFunctions.Function, SHIPFunctions.Rate FROM SHIPFunctions INNER JOIN Shipping ON SHIPFunctions.Function = Shipping.Function WHERE (Shipping.DTE=@Date1) AND (Shipping.Shift = 3)"

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
        'end Connection String

        Dim sqlStrFunctCount As String = "SELECT Count(SHIPFunctions.Function) AS CountOfFunction FROM SHIPFunctions INNER JOIN Shipping ON SHIPFunctions.Function = Shipping.Function GROUP BY Shipping.DTE, Shipping.Function, Shipping.Shift HAVING (Shipping.DTE) Between @Date1 And @Date2 AND (Shipping.Function)=@Funct AND (Shipping.Shift)=3"

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

    Dim FunctHoursTotal As Decimal = 0
    Dim GoalHoursTotal As Decimal = 0
    Dim FinalPercentofGoal As Decimal = 0
    Dim MiscHoursTotal As Decimal = 0
    Dim TotalHoursTotal As Decimal = 0
    Dim AmtTotal As Decimal = 0

    Public Sub DistFunct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DistFunct.ItemDataBound


        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            FunctHoursTotal = 0
            GoalHoursTotal = 0
            FinalPercentofGoal = 0
            MiscHoursTotal = 0
            TotalHoursTotal = 0
            AmtTotal = 0
            NullCount = 0

            Dim FunctLabel As Label = e.Item.FindControl("FunctLabel")
            Dim RateLabel As Label = e.Item.FindControl("RateLabel")

            Dim DteLabel As Label = e.Item.NamingContainer.NamingContainer.FindControl("DteLabel")

            Dim GoalRateLabel As Label = e.Item.FindControl("GoalRateLabel")

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection String

            Dim DateSelected As TextBox = Page.FindControl("ctl00$MainContent$DateSelected")
            DateSelected.Text = OuterDateField.Value

            Dim TMProd As GridView
            TMProd = e.Item.FindControl("TMProd")

            Dim Funct As HiddenField = e.Item.FindControl("HiddenFunctionField")

            'Now it's where the date is the date on the hidden field of the outer repeater

            Dim sqlStringProd As String = "SELECT Shipping.DTE, SHIPFunctions.Description, Shipping.EmployeeID, Shipping.Function, Shipping.Amount, Shipping.FunctHours, Shipping.MiscHours, SHIPFunctions.Rate, Shipping.Amount / Shipping.FunctHours AS ActualRate, Shipping.Amount / SHIPFunctions.Rate AS GoalHours, Shipping.FunctHours + Shipping.MiscHours AS TotalHours, Shipping.Amount / SHIPFunctions.Rate / Shipping.FunctHours AS PercentAvg, Shipping.Shift FROM (SHIPFunctions INNER JOIN Shipping ON SHIPFunctions.Function = Shipping.Function) WHERE (Shipping.DTE=@Date1) AND (Shipping.Function = @Funct)  AND (Shipping.Shift = 3)  ORDER BY Shipping.DTE, SHIPFunctions.Description, Shipping.EmployeeID"


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
                e.Row.Cells(1).Text = AmtTotal.ToString("f0")
                e.Row.Cells(4).Text = GoalHoursTotal.ToString("f1")

                If NullCount = 0 Then

                    e.Row.Cells(2).Text = FunctHoursTotal.ToString("f1")
                    e.Row.Cells(3).Text = ActRate.ToString("f1")
                    e.Row.Cells(5).Text = FinalPercentofGoal.ToString("p1")
                    e.Row.Cells(6).Text = MiscHoursTotal.ToString("f1")
                    e.Row.Cells(7).Text = TotalHoursTotal.ToString("f1")
                End If
            End If



        End If
    End Sub

    Protected Sub txtStartDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged
        txtEndDate.Text = txtStartDate.Text

    End Sub
End Class

