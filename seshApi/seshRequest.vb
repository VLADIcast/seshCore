Public Class seshRequest

    Public Property methodName As String

    Public Property clientID As String

    Public Property developerKey As String

    Public Property userGUID As String

    Public Property parameterList As Dictionary(Of String, String)

    Public Sub getParameters(parameters As String)

        parameterList = New Dictionary(Of String, String)
        Dim key As String = ""
        Dim value As String = ""
        For Each pair As String In Split(parameters, "&")
            Dim pairArr = Split(pair, "=")
            key = pairArr(0)
            value = pairArr(1)
            parameterList.Add(key, value)
        Next


    End Sub

    Public Sub New(userGUID As String, parameters As String)
        _userGUID = userGUID
        getParameters(parameters)
    End Sub

    Public Sub New(userGUID As String, parameterList As Dictionary(Of String, String))
        _userGUID = userGUID
        _parameterList = parameterList
    End Sub

    Public Sub New()

    End Sub
End Class
