
Public Class mediaItem
    Inherits baseItem

    Public Enum itemMediaType As Integer
        LINK = 0
        VIDEO = 1
        AUDIO = 2
    End Enum

    Public Property mediaType As itemMediaType

    Public Property URL As String

    Public Property description As String

    Public Sub New()

    End Sub

End Class

