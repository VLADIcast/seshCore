Public Class bandleader
    Inherits responseObj

    Public Property band As seshCore.band
    Public Property bandLeaderResponse As seshCore.bandMember


    Public Sub initRequiredParams()
        ' build up the required params
        requiredParameterMap = New Dictionary(Of String, String)
        requiredParameterMap("user.bandleader.bandmember.approve") = "bandGUID,bandMemberGUID"
        requiredParameterMap("user.bandleader.bandmember.drop") = "bandGUID,bandMemberGUID"
        requiredParameterMap("user.bandleader.bandmembers.list") = "bandGUID"


    End Sub


    Public Sub userBandleaderBandmemberDrop()
        Dim anotherBandMember As seshCore.bandMember
        anotherBandMember = New seshCore.bandMember(param("bandMemberGUID"))
        bandLeaderResponse.joinStatus = seshCore.bandMember.joinStatusType.MEMBER_REJECTED
        bandLeaderResponse.updateBandMembershipStatus(anotherBandMember)

    End Sub
    Public Sub userBandleaderBandmemberApprove()
        Dim anotherBandMember As seshCore.bandMember
        anotherBandMember = New seshCore.bandMember(param("bandMemberGUID"))
        bandLeaderResponse.joinStatus = seshCore.bandMember.joinStatusType.MEMBER_JOINED_ACCEPTED
        bandLeaderResponse.updateBandMembershipStatus(anotherBandMember)

    End Sub

    Public Sub userBandleaderBandmembersList()
        Dim statuses As System.Collections.ObjectModel.Collection(Of seshCore.bandMember.joinStatusType)
        statuses = New System.Collections.ObjectModel.Collection(Of seshCore.bandMember.joinStatusType)
        statuses.Add(seshCore.bandMember.joinStatusType.APPLIED_NOTJOINED)
        statuses.Add(seshCore.bandMember.joinStatusType.MEMBER_JOINED_ACCEPTED)
        band.getMembers(statuses)
    End Sub


    Public Sub New(methodName As String, userGUID As String, param As Dictionary(Of String, String))
        MyBase.New(methodName, param)


        _band = New seshCore.band(param("bandGUID"))

        _bandLeaderResponse = _band.leader

        'get the band leader GUID
        Dim bandleaderGUID As String = _bandLeaderResponse.user.GUID


        If bandleaderGUID <> userGUID Then
            errorCode = seshResponse.errorType.ACCESS_DENIED
        End If

        If errorCode = seshResponse.errorType.NO_ERROR Then

            If methodName.StartsWith("user.bandleader.bandmember.") Then
                Dim hasAccessToBandMember As Boolean = False
                For Each bm As seshCore.bandMember In band.members
                    If bm.bandMemberGUID = param("bandMemberGUID") Then
                        hasAccessToBandMember = True
                    End If
                Next
                If hasAccessToBandMember = False Then
                    errorCode = seshResponse.errorType.ACCESS_TO_BANDMEMBER_DENIED
                End If

            End If



            If errorCode = seshResponse.errorType.NO_ERROR Then

                If methodName = "user.bandleader.bandmember.approve" Then
                    ' retrieve artist info from core artist object
                    userBandleaderBandmemberApprove()
                End If

                If methodName = "user.bandleader.bandmember.drop" Then
                    ' retrieve artist info from core artist object
                    userBandleaderBandmemberDrop()
                End If

                If methodName = "user.bandleader.bandmembers.list" Then
                    userBandleaderBandmembersList()
                End If

            End If


        End If

    End Sub
End Class
