Imports System
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Web
Imports System.Text.Encoding
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class UpdateClient
	
    Public Shared Sub Index(ByVal uuid As JToken)
	    
        Dim createEvent as New CreateEvent
    
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        
        request = DirectCast(WebRequest.Create(apiUrl + "clients/" + uuid.ToString), HttpWebRequest)
        request.Method = "PATCH"
        request.ContentType = "application/x-www-form-urlencoded"
        request.Accept = "application/json"
        request.Headers.Add("Authorization", "Bearer " & accessToken)

        Dim data As StringBuilder
        Dim byteData() As Byte
        Dim postStream As Stream = Nothing

        data = New StringBuilder()

        data.Append("first_name=alphonse")
        data.Append("&last_name=capone")
        data.Append("&language=es")

        ' Create a byte array of the data we want to send  
        byteData = UTF8Encoding.UTF8.GetBytes(data.ToString())

        ' Set the content length in the request headers  
        request.ContentLength = byteData.Length

        ' Write data  
        Try
            postStream = request.GetRequestStream()
            postStream.Write(byteData, 0, byteData.Length)
        Finally
            If Not postStream Is Nothing Then postStream.Close()
        End Try

        Try
            ' Get response  
            response = DirectCast(request.GetResponse(), HttpWebResponse)

            ' Get the response stream into a reader  
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()
           
            
            If Not Response Is Nothing Then createEvent.Index(uuid)

        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
    
	End Sub
End Class
