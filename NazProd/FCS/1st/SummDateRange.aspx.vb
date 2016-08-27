Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration
Partial Class FCS
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim TodaysDate As Date = DateTime.Today()
            Dim SugImpDate As Date
            Dim DTE As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
            Dim DTE2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")

            If TodaysDate.DayOfWeek = DayOfWeek.Monday Then
                SugImpDate = DateTime.Today.AddDays(-3)
            Else
                SugImpDate = DateTime.Today.AddDays(-1)
            End If

            DTE.Text = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            DTE2.Text = FormatDateTime(SugImpDate, DateFormat.ShortDate)
            CalendarExtender1.EndDate = FormatDateTime(SugImpDate, DateFormat.ShortDate)

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim sqlStrProdbyTMAll As String = "INSERT INTO SummTempTable ( Department, Shift, Rate, SumOfAmount, SumOfFunctHours, Description ) SELECT 3 AS Department, 1 AS Shift, Functions.Rate, Sum(Prod.Amount) AS SumOfAmount, Sum(Prod.FunctHours) AS SumOfFunctHours,  Functions.Description FROM Functions  INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE(Prod.Shift) = 1 AND (Prod.Department) =3 GROUP BY Functions.Rate, Functions.Description, Prod.DTE, Prod.Function HAVING (Prod.DTE) Between @Date1 And @Date2"

            Dim sqlStrProdbyTM As String = "INSERT INTO SummTempTable ( Department, Shift, Rate, SumOfAmount, SumOfFunctHours,  Description ) SELECT 3 AS Department, 1 AS Shift, Functions.Rate, Sum(Prod.Amount) AS SumOfAmount, Sum(Prod.FunctHours) AS SumOfFunctHours, Functions.Description FROM Functions  INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE(Prod.Shift) = 1 AND (Prod.Department) =3 GROUP BY Functions.Rate, Functions.Description, Prod.DTE, Prod.Function HAVING (Prod.DTE) Between @Date1 And @Date2 AND (Prod.Function)=@Funct"

            Dim sqlStrFinal As String = "SELECT SummOfSummFCS1stSummTempTable.Description, SummOfSummFCS1stSummTempTable.Rate, SummOfSummFCS1stSummTempTable.SumOfSumOfAmount AS Amount, SummOfSummFCS1stSummTempTable.SumOfSumOfFunctHours AS FunctHours,  [SumOfSumOfAmount]/[SumOfSumOfFunctHours] AS ActualRate, [SumOfSumofAmount]/[Rate] AS GoalHours, ([SumOfSumOfAmount]/[Rate])/[SumOfSumOfFunctHours] AS PercentAvg FROM SummOfSummFCS1stSummTempTable"

            Dim sqlStrDeleteTmpRec As String = "DELETE SummTempTable.* FROM SummTempTable"

            Dim Date1 As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
            Dim Date2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")
            Dim FunctList As DropDownList = Page.FindControl("ctl00$MainContent$FunctList")

            Dim dbConnDeleteTmpRec As New OleDbCommand(sqlStrDeleteTmpRec, dbConn)

            Dim dbCommProdbyTMAll As New OleDbCommand(sqlStrProdbyTMAll, dbConn)
            dbCommProdbyTMAll.Parameters.Add("@Date1", OleDbType.Date)
            dbCommProdbyTMAll.Parameters("@Date1").Value = Date1.Text
            dbCommProdbyTMAll.Parameters.Add("@Date2", OleDbType.Date)
            dbCommProdbyTMAll.Parameters("@Date2").Value = Date2.Text

            Dim dbCommProdbyTM As New OleDbCommand(sqlStrProdbyTM, dbConn)
            dbCommProdbyTM.Parameters.Add("@Date1", OleDbType.Date)
            dbCommProdbyTM.Parameters("@Date1").Value = Date1.Text
            dbCommProdbyTM.Parameters.Add("@Date2", OleDbType.Date)
            dbCommProdbyTM.Parameters("@Date2").Value = Date2.Text
            dbCommProdbyTM.Parameters.Add("@Funct", OleDbType.VarWChar)
            dbCommProdbyTM.Parameters("@Funct").Value = FunctList.SelectedValue

            Dim dbCommFinal As New OleDbCommand(sqlStrFinal, dbConn)

            Try
                dbConn.Open()
                dbConnDeleteTmpRec.ExecuteNonQuery()
            Finally
                dbConn.Close()
            End Try

            Try
                dbConn.Open()
                dbCommProdbyTMAll.ExecuteNonQuery()
            Finally
                dbConn.Close()
            End Try

            Try
                dbConn.Open()

                Dim ReaderAll As OleDbDataReader = dbCommFinal.ExecuteReader()

                TMProd.Visible = "True"
                TMProd.DataSource = ReaderAll
                TMProd.DataBind()
                ReaderAll.Close()

            Finally
                dbConn.Close()

            End Try

        End If


    End Sub

    Protected Sub SearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click

        'Connection Strings to Access DB
        Dim connectionStringAccess As String = _
       ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString

        Dim dbConn As New OleDbConnection(connectionStringAccess)
        'end Connection String

        Dim sqlStrProdbyTMAll As String = "INSERT INTO SummTempTable ( Department, Shift, Rate, SumOfAmount, SumOfFunctHours, Description ) SELECT 3 AS Department, 1 AS Shift, Functions.Rate, Sum(Prod.Amount) AS SumOfAmount, Sum(Prod.FunctHours) AS SumOfFunctHours, Functions.Description FROM Functions  INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE(Prod.Shift) = 1 AND (Prod.Department) =3 GROUP BY Functions.Rate, Functions.Description, Prod.DTE, Prod.Function HAVING (Prod.DTE) Between @Date1 And @Date2"

        Dim sqlStrProdbyTM As String = "INSERT INTO SummTempTable ( Department, Shift, Rate, SumOfAmount, SumOfFunctHours,  Description ) SELECT 3 AS Department, 1 AS Shift, Functions.Rate, Sum(Prod.Amount) AS SumOfAmount, Sum(Prod.FunctHours) AS SumOfFunctHours, Functions.Description FROM Functions  INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE(Prod.Shift) = 1 AND (Prod.Department) =3 GROUP BY Functions.Rate, Functions.Description, Prod.DTE, Prod.Function HAVING (Prod.DTE) Between @Date1 And @Date2 AND (Prod.Function)=@Funct"

        Dim sqlStrFinal As String = "SELECT SummOfSummFCS1stSummTempTable.Description, SummOfSummFCS1stSummTempTable.Rate, SummOfSummFCS1stSummTempTable.SumOfSumOfAmount AS Amount, SummOfSummFCS1stSummTempTable.SumOfSumOfFunctHours AS FunctHours,  [SumOfSumOfAmount]/[SumOfSumOfFunctHours] AS ActualRate, [SumOfSumofAmount]/[Rate] AS GoalHours, ([SumOfSumOfAmount]/[Rate])/[SumOfSumOfFunctHours] AS PercentAvg FROM SummOfSummFCS1stSummTempTable"

        Dim sqlStrDeleteTmpRec As String = "DELETE SummTempTable.* FROM SummTempTable"

        Dim Date1 As TextBox = Page.FindControl("ctl00$MainContent$txtStartDate")
        Dim Date2 As TextBox = Page.FindControl("ctl00$MainContent$txtEndDate")
        Dim FunctList As DropDownList = Page.FindControl("ctl00$MainContent$FunctList")

        Dim dbConnDeleteTmpRec As New OleDbCommand(sqlStrDeleteTmpRec, dbConn)

        Dim dbCommProdbyTMAll As New OleDbCommand(sqlStrProdbyTMAll, dbConn)
        dbCommProdbyTMAll.Parameters.Add("@Date1", OleDbType.Date)
        dbCommProdbyTMAll.Parameters("@Date1").Value = Date1.Text
        dbCommProdbyTMAll.Parameters.Add("@Date2", OleDbType.Date)
        dbCommProdbyTMAll.Parameters("@Date2").Value = Date2.Text

        Dim dbCommProdbyTM As New OleDbCommand(sqlStrProdbyTM, dbConn)
        dbCommProdbyTM.Parameters.Add("@Date1", OleDbType.Date)
        dbCommProdbyTM.Parameters("@Date1").Value = Date1.Text
        dbCommProdbyTM.Parameters.Add("@Date2", OleDbType.Date)
        dbCommProdbyTM.Parameters("@Date2").Value = Date2.Text
        dbCommProdbyTM.Parameters.Add("@Funct", OleDbType.VarWChar)
        dbCommProdbyTM.Parameters("@Funct").Value = FunctList.SelectedValue

        Dim dbCommFinal As New OleDbCommand(sqlStrFinal, dbConn)

        Try
            dbConn.Open()
            dbConnDeleteTmpRec.ExecuteNonQuery()
        Finally
            dbConn.Close()
        End Try


        If FunctList.SelectedValue = "All" Then

            Try
                dbConn.Open()
                dbCommProdbyTMAll.ExecuteNonQuery()
            Finally
                dbConn.Close()
            End Try

            Try
                dbConn.Open()

                Dim ReaderAll As OleDbDataReader = dbCommFinal.ExecuteReader()
                
                TMProd.Visible = "True"
                TMProd.DataSource = ReaderAll
                TMProd.DataBind()
                ReaderAll.Close()

            Finally
                dbConn.Close()

            End Try


        Else


            Try
                dbConn.Open()
                dbCommProdbyTM.ExecuteNonQuery()
            Finally
                dbConn.Close()
            End Try

            Try
                dbConn.Open()

                Dim ReaderSelected As OleDbDataReader = dbCommFinal.ExecuteReader()

                TMProd.Visible = "True"
                TMProd.DataSource = ReaderSelected
                TMProd.DataBind()
                ReaderSelected.Close()

            Finally
                dbConn.Close()

            End Try

        End If


    End Sub

    Protected Sub ClearSearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearSearchButton.Click


        FunctList.SelectedValue = "All"
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        TMProd.Visible = "False"

    End Sub

    Protected Sub txtStartDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStartDate.TextChanged
        txtEndDate.Text = txtStartDate.Text

    End Sub
End Class
