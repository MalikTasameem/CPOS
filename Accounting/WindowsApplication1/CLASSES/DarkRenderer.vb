Public Class DarkRenderer
    Inherits ToolStripProfessionalRenderer

    Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(30, 30, 30)), e.AffectedBounds)
    End Sub

    Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(45, 45, 45)), e.Item.ContentRectangle)
    End Sub

    Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
        e.TextColor = Color.White
        MyBase.OnRenderItemText(e)
    End Sub
End Class

