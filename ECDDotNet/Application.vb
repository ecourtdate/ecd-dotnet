Imports System
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Web
Imports System.Text.Encoding
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module CreateClient
    Sub Main()
        
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        
        Dim getClients as New GetClients
        Dim updateClient as New UpdateClient
        
        request = DirectCast(WebRequest.Create(apiUrl + "clients"), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        request.Accept = "application/json"
        request.Headers.Add("Authorization", "Bearer " & accessToken)

        Dim data As StringBuilder
        Dim byteData() As Byte
        Dim postStream As Stream = Nothing

        data = New StringBuilder()

        data.Append("first_name=joe")
        data.Append("&last_name=adonis")
        'data.Append("&phone=444222555")

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

            ' Console application output  
            ''Console.Write(reader.ReadToEnd())
            
            Dim rawresp As String
            rawresp = reader.ReadToEnd()
            
            Dim uuid as JToken
            uuid = JObject.Parse(rawresp)("uuid")
            
            If Not Response Is Nothing Then updateClient.Index(uuid)
            
            'Console.WriteLine(uuid)

        Catch e As Exception
            Console.WriteLine(e.Message)

        Finally
            Console.WriteLine("finished...")
            'If Not Response Is Nothing Then getClients.Index
            'If Not Response Is Nothing Then updateClient.Index
            'If Not response Is Nothing Then response.Close()
        End Try

    
    End Sub
End Module