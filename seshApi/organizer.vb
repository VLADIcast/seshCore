Public Class organizer
    Inherits responseObj


    Public Property seshEvent As seshCore.seshEvent


    Public Property organizer As seshCore.organizer

    Public Sub initRequiredParams()
        ' build up the required params
        requiredParameterMap = New Dictionary(Of String, String)
        requiredParameterMap("user.organizer.band.approve") = "bandGUID"
        requiredParameterMap("user.organizer.band.drop") = "bandGUID"
        requiredParameterMap("user.organizer.seshevent.update") = "eventDateTime,address,address2,city,country,state,zipcode,name"
        requiredParameterMap("user.organizer.photo.remove") = "venuePhotoGUID"
        requiredParameterMap("user.organizer.photo.add") = "pathToImage,height,width"

    End Sub

    Public Sub userOrganizerBandApprove()
        Dim band As seshCore.band
        band = New seshCore.band(param("bandGUID"))
        band.playStatus = seshCore.band.playStatusType.PLAYING
        band.updatePlayingStatus()
    End Sub

    Public Sub userOrganizerBandDrop()
        Dim band As seshCore.band
        band = New seshCore.band(param("bandGUID"))
        band.playStatus = seshCore.band.playStatusType.FORMED
        band.updatePlayingStatus()
    End Sub

    Public Sub userOrganizerSesheventUpdate()
        organizer.seshEvent.eventDateTime = param("eventDateTime")
        organizer.seshEvent.location = New seshCore.geo
        organizer.seshEvent.location.address = param("address")
        organizer.seshEvent.location.address2 = param("address2")
        organizer.seshEvent.location.city = param("city")
        organizer.seshEvent.location.country = param("country")
        organizer.seshEvent.location.state = param("state")
        organizer.seshEvent.location.zipcode = param("zipcode")
        organizer.seshEvent.name = param("name")
        organizer.seshEvent.venueName = param("venueName")
        organizer.seshEvent.description = param("description")
        organizer.updateSeshEvent()
    End Sub

    Public Sub userOrganizerPhotoRemove()
        Dim venuePhoto As seshCore.venuePhoto
        venuePhoto = New seshCore.venuePhoto(param("venuePhotoGUID"))
        organizer.removeSeshVenuePhoto(venuePhoto)
    End Sub

    Public Sub userOrganizerPhotoAdd()
        Dim venuePhoto As seshCore.venuePhoto
        venuePhoto = New seshCore.venuePhoto()
        venuePhoto.pathToImage = param("pathToImage")
        venuePhoto.height = param("height")
        venuePhoto.width = param("width")
        organizer.addSeshVenuePhoto(venuePhoto)
    End Sub

    Public Sub userOrganizerBandsList()

    End Sub
    

    Public Sub New(methodName As String, userGUID As String, param As Dictionary(Of String, String))
        MyBase.New(methodName, param)

        _seshEvent = New seshCore.seshEvent(param("seshGUID"))

        Dim isValidOrganizer As Boolean = False
        For Each org As seshCore.organizer In _seshEvent.organizers
            If org.user.GUID = userGUID Then
                isValidOrganizer = True
                organizer = org
            End If
        Next

        If isValidOrganizer = False Then
            errorCode = seshResponse.errorType.ACCESS_DENIED
        End If

        If errorCode = seshResponse.errorType.NO_ERROR Then
            If methodName = "user.organizer.band.approve" Then
                userOrganizerBandApprove()
            End If

            If methodName = "user.organizer.band.drop" Then
                userOrganizerBandDrop()
            End If

            If methodName = "user.organizer.seshevent.update" Then
                userOrganizerSesheventUpdate()
            End If

            If methodName = "user.organizer.photo.remove" Then
                userOrganizerPhotoRemove()
            End If

            If methodName = "user.organizer.photo.add" Then
                userOrganizerPhotoAdd()
            End If

        End If

    End Sub
End Class
