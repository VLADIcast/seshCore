Public Class slot
    Inherits baseItem

    Public Enum slotActionError As Integer
        NO_ERROR = 0
        BAND_ACCEPTED_NO_ADDS_ALLOWED = 1
        SLOT_FILLED_CANNOT_BE_REMOVED = 2
    End Enum
    Public Property instrument As String

    Public Property band As band

    Public Sub Add()
        ' if band is formed and accepted no other adds are allowed

    End Sub

    Public Sub Remove()
        ' if slot has been filled, it cannot be removed

    End Sub

    Public Sub New(band As band, instrument As String)
        _band = band
        _instrument = instrument
    End Sub



    Public Sub New(GUID As String)
        MyBase.GUID = GUID
    End Sub

    Public Sub New()

    End Sub
End Class
