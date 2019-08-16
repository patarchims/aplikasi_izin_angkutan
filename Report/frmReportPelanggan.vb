Public Class frmReportPelanggan
           
    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub frmReportPelanggan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        TampilReport()
    End Sub

    Private Sub frmReportPelanggan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TampilReport()
    End Sub
    Sub TampilReport()
        CRV.ReportSource = Nothing
        CRV.RefreshReport()
        RptPelanggan1.Load("RptPelanggan1.rpt")
        CRV.ReportSource = RptPelanggan1
        CRV.RefreshReport()
    End Sub

    Private Sub CRV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRV.Load

    End Sub
End Class