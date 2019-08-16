Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports JS_FlatMaterialUI.CustomMessageBox
Public Class Kartu

    Private Sub Kartu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GrdPemilik.Visible = False
        TidakAktif()
        lblkode.Visible = False
        TampilGrid()
        lblkdKartu.Visible = False

    End Sub

    Private Sub GrdPemilik_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdPemilik.CellContentClick
        Try
            lblkode.Text = GrdPemilik.CurrentRow.Cells(1).Value
            txtpemilik.Text = GrdPemilik.CurrentRow.Cells(0).Value
            txttnk.Text = GrdPemilik.CurrentRow.Cells(2).Value
            GrdPemilik.Visible = False
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub BtnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTambah.Click
        Aktif()
        Bersih()
        KodeAuto()
        BtnSimpan.Enabled = True
        BtnBatal.Enabled = True
        BtnTambah.Enabled = False
    End Sub
    Sub KodeAuto()
        Try
            tutupkoneksi()
            cekkoneksi()
            Dim strSementara As String = ""
            Dim strIsi As String = ""
            sqlcommand = New MySqlCommand("select * from kartu order by no_kartu desc", conn)
            Dr = sqlcommand.ExecuteReader
            If Dr.Read Then
                strSementara = Mid(Dr.Item("no_kartu"), 3, 6)
                strIsi = Val(strSementara) + 1
                lblkdKartu.Text = "K-" + Mid("000000", 1, 6 - strIsi.Length) & strIsi
            Else
                lblkdKartu.Text = "K-000001"
            End If
            txtpemilik.Focus()
            txttnk.Enabled = False
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub
    Sub Aktif()
        BtnBatal.Enabled = True
        BtnSimpan.Enabled = True
        BtnHapus.Enabled = True
        txttnk.Enabled = True
        lblkode.Enabled = True
        txttnk.Enabled = True
        txtpemilik.Enabled = True
        dptdariTanggal.Enabled = True
        dptTglSampai.Enabled = True
    End Sub
    Sub TidakAktif()
        txttnk.Enabled = False
        BtnBatal.Enabled = False
        BtnHapus.Enabled = False
        BtnSimpan.Enabled = False
        txttnk.Enabled = False
        lblkode.Enabled = False
        txttnk.Enabled = False
        txtpemilik.Enabled = False
        dptdariTanggal.Enabled = False
        dptTglSampai.Enabled = False
    End Sub
    Sub Bersih()
        txttnk.Text = ""
        txtpemilik.Text = ""
        lblkode.Text = ""
    End Sub
    Sub TampilGrid()
        Try
            tutupkoneksi()
            cekkoneksi()
            Dim table As New DataTable()
            Dim Adapter As New MySqlDataAdapter("SELECT kartu.no_kartu, kendaraan.tnk, pelanggan.pemilik, kartu.dari_tanggal, kartu.sampai_tanggal, jenis_kendaraan.biaya from kartu inner join pelanggan on pelanggan.kd_po=kartu.kd_po inner join kendaraan on kendaraan.tnk=kartu.tnk inner join jenis_kendaraan on jenis_kendaraan.kd_jenis=kendaraan.kd_jenis where " & _
            "kendaraan.tnk like '" & Trim(txtcari.Text) & "%' OR pelanggan.pemilik like '" & Trim(txtcari.Text) & "%' order by tnk ASC", conn)
            Adapter.Fill(table)
            GrdView.DataSource = table
            GrdView.Refresh()
            tutupkoneksi()
            With GrdView
                .Columns(0).HeaderText = "NO Kartu"
                .Columns(1).HeaderText = "TNK"
                .Columns(2).HeaderText = "Pemilik"
                .Columns(3).HeaderText = "Dari Tanggal"
                .Columns(4).HeaderText = "Sampai Tanggal"
                .Columns(5).HeaderText = "Biaya"
                .Columns(0).Width = 125
                .Columns(1).Width = 120
                .Columns(2).Width = 210
                .Columns(3).Width = 110
                .Columns(4).Width = 110
                .Columns(5).Width = 115
            End With

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
    Sub TampilPemilik()
        Try
            tutupkoneksi()
            cekkoneksi()
            Dim table As New DataTable()
            Dim Adapter As New MySqlDataAdapter(" SELECT pelanggan.pemilik, kendaraan.kd_po,  kendaraan.tnk, jenis_kendaraan.nm_jenis, merek.nm_merek, kendaraan.tahun from kendaraan inner join pelanggan on pelanggan.kd_po=kendaraan.kd_po inner join jenis_kendaraan on jenis_kendaraan.kd_jenis=kendaraan.kd_jenis inner join merek on merek.kd_merek=kendaraan.kd_merek  where " & _
            "pelanggan.pemilik like '" & Trim(txtpemilik.Text) & "%' order by kd_po ASC", conn)
            Adapter.Fill(table)
            GrdPemilik.DataSource = table
            GrdPemilik.Refresh()
            tutupkoneksi()
            With Me.GrdPemilik
                .Columns(0).HeaderText = "Pemilik"
                .Columns(1).HeaderText = "Kode PO"
                .Columns(2).HeaderText = "TNK"
                .Columns(3).HeaderText = "Jenis"
                .Columns(4).HeaderText = "Merek"
                .Columns(5).HeaderText = "Tahun"
                .Columns(0).Width = 75
                .Columns(1).Width = 100
                .Columns(2).Width = 115
                .Columns(3).Width = 100
                .Columns(4).Width = 115
                .Columns(5).Width = 100
            End With
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        Try
            If txttnk.Text = "" Or txtpemilik.Text = "" Then
                ShowMessageBox("Data Isian Harus Lengkap", "Critical", MessageBoxType.Critical)
            Else
                tutupkoneksi()
                cekkoneksi()
                sql = "SELECT * FROM kartu  WHERE no_kartu='" & Trim(lblkdKartu.Text) & "'"
                sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                Dr.Close()
                Dr = sqlcommand.ExecuteReader()
                Dr.Read()
                If Not Dr.HasRows Then
                    sql = "INSERT INTO kartu values ('" & lblkdKartu.Text & "','" & _
                          txttnk.Text & "','" & lblkode.Text & "','" & Format(dptdariTanggal.Value, "yyyy/MM/dd") & "','" & Format(dptTglSampai.Value, "yyyy/MM/dd") & "')"
                    sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                    tutupkoneksi()
                    cekkoneksi()
                    sqlcommand.ExecuteNonQuery()
                    tutupkoneksi()
                    TampilGrid()
                    ShowMessageBox("Data Berhasil Disimpan.", "Infomasi!", MessageBoxType.Information)
                    TidakAktif()
                    BtnTambah.Enabled = True
                    frmReportKartu.ShowDialog()
                Else
                    sql = "UPDATE kartu set tnk='" & txttnk.Text & "',kd_po='" & lblkode.Text & "',dari_tanggal='" & Format(dptdariTanggal.Value, "yyyy/MM/dd") & "', sampai_tanggal='" & Format(dptTglSampai.Value, "yyyy/MM/dd") & "' WHERE no_kartu='" & Trim(lblkdKartu.Text) & "'"
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

    Private Sub GrdView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdView.CellContentClick
        KlikGrid()
    End Sub
    Sub KlikGrid()
        Try
            Aktif()
            lblkdKartu.Text = GrdView.CurrentRow.Cells(0).Value
            txttnk.Text = GrdView.CurrentRow.Cells(1).Value
            txtpemilik.Text = GrdView.CurrentRow.Cells(2).Value
            dptdariTanggal.Text = GrdView.CurrentRow.Cells(3).Value
            dptTglSampai.Text = GrdView.CurrentRow.Cells(4).Value
            lblkdKartu.Visible = False
            GrdPemilik.Visible = False
            lblkode.Visible = False
            SelectPemilik()
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
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

    Private Sub GrdView_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdView.CellContentDoubleClick
        KlikGrid()
    End Sub

    Private Sub BtnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHapus.Click
        Dim Message As DialogResult
        tutupkoneksi()
        cekkoneksi()
        sql = "SELECT * FROM kartu where no_kartu='" & lblkdKartu.Text & "'"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        Dr.Read()
        If Not Dr.HasRows Then tutupkoneksi() : Exit Sub
        Message = ShowMessageBox("Apakah Anda Yakin Untuk Menghapus Data ini?", "Konfirmasi", MessageBoxType.Question)
        If Message = Windows.Forms.DialogResult.Yes Then
            tutupkoneksi()
            sql = "Delete From kartu where no_kartu ='" & lblkdKartu.Text & "'"
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