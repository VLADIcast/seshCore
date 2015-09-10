Public Class apiMethod

    Inherits seshResponse

    Private Property seshRequest As seshRequest


    Public Sub doResponse(inResponse As Object, inErrorCode As seshResponse.errorType, missingParameters As System.Collections.ObjectModel.Collection(Of String))
        errorCode = inErrorCode
        response = inResponse
        MyBase.missingParameters = missingParameters
    End Sub

    ' process each type of method
    Public Sub processMethod()
        errorCode = errorType.NO_ERROR

        ' authenticate user GUID
        If methodName.StartsWith("user.") Then
            If seshRequest.userGUID = "" Then
                errorCode = errorType.USER_GUID_MISSING
            Else
                Dim user As seshCore.user
                user = New seshCore.user(seshRequest.userGUID)
                If user.isUserValid = False Then
                    errorCode = errorType.USER_NOT_FOUND
                End If
            End If


        End If

        ' no error so continue
        If errorCode = errorType.NO_ERROR Then

            ' user.artist methods
            If methodName.StartsWith("user.artist.") Then
                Dim artist As seshApi.artist
                artist = New seshApi.artist(methodName, seshRequest.userGUID, seshRequest.parameterList)
                doResponse(artist, artist.errorCode, artist.missingParameters)
            End If

            ' user.bandmember methods
            If methodName.StartsWith("user.bandmember.") Then
                Dim bandmember As seshApi.bandmember
                bandmember = New seshApi.bandmember(methodName, seshRequest.userGUID, seshRequest.parameterList)
                doResponse(bandmember, bandmember.errorCode, bandmember.missingParameters)
            End If

            ' user.bandleader methods
            If methodName.StartsWith("user.bandleader.") Then
                Dim bandleader As seshApi.bandleader
                bandleader = New seshApi.bandleader(methodName, seshRequest.userGUID, seshRequest.parameterList)
                doResponse(bandleader, bandleader.errorCode, bandleader.missingParameters)
            End If


        End If
    End Sub

    ' intantiate for dictionary parameters
    Public Sub New(methodName As String, userGUID As String, parameterList As Dictionary(Of String, String))
        MyBase.New(methodName)
        _seshRequest = New seshRequest(userGUID, parameterList)

        processMethod()
    End Sub

    ' intantiate for string parameters
    Public Sub New(methodName As String, userGUID As String, parameters As String)
        MyBase.New(methodName)
        _seshRequest = New seshRequest(userGUID, parameters)

        processMethod()



    End Sub

    Public Sub New()

    End Sub

End Class
