Public Class responseObj

    Public Property methodName As String

    Public Property errorCode As seshResponse.errorType

    Public Property param As Dictionary(Of String, String)

    Public Property requiredParameters As String

    Public Property requiredParameterMap As Dictionary(Of String, String)

    Public Property missingParameters As System.Collections.ObjectModel.Collection(Of String)

    Public Sub checkRequiredParams()
        missingParameters = New System.Collections.ObjectModel.Collection(Of String)
        ' check the required params
        If InStr(requiredParameterMap(methodName), ",") = 0 Then
            If param(requiredParameterMap(methodName)) = "" Then
                errorCode = seshResponse.errorType.MISSING_PARAMETERS
                missingParameters.Add(requiredParameterMap(methodName))
            End If
        Else
            For Each cparam As String In Split(requiredParameterMap(methodName), ",")
                If param(cparam) = "" Then
                    errorCode = seshResponse.errorType.MISSING_PARAMETERS
                    missingParameters.Add(cparam)
                End If
            Next
        End If

    End Sub

    Public Sub New(methodName As String, param As Dictionary(Of String, String))
        _methodName = methodName
        _param = param

        errorCode = seshResponse.errorType.NO_ERROR
    End Sub

    Public Sub New()

    End Sub
End Class
