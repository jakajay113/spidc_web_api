Imports System.IO
Imports DotEnv
Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Filters
Imports System.Net
Imports System
Imports System.Collections.Generic


Public Class Spidc_Web_API_Config

#Region "Varriable Web API Config"
    Public Shared Property _mconfig As String
    Public Shared Property _mAppName As String
    Public Shared Property _mAppKey As String
    Public Shared Property _mAppJWTTokenSecurityKey As String
    Public Shared Property _mAppJWTTokenISSUER As String
    Public Shared Property _mAppNameOAIMS As String
    Public Shared Property _mAppNameFAMS As String
    Public Shared Property _mAppNameEIS As String
    Public Shared Property _mAppNameGAAMS As String
    Public Shared Property _mAppNameTIMS As String
    Public Shared Property _mAppNameTOIMS As String
    Public Shared Property _mAppNameCedula As String
    Public Shared Property _mAppNameEmailApp As String
    Public Shared Property _mAppNameUniversalCheckout As String
    Public Shared Property _mAppNameWebhooks As String

    Public Shared Property _mApiProxyName As String
    Public Shared Property _mApiNameFAMS As String
    Public Shared Property _mApiNameEIS As String
    Public Shared Property _mApiNameTOIMS As String
    Public Shared Property _mApiNameTIMS As String
    Public Shared Property _mApiNameCedula As String
    Public Shared Property _mApiNameEmailApp As String
    Public Shared Property _mApiNameUniversalCheckout As String
    Public Shared Property _mApiNameWebhooks As String

    Public Shared Property _mApiName As String
    Public Shared Property _mApiProxy As String
    Public Shared Property _mApiEndPoint As String
    Public Shared Property _mApiEndPointFAMS As String
    Public Shared Property _mApiEndPointEIS As String
    Public Shared Property _mApiEndPointTOIMS As String
    Public Shared Property _mApiEndPointTIMS As String
    Public Shared Property _mApiEndPointCEDULA As String
    Public Shared Property _mApiEndPointEmailApp As String
    Public Shared Property _mApiEndPointUniversalCheckout As String
    Public Shared Property _mUniversalCheckoutURL As String
    Public Shared Property _mApiEndPointWebhooks As String

    Public Shared Property _mApiMailMailer As String
    Public Shared Property _mApiMailHost As String
    Public Shared Property _mApiMailPort As String
    Public Shared Property _mApiMailUsername As String
    Public Shared Property _mApiMailPassword As String
    Public Shared Property _mApiMailEncryption As String
    Public Shared Property _mApiMailFromAddress As String
    Public Shared Property _mApiMailFromName As String

    Public Shared Property _mApiKey As String
    Public Shared Property _mApiHeader As String
    Public Shared Property _mApiHeaderToken As String
    Public Shared Property _mApiContent As String
    Public Shared Property _mApiContentType As String

    Public Shared Property _mApiResponese401Status As String
    Public Shared Property _mApiResponese401Data As String
    Public Shared Property _mApiResponese401Message As String
    Public Shared Property _mApiResponese401Code As String

    Public Shared Property _mApiRespone400Status As String
    Public Shared Property _mApiRespone400Data As String
    Public Shared Property _mApiRespone400Message As String
    Public Shared Property _mApiRespone400Code As String

    Public Shared Property _mApiUrlParam As String
    Public Shared Property _mApiUrlParamOAIMS As String
    Public Shared Property _mApiUrlParamFAMS As String
    Public Shared Property _mApiUrlParamEIS As String
    Public Shared Property _mApiUrlParamTOIMS As String
    Public Shared Property _mApiUrlParamTIMS As String
    Public Shared Property _mApiUrlParamCedula As String
    Public Shared Property _mApiUrlParamEmailApp As String
    Public Shared Property _mApiUrlParamUniversalCheckout As String
    Public Shared Property _mApiUrlParamWebhooks As String

    Private Shared ReadOnly _mhttpClient As New HttpClient()

#End Region

#Region "Varriable FAMS Param Method GET/POST/PUT/DELETE Web API Config "
    Public Shared Property _mAppGetParamFams As String
    Public Shared Property _mAppGetParam1Fams As String
    Public Shared Property _mAppGetParam2Fams As String
    Public Shared Property _mAppGetParam3Fams As String
    Public Shared Property _mAppGetParam4Fams As String
    Public Shared Property _mAppGetParam5Fams As String
    Public Shared Property _mAppGetParam6Fams As String
    Public Shared Property _mAppGetParam7Fams As String
    Public Shared Property _mAppGetParam8Fams As String
    Public Shared Property _mAppGetParam9Fams As String
    Public Shared Property _mAppGetParam10Fams As String
    Public Shared Property _mAppGetParam11Fams As String
    Public Shared Property _mAppGetParam12Fams As String
    Public Shared Property _mAppGetParam13Fams As String
    Public Shared Property _mAppGetParam14Fams As String

    Public Shared Property _mAppPostParam1Fams As String
    Public Shared Property _mAppPostParam2Fams As String
    Public Shared Property _mAppPostParam3Fams As String
    Public Shared Property _mAppPostParam4Fams As String
    Public Shared Property _mAppPostParam5Fams As String
    Public Shared Property _mAppPostParam6Fams As String
    Public Shared Property _mAppPostParam7Fams As String
    Public Shared Property _mAppPostParam8Fams As String
    Public Shared Property _mAppPostParam9Fams As String
    Public Shared Property _mAppPostParam10Fams As String
    Public Shared Property _mAppPostParam11Fams As String
    Public Shared Property _mAppPostParam12Fams As String
    Public Shared Property _mAppPostParam13Fams As String
    Public Shared Property _mAppPostParam14Fams As String

    Public Shared Property _mAppPutParam1Fams As String
    Public Shared Property _mAppPutParam2Fams As String
    Public Shared Property _mAppPutParam3Fams As String
    Public Shared Property _mAppPutParam4Fams As String
    Public Shared Property _mAppPutParam5Fams As String
    Public Shared Property _mAppPutParam6Fams As String
    Public Shared Property _mAppPutParam7Fams As String
    Public Shared Property _mAppPutParam8Fams As String
    Public Shared Property _mAppPutParam9Fams As String
    Public Shared Property _mAppPutParam10Fams As String
    Public Shared Property _mAppPutParam11Fams As String
    Public Shared Property _mAppPutParam12Fams As String
    Public Shared Property _mAppPutParam13Fams As String
    Public Shared Property _mAppPutParam14Fams As String

    Public Shared Property _mAppDeleteParam1Fams As String
    Public Shared Property _mAppDeleteParam2Fams As String
    Public Shared Property _mAppDeleteParam3Fams As String
    Public Shared Property _mAppDeleteParam4Fams As String
    Public Shared Property _mAppDeleteParam5Fams As String
    Public Shared Property _mAppDeleteParam6Fams As String
    Public Shared Property _mAppDeleteParam7Fams As String
    Public Shared Property _mAppDeleteParam8Fams As String
    Public Shared Property _mAppDeleteParam9Fams As String
    Public Shared Property _mAppDeleteParam10Fams As String
    Public Shared Property _mAppDeleteParam11Fams As String
    Public Shared Property _mAppDeleteParam12Fams As String
    Public Shared Property _mAppDeleteParam13Fams As String
    Public Shared Property _mAppDeleteParam14Fams As String

#End Region

#Region "Varriable EIS Param Method GET/POST/PUT/DELETE Web API Config "
    Public Shared Property _mAppGetParam1Eis As String
    Public Shared Property _mAppGetParam2Eis As String
   
#End Region

#Region "Varriable CEDULA Param Method GET/POST/PUT/DELETE Web API Config "

    Public Shared Property _mAppGetParam1Tims As String
    Public Shared Property _mAppGetParam2Tims As String
    Public Shared Property _mAppGetParam3Toims As String
    Public Shared Property _mAppGetParam4Toims As String
    Public Shared Property _mAppGetParam5Toims As String
    Public Shared Property _mAppGetParam6Toims As String
    Public Shared Property _mAppGetParam7Toims As String
    Public Shared Property _mAppGetParam8Tims As String
    Public Shared Property _mAppGetParam9Tims As String
    Public Shared Property _mAppPostParam As String
    Public Shared Property _mAppPostParam1Tims As String
    Public Shared Property _mAppPostParam2Tims As String

    Public Shared Property _mAppPutParam1Tims As String



#End Region

#Region "Varriable Email App Param Method POST Web API Config "
    Public Shared Property _mAppPostParamEmailApp As String
#End Region

#Region "Varriable Universal Checkout Param Method POST Web API Config "
    Public Shared Property _mAppPostParamUniversalCheckOut As String
    Public Shared Property _mAppPostParamUniversalCheckOutProceed As String
    Public Shared Property _mAppGetParamUniversalCheckOut As String
    Public Shared Property _mAppGetParamUniversalCheckOutPaymentMethod As String
    Public Shared Property _mAppDeleteParamUniversalCheckOut As String
#End Region

#Region "Varriable Webhooks Param Method POST Web API Config "
    Public Shared Property _mAppPostParamWebhooks As String
#End Region

#Region "Property Web API Config"
    ' Web API routes
    Public Shared Sub Register(ByVal config As HttpConfiguration)
        'Default Spidc API Proxy Route
        config.Routes.MapHttpRoute( _
            name:="DefaultApiProxy", _
             routeTemplate:="api/v1/{controller}/{param}/{id}", _
             defaults:=New With {.id = RouteParameter.Optional} _
        )

        'Spidc API Route
        config.Routes.MapHttpRoute( _
            name:="DefaultApi", _
            routeTemplate:="api/v1/{controller}/{param}/{id}", _
            defaults:=New With {.id = RouteParameter.Optional} _
        )
    End Sub

    ' Web API Config
    Public Shared Sub WebApiConfig()
        'Path .env
        _mconfig = HttpContext.Current.Server.MapPath("~/Configuration/config.env")
        If File.Exists(_mconfig) Then
            ' Read the .env file line by line
            Dim lines As String() = File.ReadAllLines(_mconfig)
            For Each line As String In lines
                Dim parts As String() = line.Split("=")
                If parts.Length = 2 Then
                    Dim key As String = parts(0).Trim()
                    Dim value As String = parts(1).Trim()
                    Environment.SetEnvironmentVariable(key, value)
                End If
            Next
            ' Now you can access the environment variables
            'APP NAME
            _mAppName = Environment.GetEnvironmentVariable("APP_NAME")
            _mAppNameOAIMS = Environment.GetEnvironmentVariable("APP_NAME_OAIMS")
            _mAppNameFAMS = Environment.GetEnvironmentVariable("APP_NAME_FAMS")
            _mAppNameEIS = Environment.GetEnvironmentVariable("APP_NAME_EIS")
            _mAppNameGAAMS = Environment.GetEnvironmentVariable("APP_NAME_GAAMS")
            _mAppNameTOIMS = Environment.GetEnvironmentVariable("APP_NAME_TOIMS")
            _mAppNameTIMS = Environment.GetEnvironmentVariable("APP_NAME_TIMS")
            _mAppNameCedula = Environment.GetEnvironmentVariable("APP_NAME_CEDULA")
            _mAppNameEmailApp = Environment.GetEnvironmentVariable("APP_NAME_EMAIL_APP")
            _mAppNameUniversalCheckout = Environment.GetEnvironmentVariable("APP_NAME_UNIVERSAL_CHECKOUT")
            _mAppNameWebhooks = Environment.GetEnvironmentVariable("APP_NAME_WEBHOOKS")
            'APP KEY /API KEY /SECURITY KEY
            _mAppKey = Environment.GetEnvironmentVariable("APP_KEY")
            _mApiKey = Environment.GetEnvironmentVariable("API_KEY")
            _mAppJWTTokenSecurityKey = Environment.GetEnvironmentVariable("API_JWTTOKEN_SECURITY_KEY")
            _mAppJWTTokenISSUER = Environment.GetEnvironmentVariable("API_JWTTOKEN_ISSUER")
            'API PROXY
            _mApiProxy = Environment.GetEnvironmentVariable("API_PROXY")
            'API END POINT
            _mApiEndPoint = Environment.GetEnvironmentVariable("API_END_POINT")
            _mApiEndPointFAMS = Environment.GetEnvironmentVariable("API_END_POINT_FAMS")
            _mApiEndPointEIS = Environment.GetEnvironmentVariable("API_END_POINT_EIS")
            _mApiEndPointTOIMS = Environment.GetEnvironmentVariable("API_END_POINT_TOIMS")
            _mApiEndPointTIMS = Environment.GetEnvironmentVariable("API_END_POINT_TIMS")
            _mApiEndPointCEDULA = Environment.GetEnvironmentVariable("API_END_POINT_CEDULA")
            _mApiEndPointEmailApp = Environment.GetEnvironmentVariable("API_END_POINT_EMAIL_APP")
            _mApiEndPointUniversalCheckout = Environment.GetEnvironmentVariable("API_END_POINT_UNIVERSAL_CHECKOUT")
            _mUniversalCheckoutURL = Environment.GetEnvironmentVariable("UNIVERSAL_CHECKOUT_URL")
            _mApiEndPointWebhooks = Environment.GetEnvironmentVariable("API_END_POINT_WEBHOOKS")
            'SMTP EMAIL
            _mApiMailMailer = Environment.GetEnvironmentVariable("APP_MAIL_MAILER")
            _mApiMailHost = Environment.GetEnvironmentVariable("APP_MAIL_HOST")
            _mApiMailPort = Environment.GetEnvironmentVariable("APP_MAIL_PORT")
            _mApiMailUsername = Environment.GetEnvironmentVariable("APP_MAIL_USERNAME")
            _mApiMailPassword = Environment.GetEnvironmentVariable("APP_MAIL_PASSWORD")
            _mApiMailEncryption = Environment.GetEnvironmentVariable("APP_MAIL_ENCRYPTION")
            _mApiMailFromAddress = Environment.GetEnvironmentVariable("APP_MAIL_FROM_ADDRESS")
            _mApiMailFromName = Environment.GetEnvironmentVariable("APP_MAIL_FROM_NAME")
            'API HEADER AND CONTENT AND OTHER
            _mApiHeader = Environment.GetEnvironmentVariable("API_HEADER")
            _mApiHeaderToken = Environment.GetEnvironmentVariable("API_HEADER_TOKEN")
            _mApiContent = Environment.GetEnvironmentVariable("API_CONTENT")
            _mApiContentType = Environment.GetEnvironmentVariable("API_CONTENT_TYPE")
            _mApiProxyName = Environment.GetEnvironmentVariable("API_PROXY_NAME")
            'API RESPONE MESSAGE 401
            _mApiResponese401Status = Environment.GetEnvironmentVariable("API_RESPONSE_401_STATUS")
            _mApiResponese401Data = Environment.GetEnvironmentVariable("API_RESPONSE_401_DATA")
            _mApiResponese401Message = Environment.GetEnvironmentVariable("API_RESPONSE_401_MESSAGE")
            _mApiResponese401Code = Environment.GetEnvironmentVariable("API_RESPONSE_401_CODE")
            'API RESPONE MESSAGE 400
            _mApiRespone400Status = Environment.GetEnvironmentVariable("API_RESPONSE_400_STATUS")
            _mApiRespone400Data = Environment.GetEnvironmentVariable("API_RESPONSE_400_DATA")
            _mApiRespone400Message = Environment.GetEnvironmentVariable("API_RESPONSE_400_MESSAGE")
            _mApiRespone400Code = Environment.GetEnvironmentVariable("API_RESPONSE_400_CODE")
            'API NAME
            _mApiName = Environment.GetEnvironmentVariable("API_NAME")
            _mApiNameFAMS = Environment.GetEnvironmentVariable("API_NAME_FAMS")
            _mApiNameEIS = Environment.GetEnvironmentVariable("API_NAME_EIS")
            _mApiNameTOIMS = Environment.GetEnvironmentVariable("API_NAME_TOIMS")
            _mApiNameTOIMS = Environment.GetEnvironmentVariable("API_NAME_TIMS")
            _mApiNameCedula = Environment.GetEnvironmentVariable("API_NAME_CEDULA")
            _mApiNameEmailApp = Environment.GetEnvironmentVariable("API_NAME_EMAIL_APP")
            _mApiNameUniversalCheckout = Environment.GetEnvironmentVariable("API_NAME_UNIVERSAL_CHECKOUT")
            _mApiNameWebhooks = Environment.GetEnvironmentVariable("API_NAME_WEBHOOKS")
            'API URL NAME PARAM OR APP NAME
            _mApiUrlParam = Environment.GetEnvironmentVariable("APP_URL_PARAM")
            _mApiUrlParamOAIMS = Environment.GetEnvironmentVariable("APP_URL_PARAM_OAIMS")
            _mApiUrlParamFAMS = Environment.GetEnvironmentVariable("APP_URL_PARAM_FAMS")
            _mApiUrlParamEIS = Environment.GetEnvironmentVariable("APP_URL_PARAM_EIS")
            _mApiUrlParamTOIMS = Environment.GetEnvironmentVariable("APP_URL_PARAM_TOIMS")
            _mApiUrlParamTIMS = Environment.GetEnvironmentVariable("APP_URL_PARAM_TIMS")
            _mApiUrlParamCedula = Environment.GetEnvironmentVariable("APP_URL_PARAM_CEDULA")
            _mApiUrlParamEmailApp = Environment.GetEnvironmentVariable("APP_URL_PARAM_EMAIL_APP")
            _mApiUrlParamUniversalCheckout = Environment.GetEnvironmentVariable("APP_URL_PARAM_UNIVERSAL_CHECKOUT")
            _mApiUrlParamWebhooks = Environment.GetEnvironmentVariable("APP_URL_PARAM_WEBHOOKS")
            'WEB API Config Method

            'FAMS WEB API Config Method
            'Get Method
            _mAppGetParam1Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM1_FAMS")
            _mAppGetParam2Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM2_FAMS")
            _mAppGetParam3Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM3_FAMS")
            _mAppGetParam4Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM4_FAMS")
            _mAppGetParam5Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM5_FAMS")
            _mAppGetParam6Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM6_FAMS")
            _mAppGetParam7Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM7_FAMS")
            _mAppGetParam8Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM8_FAMS")
            _mAppGetParam9Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM9_FAMS")
            _mAppGetParam10Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM10_FAMS")
            _mAppGetParam11Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM11_FAMS")
            _mAppGetParam12Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM12_FAMS")
            _mAppGetParam13Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM13_FAMS")
            _mAppGetParam14Fams = Environment.GetEnvironmentVariable("APP_GET_PARAM14_FAMS")
            'Post Method
            _mAppGetParamFams = Environment.GetEnvironmentVariable("APP_POST_PARAM_FAMS")
            _mAppPostParam1Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM1_FAMS")
            _mAppPostParam2Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM2_FAMS")
            _mAppPostParam3Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM3_FAMS")
            _mAppPostParam4Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM4_FAMS")
            _mAppPostParam5Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM5_FAMS")
            _mAppPostParam6Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM6_FAMS")
            _mAppPostParam7Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM7_FAMS")
            _mAppPostParam8Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM8_FAMS")
            _mAppPostParam9Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM9_FAMS")
            _mAppPostParam10Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM10_FAMS")
            _mAppPostParam11Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM11_FAMS")
            _mAppPostParam12Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM12_FAMS")
            _mAppPostParam13Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM13_FAMS")
            _mAppPostParam14Fams = Environment.GetEnvironmentVariable("APP_POST_PARAM14_FAMS")
            'Pust Method
            _mAppPutParam1Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM1_FAMS")
            _mAppPutParam2Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM2_FAMS")
            _mAppPutParam3Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM3_FAMS")
            _mAppPutParam4Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM4_FAMS")
            _mAppPutParam5Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM5_FAMS")
            _mAppPutParam6Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM6_FAMS")
            _mAppPutParam7Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM7_FAMS")
            _mAppPutParam8Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM8_FAMS")
            _mAppPutParam9Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM9_FAMS")
            _mAppPutParam10Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM10_FAMS")
            _mAppPutParam11Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM11_FAMS")
            _mAppPutParam12Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM12_FAMS")
            _mAppPutParam13Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM13_FAMS")
            _mAppPutParam14Fams = Environment.GetEnvironmentVariable("APP_PUT_PARAM14_FAMS")
            'Delete Method
            _mAppDeleteParam1Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM1_FAMS")
            _mAppDeleteParam2Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM2_FAMS")
            _mAppDeleteParam3Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM3_FAMS")
            _mAppDeleteParam4Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM4_FAMS")
            _mAppDeleteParam5Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM5_FAMS")
            _mAppDeleteParam6Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM6_FAMS")
            _mAppDeleteParam7Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM7_FAMS")
            _mAppDeleteParam8Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM8_FAMS")
            _mAppDeleteParam9Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM9_FAMS")
            _mAppDeleteParam10Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM10_FAMS")
            _mAppDeleteParam11Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM11_FAMS")
            _mAppDeleteParam12Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM12_FAMS")
            _mAppDeleteParam13Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM13_FAMS")
            _mAppDeleteParam14Fams = Environment.GetEnvironmentVariable("APP_DELETE_PARAM14_FAMS")

            'EIS WEB API Config Method
            _mAppGetParam1Eis = Environment.GetEnvironmentVariable("APP_GET_PARAM1_EIS")
            _mAppGetParam2Eis = Environment.GetEnvironmentVariable("APP_GET_PARAM2_EIS")


            'Cedula WEB API Config Method
            'Get Method
            _mAppGetParam1Tims = Environment.GetEnvironmentVariable("APP_GET_PARAM1_CEDULA")
            _mAppGetParam2Tims = Environment.GetEnvironmentVariable("APP_GET_PARAM2_CEDULA")
            _mAppGetParam3Toims = Environment.GetEnvironmentVariable("APP_GET_PARAM3_CEDULA")
            _mAppGetParam4Toims = Environment.GetEnvironmentVariable("APP_GET_PARAM4_CEDULA")
            _mAppGetParam5Toims = Environment.GetEnvironmentVariable("APP_GET_PARAM5_CEDULA")
            _mAppGetParam6Toims = Environment.GetEnvironmentVariable("APP_GET_PARAM6_CEDULA")
            _mAppGetParam7Toims = Environment.GetEnvironmentVariable("APP_GET_PARAM7_CEDULA")
            _mAppGetParam8Tims = Environment.GetEnvironmentVariable("APP_GET_PARAM8_CEDULA")
            _mAppGetParam9Tims = Environment.GetEnvironmentVariable("APP_GET_PARAM9_CEDULA")

            'Post Method
            _mAppPostParam = Environment.GetEnvironmentVariable("APP_POST_PARAM_CEDULA")
            _mAppPostParam1Tims = Environment.GetEnvironmentVariable("APP_POST_PARAM1_CEDULA")
            _mAppPostParam2Tims = Environment.GetEnvironmentVariable("APP_POST_PARAM2_CEDULA")

            'Put Method
            _mAppPutParam1Tims = Environment.GetEnvironmentVariable("APP_PUT_PARAM1_CEDULA")


            'Delete Method

            'EMAIL APP WEB API Config Method
            'Post Method
            _mAppPostParamEmailApp = Environment.GetEnvironmentVariable("APP_POST_PARAM1_EMAILAPP")

            'UNIVERSAL CHECKOUT
            _mAppPostParamUniversalCheckOut = Environment.GetEnvironmentVariable("APP_POST_PARAM1_UNIVERSALCHECKOUT")
            _mAppPostParamUniversalCheckOutProceed = Environment.GetEnvironmentVariable("APP_POST_PARAM2_UNIVERSALCHECKOUT")

            _mAppGetParamUniversalCheckOut = Environment.GetEnvironmentVariable("APP_GET_PARAM1_UNIVERSALCHECKOUT")

            _mAppDeleteParamUniversalCheckOut = Environment.GetEnvironmentVariable("APP_DELETE_PARAM1_UNIVERSALCHECKOUT")
            'PAYMENT METHOD
            _mAppGetParamUniversalCheckOutPaymentMethod = Environment.GetEnvironmentVariable("APP_GET_PARAM2_UNIVERSALCHECKOUT_PAYMENTMETHOD")

            'Webhooks
            'Post Method
            _mAppPostParamWebhooks = Environment.GetEnvironmentVariable("APP_POST_PARAM1_WEBHOOKS")


        Else
            Console.WriteLine(".env file not found.")
        End If

    End Sub

#End Region




End Class

