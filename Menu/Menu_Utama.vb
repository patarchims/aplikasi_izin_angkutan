Imports JS_FlatMaterialUI.CustomMessageBox

Public Class Menu_Utama
    Private Sub Menu_Utama_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        lblhari.Text = Format(Now, "dd  MMMM yyyy")
        Dim sekarang As New DateTime()
        'sekarang = Format(Now, "yyyy/MM/dd")
    End Sub
    Sub jhSekarang()
        Dim sekarang As New DateTime()
        'sekarang = Format(Now, "yyyy/MM/dd")
    End Sub

    Private Sub Menu_Utama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblClosed.BackColor = Color.FromArgb(58, 87, 89)
        LblMinimaze.BackColor = Color.FromArgb(58, 87, 89)
        Dasboard()
        Timer1.Start()
    End Sub
    Sub Dasboard()
        Panel_Fill.Controls.Clear()
        Dashboard.TopLevel = False
        Panel_Fill.Controls.Add(Dashboard)
        Dashboard.Show()
    End Sub
    Private Sub LblMinimaze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblMinimaze.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub LblMinimaze_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblMinimaze.MouseEnter
        LblMinimaze.BackColor = Color.FromArgb(71, 79, 80)
    End Sub

    Private Sub LblMinimaze_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblMinimaze.MouseLeave
        LblMinimaze.BackColor = Color.FromArgb(58, 87, 89)
    End Sub

    Private Sub lblClosed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblClosed.Click

        Dim Reply As New DialogResult
        Reply = ShowMessageBox("Apakah Anda Yakin Untuk Keluar dari Aplikasi ini?", "Konfirmasi", MessageBoxType.Question)

        If Reply = Windows.Forms.DialogResult.Yes Then
            End
        Else
            Me.Show()
        End If

    End Sub

    Private Sub lblClosed_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblClosed.MouseEnter
        lblClosed.BackColor = Color.Crimson
    End Sub

    Private Sub lblClosed_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblClosed.MouseLeave
        lblClosed.BackColor = Color.FromArgb(58, 87, 89)
    End Sub

    Private Sub Panel_Fill_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Fill.Paint

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbltime.Text = TimeOfDay.ToString("h:mm:ss tt")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Reply As New DialogResult
        Reply = ShowMessageBox("Apakah Anda Yakin Untuk Keluar dari Aplikasi ini?", "Konfirmasi", MessageBoxType.Question)

        If Reply = Windows.Forms.DialogResult.Yes Then
            End
        Else
            Me.Show()
        End If
    End Sub

    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub CetakStikerBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'frmStikerBarang.ShowDialog()
    End Sub


    Private Sub CetakStikerPCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'frmStikerPC.ShowDialog()
    End Sub

    Private Sub PenerbanganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenerbanganToolStripMenuItem.Click
        Merek_Kendaraan()
    End Sub

    Sub Merek_Kendaraan()
        Panel_Fill.Controls.Clear()
        Merek.TopLevel = False
        Panel_Fill.Controls.Add(Merek)
        Merek.Show()
    End Sub

    Private Sub JenisMaskapaiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JenisMaskapaiToolStripMenuItem.Click
        JenisKendaraan()
    End Sub
    Sub JenisKendaraan()
        Panel_Fill.Controls.Clear()
        Jenis_Kendaraan.TopLevel = False
        Panel_Fill.Controls.Add(Jenis_Kendaraan)
        Jenis_Kendaraan.Show()
    End Sub

    Private Sub MahasiswaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MahasiswaToolStripMenuItem.Click
        Pelanggan_SHow()
    End Sub
    Sub Pelanggan_SHow()
        Panel_Fill.Controls.Clear()
        Pelanggan.TopLevel = False
        Panel_Fill.Controls.Add(Pelanggan)
        Pelanggan.Show()
    End Sub

    Private Sub PengambilanBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PengambilanBarangToolStripMenuItem.Click
        TampilKendaraan()
    End Sub

    Private Sub BarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Sub TampilKendaraan()
        Panel_Fill.Controls.Clear()
        Kendaraan.TopLevel = False
        Panel_Fill.Controls.Add(Kendaraan)
        Kendaraan.Show()
    End Sub

    Private Sub MenuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuToolStripMenuItem.Click

    End Sub

    Private Sub Panel_Footer_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Footer.Paint

    End Sub

    Private Sub BarangMasukToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarangMasukToolStripMenuItem.Click
        TampilKartu()
    End Sub
    Sub TampilKartu()
        Panel_Fill.Controls.Clear()
        Kartu.TopLevel = False
        Panel_Fill.Controls.Add(Kartu)
        Kartu.Show()
    End Sub

    Private Sub TandaTerimaBarnagToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TandaTerimaBarnagToolStripMenuItem.Click
        TampilReportPelanggan()
    End Sub
    Sub TampilReportPelanggan()
        Panel_Fill.Controls.Clear()
        frmReportPelanggan.TopLevel = False
        Panel_Fill.Controls.Add(frmReportPelanggan)
        frmReportPelanggan.Show()
        frmReportPelanggan.TampilReport()
    End Sub
End Class