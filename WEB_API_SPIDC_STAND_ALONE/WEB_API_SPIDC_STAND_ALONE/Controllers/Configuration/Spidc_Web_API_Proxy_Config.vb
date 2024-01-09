Imports System.Net
Imports System.Web.Http
Imports System.Net.Http
Imports System.Threading.Tasks
Imports DotEnv
Imports System.IO
Imports Newtonsoft.Json

Public Class Spidc_Web_API_Proxy_Config

    Private Shared ReadOnly httpClient As New HttpClient
    Private Shared apiEndPoint As String
    Private Shared targetRequestBody As String

    Public Shared Function ProxyTheRequest(ByVal httpMethod As HttpMethod, ByVal apiEndPoint As String, Optional requestBody As Object = Nothing, Optional apikey As String = Nothing, Optional token As String = Nothing, Optional param As String = Nothing)
        'Config
        Spidc_Web_API_Config.WebApiConfig()
        targetRequestBody = JsonConvert.SerializeObject(requestBody)
        Dim Body As New StringContent(targetRequestBody, System.Text.Encoding.UTF8, Spidc_Web_API_Config._mApiContentType)
        ' Create a new request to the target API
        Dim targetRequest As New HttpRequestMessage(httpMethod, apiEndPoint)
        ' Check if the request is a  Post or Put and if not dont add  targetRequest.Content = Body
        If httpMethod = httpMethod.Post Or httpMethod = httpMethod.Put Then
            targetRequest.Content = Body
        Else
            'Do Nothing
        End If
        ' Add headers as needed (e.g.,content type, authentication tokens, API keys) Note If param is for login make a api key header else make a token key 
        Select Case param 'Login/Send Email/Universal Checkout /Webhooks Proxy Header For API KEY 
            Case Spidc_Web_API_Config._mAppPostParam, Spidc_Web_API_Config._mAppPostParamEmailApp, Spidc_Web_API_Config._mAppPostParamUniversalCheckOut, Spidc_Web_API_Config._mAppPostParamWebhooks
                targetRequest.Headers.Add(Spidc_Web_API_Config._mApiHeader, apikey)
            Case Else 'Other Proxy Header For Token
                targetRequest.Headers.Add(Spidc_Web_API_Config._mApiHeaderToken, token)
        End Select
        ' Forward the request to the target API and get the response
        Dim targetResponse As HttpResponseMessage = httpClient.SendAsync(targetRequest).Result
        ' Create a response with the same status code and content as the target API response
        Dim response As New HttpResponseMessage(targetResponse.StatusCode)
        response.Content = targetResponse.Content
        ' Copy the target API response headers to the proxy response
        For Each header In targetResponse.Headers
            response.Headers.Add(header.Key, header.Value)
        Next
        Return response
    End Function



End Class
