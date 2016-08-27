Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration

Partial Class AddTM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim Amount As TextBox = Page.FindControl("ctl00$MainContent$AddTMProd$Amount")
            Dim FunctHours As TextBox = Page.FindControl("ctl00$MainContent$AddTMProd$FunctHours")
            Dim MiscHours As TextBox = Page.FindControl("ctl00$MainContent$AddTMProd$MiscHours")
           
            Amount.Text = 1
            FunctHours.Text = ""
            MiscHours.Text = 0
         

        End If
    End Sub


    Protected Sub AddTMProd_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewCommandEventArgs)


        Dim DTE As TextBox = Page.FindControl("ctl00$MainContent$AddTMProd$DTE")
        Dim EmpID As DropDownList = Page.FindControl("ctl00$MainContent$AddTMProd$EmployeeID")
        Dim Funct As DropDownList = Page.FindControl("ctl00$MainContent$AddTMProd$Function")
        Dim Amount As TextBox = Page.FindControl("ctl00$MainContent$AddTMProd$Amount")
        Dim FunctHours As TextBox = Page.FindControl("ctl00$MainContent$AddTMProd$FunctHours")
        Dim MiscHours As TextBox = Page.FindControl("ctl00$MainContent$AddTMProd$MiscHours")
       
        If e.CommandName = "Add" Then

            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim RecordCount As Int32 = 0

            Dim sqlStrTestInsert As String
            sqlStrTestInsert = "SELECT Prod.DTE, Prod.EmployeeID, Prod.Function FROM Prod WHERE (Prod.DTE)=@DTE AND (Prod.EmployeeID)=@EmployeeID AND (Prod.Function)=@Function AND (Prod.Department)=2 AND (Prod.Shift)=1"

            Dim InsertSqlString As String
            InsertSqlString = "INSERT INTO Prod (DTE, EmployeeID, Function, Department, Shift, Amount, FunctHours, MiscHours) VALUES (@DTE,@EmployeeID,@Function,@Dept,@Shift, @Amount,@FunctHours,@MiscHours)"


            Dim dbCommTestInsert As New OleDbCommand(sqlStrTestInsert, dbConn)
            dbCommTestInsert.Parameters.Add("@DTE", OleDbType.VarWChar)
            dbCommTestInsert.Parameters("@DTE").Value = DTE.Text
            dbCommTestInsert.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
            dbCommTestInsert.Parameters("@EmployeeID").Value = EmpID.SelectedValue
            dbCommTestInsert.Parameters.Add("@Function", OleDbType.VarWChar)
            dbCommTestInsert.Parameters("@Function").Value = Funct.SelectedValue

            Dim dbCommInsert As New OleDbCommand(InsertSqlString, dbConn)
            dbCommInsert.Parameters.Add("@DTE", OleDbType.Date)
            dbCommInsert.Parameters("@DTE").Value = DTE.Text
            dbCommInsert.Parameters.Add("@EmployeeID", OleDbType.VarWChar)
            dbCommInsert.Parameters("@EmployeeID").Value = EmpID.SelectedValue
            dbCommInsert.Parameters.Add("@Function", OleDbType.VarWChar)
            dbCommInsert.Parameters("@Function").Value = Funct.SelectedValue
            dbCommInsert.Parameters.Add("@Dept", OleDbType.Integer)
            dbCommInsert.Parameters("@Dept").Value = Convert.ToInt32(2)
            dbCommInsert.Parameters.Add("@Shift", OleDbType.Integer)
            dbCommInsert.Parameters("@Shift").Value = Convert.ToInt32(1)
            dbCommInsert.Parameters.Add("@Amount", OleDbType.Integer)
            dbCommInsert.Parameters("@Amount").Value = Convert.ToInt32(Amount.Text)
            dbCommInsert.Parameters.Add("@FunctHours", OleDbType.Double)
            dbCommInsert.Parameters("@FunctHours").Value = Convert.ToDouble(FunctHours.Text)
            dbCommInsert.Parameters.Add("@MiscHours", OleDbType.Double)
            dbCommInsert.Parameters("@MiscHours").Value = Convert.ToDouble(MiscHours.Text)
          

            Try
                dbConn.Open()
                Dim Reader As OleDbDataReader = dbCommTestInsert.ExecuteReader()

                While Reader.Read()
                    RecordCount += 1
                End While
                Reader.Close()
                dbConn.Close()

                If RecordCount = 1 Then

                    AddLabel.Text = "That productivity data already exists. Edit TM Productivity before adding."

                    Exit Sub
                End If

            Finally
                dbConn.Close()

            End Try

            Try
                dbConn.Open()
                dbCommInsert.ExecuteNonQuery()

            Finally
                dbConn.Close()
            End Try







            AddLabel.Text = "Add Full Case Pick 1st Shift Productivity"
            DTE.Text = ""
            Amount.Text = 1
            FunctHours.Text = ""
            MiscHours.Text = 0
         

        ElseIf e.CommandName = "Reset" Then

            AddLabel.Text = "Add Full Case Pick 1st Shift Productivity"
            DTE.Text = ""
            Amount.Text = 1
            FunctHours.Text = ""
            MiscHours.Text = 0
           
        End If


    End Sub
    Protected Sub AddTMProd_ModeChanging(ByVal sender As Object, ByVal e As DetailsViewModeEventArgs)


        Response.Write("<script type='text/javascript'>window.open('Shift.aspx','_parent')</script>")


    End Sub
End Class
