Imports Entidades

Public Class FrmDefinirKms
    Private Sub btnDefinirKm_Click(sender As Object, e As EventArgs) Handles btnDefinirKm.Click
        If String.IsNullOrWhiteSpace(txtNumKm.Text) OrElse String.IsNullOrWhiteSpace(txtDireccion.Text) OrElse String.IsNullOrWhiteSpace(txtLocalidad.Text) OrElse String.IsNullOrWhiteSpace(txtProvincia.Text) Then
            MessageBox.Show("No pueden haber campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        MessageBox.Show(korrika.DefinirKm(txtNumKm.Text, txtDireccion.Text, txtLocalidad.Text, txtProvincia.Text))
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Close()
    End Sub

    Private Sub FrmDefinirKms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDatosKorrika.Text = korrika.ToString
    End Sub
End Class