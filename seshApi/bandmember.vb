Public Class bandmember
    Inherits responseObj


    Public Property bandMemberResponse As seshCore.bandMember

    Public Sub initRequiredParams()
        ' build up the required params
        requiredParameterMap = New Dictionary(Of String, String)
        requiredParameterMap("user.bandmember.dropout") = "bandMemberGUID"


    End Sub

    Public Sub userBandmemberDropout()
        bandMemberResponse.dropOutOfBand()

        ' repopulate after dropout
        bandMemberResponse.PopulateBandMember()

    End Sub

    Public Sub New(methodName As String, userGUID As String, param As Dictionary(Of String, String))
        MyBase.New(methodName, param)

        _bandMemberResponse = New seshCore.bandMember(param("bandMemberGUID"))

        If _bandMemberResponse.user.GUID <> userGUID Then
            errorCode = seshResponse.errorType.ACCESS_DENIED
        End If

        If errorCode = seshResponse.errorType.NO_ERROR Then
            If methodName = "user.bandmember.dropout" Then
                ' retrieve artist info from core artist object
                userBandmemberDropout()
            End If
        End If

    End Sub
End Class
