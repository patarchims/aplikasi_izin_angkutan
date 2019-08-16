Public Class frmReportKartu

    Private Sub frmReportKartu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CRV.ReportSource = Nothing
        CRV.RefreshReport()
        'CRV.SelectionFormula = "({cabang})='" & Val(cboCabang.Text) & "'"
        CRV.SelectionFormula = "({command.no_kartu}) like'" & Kartu.lblkdKartu.Text & "'"
        RptKartu1.Load("RptKartu1.rpt")
        CRV.ReportSource = RptKartu1
        CRV.RefreshReport()
        Kartu.Bersih()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
    Private Sub lblClosed_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblClosed.MouseEnter
        lblClosed.BackColor = Color.Crimson
    End Sub

    Private Sub lblClosed_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblClosed.MouseLeave
        lblClosed.BackColor = Color.FromArgb(73, 82, 83)
    End Sub

    Private Sub lblClosed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblClosed.Click
        Me.Close()
    End Sub

    Private Sub CRV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRV.Load

    End Sub
End Class