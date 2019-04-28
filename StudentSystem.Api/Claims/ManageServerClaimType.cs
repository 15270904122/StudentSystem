using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSystem.Api.Claims
{
    public class ManageServerClaimType
    {
        /// <summary>
        /// The UserId type claim
        /// </summary>
        public const string UserId = "http://www.test.com/identity/claims/userid";

        /// <summary>
        /// for UserName claim
        /// </summary>
        public const string UserName = "http://www.test.com/identity/claims/username";


        /// <summary>
        /// for UserType claim
        /// </summary>
        public const string UserType = "http://www.test.com/identity/claims/usertype";

    }
}