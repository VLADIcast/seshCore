Public Class bandmember
    Inherits responseObj


    Public Property bandMember As seshCore.bandMember

    Public Sub initRequiredParams()
        ' build up the required params
        requiredParameterMap = New Dictionary(Of String, String)
        requiredParameterMap("user.bandmember.dropout") = "bandMemberGUID"


    End Sub

    Public Sub userBandmemberDropout()
        bandMember.dropOutOfBand()

        ' repopulate after dropout
        bandMember.PopulateBandMember()

    End Sub

    Public Sub New(methodName As String, userGUID As String, param As Dictionary(Of String, String))
        MyBase.New(methodName, param)

        _bandMember = New seshCore.bandMember
        _bandMember.loadBandMemberfromBandmemberGUID(param("bandMemberGUID"))

        If _bandMember.user.GUID <> userGUID Then
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
