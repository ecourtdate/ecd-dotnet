Imports System

Public Class GetClients
	
	Public Shared Sub Index()

        ' Get Clients
        Dim request As System.Net.WebRequest = System.Net.HttpWebRequest.Create(apiUrl & "clients/")
        request.Method = "GET"
        request.ContentType = "application/json"
        request.Headers.Add("Authorization", "Bearer " & accessToken)

        Using response As System.Net.WebResponse = request.GetResponse()
            Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                Dim jsonResponseText as Object = streamReader.ReadToEnd()
                Console.WriteLine(value:=jsonResponseText)
            End Using
        End Using
    
	End Sub
End Class