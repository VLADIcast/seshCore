Public Class mySeshoonItem
    Inherits seshEvent

    Public Property isOrganizer As Boolean

    Public Property bandsLeading As System.Collections.ObjectModel.Collection(Of band)

    Public Property musicianInBands As System.Collections.ObjectModel.Collection(Of bandMember)


    Public Sub New()

    End Sub
End Class

Public Class mySeshoons

    Inherits user

    Public Property mySeshoonItems As System.Collections.ObjectModel.Collection(Of mySeshoonItem)


    Public Sub getMySeshoons()

    End Sub
    Public Sub New()

    End Sub
End Class


