
namespace PayUOperation
{
    public static class StaticString
    {
        public static string NameSpace ="http://www.openpayu.com/openpayu.xsd";
        public static string OpenPayU ="OpenPayU";
        public static string OrderCreateRequest ="OrderCreateRequest";
        public static string ReqId ="ReqId";
        public static string CustomerIp ="CustomerIp";
        public static string ExtOrderId ="ExtOrderId";
        public static string ExtOrderIdValue="ExtOrderId0";
        public static string MerchantPosId ="MerchantPosId";
        public static string MerchantPosIdValue = "OPU_DEMO";
        public static string Description ="Description";
        public static string CurrencyCode ="CurrencyCode";
        public static string CurrencyCodeValue = "TRY";
        public static string TotalAmount ="TotalAmount";
        public static string Buyer ="Buyer";
        public static string FirstName ="FirstName";
        public static string LastName ="LastName";
        public static string CountryCode ="CountryCode";
        public static string CountryCodeValue = "CountryCode";
        public static string Email ="Email";
        public static string PhoneNumber ="PhoneNumber";
        public static string Language ="Language";
        public static string LanguageValue = "EN";
        public static string Products ="Products";
        public static string Product ="Product";
        public static string Name ="Name";
        public static string UnitPrice ="UnitPrice";
        public static string Quantity ="Quantity";
        public static string PayMethod ="PayMethod";
        public static string Default = "DEFAULT";
    }
    public static class ErrorContent
    {
        public static string MerchantPostId = "merchantPostId should not be null or empty";
        public static string SecretKey = "secretKey  should not be null or empty";
        public static string OpenPayuEndPointUrl = "openPayuEndPointUrl  should not be null or empty ";
    }
}
