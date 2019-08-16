Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports JS_FlatMaterialUI.CustomMessageBox
Public Class Kendaraan

    Private Sub Kendaraan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dptTanggal.Value = Format(Now, "yyyy/MM/dd")
        dptTanggal.Hide()
    End Sub
       

    Private Sub Kendaraan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GrdPemilik.Visible = False
        TidakAktif()
        LblKdMerek.Visible = False
        lblkode.Visible = False
        lblKodeJenis.Visible = False
        TampilGrid()
        dptTanggal.Hide()

    End Sub
    Private Sub BtnTambah_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTambah.MouseEnter
        BtnTambah.BackColor = Color.FromArgb(217, 245, 247)
        BtnTambah.ForeColor = Color.Black
    End Sub

    Private Sub BtnTambah_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTambah.MouseLeave
        BtnTambah.BackColor = Color.FromArgb(58, 87, 89)
        BtnTambah.ForeColor = Color.White
    End Sub

    Private Sub BtnSimpan_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSimpan.MouseEnter
        BtnSimpan.BackColor = Color.FromArgb(217, 245, 247)
        BtnSimpan.ForeColor = Color.Black
    End Sub

    Private Sub BtnSimpan_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSimpan.MouseLeave
        BtnSimpan.BackColor = Color.FromArgb(58, 87, 89)
        BtnSimpan.ForeColor = Color.White
    End Sub

    Private Sub BtnBatal_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBatal.MouseEnter
        BtnBatal.BackColor = Color.FromArgb(217, 245, 247)
        BtnBatal.ForeColor = Color.Black
    End Sub

    Private Sub BtnBatal_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBatal.MouseLeave
        BtnBatal.BackColor = Color.FromArgb(58, 87, 89)
        BtnBatal.ForeColor = Color.White
    End Sub

    Private Sub BtnHapus_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnHapus.MouseEnter
        BtnHapus.BackColor = Color.FromArgb(217, 245, 247)
        BtnHapus.ForeColor = Color.Black
    End Sub

    Private Sub BtnHapus_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnHapus.MouseLeave
        BtnHapus.BackColor = Color.FromArgb(58, 87, 89)
        BtnHapus.ForeColor = Color.White
    End Sub
    Sub Aktif()
        BtnBatal.Enabled = True
        BtnSimpan.Enabled = True
        BtnHapus.Enabled = True
        txttnk.Enabled = True
        txtdao.Enabled = True
        txtdab.Enabled = True
        cbomerek.Enabled = True
        cbojenis.Enabled = True
        cbojenis.Enabled = True
        txtpemilik.Enabled = True
        cbotahun.Enabled = True
        cbowarna.Enabled = True
        lblkode.Enabled = True
    End Sub
    Sub TidakAktif()
        txttnk.Enabled = False
        BtnBatal.Enabled = False
        BtnHapus.Enabled = False
        BtnSimpan.Enabled = False
        txttnk.Enabled = False
        txtdao.Enabled = False
        txtdab.Enabled = False
        cbomerek.Enabled = False
        cbojenis.Enabled = False
        cbojenis.Enabled = False
        txtpemilik.Enabled = False
        cbotahun.Enabled = False
        cbowarna.Enabled = False
        lblkode.Enabled = False
    End Sub


    Private Sub BtnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTambah.Click
        Aktif()
        Bersih()
        txttnk.Focus()
        BtnSimpan.Enabled = True
        BtnBatal.Enabled = True
        BtnTambah.Enabled = False
    End Sub
    Sub Bersih()
        txttnk.Text = ""
        lblkode.Text = ""
        txtdab.Text = ""
        txtdao.Text = ""
        txtpemilik.Text = ""
        Warna()
        Years()
        Jenis()
        Merek()
    End Sub
    Sub Warna()
        Try
            cbowarna.Text = "---PILIH WARNA---"
            With cbowarna
                .Items.Clear()
                .Items.Add("Hitam")
                .Items.Add("Putih")
                .Items.Add("Silver")
                .Items.Add("Merah")
                .Items.Add("Coklat")
                .Items.Add("Kuning")
                .Items.Add("Ungu")
                .Items.Add("Pink")
                .Items.Add("Orange")
            End With
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try

    End Sub


    Sub Years()
        Try
            Dim Tahun As Integer = 0
            cbotahun.Items.Clear()
            cbotahun.Text = "----PILIH TAHUN----"
            For Tahun = 1990 To 2019
                cbotahun.Items.Add(Tahun)
            Next Tahun
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try

    End Sub

    Private Sub txtpemilik_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpemilik.TextChanged
        If txtpemilik.Text = "" Then
            GrdPemilik.Visible = False
        Else
            GrdPemilik.Visible = True
            TampilPemilik()
        End If
    End Sub
    Sub TampilGrid()
        Try
            tutupkoneksi()
            cekkoneksi()
            Dim table As New DataTable()
            Dim Adapter As New MySqlDataAdapter("SELECT kendaraan.tnk, kendaraan.tgl_daftar, kendaraan.dao, kendaraan.dab, merek.nm_merek, jenis_kendaraan.nm_jenis, pelanggan.pemilik, kendaraan.tahun, kendaraan.warna from kendaraan inner join jenis_kendaraan on jenis_kendaraan.kd_jenis=kendaraan.kd_jenis inner join merek on merek.kd_merek=kendaraan.kd_merek inner join pelanggan on pelanggan.kd_po=kendaraan.kd_po where " & _
            "tnk like '" & Trim(txtcari.Text) & "%' OR pelanggan.pemilik like '" & Trim(txtcari.Text) & "%' order by tnk ASC", conn)
            Adapter.Fill(table)
            GrdView.DataSource = table
            GrdView.Refresh()
            tutupkoneksi()
            'AturGrid()
            With GrdView
                .Columns(0).HeaderText = "TNK"
                .Columns(1).HeaderText = "Tanggal Daftar"
                .Columns(2).HeaderText = "DAO"
                .Columns(3).HeaderText = "DAB"
                .Columns(4).HeaderText = "Merek"
                .Columns(5).HeaderText = "Jenis"
                .Columns(6).HeaderText = "Pemilik"
                .Columns(7).HeaderText = "Tahun"
                .Columns(8).HeaderText = "Warna"
                .Columns(0).Width = 95
                .Columns(1).Width = 115
                .Columns(2).Width = 85
                .Columns(3).Width = 75
                .Columns(4).Width = 110
                .Columns(5).Width = 115
                .Columns(6).Width = 135
                .Columns(7).Width = 100
                .Columns(8).Width = 115
            End With

        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub
    Sub TampilPemilik()
        Try
            tutupkoneksi()
            cekkoneksi()
            Dim table As New DataTable()
            Dim Adapter As New MySqlDataAdapter(" SELECT pelanggan.kd_po, pelanggan.nm_po, pelanggan.pemilik from pelanggan  where " & _
            "pelanggan.nm_po like '" & Trim(txtpemilik.Text) & "%' order by kd_po ASC", conn)
            Adapter.Fill(table)
            GrdPemilik.DataSource = table
            GrdPemilik.Refresh()
            tutupkoneksi()
            With Me.GrdPemilik
                .Columns(0).HeaderText = "Kode PO"
                .Columns(1).HeaderText = "Nama PO"
                .Columns(2).HeaderText = "Pemilik"
                .Columns(0).Width = 75
                .Columns(1).Width = 100
                .Columns(2).Width = 115
            End With
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub txtdao_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdao.KeyPress
        On Error Resume Next
        txtdao.MaxLength = 2
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789.,").IndexOf(e.KeyChar) = -1) Or (e.KeyChar = "." And txtdao.Text.ToCharArray().Count(Function(c) c = ".") > 0) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtdao_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdao.TextChanged

    End Sub

    Private Sub txtdab_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdab.KeyPress
        On Error Resume Next
        txtdab.MaxLength = 5
        If (Not e.KeyChar = ChrW(Keys.Back) And ("0123456789.,").IndexOf(e.KeyChar) = -1) Or (e.KeyChar = "." And txtdab.Text.ToCharArray().Count(Function(c) c = ".") > 0) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtdab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdab.TextChanged

    End Sub

    Sub Merek()
        On Error Resume Next
        cbomerek.Items.Clear()
        cekkoneksi()
        sql = "Select * From merek"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        If Dr.HasRows Then
            Do Until Not Dr.Read
                cbomerek.Items.Add(Dr!nm_merek)
            Loop
        Else
            cbomerek.Items.Clear()
        End If
        tutupkoneksi()
    End Sub

    Sub Jenis()
        On Error Resume Next
        cbojenis.Items.Clear()
        cekkoneksi()
        sql = "Select * From jenis_kendaraan"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        If Dr.HasRows Then
            Do Until Not Dr.Read
                cbojenis.Items.Add(Dr!nm_jenis)

            Loop
        Else
            cbojenis.Items.Clear()
        End If
        tutupkoneksi()
    End Sub

    Private Sub cbojenis_ChangeUICues(ByVal sender As Object, ByVal e As System.Windows.Forms.UICuesEventArgs) Handles cbojenis.ChangeUICues

    End Sub

    Private Sub cbojenis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbojenis.Click

    End Sub
    Sub SelectJenis()
        On Error Resume Next
        cekkoneksi()
        sql = "Select * From jenis_kendaraan where nm_jenis like '" & Trim(cbojenis.Text) & "%'"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        If Dr.HasRows Then
            Do Until Not Dr.Read
                lblKodeJenis.Text = Dr!kd_jenis
            Loop
        Else
            lblKodeJenis.Text = ""
        End If
        tutupkoneksi()
    End Sub
    Private Sub cbojenis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbojenis.SelectedIndexChanged
        SelectJenis()
    End Sub
    Sub SelectMErek()
        On Error Resume Next
        cekkoneksi()
        sql = "Select * From merek where nm_merek = '" & Trim(cbomerek.Text) & "'"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        If Dr.HasRows Then
            Do Until Not Dr.Read
                LblKdMerek.Text = Dr!kd_merek
            Loop
        Else
            LblKdMerek.Text = ""
        End If
        tutupkoneksi()
    End Sub
    Sub SelectPemilik()
        On Error Resume Next
        cekkoneksi()
        sql = "Select * From pelanggan where pemilik = '" & Trim(txtpemilik.Text) & "'"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        If Dr.HasRows Then
            Do Until Not Dr.Read
                lblkode.Text = Dr!kd_po
            Loop
        Else
            lblkode.Text = ""
        End If
        tutupkoneksi()
    End Sub
    Private Sub cbomerek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbomerek.SelectedIndexChanged
        SelectMErek()
    End Sub

    Private Sub GrdPemilik_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdPemilik.CellContentClick
        Try
            lblkode.Text = GrdPemilik.CurrentRow.Cells(0).Value
            txtpemilik.Text = GrdPemilik.CurrentRow.Cells(2).Value
            GrdPemilik.Visible = False
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try

    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        Try
            Dim tanggal As New DateTime

            If txttnk.Text = "" Or txtdab.Text = "" Or txtdao.Text = "" Or cbojenis.Text = "" Or cbojenis.Text = "" Or txtpemilik.Text = "" Or cbotahun.Text = "----PILIH TAHUN----" Or cbowarna.Text = "---PILIH WARNA---" Then
            ShowMessageBox("Data Isian Harus Lengkap", "Critical", MessageBoxType.Critical)
            Else
            tutupkoneksi()
            cekkoneksi()
            sql = "SELECT * FROM kendaraan  WHERE tnk='" & Trim(txttnk.Text) & "'"
            sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            Dr.Close()
            Dr = sqlcommand.ExecuteReader()
            Dr.Read()
            If Not Dr.HasRows Then
                    sql = "INSERT INTO kendaraan values ('" & txttnk.Text & "','" & _
                          Format(dptTanggal.Value, "yyyy/MM/dd") & "','" & txtdao.Text & "','" & txtdab.Text & "','" & LblKdMerek.Text & _
                         "','" & lblKodeJenis.Text & "','" & lblkode.Text & "','" & cbotahun.Text & "','" & cbowarna.Text & "')"
            Else
                sql = "UPDATE kendaraan set dao='" & txtdao.Text & "',dab='" & txtdab.Text & "',kd_merek='" & LblKdMerek.Text & "', kd_jenis='" & lblKodeJenis.Text & "',kd_po='" & lblkode.Text & "',tahun='" & cbotahun.Text & "',warna='" & cbowarna.Text & "' WHERE tnk='" & Trim(txttnk.Text) & "'"
            End If
            sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            tutupkoneksi()
            cekkoneksi()
            sqlcommand.ExecuteNonQuery()
            Bersih()
            tutupkoneksi()
            TampilGrid()
            ShowMessageBox("Data Berhasil Disimpan.", "Infomasi!", MessageBoxType.Information)
            TidakAktif()
            BtnTambah.Enabled = True
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub BtnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        Bersih()
        TidakAktif()
        BtnTambah.Enabled = True
    End Sub

    Sub KlikGrid()
        Aktif()
        On Error Resume Next
        Years()
        Warna()
        txttnk.Text = GrdView.CurrentRow.Cells(0).Value
        'dptTanggal.Text = GrdView.CurrentRow.Cells(1).Value
        txtdao.Text = GrdView.CurrentRow.Cells(2).Value
        txtdab.Text = GrdView.CurrentRow.Cells(3).Value
        cbomerek.Text = GrdView.CurrentRow.Cells(4).Value
        cbojenis.Text = GrdView.CurrentRow.Cells(5).Value
        txtpemilik.Text = GrdView.CurrentRow.Cells(6).Value
        cbotahun.Text = GrdView.CurrentRow.Cells(7).Value
        cbowarna.Text = GrdView.CurrentRow.Cells(8).Value
        SelectJenis()
        SelectPemilik()
        GrdPemilik.Visible = False
        LblKdMerek.Visible = False
        lblkode.Visible = False
        lblKodeJenis.Visible = False
        Merek()
        Jenis()
        txttnk.Enabled = False
    End Sub

    Private Sub GrdView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdView.CellContentClick
        KlikGrid()
    End Sub

    Private Sub BtnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHapus.Click
        Dim Message As DialogResult
        tutupkoneksi()
        cekkoneksi()
        sql = "SELECT * FROM kendaraan where tnk='" & txttnk.Text & "'"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        Dr.Read()
        If Not Dr.HasRows Then tutupkoneksi() : Exit Sub
        Message = ShowMessageBox("Apakah Anda Yakin Untuk Menghapus Data ini?", "Konfirmasi", MessageBoxType.Question)
        If Message = Windows.Forms.DialogResult.Yes Then
            tutupkoneksi()
            sql = "Delete From kendaraan where tnk ='" & txttnk.Text & "'"
            cekkoneksi()
            sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            sqlcommand.ExecuteNonQuery()
            sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            tutupkoneksi()
            cekkoneksi()
            sqlcommand.ExecuteNonQuery()
            TampilGrid()
            Bersih()
            tutupkoneksi()
            ShowMessageBox("Data Berhasil Dihapus.", "Infomasi!", MessageBoxType.Information)
            TidakAktif()
            BtnTambah.Enabled = True
        Else
            Exit Sub
            TampilGrid()
        End If
    End Sub

    Private Sub txtcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcari.TextChanged
        TampilGrid()

    End Sub
End Class