
Partial Class EditTM
    Inherits System.Web.UI.Page

    Protected Sub EditTMProd_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewCommandEventArgs) Handles EditTMProd.ItemCommand
        If e.CommandName = "Cancel" Then
            Response.Write("<script type='text/javascript'>window.open('Shift.aspx','_parent')</script>")
        End If
    End Sub


    Protected Sub EditTMProd_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertedEventArgs)


        EditTMProd.DataSourceID = "EditTMProdDataSource"
        EditTMProd.DataBind()


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EditTMProd.Visible = "False"
            EditTMProd.DataSourceID = ""
            EditTMProd.DataBind()


        End If
    End Sub
    Protected Sub SearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click


        EditTMProd.Visible = "True"
        EditTMProd.DataSourceID = "EditTMProdDataSource"
        EditTMProd.DataBind()



    End Sub
    Protected Sub ClearSearchButton_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearSearchButton.Click


        txtDate.Text = ""

        EditTMProd.Visible = "False"

    End Sub


End Class
