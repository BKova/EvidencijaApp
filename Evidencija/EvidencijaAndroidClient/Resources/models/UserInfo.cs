using System.Security.Cryptography;

namespace EvidencijaAndroidClient.Resources.models
{
    class UserInfo
    {
        public UserInfo()
        {
            UserName = "";
            CertificationCode = -1;
        }
        public string UserName { get; set; }

        public int CertificationCode { get; set; }
    }
}