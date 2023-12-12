Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization

Public Class test123
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _postToAPI()
    End Sub




    Public Sub _postToAPI()
        ' Specify the URL you want to send the POST request to
        Dim url As String = "https://online.spidc.com.ph/spidc_web_api/api/v1/spidcproxy/universalcheckoutOAIMS"

        ' Create the POST data (in this case, a JSON payload)
        ' Create a dictionary to represent the payload structure
        Dim payload As New Dictionary(Of String, Object)()

        ' Create sub-dictionaries for each section of the payload
        Dim payorInfo As New Dictionary(Of String, Object)() From {
            {"accountNo", "2023-11-00002"},
            {"firstName", "jay"},
            {"lastName", "jay"},
            {"middleName", "jay"},
            {"suffix", Nothing},
            {"address", "jay"},
            {"email", "spidcenduser@yopmail.com"}
        }

        Dim billingData As New Dictionary(Of String, Object)() From {
            {"transrefNo", "t1233454"},
            {"assessmentNo", "2023-11-00002"},
            {"billingDate", Nothing},
            {"sysTranDesc", "Individual Cedula"},
            {"sysTranAmt", "13.42"},
            {"sysTran_TotalAmt", "13.42"}
        }

        Dim accountCodeData As New List(Of Object)()
        accountCodeData.Add(New Dictionary(Of String, Object) From {
            {"systran_providerCode", "SF-002"},
            {"systrans_codedesc", "diploma fee"},
            {"systems_codeamt", "500.00"},
            {"systran_MainCode", "4-02-02-010-01"},
            {"systran_AncestorCode", "4-02-02-010-01"},
            {"sysTran_SubAccCode", "4-02-02-010-01-002"}
        })
       
        Dim systemInformation As New Dictionary(Of String, Object)() From {
            {"appName", "CEDULAAPP"},
            {"urlOrigin", "https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html"}
        }

        Dim notificationUrl As New Dictionary(Of String, Object)() From {
            {"url", "https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html"}
        }

        ' Populate the main payload dictionary
        payload.Add("universalCheckout", New Dictionary(Of String, Object) From {
            {"payorInfo", payorInfo},
            {"billingData", billingData},
            {"accountCodeData", accountCodeData},
            {"systemInformation", systemInformation},
            {"notificationUrl", notificationUrl}
        })

        ' Convert the payload to JSON
        Dim jsonPayload As String = SerializeObject(payload)
        'Dim postData As String = "{""key"": ""value""}"

        ' Convert the string data to a byte array
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(jsonPayload)

        ' Create a request object
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)

        ' Set the request method to POST
        request.Method = "POST"

        ' Set the content type of the request
        request.ContentType = "application/json"

        ' Set the API key in the request headers
        request.Headers.Add("Authorization", "SpidcApiKeyTest123456789")

        ' Set the content length
        request.ContentLength = byteArray.Length

        ' Get the request stream and write the data to it
        Using dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
        End Using

        ' Get the response from the server
        Try
            Using response As WebResponse = request.GetResponse()
                Using dataStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(dataStream)
                        Dim responseFromServer As String = reader.ReadToEnd()
                        Console.WriteLine("Response: " & responseFromServer)
                    End Using
                End Using
            End Using
        Catch ex As WebException
            ' Handle exceptions
            Dim response As WebResponse = ex.Response
            If response IsNot Nothing Then
                Using dataStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(dataStream)
                        Dim errorResponse As String = reader.ReadToEnd()
                        MsgBox(errorResponse)
                    End Using
                End Using
            End If
        End Try
    End Sub


    Function SerializeObject(obj As Object) As String
        Dim serializer As New JavaScriptSerializer()
        Return serializer.Serialize(obj)
    End Function


End Class