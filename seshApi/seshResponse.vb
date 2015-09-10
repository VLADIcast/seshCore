Public Class seshResponse

    Public Enum errorType As Integer
        NO_ERROR = 0
        USER_NOT_FOUND = 1
        ACCESS_DENIED = 2
        USER_GUID_MISSING = 3
        NAME_RESERVED = 4
        MISSING_PARAMETERS = 5
        INVALID_PARAMETERS = 6
        ARTIST_DATA_MISSING = 7
        ACCESS_TO_BANDMEMBER_DENIED = 8
    End Enum

    Public Property missingParameters As System.Collections.ObjectModel.Collection(Of String)

    Public Property methodName As String

    Public Property errorCode As errorType

    Public Property response As Object

    Public Sub New(methodName As String)
        _methodName = methodName
    End Sub

    Public Sub New()

    End Sub
End Class
