Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports JS_FlatMaterialUI.CustomMessageBox
Public Class Jenis_Kendaraan

    Private Sub Jenis_Kendaraan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblkode.show()
        TidakAktif()
        TampilGrid()
        btnEdit.Enabled = False

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

    Private Sub BtnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTambah.Click
        KodeAuto()
        txtnama.Enabled = True
        txtnama.Text = ""
        txtnama.Focus()
        BtnSimpan.Enabled = True
        BtnBatal.Enabled = True
        BtnTambah.Enabled = False
    End Sub

    Sub KodeAuto()
        Try
            tutupkoneksi()
            Aktif()

            cekkoneksi()
            Dim strSementara As String = ""
            Dim strIsi As String = ""
            sqlcommand = New MySqlCommand("select * from jenis_kendaraan order by kd_jenis desc", conn)
            Dr = sqlcommand.ExecuteReader
            If Dr.Read Then
                strSementara = Mid(Dr.Item("kd_jenis"), 4, 5)
                strIsi = Val(strSementara) + 1
                lblkode.Text = "JSK" + Mid("00000", 1, 5 - strIsi.Length) & strIsi
            Else
                lblkode.Text = "JSK00001"
            End If
            txtnama.Focus()
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)

        End Try
        
    End Sub
    Sub Aktif()
        BtnBatal.Enabled = True
        BtnSimpan.Enabled = True
        BtnHapus.Enabled = True
    End Sub
    Sub TidakAktif()
        txtnama.Enabled = False
        BtnBatal.Enabled = False
        BtnHapus.Enabled = False
        BtnSimpan.Enabled = False
    End Sub

    Private Sub BtnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        Bersih()
        txtnama.Enabled = False
        TidakAktif()
        BtnTambah.Enabled = True
    End Sub
    Sub Bersih()
        txtbiaya.Text = ""
        txtnama.Text = ""
        lblkode.Text = ""
    End Sub
    Sub TampilGrid()
        tutupkoneksi()
        cekkoneksi()
        Dim table As New DataTable()
        Dim Adapter As New MySqlDataAdapter(" Select * FROM jenis_kendaraan  where " & _
        "nm_jenis like '" & Trim(txtcari.Text) & "%' order by kd_jenis", conn)
        Adapter.Fill(table)
        GrdView.DataSource = table
        GrdView.Refresh()
        tutupkoneksi()
        AturGrid()
        Me.GrdView.Columns(0).HeaderText = "Kode Jenis Kendaraan"
        Me.GrdView.Columns(1).HeaderText = "Nama Jenis Kendaraan"
        Me.GrdView.Columns(2).HeaderText = "Biaya"
    End Sub
    Sub AturGrid()
        GrdView.Columns(0).Width = 115
        GrdView.Columns(1).Width = 550
        GrdView.Columns(2).Width = 200
    End Sub

    Private Sub BtnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHapus.Click
        Dim Message As DialogResult
        tutupkoneksi()
        cekkoneksi()
        sql = "SELECT * FROM jenis_kendaraan where kd_jenis='" & lblkode.Text & "'"
        sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
        Dr.Close()
        Dr = sqlcommand.ExecuteReader
        Dr.Read()
        If Not Dr.HasRows Then tutupkoneksi() : Exit Sub
        Message = ShowMessageBox("Apakah Anda Yakin Untuk Menghapus Data ini?", "Konfirmasi", MessageBoxType.Question)
        If Message = Windows.Forms.DialogResult.Yes Then
            tutupkoneksi()
            sql = "Delete From jenis_kendaraan where kd_jenis ='" & lblkode.Text & "'"
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

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If txtnama.Text = "" Or txtbiaya.Text = "" Then
            ShowMessageBox("Data Isian Harus Lengkap", "Critical", MessageBoxType.Critical)
        Else
            tutupkoneksi()
            cekkoneksi()
            sql = "SELECT * FROM jenis_kendaraan  WHERE kd_jenis='" & Trim(lblkode.Text) & "'"
            sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            Dr.Close()
            Dr = sqlcommand.ExecuteReader()
            Dr.Read()
            If Not Dr.HasRows Then
                sql = "INSERT INTO jenis_kendaraan values ('" & lblkode.Text & "','" & _
                     txtnama.Text & "','" & txtbiaya.Text & "')"
            Else
                sql = "UPDATE jenis_kendaraan set nm_jenis='" & txtnama.Text & "',biaya='" & txtbiaya.Text & "' WHERE kd_jenis='" & Trim(lblkode.Text) & "'"
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
            btnEdit.Enabled = False
        End If
    End Sub

    Private Sub GrdView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdView.CellContentClick
        KlikGrid()
    End Sub
    Sub KlikGrid()
        Aktif()
        txtnama.Enabled = True
        lblkode.Enabled = False
        BtnSimpan.Enabled = False
        btnEdit.Enabled = True
        lblkode.Text = GrdView.CurrentRow.Cells(0).Value
        txtnama.Text = GrdView.CurrentRow.Cells(1).Value
        txtbiaya.Text = GrdView.CurrentRow.Cells(2).Value
    End Sub

    Private Sub GrdView_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdView.CellContentDoubleClick
        KlikGrid()
    End Sub

    Private Sub txtcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TampilGrid()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub


    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtnama.Text = "" Or txtbiaya.Text = "" Then
            ShowMessageBox("Data Isian Harus Lengkap", "Critical", MessageBoxType.Critical)
        Else
            tutupkoneksi()
            cekkoneksi()
            sql = "SELECT * FROM jenis_kendaraan  WHERE kd_jenis='" & Trim(lblkode.Text) & "'"
            sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            Dr.Close()
            Dr = sqlcommand.ExecuteReader()
            Dr.Read()
            If Not Dr.HasRows Then
                sql = "INSERT INTO jenis_kendaraan values ('" & lblkode.Text & "','" & _
                     txtnama.Text & "','" & txtbiaya.Text & "')"
            Else
                sql = "UPDATE jenis_kendaraan set nm_jenis='" & txtnama.Text & "',biaya='" & txtbiaya.Text & "' WHERE kd_jenis='" & Trim(lblkode.Text) & "'"
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
            btnEdit.Enabled = False
        End If
    End Sub
End Class