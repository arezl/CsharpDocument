using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace JwtTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var payload = new Dictionary<string, object>
            {
                { "UserId", 123 },
                { "UserName", "admin" },
                {"exp"}

            };
            var secret = "GQDst322332OuXasfdasfda@0uFiwDVk";//不要泄露

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            Console.WriteLine(token);
            */
            
            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJVc2VySWQiOjEyMywiVXNlck5hbWUiOiJhZG1pbiJ9.1R6jK7awuNKxK6-IPVq9lftl8vxUKSaXlJy84BDIGgM";
            var secret = "GQDst322332OuXasfdasfda@0uFiwDVk";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                //var json = decoder.Decode(token);
                var json = decoder.Decode(token, secret, verify: true);
                Console.WriteLine(json);
            }
            catch (FormatException)
            {
                Console.WriteLine("Token format invalid");
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }

            Console.ReadKey();
        }
    }
}
