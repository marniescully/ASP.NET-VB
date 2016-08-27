Imports System.Data.OleDb
Imports System.Web.UI.Page
Imports System.Data
Imports System.Configuration
Partial Class FCP
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load, ShiftTM.DataBound


        If Not IsPostBack Then
            TMorSumm.Text = "Verify Team Members"
            ShiftTM.Visible = "false"
            ShiftTM.Columns(3).Visible = False
            ShiftTM.Columns(4).Visible = False
        End If

        If TMorSumm.Text = "View Summary" Then


            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim TMCount As Integer = 0

            Dim CountSqlString As String
            CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=2 AND (TMs.ShiftID)=1"

            Dim dbComm As New OleDbCommand(CountSqlString, dbConn)



            Try
                dbConn.Open()
                TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
                TMCountLabel.Text = TMCount & " Team Members"

                dbConn.Close()

            Finally
                dbConn.Close()
            End Try

        End If

    End Sub


    Protected Sub ShiftTM_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ShiftTM.RowCommand
        If e.CommandName = "Edit" Then
            ShiftTM.Columns(3).Visible = True
            ShiftTM.Columns(4).Visible = True
        End If

        If e.CommandName = "Cancel" Then
            ShiftTM.Columns(3).Visible = False
            ShiftTM.Columns(4).Visible = False
        End If

    End Sub

    Protected Sub ShiftTM_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles ShiftTM.RowUpdated
        ShiftTM.Columns(3).Visible = False
        ShiftTM.Columns(4).Visible = False
    End Sub


    Protected Sub TMorSumm_click(ByVal sender As Object, ByVal e As EventArgs)
        If TMorSumm.Text = "Verify Team Members" Then
            'Connection Strings to Access DB
            Dim connectionStringAccess As String = _
           ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim dbConn As New OleDbConnection(connectionStringAccess)
            'end Connection Strings

            Dim TMCount As Integer = 0

            Dim CountSqlString As String
            CountSqlString = "SELECT Count(TMs.EmployeeID) AS CountOfEmployeeID FROM TMs WHERE (TMs.DepartmentID)=2 AND (TMs.ShiftID)=1"

            Dim dbComm As New OleDbCommand(CountSqlString, dbConn)

            Try
                dbConn.Open()
                TMCount = Convert.ToInt32(dbComm.ExecuteScalar())
                TMCountLabel.Text = TMCount & " Team Members"

                dbConn.Close()

            Finally
                dbConn.Close()
            End Try

            Summ.Visible = "False"
            ShiftTM.Visible = "True"
            ShiftTM.Columns(3).Visible = False
            ShiftTM.Columns(4).Visible = False
            TMorSumm.Text = "View Summary"
        Else
            TMCountLabel.Text = ""
            Summ.Visible = "True"
            ShiftTM.Visible = "False"
            TMorSumm.Text = "Verify Team Members"
        End If

    End Sub
   
End Class